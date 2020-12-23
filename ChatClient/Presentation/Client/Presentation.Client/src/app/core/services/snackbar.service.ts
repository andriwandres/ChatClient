import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';

@Injectable({ providedIn: 'root' })
export class SnackBarService {
  constructor(
    private readonly snackbar: MatSnackBar,
    private readonly translateService: TranslateService
  ) {}

  openSnackBar(messageKey: string, action = '', config?: MatSnackBarConfig): void {
    const translatedMessage = this.translateService.instant(messageKey);

    this.snackbar.open(translatedMessage, action, config);
  }
}
