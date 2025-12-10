import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@chakra-ui/react';
import { RiLoginBoxLine } from 'react-icons/ri';

const LoginButton = () => {
  const { loginWithRedirect } = useAuth0();
  const handleLogin = async () => {
    await (async () => {
      try {
        await loginWithRedirect({ authorizationParams: { screen_hint: 'signin' } });
      } catch (err) {
        console.error(err);
      }
    })();
  };

  return (
    <Button
      onClick={() => {
        void handleLogin();
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
