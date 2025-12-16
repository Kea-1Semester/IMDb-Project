import { Provider } from '@/components/ui/provider.tsx';
import { Auth0Provider } from '@auth0/auth0-react';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import ApolloProviderWithAuth0 from '@/components/custom/ApolloProviderWithAuth0.tsx';
import RouterProviderWithAuth0 from '@/routes.tsx';
import * as Sentry from '@sentry/react';
import './index.css';

Sentry.init({
  dsn: String(import.meta.env.VITE_SENTRY_DSN),
  environment: String(import.meta.env.MODE),
  integrations: [
    Sentry.browserTracingIntegration(),
    Sentry.consoleLoggingIntegration({ levels: ['log', 'warn', 'error'] }),
  ],
  tracesSampleRate: 1.0,
  tracePropagationTargets: [String(import.meta.env.BASE_URL), String(import.meta.env.VITE_API_URL)],
  enableLogs: true,
  sendDefaultPii: true,
});

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
          <RouterProviderWithAuth0 />
        </Provider>
      </ApolloProviderWithAuth0>
    </Auth0Provider>
  </StrictMode>,
);
