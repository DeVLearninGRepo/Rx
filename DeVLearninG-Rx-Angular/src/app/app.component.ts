import { Component, OnInit } from '@angular/core';
import { Channel } from './model/model';
import { ChannelService } from './services/channel.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Rx Angular samples';

  constructor(
    private channelService: ChannelService) {

  }

  public channelChanged(channel: Channel) {
    //this.channelService.setCurrentChannel(channel);
  }
}
