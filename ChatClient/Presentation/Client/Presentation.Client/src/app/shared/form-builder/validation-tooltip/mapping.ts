
export interface RuleMappings {
  [key: string]: Rule;
}

export interface Rule {
  order: number;
  description: string;
  showInitially: boolean;
}
