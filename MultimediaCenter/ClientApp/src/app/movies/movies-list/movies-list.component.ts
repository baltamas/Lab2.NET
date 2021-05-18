import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../movie.model';

@Component({
  selector: 'app-list-movies',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.css']
})
export class MoviesListComponent implements OnInit {

  public movies: Movie[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Movie[]>(apiUrl + 'movies').subscribe(result => {
      this.movies = result;
    }, error => console.error(error));
  }


  ngOnInit() {
  }

}
