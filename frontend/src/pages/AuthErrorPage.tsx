import { Container, Heading, Text } from '@chakra-ui/react';

const ErrorPage = () => {
  return (
    <Container textAlign="center" mt={10}>
      <Heading>Authentication Error!</Heading>
      <Text>User not logged in or doesn't have permission</Text>
    </Container>
  );
};

export default ErrorPage;
