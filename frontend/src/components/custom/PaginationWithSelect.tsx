import { ButtonGroup, createListCollection, HStack, IconButton, Pagination, Portal, Select } from '@chakra-ui/react';
import { LuChevronLeft, LuChevronRight } from 'react-icons/lu';

function PaginationWithSelect({
  setSkip,
  setTake,
  take,
  defaultPageSize,
  count,
}: {
  setSkip: (page: number) => void;
  setTake: (take: number) => void;
  take: number;
  defaultPageSize: number;
  count: number | undefined;
}) {
  const defaultPageSizeLabel = `${defaultPageSize}`;
  const itemsPerPageOptions = createListCollection({
    items: [
      { label: '10', value: 10 },
      { label: defaultPageSizeLabel, value: defaultPageSize },
      { label: '50', value: 50 },
      { label: '100', value: 100 },
    ],
  });

  return (
    <HStack justifyContent={'center'} marginY={4}>
      <Pagination.Root
        count={count}
        pageSize={take}
        defaultPage={1}
        defaultPageSize={defaultPageSize}
        onPageChange={(e) => void setSkip(e.page)}
      >
        <ButtonGroup variant={'outline'}>
          <Pagination.PrevTrigger asChild>
            <IconButton>
              <LuChevronLeft />
            </IconButton>
          </Pagination.PrevTrigger>

          <Pagination.Items
            render={(page) => <IconButton variant={{ base: 'outline', _selected: 'solid' }}>{page.value}</IconButton>}
          />

          <Pagination.NextTrigger asChild>
            <IconButton>
              <LuChevronRight />
            </IconButton>
          </Pagination.NextTrigger>

          <Select.Root
            collection={itemsPerPageOptions}
            variant={'outline'}
            width={'75px'}
            value={[defaultPageSizeLabel]}
            defaultValue={[defaultPageSizeLabel]}
            positioning={{ sameWidth: true }}
            onValueChange={(details) => void setTake(Number(details.value))}
          >
            <Select.HiddenSelect />
            <Select.Control>
              <Select.Trigger>
                <Select.ValueText placeholder={'Items'} />
              </Select.Trigger>
              <Select.IndicatorGroup>
                <Select.Indicator />
              </Select.IndicatorGroup>
            </Select.Control>
            <Portal>
              <Select.Positioner>
                <Select.Content>
                  {itemsPerPageOptions.items.map((item) => (
                    <Select.Item item={item} key={item.value}>
                      {item.label}
                      <Select.ItemIndicator />
                    </Select.Item>
                  ))}
                </Select.Content>
              </Select.Positioner>
            </Portal>
          </Select.Root>
        </ButtonGroup>
      </Pagination.Root>
    </HStack>
  );
}

export default PaginationWithSelect;
