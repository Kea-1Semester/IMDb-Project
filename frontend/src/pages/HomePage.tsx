import PaginationWithSelect from '@/components/custom/PaginationWithSelect';
import QueryResult from '@/components/custom/QueryResult';
import TitleCard from '@/components/custom/TitleCard';
import TitleCardContainer from '@/components/custom/TitleCardContainer';
import { gql } from '@/generated/gql';
import { useQuery } from '@apollo/client/react';
import { Box, SimpleGrid } from '@chakra-ui/react';
import { useState } from 'react';

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

const HomePage = () => {
  const defaultPageSize = 25;
  const [skip, setSkip] = useState<number>(0);
  const [take, setTake] = useState<number>(defaultPageSize);
  const { loading, error, data } = useQuery(TITLES, { variables: { skip: skip, take: take } });

  return (
    <Box>
      <QueryResult error={error} loading={loading} data={data}>
        <SimpleGrid columns={{ base: 1, md: 2, lg: 3 }} gap={4} mb={4}>
          {data?.mysqlTitles?.items?.map((title, index) => (
            <TitleCardContainer key={index}>
              <TitleCard title={title} />
            </TitleCardContainer>
          ))}
        </SimpleGrid>
      </QueryResult>
      <PaginationWithSelect
        setSkip={setSkip}
        setTake={setTake}
        take={take}
        defaultPageSize={defaultPageSize}
        count={data?.mysqlTitles?.totalCount ?? 0}
      />
    </Box>
  );
};

export default HomePage;
