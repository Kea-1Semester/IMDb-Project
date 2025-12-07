import { type ReactNode, type FC, useMemo } from 'react';
import { ApolloClient, HttpLink, InMemoryCache, ApolloLink, Observable } from '@apollo/client';
import { ApolloProvider } from '@apollo/client/react';
import { useAuth0 } from '@auth0/auth0-react';

const ApolloProviderWithAuth0: FC<{ children: ReactNode }> = ({ children }) => {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();

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
      }),
    [],
  );

  const client = useMemo(
    () =>
      new ApolloClient({
        link: ApolloLink.from([authLink, httpLink]),
        cache: new InMemoryCache(),
      }),
    [authLink, httpLink],
  );

  return <ApolloProvider client={client}>{children}</ApolloProvider>;
};

export default ApolloProviderWithAuth0;
