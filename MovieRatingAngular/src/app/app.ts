import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MovieService } from './movie';
import { TvShowService } from './tvshow';
import { CommonModule } from '@angular/common';
import { debounceTime, Subject } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { Movie } from './movie.model';
import { TvShow } from './tvshow.model';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private cdRef = inject(ChangeDetectorRef);

  private movieService = inject(MovieService);
  private tvShowService = inject(TvShowService);
  hoverRating: { [key: string]: number } = {};
  protected title = 'frontend';
  weatherForecasts: any[] = [];
  private searchSubject = new Subject<string>();
  searchText = '';
  moviePage = 0;
  tvShowPage = 0;
  totalMovieCount = 0;
  totalTvShowCount = 0;

  moviesAll: Movie[] = [];
  displayedMovies: Movie[] = [];
  tvshowsAll: TvShow[] = [];
  displayedTvshows: TvShow[] = [];


  activeTab: 'movies' | 'tvshows' = 'movies';

  ngOnInit(): void {
    this.loadCurrentTabData();
  }
  constructor() {

  }

  private loadCurrentTabData() {
    if (this.activeTab === 'movies') {
      this.loadMovies();
    } else {
      this.loadTvShows();
    }
  }

  private loadMovies(append: boolean = false) {
    this.movieService.getAll(this.searchText, 10, this.moviePage).subscribe({
      next: (res) => {
        this.totalMovieCount = res.count;
        const movies = res.result.map((m: Movie) => ({
          ...m,
          id: m.movieId
        }));


        if (append) {
          this.moviesAll = [...this.moviesAll, ...movies];
        } else {
          this.moviesAll = movies;
        }

        this.updateDisplayedMovies();
        this.cdRef.detectChanges();
      },
      error: (err) => console.error('Movie error:', err)
    });
  }

  private loadTvShows(append: boolean = false) {
    this.tvShowService.getAll(this.searchText, 10, this.tvShowPage).subscribe({
      next: (res) => {
        this.totalTvShowCount = res.count;
        const shows = res.result.map((s: TvShow) => ({
          ...s,
          id: s.tvShowId
        }));


        if (append) {
          this.tvshowsAll = [...this.tvshowsAll, ...shows];
        } else {
          this.tvshowsAll = shows;
        }

        this.updateDisplayedTvshows();
        this.cdRef.detectChanges();
      },
      error: (err) => console.error('TV Show error:', err)
    });
  }
  loadMore() {
    if (this.activeTab === 'movies') {
      this.moviePage++;
      this.loadMovies(true);
    } else {
      this.tvShowPage++;
      this.loadTvShows(true);
    }
  }


  private updateDisplayedMovies() {
    this.displayedMovies = this.moviesAll
      .slice()
      .sort((a, b) => this.getAverageRating(b.ratings) - this.getAverageRating(a.ratings));
  }

  private updateDisplayedTvshows() {
    this.displayedTvshows = this.tvshowsAll
      .slice()
      .sort((a, b) => this.getAverageRating(b.ratings) - this.getAverageRating(a.ratings));
  }


  switchTab(tab: 'movies' | 'tvshows') {
    this.activeTab = tab;
    this.searchText = '';

    if (tab === 'movies') {
      this.moviePage = 0;
      this.moviesAll = [];
    } else {
      this.tvShowPage = 0;
      this.tvshowsAll = [];
    }

    this.loadCurrentTabData();
  }


  onSearchChange(value: string) {
    this.searchText = value.trim();

    const starExactMatch = /\b(\d)\s*stars?\b/i;
    const starAtLeastMatch = /\bat\s*least\s*(\d)\s*stars?\b/i;
    const afterYearMatch = /\bafter\s+(\d{4})$/i;

    const olderThanYearsMatch = /\bolder\s+than\s+(\d{1,4})\s*years?\b/i;

    const isCompletePhrase =
      starExactMatch.test(this.searchText) ||
      starAtLeastMatch.test(this.searchText) ||
      afterYearMatch.test(this.searchText) ||
      olderThanYearsMatch.test(this.searchText);

    if (this.searchText.length >= 2 && (isCompletePhrase || !this.containsAnyKeyword(this.searchText))) {
      if (this.activeTab === 'movies') {
        this.moviePage = 0;
        this.moviesAll = [];
      } else {
        this.tvShowPage = 0;
        this.tvshowsAll = [];
      }
      this.searchWithQuery(this.searchText);
    } else if (this.searchText.length === 0) {
      this.moviePage = 0;
      this.tvShowPage = 0;
      this.moviesAll = [];
      this.tvshowsAll = [];
      this.loadCurrentTabData();
    }
  }

  private containsAnyKeyword(query: string): boolean {
    const keywords = ['star', 'stars', 'at least', 'after', 'older than'];
    return keywords.some(kw => query.toLowerCase().includes(kw));
  }



  private searchWithQuery(query: string) {
    if (this.activeTab === 'movies') {
      this.movieService.getAll(query, 10).subscribe({
        next: (res) => {
          this.moviesAll = res.result || [];
          this.updateDisplayedMovies();
          this.cdRef.detectChanges();
        },
        error: (err) => console.error('Movie search error:', err)
      });
    } else {
      this.tvShowService.getAll(query, 10).subscribe({
        next: (res) => {
          this.tvshowsAll = res.result || [];
          this.updateDisplayedTvshows();
          this.cdRef.detectChanges();
        },
        error: (err) => console.error('TV show search error:', err)
      });
    }
  }


  getAverageRating(ratings: any[]): number {
    if (!ratings || ratings.length === 0) return 0;
    const sum = ratings.reduce((acc, r) => acc + r.value, 0);
    return +(sum / ratings.length).toFixed(1);
  }


  getYear(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    if (!isNaN(date.getTime())) {
      return date.getFullYear().toString();
    }
    return dateString.substring(0, 4);
  }
  rate(type: 'movies' | 'tvshows', id: number, value: number): void {
    if (!id || value < 1 || value > 5) {
      console.warn('Invalid rate call:', { type, id, value });
      return;
    }

    const service = type === 'movies' ? this.movieService : this.tvShowService;

    service.rate(id, value).subscribe({
      next: () => {
        console.log(`Successfully rated ${type} ${id} with ${value}`);

        if (type === 'movies') {
          this.moviePage = 0;
          this.moviesAll = [];
        } else {
          this.tvShowPage = 0;
          this.tvshowsAll = [];
        }

        this.loadCurrentTabData();
      },
      error: (err) => {
        console.error(`Rating failed for ${type} ${id}`, err);
      }
    });
  }
}

