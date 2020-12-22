import { ErrorMapping } from 'src/app/shared/form-builder';

export const userNameOrEmailMapping: ErrorMapping = {
  required: {
    order: 1,
    message: 'User name or e-mail is required'
  }
};

export const passwordMapping: ErrorMapping = {
  required: {
    order: 1,
    message: 'Password is required'
  },
  credentialsIncorrect: {
    order: 2,
    message: 'User name, e-mail or password were incorrect'
  }
};
