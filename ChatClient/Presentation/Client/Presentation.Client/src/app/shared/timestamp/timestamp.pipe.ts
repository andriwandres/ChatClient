import { formatDate, WeekDay } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

const ONE_DAY_IN_MILLISECONDS = 1000 * 60 * 60 * 24;

@Pipe({ name: 'timestamp' })
export class TimestampPipe implements PipeTransform {
  constructor(private readonly translateService: TranslateService) {}

  transform(date: Date): Observable<string> {
    date = new Date(date);

    const locale = 'en-us';
    const now = new Date();

    // Date is today
    if (datesAreOnSameDay(date, now)) {
      const formattedDate = formatDate(date, 'HH:mm', locale);

      return of(formattedDate);
    }

    const difference = getDifferenceInDays(date, now);

    // Date was yesterday
    if (difference === 1) {
      const formattedDate = formatDate(date, 'HH:mm', locale);

      return this.translateService.get('Yesterday').pipe(
        map(value => value + ', ' + formattedDate)
      );
    }

    // Date was less than a week ago
    if (difference <= 6) {
      const key = 'Weekdays.' + WeekDay[date.getDay()];

      return this.translateService.get(key);
    }

    return of(formatDate(date, 'dd.MM.yyyy', locale));
  }
}

function getDifferenceInDays(d1: Date, d2: Date): number {
  const utc1 = Date.UTC(d1.getFullYear(), d1.getMonth(), d1.getDate());
  const utc2 = Date.UTC(d2.getFullYear(), d2.getMonth(), d2.getDate());

  return Math.abs(Math.floor((utc2 - utc1) / ONE_DAY_IN_MILLISECONDS));
}

function datesAreOnSameDay(d1: Date, d2: Date): boolean {
  return (
    d1.getFullYear() === d2.getFullYear() &&
    d1.getMonth() === d2.getMonth() &&
    d1.getDate() === d2.getDate()
  );
}
