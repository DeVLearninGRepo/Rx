import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';
import { LoggerService } from 'src/app/services/logger.service';

@Component({
  selector: 'app-interval',
  templateUrl: './interval.component.html',
  styleUrls: ['./interval.component.css'],
})
export class IntervalComponent implements OnInit {

  public title = 'rx-app-sample';
  public value: number;

  constructor(private loggerService: LoggerService) {}

  public ngOnInit(): void {
    this.loggerService.logComponentInitInfo('Interval Component', '#993636');

    const secondsCounter = interval(1000);

    secondsCounter.subscribe((x) => {
      this.loggerService.logDebug(x, '#993636');

      this.value = x;
    });
  }

}
