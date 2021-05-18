import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../movie.model';
import { MoviesService } from '../movies.service';

@Component({
  selector: 'app-list-movies',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.css']
})
export class MoviesListComponent{

  public movies: Movie[];

  constructor(private moviesService: MoviesService) {
   
  }

  getMovies() {
    this.moviesService.getMovies().subscribe(m => this.movies = m)
  }

  ngOnInit() {
    this.getMovies();
  }

}
