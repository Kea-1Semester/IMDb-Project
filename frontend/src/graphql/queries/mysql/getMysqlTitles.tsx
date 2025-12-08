import type { SortEnumType } from '@/graphql/types/SortEnumType';
import type { MysqlTitle } from '@/graphql/types/MysqlTitle';
import { gql, type TypedDocumentNode } from '@apollo/client';

type GetMysqlTitlesQuery = {
  mysqlTitles: {
    totalCount: number;
    items: MysqlTitle[];
  };
};

type GetMysqlTitlesQueryVariables = {
  take: number;
  skip: number;
  order: TitlesSortInput;
};

type TitlesSortInput = {
  titleId?: SortEnumType;
  titleType?: SortEnumType;
  primaryTitle?: SortEnumType;
  originalTitle?: SortEnumType;
  isAdult?: SortEnumType;
  startYear?: SortEnumType;
  endYear?: SortEnumType;
  runtimeMinutes?: SortEnumType;
};

const GET_MYSQL_TITLES: TypedDocumentNode<GetMysqlTitlesQuery, GetMysqlTitlesQueryVariables> = gql`
  query GetMysqlTitles($take: Int!, $skip: Int!, $order: [TitlesSortInput!]) {
    mysqlTitles(take: $take, skip: $skip, order: $order) {
      totalCount
      items {
        titleId
        primaryTitle
        originalTitle
        startYear
        endYear
      }
    }
  }
`;

export { GET_MYSQL_TITLES };
