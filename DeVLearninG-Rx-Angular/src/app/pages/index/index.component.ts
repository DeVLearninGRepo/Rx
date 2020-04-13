import { Component, OnInit } from '@angular/core';
import { Channel } from 'src/app/model/model';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  public channelChanged(channel: Channel) {
    console.debug(`IndexComponent - channelChanged: ${channel.name}`)
  }
}
