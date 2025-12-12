import type { Titles } from '@/generated/graphql';
import { Card, Heading, Text } from '@chakra-ui/react';
import type { FC } from 'react';
import { useNavigate } from 'react-router';

const TitleCard: FC<{ title: Partial<Titles> }> = ({ title }) => {
  const navigate = useNavigate();
  return (
    <Card.Root
      shadow={'sm'}
      _hover={{ bg: 'gray.subtle', cursor: 'pointer' }}
      onClick={() => {
        if (!title.titleId) return;
        void navigate(`/mysqltitle/${title.titleId}`);
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
