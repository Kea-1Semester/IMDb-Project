import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  schema: 'http://localhost:5000/graphql/schema.graphql',
  documents: ['src/**/*.tsx'],
  generates: {
    './src/generated/': {
      preset: 'client',
      config: {
        scalars: {
          UUID: 'string',
        },
      },
    },
  },
  ignoreNoDocuments: true,
};

export default config;
