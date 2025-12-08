import type { FC, ReactNode } from 'react';
import { Box } from '@chakra-ui/react';

const TitleCardContainer: FC<{ children: ReactNode }> = ({ children }) => {
  return <Box>{children}</Box>;
};

export default TitleCardContainer;
