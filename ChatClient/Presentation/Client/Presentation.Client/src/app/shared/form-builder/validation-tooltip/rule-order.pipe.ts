import { KeyValue } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Rule } from './mapping';

@Pipe({ name: 'ruleOrder' })
export class RuleOrderPipe implements PipeTransform {
  transform(values: KeyValue<string, Rule>[]): KeyValue<string, Rule>[] {
    return values.sort((a, b) => {
      return a.value.order.toString().localeCompare(b.value.order.toString());
    });
  }
}
