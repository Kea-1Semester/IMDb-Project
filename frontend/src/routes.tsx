import { createBrowserRouter, RouterProvider } from 'react-router';
import Layout from '@/pages/Layout';
import MysqlTitleDetails from '@/pages/MysqlTitleDetails';
import HomePage from '@/pages/HomePage';
import ErrorPage from '@/pages/ErrorPage';
import MysqlTitleEdit from './pages/MysqlTitleEdit';
import { useAuth0 } from '@auth0/auth0-react';
import AuthErrorPage from '@/pages/AuthErrorPage';
import { jwtDecode, type JwtPayload } from 'jwt-decode';
import { useEffect, useState } from 'react';

interface JwtAccessToken extends JwtPayload {
  permissions?: string[];
}

const RouterProviderWithAuth0 = () => {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [permissions, setPermissions] = useState<string[]>([]);

  useEffect(() => {
    const loadToken = async () => {
      try {
        const token = await getAccessTokenSilently();
        const decoded = jwtDecode<JwtAccessToken>(token);
        setPermissions(decoded.permissions ?? []);
      } catch {
        setPermissions([]);
      }
    };

    if (isAuthenticated) {
      void loadToken();
    }
  }, [isAuthenticated, getAccessTokenSilently]);

  const router = createBrowserRouter([
    {
      path: '/',
      Component: Layout,
      ErrorBoundary: ErrorPage,
      children: [
        { index: true, Component: HomePage },
        {
          path: 'mysqltitle',
          children: [
            { path: ':id', Component: MysqlTitleDetails },
            {
              path: ':id/edit',
              Component:
                isAuthenticated && permissions.includes('update:data') && permissions.includes('delete:data')
                  ? MysqlTitleEdit
                  : AuthErrorPage,
            },
          ],
        },
      ],
    },
  ]);
  return <RouterProvider router={router} />;
};

export default RouterProviderWithAuth0;
