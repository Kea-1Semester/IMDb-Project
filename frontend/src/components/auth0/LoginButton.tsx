import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@chakra-ui/react';
import { RiLoginBoxLine } from 'react-icons/ri';

const LoginButton = () => {
  const { loginWithRedirect } = useAuth0();
  return (
    <Button
      onClick={() => {
        void loginWithRedirect({ authorizationParams: { screen_hint: 'signin' } });
      }}
      font={'lg'}
      fontWeight={'bold'}
      variant={'solid'}
      colorPalette={'teal'}
    >
      Log In <RiLoginBoxLine />
    </Button>
  );
};

export default LoginButton;
