import { KeyValue } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { ErrorDescriptor } from './error-mapping';

@Pipe({ name: 'errorOrder' })
export class RuleOrderPipe implements PipeTransform {
  transform(values: KeyValue<string, ErrorDescriptor>[]): KeyValue<string, ErrorDescriptor>[] {
    return values.sort((a, b) => {
      return a.value.order.toString().localeCompare(b.value.order.toString());
    });
  }
}
