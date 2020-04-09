import { Component, OnInit } from '@angular/core';
import { Channel } from './model/model';
import { ChannelService } from './services/channel.service';
import { Router, NavigationStart } from '@angular/router';
import { filter } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {

  public navStart: Observable<NavigationStart>;
  public title = 'DeVLearninG - Rx Angular';

  constructor(
    private router: Router) {

    this.navStart = router.events.pipe(
      filter(evt => evt instanceof NavigationStart)
    ) as Observable<NavigationStart>;
  }

  public ngOnInit() {
    this.navStart.subscribe(evt => console.log('Navigation Started!'));
  }
}
