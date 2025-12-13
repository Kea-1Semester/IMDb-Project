import { ActionBar, Container, Grid, GridItem, Portal } from '@chakra-ui/react';
import '@/App.css';
import Navbar from '@/components/custom/Navbar';
import { Outlet } from 'react-router';
import { ColorModeButton } from '@/components/ui/color-mode';

const Layout = () => {
  return (
    <Container padding={0}>
      <Grid templateAreas={`'header' 'main'`} gap="4">
        <GridItem area={'header'} shadow={'sm'}>
          <Navbar />
        </GridItem>
        <GridItem area={'main'}>
          <Container as={'main'}>
            <Outlet />
          </Container>
        </GridItem>
      </Grid>

      <ActionBar.Root>
        <Portal>
          <ActionBar.Positioner>
            <ActionBar.Content>
              <ColorModeButton />
            </ActionBar.Content>
          </ActionBar.Positioner>
        </Portal>
      </ActionBar.Root>
    </Container>
  );
};

export default Layout;
