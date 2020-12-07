
export interface AuthenticatedUser {
  token: string;
  userId: number;
  userName: string;
}

export interface LoginCredentials {
  userNameOrEmail: string;
  password: string;
}

export interface CreateAccountCredentials {
  userName: string;
  email: string;
  password: string;
}
