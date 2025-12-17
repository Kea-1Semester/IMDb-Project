import { useApolloClient, useMutation } from '@apollo/client/react';
import { useNavigate } from 'react-router';
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
  Spinner,
  Switch,
  type SwitchCheckedChangeDetails,
  Text,
} from '@chakra-ui/react';
import { type TitlesDtoInput } from '@/generated/graphql';
import { RiAddCircleLine, RiArrowLeftLine } from 'react-icons/ri';
import { type ChangeEvent, useState } from 'react';
import { CREATE_MYSQL_TITLE } from '@/queries/mysqlTitleCreate';

const MysqlTitleCreate = () => {
  const navigate = useNavigate();
  const [createTitle, { loading, error, data }] = useMutation(CREATE_MYSQL_TITLE);
  const client = useApolloClient();

  const [createTitleDto, setCreateTitleDto] = useState<TitlesDtoInput>({
    primaryTitle: '',
    originalTitle: '',
    titleType: '',
    isAdult: false,
    startYear: new Date().getUTCFullYear(),
    endYear: null,
    runtimeMinutes: null,
  });

  const handlePrimaryTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setCreateTitleDto({
      ...createTitleDto,
      primaryTitle: e.target.value,
    });
  };

  const handleOriginalTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setCreateTitleDto({
      ...createTitleDto,
      originalTitle: e.target.value,
    });
  };

  const handleTitleTypeChange = (e: ChangeEvent<HTMLInputElement>) => {
    setCreateTitleDto({
      ...createTitleDto,
      titleType: e.target.value,
    });
  };

  const handleStartYearChange = (e: NumberInputValueChangeDetails) => {
    setCreateTitleDto({
      ...createTitleDto,
      startYear: e.valueAsNumber,
    });
  };

  const handleEndYearChange = (e: NumberInputValueChangeDetails) => {
    setCreateTitleDto({
      ...createTitleDto,
      endYear: e.valueAsNumber,
    });
  };

  const handleRuntimeMinutesChange = (e: NumberInputValueChangeDetails) => {
    setCreateTitleDto({
      ...createTitleDto,
      runtimeMinutes: e.valueAsNumber,
    });
  };

  const handleIsAdultChange = (e: SwitchCheckedChangeDetails) => {
    setCreateTitleDto({
      ...createTitleDto,
      isAdult: e.checked,
    });
  };

  const saveChanges = async () => {
    const result = await createTitle({ variables: { title: createTitleDto } });

    const createdTitle = result.data?.createMysqlTitle?.titles;

    if (!createdTitle) {
      return;
    }

    await client.clearStore();

    await navigate(`/mysqltitle/${createdTitle.titleId}`, { relative: 'path' });
  };

  if (loading) {
    return (
      <Box textAlign={'center'}>
        <Spinner size={'lg'}></Spinner>
      </Box>
    );
  }

  return (
    <Box>
      <Card.Root shadow={'sm'}>
        <Card.Header>
          <Heading as={'h1'}>Create Title</Heading>
        </Card.Header>
        <Card.Body>
          {error && <Text>{error.message}</Text>}
          {data?.createMysqlTitle.errors?.map((error) => (
            <Text key={error.message} color={'red'} marginBottom={'0.5rem'}>
              {error.message}
            </Text>
          ))}
          <Field.Root>
            <Field.Label>PrimaryTitle</Field.Label>
            <Input
              name={'primaryTitle'}
              value={createTitleDto.primaryTitle}
              onChange={(event) => handlePrimaryTitleChange(event)}
            />
          </Field.Root>

          <Field.Root>
            <Field.Label>OriginalTitle</Field.Label>
            <Input
              name={'originalTitle'}
              value={createTitleDto.originalTitle}
              onChange={(event) => handleOriginalTitleChange(event)}
            />
          </Field.Root>

          <Field.Root>
            <Field.Label>TitleType</Field.Label>
            <Input
              name={'titleType'}
              value={createTitleDto.titleType}
              onChange={(event) => handleTitleTypeChange(event)}
            />
          </Field.Root>

          <Field.Root>
            <Field.Label>StartYear</Field.Label>
            <NumberInput.Root
              name={'startYear'}
              value={createTitleDto.startYear.toString()}
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
              value={createTitleDto.endYear ? createTitleDto.endYear.toString() : undefined}
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
              value={createTitleDto.runtimeMinutes ? createTitleDto.runtimeMinutes.toString() : undefined}
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
              checked={createTitleDto.isAdult}
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
              <RiArrowLeftLine /> Back
            </Button>
            <Button variant="solid" colorPalette={'teal'} fontWeight={'bold'} onClick={() => void saveChanges()}>
              <RiAddCircleLine />
              Create
            </Button>
          </HStack>
        </Card.Footer>
      </Card.Root>
    </Box>
  );
};

export default MysqlTitleCreate;
