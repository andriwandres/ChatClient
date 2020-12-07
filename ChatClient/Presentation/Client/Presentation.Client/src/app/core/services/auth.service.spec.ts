import { HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ApiError, AuthenticatedUser, CreateAccountCredentials, LoginCredentials } from '@chat-client/core/models';
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
    it('should return the authenticated user', () => {
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
  });

  describe('#logIn()', () => {
    it('should throw error when credentials are incorrect', () => {
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
    it('should send a request to create an account', () => {
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

      request.flush({});
    });
  });
});
