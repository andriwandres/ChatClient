import { Pipe, PipeTransform } from '@angular/core';
import { formatDate, WeekDay } from '@angular/common';

const ONE_DAY_IN_MILLISECONDS = 1000 * 60 * 60 * 24;

@Pipe({
  name: 'chatTimestamp',
  pure: true,
})
export class ChatTimestampPipe implements PipeTransform {
  transform(date: Date): string {
    date = new Date(date);

    const now = new Date();
    const locale = 'en-US';

    if (this.datesOnSameDay(date, now)) {
      return formatDate(date, 'HH:mm', locale);
    }

    const difference = this.getDifferenceInDays(date, now);

    if (difference === 1) {
      return 'Yesterday, ' + formatDate(date, 'HH:mm', locale);
    }

    if (difference <= 6) {
      return WeekDay[date.getDay()];
    }

    return formatDate(date, 'dd.MM.yyyy', locale);
  }

  private getDifferenceInDays(d1: Date, d2: Date): number {
    const utc1 = Date.UTC(d1.getFullYear(), d1.getMonth(), d1.getDate());
    const utc2 = Date.UTC(d2.getFullYear(), d2.getMonth(), d2.getDate());

    return Math.abs(Math.floor((utc2 - utc1) / ONE_DAY_IN_MILLISECONDS));
  }

  private datesOnSameDay(d1: Date, d2: Date): boolean {
    return d1.getFullYear() === d2.getFullYear() &&
      d1.getMonth() === d2.getMonth() &&
      d1.getDate() === d2.getDate();
  }
}
