import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@chakra-ui/react';

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
      Log In
    </Button>
  );
};

export default LoginButton;
