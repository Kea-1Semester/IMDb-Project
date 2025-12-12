import { useQuery } from '@apollo/client/react';
import { useNavigate, useParams } from 'react-router';
import { Box, Button, Card, Heading, HStack, Text } from '@chakra-ui/react';
import QueryResult from '@/components/custom/QueryResult';
import { RiArrowLeftLine, RiDeleteBin2Line, RiEditBoxLine } from 'react-icons/ri';
import { MYSQL_TITLE } from '@/queries/mysqlTitle';
import { useAuth0 } from '@auth0/auth0-react';

const MysqlTitleDetails = () => {
  const { id } = useParams<{ id: string }>();
  const { isAuthenticated } = useAuth0();
  const { loading, error, data } = useQuery(MYSQL_TITLE, { variables: { id: id ?? '' } });
  const navigate = useNavigate();
  const title = data?.mysqlTitles?.items?.at(0);

  if (!title) return <Text>No Title with id: {id}</Text>;

  return (
    <Box>
      <QueryResult loading={loading} error={error} data={data}>
        <Card.Root shadow={'sm'}>
          <Card.Header>
            <Heading as={'h1'}>Title Details</Heading>
          </Card.Header>
          <Card.Body>
            {title && (
              <Box key={String(title.titleId)}>
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
                  <Text>{title.genresGenre?.map((genre) => genre?.genre).join(', ')}</Text>
                </HStack>
              </Box>
            )}
          </Card.Body>
          <Card.Footer>
            <HStack justify={'space-between'} w={'100%'}>
              <Button variant="outline" fontWeight={'bold'} onClick={() => void navigate(-1)}>
                <RiArrowLeftLine /> Back
              </Button>
              <Box>
                <HStack gap={'0.5rem'}>
                  {isAuthenticated && (
                    <>
                      <Button
                        variant="solid"
                        colorPalette={'teal'}
                        fontWeight={'bold'}
                        onClick={() => void navigate(`/mysqltitle/${id}/edit`)}
                      >
                        <RiEditBoxLine /> Edit
                      </Button>
                      <Button
                        variant="solid"
                        colorPalette={'red'}
                        fontWeight={'bold'}
                        onClick={() => void navigate(-1)}
                      >
                        <RiDeleteBin2Line /> Delete
                      </Button>
                    </>
                  )}
                </HStack>
              </Box>
            </HStack>
          </Card.Footer>
        </Card.Root>
      </QueryResult>
    </Box>
  );
};

export default MysqlTitleDetails;
