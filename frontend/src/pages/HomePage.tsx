import PaginationWithSelect from '@/components/custom/PaginationWithSelect';
import QueryResult from '@/components/custom/QueryResult';
import TitleCard from '@/components/custom/TitleCard';
import TitleCardContainer from '@/components/custom/TitleCardContainer';
import { MYSQL_TITLES } from '@/queries/mysqlTitles';
import { useQuery } from '@apollo/client/react';
import { Box, SimpleGrid } from '@chakra-ui/react';
import { useState } from 'react';

const HomePage = () => {
  const defaultPageSize = 25;
  const [skip, setSkip] = useState<number>(0);
  const [take, setTake] = useState<number>(defaultPageSize);
  const { loading, error, data } = useQuery(MYSQL_TITLES, { variables: { skip: skip, take: take } });

  return (
    <Box>
      <PaginationWithSelect
        setSkip={setSkip}
        setTake={setTake}
        take={take}
        defaultPageSize={defaultPageSize}
        count={(data && data.mysqlTitles?.totalCount) ?? 0}
      />
      <QueryResult error={error} loading={loading} data={data}>
        <SimpleGrid columns={{ base: 1, md: 2, lg: 3 }} gap={4} mb={4}>
          {data?.mysqlTitles?.items?.map((title) => (
            <TitleCardContainer key={String(title.titleId)}>
              <TitleCard title={title} />
            </TitleCardContainer>
          ))}
        </SimpleGrid>
      </QueryResult>
    </Box>
  );
};

export default HomePage;
