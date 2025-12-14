import { useAuth0 } from '@auth0/auth0-react';
import { jwtDecode, type JwtPayload } from 'jwt-decode';
import { useEffect, useState } from 'react';

interface JwtAccessToken extends JwtPayload {
  permissions?: string[];
}

const usePermissions = () => {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [permissions, setPermissions] = useState<string[]>([]);

  useEffect(() => {
    const loadToken = async () => {
      try {
        const token = await getAccessTokenSilently();
        const decoded = jwtDecode<JwtAccessToken>(token);
        setPermissions(decoded.permissions ?? []);
      } catch {
        setPermissions([]);
      }
    };

    if (isAuthenticated) {
      void loadToken();
    }
  }, [isAuthenticated, getAccessTokenSilently]);

  return permissions;
};

export default usePermissions;
