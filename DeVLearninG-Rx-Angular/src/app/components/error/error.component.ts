import { Component, OnInit } from '@angular/core';
import { LoggerService } from 'src/app/services/logger.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

  public currentVideo: Video;

  constructor(private loggerService: LoggerService) { }

  ngOnInit(): void {
    var observable$ = new Observable<Video>(x => {
      this.loggerService.logDebug("Observable start", '#467cd4');

      x.next(new Video(1, "video 1"));
      x.next(new Video(2, "video 2"));
      x.next(new Video(3, "video 3"));

      setTimeout(() => {
        x.error("An error occurred, sorry 😕");
        x.next(new Video(4, "video 4"));
        x.complete();
      }, 1000);

      this.loggerService.logDebug("Observable end", '#467cd4');
    });

    this.loggerService.logDebug("Sottoscrizione", '#369950');
    observable$.subscribe(x => {
      this.loggerService.logDebug(x.name, '#369950');

      this.currentVideo = x;
    }, (e) => {
      this.loggerService.logDebug("ERROR " + e, '#d44646');
    });

  }

}

export class Video {
  id: number;
  name: string;

  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
  }
}