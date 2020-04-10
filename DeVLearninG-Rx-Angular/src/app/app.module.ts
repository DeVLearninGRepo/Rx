import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { IntervalComponent } from './components/interval/interval.component';
import { PipeComponent } from './components/pipe/pipe.component';
import { ChannelListComponent } from './components/channels/channel-list.component';
import { VideoListComponent } from './components/video-list/video-list.component';
import { AboutComponent } from './components/about/about.component';
import { IndexComponent } from './components/index/index.component';
import { HttpRequestComponent } from './components/http-request/http-request.component';
import { SearchComponent } from './components/search/search.component';

const appRoutes: Routes = [
  { path: 'index', component: IndexComponent },
  { path: 'about', component: AboutComponent },
  { path: '', redirectTo: '/index', pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AppComponent,
    IntervalComponent,
    PipeComponent,
    ChannelListComponent,
    VideoListComponent,
    IndexComponent,
    HttpRequestComponent,
    SearchComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false }
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
