import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Channel } from '../model/model';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root',
})
export class ChannelService {

  private channelSubject: BehaviorSubject<Channel>;

  constructor(
    private loggerService: LoggerService
  ) {
    this.channelSubject = new BehaviorSubject<Channel>(null);
  }

  get ChannelSubject() {
    return this.channelSubject;
  }

  public setCurrentChannel(channel: Channel) {
    if (this.channelSubject.value === null || this.channelSubject.value.id !== channel.id) {
      this.loggerService.logDebug("ChannelService " + channel.name, '#324ea8');
      this.channelSubject.next(channel);
    }
  }

  
}
