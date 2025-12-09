import type { MysqlTitle } from '@/graphql/types/MysqlTitlesTypes';

type MysqlGenre = {
  genreId: string;
  genre: string;
  titlesTitle: { title: MysqlTitle[] };
};

export type { MysqlGenre };
