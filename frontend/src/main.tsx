import { Provider } from '@/components/ui/provider.tsx';
import { Auth0Provider } from '@auth0/auth0-react';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import App from './App.tsx';
import './index.css';
import ApolloWrapper from '@/components/custom/wrappers/ApolloWrapper.tsx';

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
      <ApolloWrapper>
        <Provider>
          <App />
        </Provider>
      </ApolloWrapper>
    </Auth0Provider>
  </StrictMode>,
);
