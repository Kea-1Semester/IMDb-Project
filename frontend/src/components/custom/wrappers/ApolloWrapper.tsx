import { useState, useEffect, type ReactNode } from 'react';
import { ApolloClient, HttpLink, InMemoryCache } from '@apollo/client';
import { ApolloProvider } from '@apollo/client/react';
import { useAuth0 } from '@auth0/auth0-react';

const ApolloWrapper = ({ children }: { children: ReactNode }) => {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [bearerToken, setBearerToken] = useState('');

  useEffect(() => {
    const getToken = async () => {
      const token = isAuthenticated ? await getAccessTokenSilently() : '';
      setBearerToken(token);
    };
    void getToken();
  }, [getAccessTokenSilently, isAuthenticated]);

  const httpLink = () => {
    return new HttpLink({
      uri: String(import.meta.env.VITE_API_URL),
      headers: {
        authorization: `Bearer ${bearerToken}`,
      },
    });
  };

  const client = new ApolloClient({
    link: httpLink(),
    cache: new InMemoryCache(),
  });

  return <ApolloProvider client={client}>{children}</ApolloProvider>;
};

export default ApolloWrapper;
