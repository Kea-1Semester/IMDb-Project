import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  overwrite: true,
  schema: 'http://localhost:5000/graphql/schema.graphql',
  documents: ['src/**/*.{ts,tsx}'],
  ignoreNoDocuments: true,
  generates: {
    './src/generated/': {
      preset: 'client',
      presetConfig: {
        fragmentMasking: false,
      },
      config: {
        avoidOptionals: {
          field: true,
          inputValue: false,
        },
        useTypeImports: true,
        defaultScalarType: 'unknown',
        nonOptionalTypename: true,
        skipTypeNameForRoot: true,
      },
    },
  },
};

export default config;
