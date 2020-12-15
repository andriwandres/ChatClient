import { Component, OnInit } from '@angular/core';
import { AuthFacade } from '@chat-client/auth/store';
import { TranslateService } from '@ngx-translate/core';
import { take } from 'rxjs/operators';
import { LanguageFacade } from './shared/language/store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  readonly authenticationAttempted$ = this.authFacade.authenticationAttempted$;

  constructor(
    private readonly authFacade: AuthFacade,
    private readonly languageFacade: LanguageFacade,
    private readonly translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.languageFacade.selectedLanguageId$.pipe(
      take(1)
    ).subscribe(languageId => {
      this.translateService.use(`${languageId}`);
    });

    this.authFacade.authenticate();
  }
}
