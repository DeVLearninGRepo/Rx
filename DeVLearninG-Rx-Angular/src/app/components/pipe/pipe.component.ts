import { Component, OnInit } from '@angular/core';
import { filter, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { LoggerService } from 'src/app/services/logger.service';

@Component({
  selector: 'app-pipe',
  templateUrl: './pipe.component.html',
  styleUrls: ['./pipe.component.css'],
})
export class PipeComponent implements OnInit {
  
  public value: number;
  
  constructor(private loggerService: LoggerService) {}

  ngOnInit(): void {
    this.loggerService.logComponentInitInfo("Pipe Component", "#369950");

    const obs = of(1, 2, 3, 4, 5);

    obs
      .pipe(
        filter((n: number) => n % 2 === 0),
        map((n) => n + 1)
      )
      .subscribe((x) => {
        this.loggerService.logDebug(x, '#369950');
        
        this.value = x;
      });
  }
}
