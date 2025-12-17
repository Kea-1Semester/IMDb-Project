import { Box, Button, IconButton, Menu, Portal, Stack, VStack } from '@chakra-ui/react';
import { useAuth0 } from '@auth0/auth0-react';
import LogoutButton from '@/components/auth0/LogoutButton';
import LoginButton from '@/components/auth0/LoginButton';
import RegisterButton from '@/components/auth0/RegisterButton';
import { useNavigate } from 'react-router';
import { ColorModeButton } from '@/components/ui/color-mode';
import { RiAddCircleLine, RiHome2Line, RiMenuLine } from 'react-icons/ri';
import usePermissions from '@/hooks/UsePermissions';
import { auth0Permissions } from '@/types/Auth0Permissions';

const Navbar = () => {
  const { isAuthenticated } = useAuth0();
  const permissions = usePermissions();
  const navigate = useNavigate();

  return (
    <Stack direction={'row'} justifyContent={{ base: 'space-between' }} paddingY={'1rem'} paddingX={'2rem'}>
      <Button variant={'solid'} colorPalette={'teal'} fontWeight={'bold'} onClick={() => void navigate('/')}>
        <RiHome2Line />
        Home
      </Button>
      <Box>
        <ColorModeButton variant={'outline'} size={'md'} marginEnd={'0.5rem'} />
        <Menu.Root size={'md'}>
          <Menu.Trigger asChild>
            <IconButton variant={'solid'} _open={{ colorPalette: 'teal' }}>
              <RiMenuLine />
            </IconButton>
          </Menu.Trigger>
          <Portal>
            <Menu.Positioner>
              <Menu.Content>
                <VStack margin={'0.2rem'}>
                  {isAuthenticated && (
                    <Menu.Item asChild value="logout">
                      <LogoutButton />
                    </Menu.Item>
                  )}
                  {!isAuthenticated && (
                    <Menu.Item asChild value="login">
                      <LoginButton />
                    </Menu.Item>
                  )}
                  {!isAuthenticated && (
                    <Menu.Item asChild value="register">
                      <RegisterButton />
                    </Menu.Item>
                  )}
                  {isAuthenticated && permissions.includes(auth0Permissions.writePermission) && (
                    <Menu.Item asChild value="register">
                      <Button
                        variant={'solid'}
                        colorPalette={'green'}
                        fontWeight={'bold'}
                        onClick={() => {
                          void navigate('mysqltitle/create');
                        }}
                      >
                        Create Title
                        <RiAddCircleLine />
                      </Button>
                    </Menu.Item>
                  )}
                </VStack>
              </Menu.Content>
            </Menu.Positioner>
          </Portal>
        </Menu.Root>
      </Box>
    </Stack>
  );
};

export default Navbar;
