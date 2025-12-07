import { ButtonGroup, Container, For, Grid, GridItem, Heading, IconButton, Pagination, Text } from '@chakra-ui/react';
import './App.css';
import { useQuery } from '@apollo/client/react';
import { GET_MYSQL_TITLES } from '@/graphql/queries/mysql/getMysqlTitles';
import Navbar from '@/components/custom/Navbar';
import { LuChevronLeft, LuChevronRight } from 'react-icons/lu';
import { useState } from 'react';

function App() {
  const [skip, setSkip] = useState<number>(0);
  const take = 20;
  const { loading, error, data } = useQuery(GET_MYSQL_TITLES, {
    variables: { skip: skip, take: take, order: { startYear: 'DESC' } },
  });

  return (
    <Container padding={0}>
      <Grid templateAreas={`'header' 'main'`} gap="4">
        <GridItem area={'header'} shadow={'sm'}>
          <Navbar />
        </GridItem>
        <GridItem area={'main'}>
          <Container>
            <Heading as={'h2'}>Titles Count: {data && data.mysqlTitles.totalCount}</Heading>
            <Grid>
              {loading && <Text>Loading...</Text>}
              {error && <Text>{error.message}</Text>}
              {data && (
                <For each={data.mysqlTitles.items} fallback={<Text>No titles found</Text>}>
                  {(title) => (
                    <GridItem key={title.titleId}>
                      <Text>
                        {title.primaryTitle} ({title.startYear} - {title.endYear ?? 'N/A'})
                      </Text>
                    </GridItem>
                  )}
                </For>
              )}
            </Grid>
            <Pagination.Root
              count={data ? data.mysqlTitles.totalCount : 0}
              pageSize={take}
              defaultPage={1}
              onPageChange={(e) => setSkip(e.page)}
            >
              <ButtonGroup>
                <Pagination.PrevTrigger asChild>
                  <IconButton>
                    <LuChevronLeft />
                  </IconButton>
                </Pagination.PrevTrigger>
              </ButtonGroup>
              <Pagination.Items
                render={(page) => (
                  <IconButton variant={{ base: 'ghost', _selected: 'outline' }}>{page.value}</IconButton>
                )}
              />
              <ButtonGroup>
                <Pagination.NextTrigger asChild>
                  <IconButton>
                    <LuChevronRight />
                  </IconButton>
                </Pagination.NextTrigger>
              </ButtonGroup>
            </Pagination.Root>
          </Container>
        </GridItem>
      </Grid>
    </Container>
  );
}

export default App;
