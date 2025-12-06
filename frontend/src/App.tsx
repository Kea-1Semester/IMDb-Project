import { Container, Grid, GridItem, Heading, Text } from '@chakra-ui/react';
import './App.css';
import AppLogIn from './pages/ApplogIn';
import { useQuery } from '@apollo/client/react';
import { gql } from '@apollo/client';

interface Reponse {
  mysqlTitles: {
    totalCount: number;
    items: Title[];
  };
}

interface Title {
  titleId: string;
  primaryTitle: string;
  originalTitle: string;
  isAdult: boolean;
  startYear: number;
  endYear: number | null;
  runtimeMinutes: number | null;
  titleType: string;
}

function App() {
  const GET_TITLES = gql`
    query GetTitles {
      mysqlTitles(take: 10, skip: 0, order: { startYear: DESC }, where: { endYear: { neq: null } }) {
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
  const { loading, error, data } = useQuery<Reponse>(GET_TITLES);

  return (
    <Container>
      <Grid templateAreas={`'header'`}>
        <GridItem area={'header'}>
          <Text>Header</Text>
          <Container>
            <Heading>Titles Count: {data?.mysqlTitles.totalCount}</Heading>
            <Grid>
              {loading && <Text>Loading...</Text>}
              {error && <Text>Error loading titles</Text>}
              {data &&
                data.mysqlTitles.items.map((title: Title) => (
                  <GridItem key={title.titleId}>
                    <Text>
                      {title.primaryTitle} ({title.startYear} - {title.endYear ?? 'N/A'})
                    </Text>
                  </GridItem>
                ))}
            </Grid>
          </Container>
        </GridItem>
      </Grid>
      <AppLogIn />
    </Container>
  );
}

export default App;
