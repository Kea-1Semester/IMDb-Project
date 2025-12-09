import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  schema: 'http://localhost:5000/graphql',
  documents: ['src/**/*.tsx'],
  generates: {
    './src/generated/': {
      preset: 'client',
      presetConfig: {
        gqlTagName: 'gql',
      },
    },
    './src/generated/types.ts': {
      plugins: ['typescript', 'typescript-operations'],
    },
  },
  // ignoreNoDocuments: true,
};

export default config;
