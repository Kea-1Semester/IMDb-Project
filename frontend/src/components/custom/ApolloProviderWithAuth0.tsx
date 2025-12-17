import { type ReactNode, type FC, useMemo } from 'react';
import {
  ApolloClient,
  HttpLink,
  InMemoryCache,
  ApolloLink,
  Observable,
  CombinedProtocolErrors,
  CombinedGraphQLErrors,
} from '@apollo/client';
import { ApolloProvider } from '@apollo/client/react';
import { useAuth0 } from '@auth0/auth0-react';
import { ErrorLink } from '@apollo/client/link/error';

const ApolloProviderWithAuth0: FC<{ children: ReactNode }> = ({ children }) => {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();

  const errorLink = useMemo(
    () =>
      new ErrorLink(({ error }) => {
        if (CombinedGraphQLErrors.is(error)) {
          error.errors.forEach(({ message, locations, path }) => {
            console.error(
              `[GraphQL error]: Message: ${message}, Location: ${JSON.stringify(locations)}, Path: ${JSON.stringify(path)}`,
            );
          });
        } else if (CombinedProtocolErrors.is(error)) {
          error.errors.forEach(({ message, extensions }) =>
            console.error(`[Protocol error]: Message: ${message}, Extensions: ${JSON.stringify(extensions)}`),
          );
        } else {
          console.error(`[Network error]: ${error.message}`);
        }
      }),
    [],
  );

  const authLink = useMemo(
    () =>
      new ApolloLink(
        (operation, forward) =>
          new Observable((observer) => {
            void (async () => {
              let token: string | null;

              if (isAuthenticated) {
                try {
                  token = await getAccessTokenSilently();
                } catch (err) {
                  console.error('Error getting access token:', err);
                }
              }

              operation.setContext(({ headers = {} }: { headers?: Record<string, string> }) => ({
                headers: {
                  ...headers,
                  ...(token ? { Authorization: `Bearer ${token}` } : {}),
                },
              }));

              try {
                const obs = forward ? forward(operation) : null;
                if (obs) {
                  obs.subscribe({
                    next: (result) => observer.next(result),
                    error: (error) => observer.error(error),
                    complete: () => observer.complete(),
                  });
                } else {
                  observer.complete();
                }
              } catch (err) {
                observer.error(err);
              }
            })();
          }),
      ),
    [isAuthenticated, getAccessTokenSilently],
  );

  const httpLink = useMemo(
    () =>
      new HttpLink({
        uri: String(import.meta.env.VITE_API_URL),
        headers: {
          'GraphQL-Cost': 'Report',
        },
      }),
    [],
  );

  const client = useMemo(
    () =>
      new ApolloClient({
        link: ApolloLink.from([errorLink, authLink, httpLink]),
        cache: new InMemoryCache(),
      }),
    [errorLink, authLink, httpLink],
  );

  return <ApolloProvider client={client}>{children}</ApolloProvider>;
};

export default ApolloProviderWithAuth0;
