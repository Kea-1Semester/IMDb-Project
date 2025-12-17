import { graphql } from '@/generated/gql';

export const DELETE_MYSQL_TITLE = graphql(`
  mutation DeleteTitle($id: UUID!) {
    deleteMysqlTitle(input: { id: $id }) {
      titles {
        endYear
        isAdult
        originalTitle
        primaryTitle
        runtimeMinutes
        startYear
        titleId
        titleType
      }
    }
  }
`);
