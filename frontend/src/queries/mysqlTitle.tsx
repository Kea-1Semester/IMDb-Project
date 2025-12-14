import { graphql } from '@/generated/gql';

export const MYSQL_TITLE = graphql(`
  query GetTitle($id: UUID) {
    mysqlTitles(where: { titleId: { eq: $id } }) {
      items {
        titleId
        primaryTitle
        originalTitle
        isAdult
        startYear
        endYear
        runtimeMinutes
        titleType
        genresGenre {
          genreId
          genre
        }
      }
    }
  }
`);
