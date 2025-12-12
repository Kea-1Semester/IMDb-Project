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
  NumberInputValueChangeDetails,
  Switch,
  SwitchCheckedChangeDetails,
  Text,
} from '@chakra-ui/react';
import { type TitlesDtoInput } from '@/generated/graphql';
import QueryResult from '@/components/custom/QueryResult';
import { RiArrowLeftLine, RiSave2Line } from 'react-icons/ri';
import { MYSQL_TITLE } from '@/queries/mysqlTitle';
import { EDIT_MYSQL_TITLE } from '@/queries/mysqlTitleEdit';
import { ChangeEvent, useState } from 'react';

const MysqlTitleEdit = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { loading, error, data } = useQuery(MYSQL_TITLE, { variables: { id: id ?? '' } });
  const [updateTitle, { error: updateError, data: updateData }] = useMutation(EDIT_MYSQL_TITLE);
  const client = useApolloClient();

  const title = data?.mysqlTitles?.items?.at(0);
  const [titleDto, setTitleDto] = useState<TitlesDtoInput>({
    primaryTitle: title?.primaryTitle ?? '',
    originalTitle: title?.originalTitle ?? '',
    titleType: title?.titleType ?? '',
    isAdult: title?.isAdult ?? false,
    startYear: title?.startYear ?? 0,
    endYear: title?.endYear,
    runtimeMinutes: title?.runtimeMinutes,
  });

  const handlePrimaryTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setTitleDto({
      ...titleDto,
      primaryTitle: e.target.value,
    });
  };

  const handleOriginalTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setTitleDto({
      ...titleDto,
      originalTitle: e.target.value,
    });
  };

  const handleTitleTypeChange = (e: ChangeEvent<HTMLInputElement>) => {
    setTitleDto({
      ...titleDto,
      titleType: e.target.value,
    });
  };

  const handleStartYearChange = (e: NumberInputValueChangeDetails) => {
    setTitleDto({
      ...titleDto,
      startYear: e.valueAsNumber,
    });
  };

  const handleEndYearChange = (e: NumberInputValueChangeDetails) => {
    setTitleDto({
      ...titleDto,
      endYear: e.valueAsNumber,
    });
  };

  const handleRuntimeMinutesChange = (e: NumberInputValueChangeDetails) => {
    setTitleDto({
      ...titleDto,
      runtimeMinutes: e.valueAsNumber,
    });
  };

  const handleIsAdultChange = (e: SwitchCheckedChangeDetails) => {
    setTitleDto({
      ...titleDto,
      isAdult: e.checked,
    });
  };

  const saveChanges = async () => {
    if (!title) return;
    const result = await updateTitle({
      variables: {
        id: title.titleId,
        title: titleDto,
      },
    });

    const updatedTitle = result.data?.updateMysqlTitle?.titles;

    if (!updatedTitle) {
      return;
    }

    await client.clearStore();

    await navigate(-1);
  };

  if (!title) return <Text>No Title with id: {id}</Text>;

  return (
    <Box>
      <QueryResult loading={loading} error={error} data={data}>
        <Card.Root shadow={'sm'}>
          <Card.Header>
            <Heading as={'h1'}>Title Edit</Heading>
          </Card.Header>
          <Card.Body>
            {updateError && <Text>{updateError.message}</Text>}
            {updateData?.updateMysqlTitle.errors?.map((error) => (
              <Text color={'red'} marginBottom={'0.5rem'}>
                {error.message}
              </Text>
            ))}
            <Field.Root>
              <Field.Label>PrimaryTitle</Field.Label>
              <Input
                name={'primaryTitle'}
                value={titleDto.primaryTitle}
                onChange={(event) => handlePrimaryTitleChange(event)}
              />
            </Field.Root>

            <Field.Root>
              <Field.Label>OriginalTitle</Field.Label>
              <Input
                name={'originalTitle'}
                value={titleDto.originalTitle}
                onChange={(event) => handleOriginalTitleChange(event)}
              />
            </Field.Root>

            <Field.Root>
              <Field.Label>TitleType</Field.Label>
              <Input name={'titleType'} value={titleDto.titleType} onChange={(event) => handleTitleTypeChange(event)} />
            </Field.Root>

            <Field.Root>
              <Field.Label>StartYear</Field.Label>
              <NumberInput.Root
                name={'startYear'}
                value={titleDto.startYear.toString()}
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
                value={titleDto.endYear ? titleDto.endYear.toString() : undefined}
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
                value={titleDto.runtimeMinutes ? titleDto.runtimeMinutes.toString() : undefined}
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
                checked={titleDto.isAdult}
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
                <RiSave2Line /> Save
              </Button>
            </HStack>
          </Card.Footer>
        </Card.Root>
      </QueryResult>
    </Box>
  );
};

export default MysqlTitleEdit;
