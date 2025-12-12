import type { ErrorLike } from '@apollo/client';
import { Container, Spinner, Text } from '@chakra-ui/react';
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
      <Container justifyItems={'center'} alignItems={'center'}>
        <Spinner size={'lg'} color={'gray'} />
      </Container>
    );
  }
  if (data) {
    return <>{children}</>;
  }
  return <Text>Nothing to show...</Text>;
};

export default QueryResult;
