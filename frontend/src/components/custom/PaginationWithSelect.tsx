import { ButtonGroup, For, HStack, IconButton, NativeSelect, Pagination } from '@chakra-ui/react';
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
  const itemsPerPageOptions: Array<number> = [10, defaultPageSize, 50, 100];

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

          <NativeSelect.Root
            variant={'outline'}
            defaultValue={defaultPageSize}
            onChange={(value) => {
              setTake(Number((value.target as HTMLSelectElement).value));
            }}
          >
            <NativeSelect.Field bg={'black'} _hover={{ bg: 'gray.900', cursor: 'pointer' }}>
              <For each={itemsPerPageOptions}>
                {(option) => (
                  <option key={option} defaultValue={defaultPageSize} value={option}>
                    {option}
                  </option>
                )}
              </For>
            </NativeSelect.Field>
            <NativeSelect.Indicator />
          </NativeSelect.Root>
        </ButtonGroup>
      </Pagination.Root>
    </HStack>
  );
}

export default PaginationWithSelect;
