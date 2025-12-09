import { GET_MYSQL_TITLE } from '@/graphql/queries/mysql/getMysqlTitles';
import { useQuery } from '@apollo/client/react';
import { useNavigate, useParams } from 'react-router';
import { Box, Button, Card, HStack, Text } from '@chakra-ui/react';

const MysqlTitleDetails = () => {
  const { id } = useParams<{ id: string }>();
  const { loading, error, data } = useQuery(GET_MYSQL_TITLE, {
    variables: { where: { titleId: { eq: id ?? '' } } },
  });

  const navigate = useNavigate();

  return (
    <Box>
      <Card.Root>
        <Card.Body>
          {loading && <Text>loading...</Text>}
          {error && <Text>Error: {error.message}</Text>}
          {data && data.mysqlTitles.items && (
            <Box>
              <HStack>
                <Text fontWeight={'bold'}>PrimaryTitle:</Text>
                <Text>{data && data.mysqlTitles.items.map((title) => title?.primaryTitle ?? '-')}</Text>
              </HStack>
              <HStack>
                <Text fontWeight={'bold'}>OriginalTitle:</Text>
                <Text>{data && data.mysqlTitles.items.map((title) => title?.originalTitle ?? '-')}</Text>
              </HStack>
              <HStack>
                <Text fontWeight={'bold'}>IsAdult:</Text>
                <Text>{data && data.mysqlTitles.items.map((title) => title?.isAdult ?? '-') ? 'No' : 'Yes'}</Text>
              </HStack>
              <HStack>
                <Text fontWeight={'bold'}>StartYear:</Text>
                <Text>{data && data.mysqlTitles.items.map((title) => title?.startYear ?? '-')}</Text>
              </HStack>
              <HStack>
                <Text fontWeight={'bold'}>EndYear:</Text>
                <Text>{data && data.mysqlTitles.items.map((title) => title?.endYear ?? '-')}</Text>
              </HStack>
              <HStack>
                <Text fontWeight={'bold'}>RuntimeMinutes:</Text>
                <Text>{data && data.mysqlTitles.items.map((title) => title?.runtimeMinutes ?? '-')}</Text>
              </HStack>
              <HStack>
                <Text fontWeight={'bold'}>Genres:</Text>
                <Text>
                  {data &&
                    data.mysqlTitles.items.map(({ genresGenre }) =>
                      genresGenre.map(({ genre }) => genre.genre).join(','),
                    )}
                </Text>
              </HStack>
            </Box>
          )}
        </Card.Body>
        <Card.Footer>
          <HStack justify={'end'}>
            <Button variant="outline" onClick={() => void navigate('/')}>
              Back
            </Button>
          </HStack>
        </Card.Footer>
      </Card.Root>
    </Box>
  );
};

export default MysqlTitleDetails;
