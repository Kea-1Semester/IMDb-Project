import { Container, Grid, GridItem } from '@chakra-ui/react';
import './App.css';
import Navbar from '@/components/custom/Navbar';
import TitlesList from './components/custom/TitlesList';

function App() {
  return (
    <Container padding={0}>
      <Grid templateAreas={`'header' 'main'`} gap="4">
        <GridItem area={'header'} shadow={'sm'}>
          <Navbar />
        </GridItem>
        <GridItem area={'main'}>
          <TitlesList />
        </GridItem>
      </Grid>
    </Container>
  );
}

export default App;
