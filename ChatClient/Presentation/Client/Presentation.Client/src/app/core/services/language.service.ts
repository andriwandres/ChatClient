import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Language } from '../models';

@Injectable({ providedIn: 'root' })
export class LanguageService {
  constructor(private readonly httpClient: HttpClient) {}

  getLanguages(): Observable<Language[]> {
    const url = environment.api.languages;

    return this.httpClient.get<Language[]>(url);
  }
}
