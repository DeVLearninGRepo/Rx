import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval, timer, Subscription, Observable } from 'rxjs';
import { LoggerService } from 'src/app/services/logger.service';

@Component({
  selector: 'app-interval',
  templateUrl: './interval.component.html',
  styleUrls: ['./interval.component.css'],
})
export class IntervalComponent implements OnInit, OnDestroy {

  public title = 'rx-app-sample';
  public value: number;
  public intervalObs$: Observable<number>
  public intervalObsSubscription: Subscription;
  public timerObsSubscription: Subscription;

  constructor(private loggerService: LoggerService) {}

  public ngOnInit(): void {
    this.loggerService.logComponentInitInfo('Interval Component', '#993636');

    // var interval$ = interval(100);

    // this.intervalSubscription = interval$s.subscribe((x) => {
    //   this.loggerService.logDebug(`interval ${x}`, '#993636');

    //   this.value = x;
    // });

    //setTimeout(() => {
    //   intervalSubscription.unsubscribe();
    // }, 2000);

    
    
    // var timer$ = timer(3000, 1000);

    // var timerSubscription = timer.subscribe((x) => {
    //   this.loggerService.logDebug(`timer ${x}`, '#993636');

    //   this.value = x;
    // });

    // setTimeout(() => {
    //   timerSubscription.unsubscribe();
    // }, 8000);

    this.intervalObs$ = interval(100);

  }

  public ngOnDestroy(){
    // this.intervalObsSubscription?.unsubscribe();
    // this.timerObsSubscription?.unsubscribe();
  }

}
