import { RuleMappings } from 'src/app/shared/form-builder';

export const userNameMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Home.CreateAccount.UserName.ValidationRule.Required',
    showInitially: true
  },
  minlength: {
    order: 2,
    description: 'Home.CreateAccount.UserName.ValidationRule.MinLength',
    showInitially: true
  },
  pattern: {
    order: 3,
    description: 'Home.CreateAccount.UserName.ValidationRule.Pattern',
    showInitially: false
  }
};

export const emailMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Home.CreateAccount.Email.ValidationRule.Required',
    showInitially: true
  },
  email: {
    order: 2,
    description: 'Home.CreateAccount.Email.ValidationRule.Email',
    showInitially: true
  },
};

export const passwordMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Home.CreateAccount.Password.ValidationRule.Required',
    showInitially: true
  },
  minlength: {
    order: 2,
    description: 'Home.CreateAccount.Password.ValidationRule.MinLength',
    showInitially: true
  },
  pattern: {
    order: 3,
    description: 'Home.CreateAccount.Password.ValidationRule.Pattern',
    showInitially: false,
  },
  // TODO: Translate description
  misMatch: {
    order: 4,
    description: 'Home.CreateAccount.PasswordConfirm.ValidationRule.MisMatch',
    showInitially: false
  },
};

export const passwordConfirmMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Home.CreateAccount.PasswordConfirm.ValidationRule.Required',
    showInitially: true
  },
  minlength: {
    order: 2,
    description: 'Home.CreateAccount.PasswordConfirm.ValidationRule.MinLength',
    showInitially: true
  },
  pattern: {
    order: 3,
    description: 'Home.CreateAccount.PasswordConfirm.ValidationRule.Pattern',
    showInitially: false,
  },
  misMatch: {
    order: 4,
    description: 'Home.CreateAccount.PasswordConfirm.ValidationRule.MisMatch',
    showInitially: false
  },
};
