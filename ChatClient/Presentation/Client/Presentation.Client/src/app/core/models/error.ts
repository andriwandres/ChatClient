
export interface ApiError {
  statusCode: number;
  message: string;
}

export interface ApiValidationError extends ApiError {
  errors: ValidationErrorCollection[];
}

export interface ValidationErrorCollection {
  [key: string]: string[];
}
