
export interface ErrorMapping {
  [key: string]: ErrorDescriptor;
}

export interface ErrorDescriptor {
  order: number;
  message: string;
}
