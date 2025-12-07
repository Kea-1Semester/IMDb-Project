import type { SortEnumType } from '@/graphql/types/SortEnumType';
import { gql, type TypedDocumentNode } from '@apollo/client';

type GetMysqlTitlesQuery = {
  mysqlTitles: {
    totalCount: number;
    items: {
      titleId: string;
      primaryTitle: string;
      originalTitle: string;
      isAdult: boolean;
      startYear: number;
      endYear: number | null;
      runtimeMinutes: number | null;
      titleType: string;
    }[];
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
