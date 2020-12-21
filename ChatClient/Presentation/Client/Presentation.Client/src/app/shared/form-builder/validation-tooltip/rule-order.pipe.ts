import { KeyValue } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Rule } from './mapping';

@Pipe({ name: 'ruleOrder' })
export class RuleOrderPipe implements PipeTransform {
  transform(values: KeyValue<string, Rule>[], ...args: any[]): KeyValue<string, Rule>[] {
    return values.sort((a, b) => {
      if (a.value.order < b.value.order) {
        return -1;
      } else if (a.value.order > b.value.order) {
        return 1;
      } else {
        return 0;
      }
    });
  }
}
