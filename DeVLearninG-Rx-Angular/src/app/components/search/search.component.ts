import { Component, OnInit, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { fromEvent } from 'rxjs';
import { map, filter, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { ApiService } from 'src/app/services/api.service';
import { Video } from 'src/app/model/model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, AfterViewInit {

  @ViewChild('videoSearchInput') videoSearchInput: ElementRef;

  public searchResult: Video[];

  constructor(
    private apiService: ApiService
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit() {
    var searchObs = fromEvent<any>(this.videoSearchInput.nativeElement, 'keyup')
      .pipe(
        map(x => (x.target as HTMLInputElement).value),
        tap(x => {
          if (x == null || x.length < 3) {
            this.searchResult = null;
          }
        }),
        filter(x => x != null && x.length >= 3),
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(x => this.apiService.searchVideos(x))
      );

    searchObs.subscribe((x) => this.searchResult = x);
  }

}
