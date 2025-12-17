import { graphql } from '@/generated/gql';

export const CREATE_MYSQL_TITLE = graphql(`
  mutation CreateMysqlTitle($title: TitlesDtoInput!) {
    createMysqlTitle(input: { title: $title }) {
      errors {
        ... on Error {
          message
        }
      }
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
