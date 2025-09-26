import './App.css';
import { Container, Grid, GridItem, Text } from '@chakra-ui/react';

function App() {
  return (
    <Container>
      <Grid templateAreas={`'header'`}>
        <GridItem area={'header'}>
          <Text>Header</Text>
        </GridItem>
      </Grid>
    </Container>
  );
}

export default App;
