import { ErrorMapping } from 'src/app/shared/form-builder';

export const userNameMappings: ErrorMapping = {
  required: {
    order: 1,
    message: 'Home.CreateAccount.UserName.ValidationRule.Required',
  },
  minlength: {
    order: 2,
    message: 'Home.CreateAccount.UserName.ValidationRule.MinLength',
  },
  pattern: {
    order: 3,
    message: 'Home.CreateAccount.UserName.ValidationRule.Pattern',
  },
  userNameExists: {
    order: 4,
    message: 'This user name is already taken by another user'
  }
};

export const emailMappings: ErrorMapping = {
  required: {
    order: 1,
    message: 'Home.CreateAccount.Email.ValidationRule.Required',
  },
  email: {
    order: 2,
    message: 'Home.CreateAccount.Email.ValidationRule.Email',
  },
  emailExists: {
    order: 3,
    message: 'This e-mail address is already taken by another user'
  }
};

export const passwordMappings: ErrorMapping = {
  required: {
    order: 1,
    message: 'Home.CreateAccount.Password.ValidationRule.Required',
  },
  minlength: {
    order: 2,
    message: 'Home.CreateAccount.Password.ValidationRule.MinLength',
  },
  pattern: {
    order: 3,
    message: 'Home.CreateAccount.Password.ValidationRule.Pattern',
  },
};

export const passwordConfirmMappings: ErrorMapping = {
  required: {
    order: 1,
    message: 'Home.CreateAccount.PasswordConfirm.ValidationRule.Required',
  },
  minlength: {
    order: 2,
    message: 'Home.CreateAccount.PasswordConfirm.ValidationRule.MinLength',
  },
  pattern: {
    order: 3,
    message: 'Home.CreateAccount.PasswordConfirm.ValidationRule.Pattern',
  },
  misMatch: {
    order: 4,
    message: 'Home.CreateAccount.PasswordConfirm.ValidationRule.MisMatch',
  },
};
