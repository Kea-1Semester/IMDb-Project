import { Provider } from '@/components/ui/provider.tsx';
import { Auth0Provider } from '@auth0/auth0-react';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import ApolloProviderWithAuth0 from '@/components/custom/ApolloProviderWithAuth0.tsx';
import { RouterProvider } from 'react-router';
import router from './routes.tsx';
import './index.css';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Auth0Provider
      domain={String(import.meta.env.VITE_AUTH0_DOMAIN)}
      clientId={String(import.meta.env.VITE_AUTH0_CLIENT_ID)}
      authorizationParams={{
        redirect_uri: globalThis.location.origin,
        audience: String(import.meta.env.VITE_API_IDENTIFIER),
      }}
    >
      <ApolloProviderWithAuth0>
        <Provider>
          <RouterProvider router={router} />
        </Provider>
      </ApolloProviderWithAuth0>
    </Auth0Provider>
  </StrictMode>,
);
