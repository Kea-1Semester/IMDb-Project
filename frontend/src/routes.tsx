import { createBrowserRouter } from 'react-router';
import Layout from '@/pages/Layout';
import MysqlTitleDetails from '@/pages/MysqlTitleDetails';
import HomePage from '@/pages/HomePage';
import ErrorPage from '@/pages/ErrorPage';

const router = createBrowserRouter([
  {
    path: '/',
    Component: Layout,
    ErrorBoundary: ErrorPage,
    children: [
      { path: '/', Component: HomePage },
      { path: 'mysqltitle/:id', Component: MysqlTitleDetails },
    ],
  },
]);

export default router;
