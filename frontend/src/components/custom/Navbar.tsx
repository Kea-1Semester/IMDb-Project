import { Grid, GridItem, Stack } from '@chakra-ui/react';
import { useAuth0 } from '@auth0/auth0-react';
import LogoutButton from '../auth0/LogoutButton';
import LoginButton from '../auth0/LoginButton';
import RegisterButton from '../auth0/RegisterButton';

const Navbar = () => {
  const { isAuthenticated } = useAuth0();

  return (
    <Stack direction={'row'} justifyContent={{ base: 'end' }} padding={'0.8rem'}>
      <Grid gap={'0.5rem'} templateColumns={'auto auto auto'}>
        <GridItem>{isAuthenticated && <LogoutButton />}</GridItem>
        <GridItem>{!isAuthenticated && <LoginButton />}</GridItem>
        <GridItem>{!isAuthenticated && <RegisterButton />}</GridItem>
      </Grid>
    </Stack>
  );
};

export default Navbar;
