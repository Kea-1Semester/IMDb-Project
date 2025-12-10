import { Button, Flex, Stack } from '@chakra-ui/react';
import { useAuth0 } from '@auth0/auth0-react';
import LogoutButton from '@/components/auth0/LogoutButton';
import LoginButton from '@/components/auth0/LoginButton';
import RegisterButton from '@/components/auth0/RegisterButton';
import { useNavigate } from 'react-router';
import { ColorModeButton } from '@/components/ui/color-mode';
import { RiHome2Line } from 'react-icons/ri';

const Navbar = () => {
  const { isAuthenticated } = useAuth0();
  const navigate = useNavigate();

  return (
    <Stack direction={'row'} justifyContent={{ base: 'space-between' }} paddingY={'1rem'} paddingX={'2rem'}>
      <Button variant={'solid'} colorPalette={'teal'} fontWeight={'bold'} onClick={() => void navigate('/')}>
        <RiHome2Line /> Home
      </Button>
      <Flex gap={'0.5rem'} alignItems={'center'}>
        <ColorModeButton variant={'outline'} size={'md'} />
        {isAuthenticated && <LogoutButton />}
        {!isAuthenticated && <LoginButton />}
        {!isAuthenticated && <RegisterButton />}
      </Flex>
    </Stack>
  );
};

export default Navbar;
