import { Box, SimpleGrid } from '@chakra-ui/react';
import { useState } from 'react';
import PaginationWithSelect from './PaginationWithSelect';
import TitleCardContainer from '@/components/custom/TitleCardContainer';
import TitleCard from '@/components/custom/TitleCard';
import { useQuery } from '@apollo/client/react';
import QueryResult from '@/components/custom/QueryResult';
import { gql } from '@/generated';

const TITLES = gql(`
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
  }`);

function TitlesList() {
  const defaultPageSize = 25;
  const [skip, setSkip] = useState<number>(0);
  const [take, setTake] = useState<number>(defaultPageSize);
  const { loading, error, data } = useQuery(TITLES, { variables: { skip: skip, take: take } });

  return (
    <Box>
      <SimpleGrid columns={{ base: 1, md: 2, lg: 3 }} gap={4} mb={4}>
        <QueryResult error={error} loading={loading} data={data}>
          {data?.mysqlTitles?.items?.map((title) => (
            <TitleCardContainer>
              <TitleCard title={title} />
            </TitleCardContainer>
          ))}
        </QueryResult>
      </SimpleGrid>
      <PaginationWithSelect
        setSkip={setSkip}
        setTake={setTake}
        take={take}
        defaultPageSize={defaultPageSize}
        count={data?.mysqlTitles?.totalCount ?? 0}
      />
    </Box>
  );
}

export default TitlesList;
