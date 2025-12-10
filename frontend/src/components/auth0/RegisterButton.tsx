import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@chakra-ui/react';
import { RiAccountCircleLine } from 'react-icons/ri';

const RegisterButton = () => {
  const { loginWithRedirect } = useAuth0();
  return (
    <Button
      onClick={() => {
        void loginWithRedirect({ authorizationParams: { screen_hint: 'signup' } });
      }}
      font={'lg'}
      fontWeight={'bold'}
      variant={'surface'}
      colorPalette={'teal'}
    >
      Register <RiAccountCircleLine />
    </Button>
  );
};

export default RegisterButton;
