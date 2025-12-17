import type { Maybe, Scalars } from '@/generated/graphql';
import { DELETE_MYSQL_TITLE } from '@/queries/mysqlTitleDelete';
import { useApolloClient, useMutation } from '@apollo/client/react';
import { Button, CloseButton, Dialog, Portal, Spinner, Text } from '@chakra-ui/react';
import type { FC } from 'react';
import { RiDeleteBin2Line } from 'react-icons/ri';
import { useNavigate } from 'react-router';

interface DeleteDialogProps {
  __typename: 'Titles';
  endYear: Maybe<Scalars['Int']['output']>;
  isAdult: Scalars['Boolean']['output'];
  originalTitle: Scalars['String']['output'];
  primaryTitle: Scalars['String']['output'];
  runtimeMinutes: Maybe<Scalars['Int']['output']>;
  startYear: Scalars['Int']['output'];
  titleId: Scalars['UUID']['output'];
  titleType: Scalars['String']['output'];
}

const TitleCard: FC<{ title: DeleteDialogProps }> = ({ title }) => {
  const [deleteTitle, { loading, error }] = useMutation(DELETE_MYSQL_TITLE);
  const navigate = useNavigate();
  const client = useApolloClient();

  const confirm = async () => {
    if (!title.titleId) return;
    const result = await deleteTitle({ variables: { id: title.titleId } });

    const deletedTitle = result.data?.deleteMysqlTitle.titles;

    if (!deletedTitle) return;

    await client.clearStore();

    await navigate('/');
  };

  return (
    <Dialog.Root>
      <Dialog.Trigger asChild>
        <Button variant="solid" colorPalette={'red'} fontWeight={'bold'}>
          <RiDeleteBin2Line />
          Delete
        </Button>
      </Dialog.Trigger>
      <Portal>
        <Dialog.Backdrop />
        <Dialog.Positioner>
          <Dialog.Content>
            <Dialog.Header>
              <Dialog.Title>Delete Title!</Dialog.Title>
            </Dialog.Header>
            <Dialog.Body>
              {error && <Text color={'red'}>{error.message}</Text>}
              {loading && <Spinner size={'lg'} />}
              {!loading && (
                <Text>
                  Are you sure you want to delete:
                  <Text fontWeight={'bold'}>{title.primaryTitle}</Text>
                </Text>
              )}
            </Dialog.Body>
            <Dialog.Footer>
              <Button disabled={loading} variant="outline" onClick={() => void confirm()}>
                Yes
              </Button>
              <Dialog.ActionTrigger asChild>
                <Button disabled={loading}>Cancel</Button>
              </Dialog.ActionTrigger>
            </Dialog.Footer>
            <Dialog.CloseTrigger asChild>
              <CloseButton size="sm" />
            </Dialog.CloseTrigger>
          </Dialog.Content>
        </Dialog.Positioner>
      </Portal>
    </Dialog.Root>
  );
};

export default TitleCard;
