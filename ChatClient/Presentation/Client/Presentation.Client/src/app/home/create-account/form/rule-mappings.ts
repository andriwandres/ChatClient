import { RuleMappings } from 'src/app/shared/form-builder';

export const userNameMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'User name is required'
  },
  minLength: {
    order: 2,
    description: 'User name must be at least 8 characters long'
  },
};

export const emailMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'E-Mail address is required'
  },
  email: {
    order: 2,
    description: 'Has to be a valid e-mail address'
  },
};

export const passwordMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Password is required'
  },
  minLength: {
    order: 2,
    description: 'Password must be at least 8 characters long'
  },
};

export const passwordConfirmMappings: RuleMappings = {
  required: {
    order: 1,
    description: 'Password is required'
  },
  misMatch: {
    order: 2,
    description: 'Both passwords must be equal'
  }
};
