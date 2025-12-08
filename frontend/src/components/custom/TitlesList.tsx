import { GET_MYSQL_TITLES } from '@/graphql/queries/mysql/getMysqlTitles';
import { useQuery } from '@apollo/client/react';
import { Container, For, SimpleGrid, Text } from '@chakra-ui/react';
import { useState } from 'react';
import PaginationWithSelect from './PaginationWithSelect';
import TitleCardContainer from './TitleCardContainer';
import TitleCard from './TitleCard';

function TitlesList() {
  const defaultPageSize = 25;
  const [skip, setSkip] = useState<number>(0);
  const [take, setTake] = useState<number>(defaultPageSize);
  const { loading, error, data } = useQuery(GET_MYSQL_TITLES, {
    variables: { skip: skip, take: take, order: { startYear: 'DESC' } },
  });

  return (
    <Container>
      <SimpleGrid columns={{ base: 1, md: 2, lg: 3 }} gap={4} mb={4}>
        {loading && <Text>Loading...</Text>}
        {error && <Text>{error.message}</Text>}
        {data && (
          <For each={data.mysqlTitles.items} fallback={<Text>No titles found</Text>}>
            {(title) => (
              <TitleCardContainer>
                <TitleCard title={title} />
              </TitleCardContainer>
            )}
          </For>
        )}
      </SimpleGrid>
      <PaginationWithSelect
        setSkip={setSkip}
        setTake={setTake}
        take={take}
        defaultPageSize={defaultPageSize}
        count={data ? data.mysqlTitles.totalCount : 0}
      />
    </Container>
  );
}

export default TitlesList;
