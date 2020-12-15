import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TranslationDictionary } from '../models';

@Injectable({ providedIn: 'root' })
export class TranslationService {
  constructor(private readonly httpClient: HttpClient) {}

  getTranslations(languageId: number): Observable<TranslationDictionary> {
    const url = `${environment.api.languages}/${languageId}/translations`;

    return this.httpClient.get<TranslationDictionary>(url);
  }
}
