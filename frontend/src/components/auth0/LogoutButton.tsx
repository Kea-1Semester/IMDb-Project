import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@chakra-ui/react';
import { RiLogoutBoxLine } from 'react-icons/ri';

const LogoutButton = () => {
  const { logout } = useAuth0();
  return (
    <Button
      onClick={() => {
        void logout({ logoutParams: { returnTo: globalThis.location.origin } });
      }}
      font={'lg'}
      fontWeight={'bold'}
      variant={'solid'}
      colorPalette={'teal'}
    >
      Log Out <RiLogoutBoxLine />
    </Button>
  );
};

export default LogoutButton;
