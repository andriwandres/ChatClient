
export interface User {
  userId: number;
  userCode: string;
  displayName: string;
  email: string;
}

export type AuthenticatedUser = User & {
  token: string;
};
