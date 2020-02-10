import { User } from 'src/models/auth/user';

export interface State {
  user: User;
  token: string;
  emailTaken: boolean | null;
  isLoading: boolean;
  error: any;
}

export const initialState: State = {
  token: localStorage.getItem('token'),
  user: null,
  emailTaken: null,
  isLoading: false,
  error: null,
};
