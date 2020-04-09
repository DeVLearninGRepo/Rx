import { Component, OnInit } from '@angular/core';
import { ChannelService } from 'src/app/services/channel.service';
import { Observable, of } from 'rxjs';
import { filter, switchMap, map, flatMap } from 'rxjs/operators';
import { ApiService } from 'src/app/services/api.service';
import { Video, Channel } from 'src/app/model/model';

@Component({
  selector: 'app-video-list',
  templateUrl: './video-list.component.html',
  styleUrls: ['./video-list.component.css']
})
export class VideoListComponent implements OnInit {

  public videos: Video[];
  public currentChannel: Channel;

  constructor(
    private apiService: ApiService,
    private channelService: ChannelService
  ) { }

  ngOnInit(): void {
    this.channelService.ChannelSubject.subscribe(x => this.currentChannel = x);

    var obs = this.channelService.ChannelSubject.asObservable();

    obs
      .pipe(
        filter(x => x != null),
        switchMap(x => this.apiService.getVideos(x.id)),
      ).subscribe(x => this.videos = x);
  }

  public selectVideoCommand(video: Video){

  }

}
