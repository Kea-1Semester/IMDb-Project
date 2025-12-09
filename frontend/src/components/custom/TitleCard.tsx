import type { MysqlTitle } from '@/graphql/types/MysqlTitlesTypes';
import { Card, Heading, Text } from '@chakra-ui/react';
import { useNavigate } from 'react-router';

interface TitleCardProps {
  title: MysqlTitle;
}

const TitleCard = ({ title }: TitleCardProps) => {
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
