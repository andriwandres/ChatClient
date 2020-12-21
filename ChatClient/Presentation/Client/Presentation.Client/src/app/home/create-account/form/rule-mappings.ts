import { RuleMappings } from 'src/app/shared/form-builder';

export const userNameMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'User name is required',
    showInitially: true
  },
  minlength: {
    order: 2,
    description: 'User name must be at least 2 characters long',
    showInitially: true
  },
  pattern: {
    order: 3,
    description: 'User name must not contain spaces',
    showInitially: false
  }
};

export const emailMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'E-Mail address is required',
    showInitially: true
  },
  email: {
    order: 2,
    description: 'Has to be a valid e-mail address',
    showInitially: true
  },
};

export const passwordMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Password is required',
    showInitially: true
  },
  minlength: {
    order: 2,
    description: 'Password must be at least 8 characters long',
    showInitially: true
  },
};

export const passwordConfirmMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Password is required',
    showInitially: true
  },
  minlength: {
    order: 2,
    description: 'Password must be at least 8 characters long',
    showInitially: true
  },
  misMatch: {
    order: 3,
    description: 'Both passwords must be equal',
    showInitially: false
  }
};
