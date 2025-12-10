import { useQuery } from '@apollo/client/react';
import { useNavigate, useParams } from 'react-router';
import { Box, Button, Card, HStack, Text } from '@chakra-ui/react';
import { gql } from '@/generated';
import type { Titles } from '@/generated/types';
import QueryResult from '@/components/custom/QueryResult';

const TITLE = gql(`
  query GetTitle ($id: UUID) {
    mysqlTitles(where: { titleId: { eq: $id } } ) {
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
  }`);

const MysqlTitleDetails = () => {
  const { id } = useParams<{ id: string }>();
  const { loading, error, data } = useQuery(TITLE, { variables: { id: id ?? '' } });

  const navigate = useNavigate();

  return (
    <Box>
      <QueryResult loading={loading} error={error} data={data}>
        <Card.Root>
          <Card.Body>
            {data?.mysqlTitles?.items?.map((title: Titles) => (
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
              <Button variant="outline" onClick={() => void navigate('/')}>
                Back
              </Button>
            </HStack>
          </Card.Footer>
        </Card.Root>
      </QueryResult>
    </Box>
  );
};

export default MysqlTitleDetails;
