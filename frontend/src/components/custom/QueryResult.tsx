import type { ErrorLike } from '@apollo/client';
import { Container, HStack, Spinner, Text } from '@chakra-ui/react';
import type { FC, PropsWithChildren } from 'react';

type QueryResultProps = {
  loading: boolean;
  error?: ErrorLike;
  data?: unknown;
};

const QueryResult: FC<PropsWithChildren<QueryResultProps>> = ({ loading, error, data, children }) => {
  if (error) {
    return <Text>ERROR: {error.message}</Text>;
  }
  if (loading) {
    return (
      <Container>
        <HStack justify={'center'}>
          <Spinner size={'lg'} color={'gray'} />
        </HStack>
      </Container>
    );
  }
  if (data) {
    return <>{children}</>;
  }
  return <Text>Nothing to show...</Text>;
};

export default QueryResult;
