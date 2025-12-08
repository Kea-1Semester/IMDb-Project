import type { MysqlTitle } from '@/graphql/types/MysqlTitle';
import { Card } from '@chakra-ui/react';

interface TitleCardProps {
  title: MysqlTitle;
}

const TitleCard = ({ title }: TitleCardProps) => {
  return (
    <Card.Root>
      <Card.Body>
        {title.primaryTitle} ({title.startYear} - {title.endYear ?? 'N/A'})
      </Card.Body>
    </Card.Root>
  );
};

export default TitleCard;
