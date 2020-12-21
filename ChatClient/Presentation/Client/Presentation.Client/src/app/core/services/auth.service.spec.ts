import { HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ApiError, ApiValidationError, AuthenticatedUser, CreateAccountCredentials, LoginCredentials } from '@chat-client/core/models';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  describe('#authenticate()', () => {
    it('should return the authenticated user when the token is valid', () => {
      // Arrange
      const userMock: AuthenticatedUser = {
        userId: 1,
        userName: 'alfred_miller',
        token: 'some.access.token'
      };

      // Act
      service.authenticate().subscribe(user => {

        // Assert
        expect(user).toEqual(userMock);
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}/me`);
      expect(request.request.method).toBe('GET');

      request.flush(userMock);
    });

    it('should throw an error when the token is expired or invalid', () => {
      // Arrange
      const expectedError: ApiError = {
        statusCode: 404,
        message: 'Unauthorized',
      };

      // Act
      service.authenticate().subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}/me`);
      expect(request.request.method).toBe('GET');

      request.flush(expectedError, {
        status: 404,
        statusText: 'Unauthorized'
      });
    });

    it('should throw an error when the server runs into an unexpected error', () => {
      // Arrange
      const expectedError: ApiError = {
        statusCode: 500,
        message: 'An unexpected error occurred',
      };

      // Act
      service.authenticate().subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}/me`);
      expect(request.request.method).toBe('GET');

      request.flush(expectedError, {
        status: 500,
        statusText: 'Internal server error'
      });
    });
  });

  describe('#logIn()', () => {
    it('should throw an error when credentials are incorrect', () => {
      // Arrange
      const credentials: LoginCredentials = {
        userNameOrEmail: 'alfred_miller',
        password: 'p4ssw0rd'
      };

      const expectedError: ApiError = {
        statusCode: 401,
        message: 'UserName, e-mail and/or password are incorrect'
      };

      // Act
      service.logIn(credentials).subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.session}`);
      expect(request.request.method).toBe('PUT');

      request.flush(expectedError, {
        status: 401,
        statusText: 'Unauthorized'
      });
    });

    it('should throw an error when credentials have failed server-side validation', () => {
      // Arrange
      const credentials: LoginCredentials = {
        userNameOrEmail: '',
        password: ''
      };

      const expectedError: ApiValidationError = {
        statusCode: 400,
        message: 'One or more validation errors occurred',
        errors: {
          userNameOrEmail: [`'UserNameOrEmail' must not be empty`],
          password: [`'Password' must not be empty`],
        }
      };

      // Act
      service.logIn(credentials).subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.session}`);
      expect(request.request.method).toBe('PUT');

      request.flush(expectedError, {
        status: 400,
        statusText: 'Bad Request'
      });
    });

    it('should throw an error when the server runs into an unexpected error', () => {
      // Arrange
      const credentials: LoginCredentials = {
        userNameOrEmail: '',
        password: ''
      };

      const expectedError: ApiError = {
        statusCode: 500,
        message: 'An unexpected error occurred',
      };

      // Act
      service.logIn(credentials).subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.session}`);
      expect(request.request.method).toBe('PUT');

      request.flush(expectedError, {
        status: 500,
        statusText: 'Internal server error'
      });
    });

    it('should return the authenticated user when passed correct credentials', () => {
      // Arrange
      const credentials: LoginCredentials = {
        userNameOrEmail: 'alfred_miller',
        password: 'p4ssw0rd'
      };

      const userMock: AuthenticatedUser = {
        userId: 1,
        userName: 'alfred_miller',
        token: 'some.access.token'
      };

      // Act
      service.logIn(credentials).subscribe(user => {

        // Assert
        expect(user).toEqual(userMock);
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.session}`);
      expect(request.request.method).toBe('PUT');

      request.flush(userMock);
    });
  });

  describe('#createAccount()', () => {
    it('should throw an error when credentials have failed server-side validation', () => {
      // Arrange
      const credentials: CreateAccountCredentials = {
        email: '',
        userName: '',
        password: ''
      };

      const expectedError: ApiValidationError = {
        statusCode: 400,
        message: 'An unexpected error occurred',
        errors: {
          email: [`'Email' must not be empty`],
          userName: [`'UserName' must not be empty`],
          password: [`'Password' must not be empty`],
        }
      };

      // Act
      service.createAccount(credentials).subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}`);
      expect(request.request.method).toBe('POST');

      request.flush(expectedError, {
        status: 400,
        statusText: 'Bad Request'
      });
    });

    it('should throw an error when credentials partially match with existing user', () => {
      // Arrange
      const credentials: CreateAccountCredentials = {
        email: 'alfred.miller@gmail.com',
        userName: 'alfred_miller',
        password: 'p4ssw0rd'
      };

      const expectedError: ApiError = {
        statusCode: 403,
        message: 'A user with the same user name or email already exists. Please use different credentials for creating an account'
      };

      // Act
      service.createAccount(credentials).subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}`);
      expect(request.request.method).toBe('POST');

      request.flush(expectedError, {
        status: 403,
        statusText: 'Forbidden'
      });
    });

    it('should throw an error when the server runs into an unexpected error', () => {
      // Arrange
      const credentials: CreateAccountCredentials = {
        email: 'alfred.miller@gmail.com',
        userName: 'alfred_miller',
        password: 'p4ssw0rd'
      };

      const expectedError: ApiError = {
        statusCode: 500,
        message: 'An unexpected error occurred'
      };

      // Act
      service.createAccount(credentials).subscribe({
        error: (response: HttpErrorResponse) => {
          expect(response.error).toEqual(expectedError);
        }
      });

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}`);
      expect(request.request.method).toBe('POST');

      request.flush(expectedError, {
        status: 500,
        statusText: 'Internal Server Error'
      });
    });

    it('should create a new account when passed valid credentials', () => {
      // Arrange
      const credentials: CreateAccountCredentials = {
        email: 'alfred.miller@gmail.com',
        userName: 'alfred_miller',
        password: 'p4ssw0rd'
      };

      // Act
      service.createAccount(credentials).subscribe();

      // Assert
      const request = httpMock.expectOne(`${environment.api.users}`);
      expect(request.request.method).toBe('POST');

      request.flush(null, {
        status: 201,
        statusText: 'No Content'
      });
    });
  });

  describe('#emailExists()', () => {
    it('should map OK response to true', () => {
      // Arrange
      const email = 'alfred.miller@gmail.com';

      // Act
      service.emailExists(email).subscribe(result => {

        // Assert
        expect(result).toBeTrue();
      });

      // Assert
      const request = httpMock.expectOne({
        method: 'HEAD',
        url: `${environment.api.users}?email=${email}`
      });

      request.flush(null, {
        status: 200,
        statusText: 'OK'
      });
    });

    it('should map NotFound response to false', () => {
      // Arrange
      const email = 'alfred.miller@gmail.com';

      // Act
      service.emailExists(email).subscribe(result => {

        // Assert
        expect(result).toBeFalse();
      });

      // Assert
      const request = httpMock.expectOne({
        method: 'HEAD',
        url: `${environment.api.users}?email=${email}`
      });

      request.flush({}, {
        status: 404,
        statusText: 'NotFound'
      });
    });

    it('should throw an error when the server runs into an unexpected error', () => {
      // Arrange
      const email = 'alfred.miller@gmail.com';

      // Act
      service.emailExists(email).subscribe({
        error: (error: HttpErrorResponse) => {
          expect(error.status).toBe(500);
        }
      });

      // Assert
      const request = httpMock.expectOne({
        method: 'HEAD',
        url: `${environment.api.users}?email=${email}`
      });

      request.flush({}, {
        status: 500,
        statusText: 'Internal Server Error'
      });
    });
  });

  describe('#userNameExists()', () => {
    it('should map OK response to true', () => {
      // Arrange
      const userName = 'alfred_miller';

      // Act
      service.userNameExists(userName).subscribe(result => {

        // Assert
        expect(result).toBeTrue();
      });

      // Assert
      const request = httpMock.expectOne({
        method: 'HEAD',
        url: `${environment.api.users}?userName=${userName}`
      });

      request.flush(null, {
        status: 200,
        statusText: 'OK'
      });
    });

    it('should map NotFound response to false', () => {
      // Arrange
      const userName = 'alfred_miller';

      // Act
      service.userNameExists(userName).subscribe(result => {

        // Assert
        expect(result).toBeFalse();
      });

      // Assert
      const request = httpMock.expectOne({
        method: 'HEAD',
        url: `${environment.api.users}?userName=${userName}`
      });

      request.flush({}, {
        status: 404,
        statusText: 'NotFound'
      });
    });

    it('should throw an error when the server runs into an unexpected error', () => {
      // Arrange
      const userName = 'alfred_miller';

      // Act
      service.userNameExists(userName).subscribe({
        error: (error: HttpErrorResponse) => {
          expect(error.status).toBe(500);
        }
      });

      // Assert
      const request = httpMock.expectOne({
        method: 'HEAD',
        url: `${environment.api.users}?userName=${userName}`
      });

      request.flush({}, {
        status: 500,
        statusText: 'Internal Server Error'
      });
    });
  });
});
