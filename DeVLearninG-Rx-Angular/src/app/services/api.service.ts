import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Channel, Video } from '../model/model';

@Injectable({
    providedIn: 'root',
})
export class ApiService {

    private baseUrl = "http://localhost:3000/";

    constructor(
        private http: HttpClient
    ) { }

    getChannels() {
        return this.http.get<Channel[]>(this.baseUrl + "channels");
    }


    getVideoList(channelId: number) {
        return this.http.get<Video[]>(this.baseUrl + "videos?channelId=" + channelId.toString());
    }

    searchVideos(term: string) {
        return this.http.get<Video[]>(this.baseUrl + "videos?q=" + term);
    }
    
}
