import { graphql } from '@/generated/gql';

export const EDIT_MYSQL_TITLE = graphql(`
  mutation EditTitle($id: UUID!, $title: TitlesDtoInput!) {
    updateMysqlTitle(input: { id: $id, title: $title }) {
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
      errors {
        ... on Error {
          message
        }
      }
    }
  }
`);
