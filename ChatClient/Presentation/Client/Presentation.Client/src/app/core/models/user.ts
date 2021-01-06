
export interface User {
  userId: number;
  userName: string;
}

export interface AuthenticatedUser {
  userId: number;
  userName: string;
  token: string;
}

export interface LoginCredentials {
  userNameOrEmail: string;
  password: string;
}

export interface CreateAccountCredentials {
  email: string;
  userName: string;
  password: string;
}

export interface TargetUser {
  userId: number;
  userName: string;
}
