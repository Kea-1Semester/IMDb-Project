import { Container, Grid, GridItem } from '@chakra-ui/react';
import '@/App.css';
import Navbar from '@/components/custom/Navbar';
import { Outlet } from 'react-router';

const Layout = () => {
  return (
    <Container padding={0}>
      <Grid templateAreas={`'header' 'main'`} gap="4">
        <GridItem area={'header'} shadow={'sm'}>
          <Navbar />
        </GridItem>
        <GridItem area={'main'}>
          <Container>
            <Outlet />
          </Container>
        </GridItem>
      </Grid>
    </Container>
  );
};

export default Layout;
