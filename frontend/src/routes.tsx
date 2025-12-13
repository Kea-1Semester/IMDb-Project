import { createBrowserRouter, RouterProvider } from 'react-router';
import Layout from '@/pages/Layout';
import MysqlTitleDetails from '@/pages/MysqlTitleDetails';
import HomePage from '@/pages/HomePage';
import ErrorPage from '@/pages/ErrorPage';
import MysqlTitleEdit from './pages/MysqlTitleEdit';
import { useAuth0 } from '@auth0/auth0-react';
import AuthErrorPage from '@/pages/AuthErrorPage';
import usePermissions from '@/hooks/UsePermissions';
import { auth0Permissions } from '@/types/Auth0Permissions';

const RouterProviderWithAuth0 = () => {
  const { isAuthenticated } = useAuth0();
  const permissions = usePermissions();

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
                isAuthenticated && permissions.includes(auth0Permissions.updatePermission)
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
