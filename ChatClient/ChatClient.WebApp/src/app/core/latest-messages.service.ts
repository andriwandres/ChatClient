import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LatestMessage } from 'src/models/messages/latest-message';

@Injectable({ providedIn: 'root' })
export class LatestMessagesService {
  constructor(private readonly http: HttpClient) { }

  getLatestMessages(): Observable<LatestMessage[]> {
    const url = `${environment.api.message}/GetLatestMessages`;

    return this.http.get<LatestMessage[]>(url).pipe(
      retry(2)
    );
  }
}
