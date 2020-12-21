import { RuleMappings } from 'src/app/shared/form-builder';

export const userNameMappings: RuleMappings = {
  required: 'User name is required',
};

export const emailMappings: RuleMappings = {
  required: 'E-Mail is required',
  email: 'Has to be a valid e-mail address'
};

export const passwordMappings: RuleMappings = {
  required: 'Password is required'
};

export const passwordConfirmMappings: RuleMappings = {
  required: 'Password is required',
  misMatch: 'Passwords must match'
};
