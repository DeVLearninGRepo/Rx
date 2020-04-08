import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IntervalComponent } from './components/interval/interval.component';
import { PipeComponent } from './components/pipe/pipe.component';
import { ChannelListComponent } from './components/channels/channel-list.component';
import { VideoListComponent } from './components/video-list/video-list.component';

@NgModule({
  declarations: [
    AppComponent,
    IntervalComponent,
    PipeComponent,
    ChannelListComponent,
    VideoListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
