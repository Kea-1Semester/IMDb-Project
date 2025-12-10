import type { ErrorLike } from '@apollo/client';
import { Container, Spinner } from '@chakra-ui/react';
import type { FC, PropsWithChildren } from 'react';

type QueryResultProps = {
  loading?: boolean;
  error: ErrorLike | undefined;
  data?: unknown;
};

const QueryResult: FC<PropsWithChildren<QueryResultProps>> = ({ loading, error, data, children }) => {
  if (error) {
    return <p>ERROR: {error.message}</p>;
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
  return <p>Nothing to show...</p>;
};

export default QueryResult;
