import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TvShowService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiURL}/TvShow`;
  private authUrl = `${environment.apiURL}/api/auth/generate`;

 getAll(search: string = '', pageSize: number = 10, page: number = 0): Observable<any> {
    return this.http.get<any>(this.authUrl).pipe(
      switchMap((authResponse) => {
        const token = authResponse.accessToken;
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        let params = new HttpParams()
          .set('Page', page.toString())
          .set('PageSize', pageSize.toString());

        if (search.length >= 2) {
          params = params.set('FTS', search);
        }

        return this.http.get(this.apiUrl, { headers, params });
      })
    );
  }
  rate(id: number, value: number): Observable<any> {
    return this.http.get<any>(this.authUrl).pipe(
      switchMap((authResponse) => {
        const token = authResponse.accessToken;
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        return this.http.post(`${this.apiUrl}/${id}/rate`, { value }, { headers });
      })
    );
  }

}
