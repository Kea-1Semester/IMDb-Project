import type {
  GetMysqlTitlesQuery,
  GetAllMysqlTitlesQueryVariables,
  GetMysqlTitleQuery,
  GetMysqlTitleQueryVariables,
} from '@/graphql/types/MysqlTitlesTypes';
import { gql, type TypedDocumentNode } from '@apollo/client';

const GET_ALL_MYSQL_TITLES: TypedDocumentNode<GetMysqlTitlesQuery, GetAllMysqlTitlesQueryVariables> = gql`
  query GetMysqlTitles($take: Int, $skip: Int) {
    mysqlTitles(take: $take, skip: $skip, order: { startYear: DESC }) {
      totalCount
      items {
        titleId
        primaryTitle
        originalTitle
        startYear
      }
    }
  }
`;

const GET_MYSQL_TITLE: TypedDocumentNode<GetMysqlTitleQuery, GetMysqlTitleQueryVariables> = gql`
  query GetMysqlTitles($where: TitlesFilterInput) {
    mysqlTitles(where: $where) {
      items {
        titleId
        primaryTitle
        originalTitle
        isAdult
        startYear
        endYear
        runtimeMinutes
        genresGenre {
          genreId
          genre
        }
      }
    }
  }
`;

export { GET_ALL_MYSQL_TITLES, GET_MYSQL_TITLE };
