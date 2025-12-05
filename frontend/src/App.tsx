import { Container, Grid, GridItem, Text } from '@chakra-ui/react';
import './App.css';
import AppLogIn from './pages/ApplogIn';

function App() {
  return (
    <Container>
      <Grid templateAreas={`'header'`}>
        <GridItem area={'header'}>
          <Text>Header</Text>
        </GridItem>
      </Grid>
      <AppLogIn />
    </Container>
  );
}

export default App;
