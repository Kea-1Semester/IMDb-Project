import { useApolloClient, useMutation, useQuery } from '@apollo/client/react';
import { useNavigate, useParams } from 'react-router';
import {
  Box,
  Button,
  Card,
  Field,
  Heading,
  HStack,
  Input,
  NumberInput,
  type NumberInputValueChangeDetails,
  Switch,
  type SwitchCheckedChangeDetails,
  Text,
} from '@chakra-ui/react';
import { type TitlesDtoInput } from '@/generated/graphql';
import QueryResult from '@/components/custom/QueryResult';
import { RiArrowLeftLine, RiSave2Line } from 'react-icons/ri';
import { MYSQL_TITLE } from '@/queries/mysqlTitle';
import { EDIT_MYSQL_TITLE } from '@/queries/mysqlTitleEdit';
import { type ChangeEvent, useState } from 'react';

const MysqlTitleEdit = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { loading, error, data } = useQuery(MYSQL_TITLE, { variables: { id: id ?? '' } });
  const [updateTitle, { loading: updateLoading, error: updateError, data: updateData }] = useMutation(EDIT_MYSQL_TITLE);
  const client = useApolloClient();

  const title = data?.mysqlTitles?.items?.at(0);
  const [updateTitleDto, setUpdateTitleDto] = useState<TitlesDtoInput>({
    primaryTitle: title?.primaryTitle ?? '',
    originalTitle: title?.originalTitle ?? '',
    titleType: title?.titleType ?? '',
    isAdult: title?.isAdult ?? false,
    startYear: title?.startYear ?? 0,
    endYear: title?.endYear,
    runtimeMinutes: title?.runtimeMinutes,
  });

  const handlePrimaryTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      primaryTitle: e.target.value,
    });
  };

  const handleOriginalTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      originalTitle: e.target.value,
    });
  };

  const handleTitleTypeChange = (e: ChangeEvent<HTMLInputElement>) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      titleType: e.target.value,
    });
  };

  const handleStartYearChange = (e: NumberInputValueChangeDetails) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      startYear: e.valueAsNumber,
    });
  };

  const handleEndYearChange = (e: NumberInputValueChangeDetails) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      endYear: e.valueAsNumber,
    });
  };

  const handleRuntimeMinutesChange = (e: NumberInputValueChangeDetails) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      runtimeMinutes: e.valueAsNumber,
    });
  };

  const handleIsAdultChange = (e: SwitchCheckedChangeDetails) => {
    setUpdateTitleDto({
      ...updateTitleDto,
      isAdult: e.checked,
    });
  };

  const saveChanges = async () => {
    if (!title) return;
    const result = await updateTitle({
      variables: {
        id: title.titleId,
        title: updateTitleDto,
      },
    });

    const updatedTitle = result.data?.updateMysqlTitle?.titles;

    if (!updatedTitle) return;

    await client.clearStore();

    await navigate(-1);
  };

  return (
    <Box>
      <QueryResult loading={loading || updateLoading} error={error} data={data}>
        <Card.Root shadow={'sm'}>
          <Card.Header>
            <Heading as={'h1'}>Title Edit</Heading>
          </Card.Header>
          <Card.Body>
            {updateError && <Text>{updateError.message}</Text>}
            {updateData?.updateMysqlTitle.errors?.map((error) => (
              <Text key={error.message} color={'red'} marginBottom={'0.5rem'}>
                {error.message}
              </Text>
            ))}
            <Field.Root>
              <Field.Label>PrimaryTitle</Field.Label>
              <Input
                name={'primaryTitle'}
                value={updateTitleDto.primaryTitle}
                onChange={(event) => handlePrimaryTitleChange(event)}
              />
            </Field.Root>

            <Field.Root>
              <Field.Label>OriginalTitle</Field.Label>
              <Input
                name={'originalTitle'}
                value={updateTitleDto.originalTitle}
                onChange={(event) => handleOriginalTitleChange(event)}
              />
            </Field.Root>

            <Field.Root>
              <Field.Label>TitleType</Field.Label>
              <Input
                name={'titleType'}
                value={updateTitleDto.titleType}
                onChange={(event) => handleTitleTypeChange(event)}
              />
            </Field.Root>

            <Field.Root>
              <Field.Label>StartYear</Field.Label>
              <NumberInput.Root
                name={'startYear'}
                value={updateTitleDto.startYear.toString()}
                onValueChange={(event) => handleStartYearChange(event)}
                min={1888}
                max={new Date().getUTCFullYear()}
              >
                <NumberInput.Input />
              </NumberInput.Root>
            </Field.Root>

            <Field.Root>
              <Field.Label>EndYear</Field.Label>
              <NumberInput.Root
                name={'endYear'}
                value={updateTitleDto.endYear ? updateTitleDto.endYear.toString() : undefined}
                onValueChange={(event) => handleEndYearChange(event)}
                max={new Date().getUTCFullYear()}
              >
                <NumberInput.Input />
              </NumberInput.Root>
            </Field.Root>

            <Field.Root>
              <Field.Label>RuntimeMinutes</Field.Label>
              <NumberInput.Root
                name={'runtimeMinutes'}
                value={updateTitleDto.runtimeMinutes ? updateTitleDto.runtimeMinutes.toString() : undefined}
                onValueChange={(event) => handleRuntimeMinutesChange(event)}
                max={500}
              >
                <NumberInput.Input />
              </NumberInput.Root>
            </Field.Root>

            <Field.Root>
              <Field.Label>IsAdult</Field.Label>
              <Switch.Root
                name={'isAdult'}
                checked={updateTitleDto.isAdult}
                onCheckedChange={(event) => handleIsAdultChange(event)}
              >
                <Switch.HiddenInput />
                <Switch.Control />
              </Switch.Root>
            </Field.Root>
          </Card.Body>
          <Card.Footer>
            <HStack justify={'space-between'} w={'100%'}>
              <Button variant="outline" fontWeight={'bold'} onClick={() => void navigate(-1)}>
                <RiArrowLeftLine />
                Back
              </Button>
              <Button variant="solid" colorPalette={'teal'} fontWeight={'bold'} onClick={() => void saveChanges()}>
                <RiSave2Line />
                Save
              </Button>
            </HStack>
          </Card.Footer>
        </Card.Root>
      </QueryResult>
    </Box>
  );
};

export default MysqlTitleEdit;
