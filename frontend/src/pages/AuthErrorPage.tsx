import { Container, Heading, Text } from '@chakra-ui/react';
import * as Sentry from '@sentry/react';
import { useEffect } from 'react';

const AuthErrorPage = () => {
  useEffect(() => {
    Sentry.captureException(new Error('Authentication error page rendered'));
  }, []);
  return (
    <Container textAlign="center" mt={10}>
      <Heading>Authentication Error!</Heading>
      <Text>User not logged in or doesn't have permission</Text>
    </Container>
  );
};

export default AuthErrorPage;
