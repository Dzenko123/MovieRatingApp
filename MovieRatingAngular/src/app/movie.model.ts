export interface MovieActor {
  actorId: number;
  actor: {
    id: number;
    name: string;
  };
}

export interface Movie {
  movieId: number;
  title: string;
  description: string;
  releaseDate: string;
  coverImageUrl?: string;
  ratings: { value: number }[];
  movieActors: MovieActor[];
  id?: number;
}
