import type { MysqlGenre } from '@/graphql/types/MysqlGenresTypes';
import type { UuidOperationsFilterInput } from './UuidOperationsFilterInput';

type MysqlTitle = {
  titleId: string;
  primaryTitle: string;
  originalTitle: string;
  isAdult: boolean;
  startYear: number;
  endYear: number | null;
  runtimeMinutes: number | null;
  titleType: string;
  genresGenre: MysqlTitleGenres[];
};

type MysqlTitleGenres = {
  genre: MysqlGenre;
};

type GetMysqlTitlesQuery = {
  mysqlTitles: {
    totalCount: number;
    items: MysqlTitle[];
  };
};

type GetMysqlTitleQuery = {
  mysqlTitles: {
    items: MysqlTitle[];
  };
};

type GetAllMysqlTitlesQueryVariables = {
  take: number;
  skip: number;
};

type TitlesFilterInput = {
  titleId: UuidOperationsFilterInput;
};

type GetMysqlTitleQueryVariables = {
  where: TitlesFilterInput;
};

export type {
  MysqlTitle,
  GetMysqlTitlesQuery,
  GetAllMysqlTitlesQueryVariables,
  GetMysqlTitleQuery,
  GetMysqlTitleQueryVariables,
};
