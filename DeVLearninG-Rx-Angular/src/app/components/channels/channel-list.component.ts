import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { Channel } from 'src/app/model/model';
import { ChannelService } from 'src/app/services/channel.service';

@Component({
  selector: 'app-channel-list',
  templateUrl: './channel-list.component.html',
  styleUrls: ['./channel-list.component.css']
})
export class ChannelListComponent implements OnInit {

  //@Output('channelChanged') selectedChannelEvent = new EventEmitter<Channel>();

  public channels: Channel[];

  constructor(
    private apiService: ApiService,
    private channelService: ChannelService) { }

  ngOnInit(): void {
    this.apiService.getChannels().subscribe((x) => {
      this.channels = x;
    });

  }

  public selectChannelCommand(channel: Channel) {    
    this.channelService.setCurrentChannel(channel);
    //this.selectedChannelEvent.emit(channel);
  }

}
