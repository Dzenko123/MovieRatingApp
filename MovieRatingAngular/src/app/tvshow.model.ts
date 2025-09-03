export interface TvShowActor {
  actorId: number;
  actor: {
    id: number;
    name: string;
  };
}

export interface TvShow {
  tvShowId: number;
  title: string;
  description: string;
  releaseDate: string;
  coverImageUrl?: string;
  ratings: { value: number }[];
  tvShowActors: TvShowActor[];
  id?: number;
}
