import { useQuery } from '@apollo/client/react';
import { useNavigate, useParams } from 'react-router';
import { Box, Button, Card, HStack, Text } from '@chakra-ui/react';
import { graphql } from '@/generated/gql';
import type { GetTitleQuery, Titles } from '@/generated/graphql';
import QueryResult from '@/components/custom/QueryResult';
import { RiArrowLeftLine } from 'react-icons/ri';

const TITLE = graphql(`
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

const MysqlTitleDetails = () => {
  const { id } = useParams<{ id: string }>();
  const { loading, error, data } = useQuery<GetTitleQuery>(TITLE, { variables: { id: id ?? '' } });

  const navigate = useNavigate();

  return (
    <Box>
      <QueryResult loading={loading} error={error} data={data}>
        <Card.Root shadow={'sm'}>
          <Card.Body>
            {data &&
              data.mysqlTitles &&
              data.mysqlTitles.items &&
              data.mysqlTitles.items.map((title: Titles) => (
                <Box key={title.titleId}>
                  <HStack>
                    <Text fontWeight={'bold'}>PrimaryTitle:</Text>
                    <Text>{title.primaryTitle ?? '-'}</Text>
                  </HStack>
                  <HStack>
                    <Text fontWeight={'bold'}>OriginalTitle:</Text>
                    <Text>{title.originalTitle ?? '-'}</Text>
                  </HStack>
                  <HStack>
                    <Text fontWeight={'bold'}>IsAdult:</Text>
                    <Text>{title.isAdult ? 'Yes' : 'No'}</Text>
                  </HStack>
                  <HStack>
                    <Text fontWeight={'bold'}>StartYear:</Text>
                    <Text>{title.startYear ?? '-'}</Text>
                  </HStack>
                  <HStack>
                    <Text fontWeight={'bold'}>EndYear:</Text>
                    <Text>{title.endYear ?? '-'}</Text>
                  </HStack>
                  <HStack>
                    <Text fontWeight={'bold'}>RuntimeMinutes:</Text>
                    <Text>{title.runtimeMinutes ?? '-'}</Text>
                  </HStack>
                  <HStack>
                    <Text fontWeight={'bold'}>Genres:</Text>
                    <Text>{title.genresGenre?.map((genre) => genre?.genre).join(', ') ?? '-'}</Text>
                  </HStack>
                </Box>
              ))}
          </Card.Body>
          <Card.Footer>
            <HStack justify={'end'}>
              <Button variant="outline" fontWeight={'bold'} onClick={() => void navigate('/')}>
                <RiArrowLeftLine /> Back
              </Button>
            </HStack>
          </Card.Footer>
        </Card.Root>
      </QueryResult>
    </Box>
  );
};

export default MysqlTitleDetails;
