import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoggerService } from 'src/app/services/logger.service';
import { HttpClient } from '@angular/common/http';
import { Video } from 'src/app/model/model';
import { environment } from 'src/environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { tap, catchError, retry, retryWhen, delay, take, map } from 'rxjs/operators';

@Component({
  selector: 'app-http-request',
  templateUrl: './http-request.component.html',
  styleUrls: ['./http-request.component.css'],
})
export class HttpRequestComponent implements OnInit, OnDestroy {

  public videoObs$: Observable<Video[]>
  public videoCollection: Video[];


  constructor(
    private loggerService: LoggerService,
    private http: HttpClient) { }

  public ngOnInit(): void {
    //this.videoObs$ = this.http.get<Video[]>(`${environment.apiBaseUrl}videos?channelId=1`);

    this.videoObs$ = this.http.get<Video[]>(`${environment.apiBaseUrl}videos?channelId=1`).pipe(
      retry(3),
      tap(x => console.debug(`VideoCollection length ${x.length}`)),
      map(res => {
        if (!res) {
          throw new Error('Value expected!');
        }
        return res;
      }),
      catchError(e => {
        console.error(e);
        return of([])
      })
    );

    // const apiData = ajax('/api/data').pipe(
    //   retry(3), // Retry up to 3 times before failing
    //   map(res => {
    //     if (!res.response) {
    //       throw new Error('Value expected!');
    //     }
    //     return res.response;
    //   }),
    //   catchError(err => of([]))
    // );

    // this.videoObs$ = this.http.get<Video[]>(`${environment.apiBaseUrl}videos?channelId=1`).pipe(
    //   tap(x => console.debug(`VideoCollection length ${x.length}`)),
    //   retryWhen(errors => errors.pipe(delay(2000), take(3))),
    // )

  }

  public ngOnDestroy() {

  }

}
