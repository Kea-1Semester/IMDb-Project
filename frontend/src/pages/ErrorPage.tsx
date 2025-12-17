import { Container, Heading, Text } from '@chakra-ui/react';
import { useRouteError } from 'react-router';
import * as Sentry from '@sentry/react';
import { useEffect } from 'react';

const ErrorPage = () => {
  const error = useRouteError() as Error;

  useEffect(() => {
    Sentry.captureException(error);
  }, [error]);

  return (
    <Container textAlign="center" mt={10}>
      <Heading>Error - 404</Heading>
      <Text>The requested URL was not found on this server.</Text>
    </Container>
  );
};

export default ErrorPage;
