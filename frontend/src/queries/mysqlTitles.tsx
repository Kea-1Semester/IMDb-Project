import { graphql } from '@/generated/gql';

export const MYSQL_TITLES = graphql(`
  query GetTitles($skip: Int, $take: Int) {
    mysqlTitles(skip: $skip, take: $take, order: { startYear: DESC }) {
      totalCount
      items {
        titleId
        primaryTitle
        originalTitle
        startYear
      }
    }
  }
`);
