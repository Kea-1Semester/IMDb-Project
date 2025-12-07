import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@chakra-ui/react';

const RegisterButton = () => {
  const { loginWithRedirect } = useAuth0();
  return (
    <Button
      onClick={() => {
        void loginWithRedirect({ authorizationParams: { screen_hint: 'signup' } });
      }}
      font={'lg'}
      fontWeight={'bold'}
      variant={'outline'}
      colorPalette={'teal'}
    >
      Register
    </Button>
  );
};

export default RegisterButton;
