import type { Titles } from '@/generated/types';
import { Card, Heading, Text } from '@chakra-ui/react';
import type { FC } from 'react';
import { useNavigate } from 'react-router';

const TitleCard: FC<{ title: Titles }> = ({ title }) => {
  const navigate = useNavigate();
  return (
    <Card.Root
      _hover={{ bg: 'gray.800', cursor: 'pointer' }}
      onClick={() => {
        if (!title.titleId) return;
        void navigate(`/MysqlTitle/${title.titleId}`);
      }}
    >
      <Card.Body>
        <Heading>{title.primaryTitle}</Heading>
        <Text>{title.originalTitle}</Text>
        <Text>Start Year: {title.startYear ?? '-'}</Text>
      </Card.Body>
    </Card.Root>
  );
};

export default TitleCard;
