/* eslint-disable */
import { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';
export type Maybe<T> = T | null;
export type InputMaybe<T> = T | null | undefined;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  UUID: { input: string; output: string; }
};

export type Actors = {
  __typename?: 'Actors';
  actorId: Scalars['UUID']['output'];
  personsPerson?: Maybe<Persons>;
  personsPersonId: Scalars['UUID']['output'];
  role: Scalars['String']['output'];
  titlesTitle?: Maybe<Titles>;
  titlesTitleId: Scalars['UUID']['output'];
};

export type ActorsFilterInput = {
  actorId?: InputMaybe<UuidOperationFilterInput>;
  and?: InputMaybe<Array<ActorsFilterInput>>;
  or?: InputMaybe<Array<ActorsFilterInput>>;
  personsPerson?: InputMaybe<PersonsFilterInput>;
  personsPersonId?: InputMaybe<UuidOperationFilterInput>;
  role?: InputMaybe<StringOperationFilterInput>;
  titlesTitle?: InputMaybe<TitlesFilterInput>;
  titlesTitleId?: InputMaybe<UuidOperationFilterInput>;
};

export type AddMysqlGenreError = ValidationError;

export type AddMysqlGenreInput = {
  genreId: Scalars['UUID']['input'];
  titleId: Scalars['UUID']['input'];
};

export type AddMysqlGenrePayload = {
  __typename?: 'AddMysqlGenrePayload';
  errors?: Maybe<Array<AddMysqlGenreError>>;
  genres?: Maybe<Genres>;
};

export type Aliases = {
  __typename?: 'Aliases';
  aliasId: Scalars['UUID']['output'];
  attributesAttribute?: Maybe<Array<Maybe<Attributes>>>;
  isOriginalTitle: Scalars['Boolean']['output'];
  language: Scalars['String']['output'];
  region: Scalars['String']['output'];
  title: Scalars['String']['output'];
  titleId: Scalars['UUID']['output'];
  titleNavigation?: Maybe<Titles>;
  typesType?: Maybe<Array<Maybe<Types>>>;
};

export type AliasesDtoInput = {
  isOriginalTitle: Scalars['Boolean']['input'];
  language: Scalars['String']['input'];
  region: Scalars['String']['input'];
  title: Scalars['String']['input'];
  titleId: Scalars['UUID']['input'];
};

export type AliasesFilterInput = {
  aliasId?: InputMaybe<UuidOperationFilterInput>;
  and?: InputMaybe<Array<AliasesFilterInput>>;
  attributesAttribute?: InputMaybe<ListFilterInputTypeOfAttributesFilterInput>;
  isOriginalTitle?: InputMaybe<BooleanOperationFilterInput>;
  language?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<AliasesFilterInput>>;
  region?: InputMaybe<StringOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  titleId?: InputMaybe<UuidOperationFilterInput>;
  titleNavigation?: InputMaybe<TitlesFilterInput>;
  typesType?: InputMaybe<ListFilterInputTypeOfTypesFilterInput>;
};

/** Defines when a policy shall be executed. */
export enum ApplyPolicy {
  /** After the resolver was executed. */
  AfterResolver = 'AFTER_RESOLVER',
  /** Before the resolver was executed. */
  BeforeResolver = 'BEFORE_RESOLVER',
  /** The policy is applied in the validation step before the execution. */
  Validation = 'VALIDATION'
}

export type ArgumentError = Error & {
  __typename?: 'ArgumentError';
  message: Scalars['String']['output'];
  paramName?: Maybe<Scalars['String']['output']>;
};

export type Attributes = {
  __typename?: 'Attributes';
  aliasesAlias?: Maybe<Array<Maybe<Aliases>>>;
  attribute: Scalars['String']['output'];
  attributeId: Scalars['UUID']['output'];
};

export type AttributesFilterInput = {
  aliasesAlias?: InputMaybe<ListFilterInputTypeOfAliasesFilterInput>;
  and?: InputMaybe<Array<AttributesFilterInput>>;
  attribute?: InputMaybe<StringOperationFilterInput>;
  attributeId?: InputMaybe<UuidOperationFilterInput>;
  or?: InputMaybe<Array<AttributesFilterInput>>;
};

export type BooleanOperationFilterInput = {
  eq?: InputMaybe<Scalars['Boolean']['input']>;
  neq?: InputMaybe<Scalars['Boolean']['input']>;
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
};

export type Comments = {
  __typename?: 'Comments';
  comment: Scalars['String']['output'];
  commentId: Scalars['UUID']['output'];
  title?: Maybe<Titles>;
  titleId: Scalars['UUID']['output'];
};

export type CommentsFilterInput = {
  and?: InputMaybe<Array<CommentsFilterInput>>;
  comment?: InputMaybe<StringOperationFilterInput>;
  commentId?: InputMaybe<UuidOperationFilterInput>;
  or?: InputMaybe<Array<CommentsFilterInput>>;
  title?: InputMaybe<TitlesFilterInput>;
  titleId?: InputMaybe<UuidOperationFilterInput>;
};

export type CreateMysqlAliasError = ValidationError;

export type CreateMysqlAliasInput = {
  alias: AliasesDtoInput;
};

export type CreateMysqlAliasPayload = {
  __typename?: 'CreateMysqlAliasPayload';
  aliases?: Maybe<Aliases>;
  errors?: Maybe<Array<CreateMysqlAliasError>>;
};

export type CreateMysqlGenreError = ValidationError;

export type CreateMysqlGenreInput = {
  genre: GenresDtoInput;
};

export type CreateMysqlGenrePayload = {
  __typename?: 'CreateMysqlGenrePayload';
  errors?: Maybe<Array<CreateMysqlGenreError>>;
  genres?: Maybe<Genres>;
};

export type CreateMysqlPersonError = ValidationError;

export type CreateMysqlPersonInput = {
  person: PersonsDtoInput;
};

export type CreateMysqlPersonPayload = {
  __typename?: 'CreateMysqlPersonPayload';
  errors?: Maybe<Array<CreateMysqlPersonError>>;
  persons?: Maybe<Persons>;
};

export type CreateMysqlTitleError = ArgumentError | InvalidOperationError;

export type CreateMysqlTitleInput = {
  title: TitlesDtoInput;
};

export type CreateMysqlTitlePayload = {
  __typename?: 'CreateMysqlTitlePayload';
  errors?: Maybe<Array<CreateMysqlTitleError>>;
  titles?: Maybe<Titles>;
};

export type DeleteMysqlAliasError = ValidationError;

export type DeleteMysqlAliasInput = {
  aliasId: Scalars['UUID']['input'];
};

export type DeleteMysqlAliasPayload = {
  __typename?: 'DeleteMysqlAliasPayload';
  aliases?: Maybe<Aliases>;
  errors?: Maybe<Array<DeleteMysqlAliasError>>;
};

export type DeleteMysqlGenreError = ValidationError;

export type DeleteMysqlGenreInput = {
  id: Scalars['UUID']['input'];
};

export type DeleteMysqlGenrePayload = {
  __typename?: 'DeleteMysqlGenrePayload';
  errors?: Maybe<Array<DeleteMysqlGenreError>>;
  genres?: Maybe<Genres>;
};

export type DeleteMysqlPersonError = ValidationError;

export type DeleteMysqlPersonInput = {
  id: Scalars['UUID']['input'];
};

export type DeleteMysqlPersonPayload = {
  __typename?: 'DeleteMysqlPersonPayload';
  errors?: Maybe<Array<DeleteMysqlPersonError>>;
  persons?: Maybe<Persons>;
};

export type DeleteMysqlTitleInput = {
  id: Scalars['UUID']['input'];
};

export type DeleteMysqlTitlePayload = {
  __typename?: 'DeleteMysqlTitlePayload';
  titles?: Maybe<Titles>;
};

export type Directors = {
  __typename?: 'Directors';
  directorsId: Scalars['UUID']['output'];
  personsPerson?: Maybe<Persons>;
  personsPersonId: Scalars['UUID']['output'];
  titlesTitle?: Maybe<Titles>;
  titlesTitleId: Scalars['UUID']['output'];
};

export type DirectorsFilterInput = {
  and?: InputMaybe<Array<DirectorsFilterInput>>;
  directorsId?: InputMaybe<UuidOperationFilterInput>;
  or?: InputMaybe<Array<DirectorsFilterInput>>;
  personsPerson?: InputMaybe<PersonsFilterInput>;
  personsPersonId?: InputMaybe<UuidOperationFilterInput>;
  titlesTitle?: InputMaybe<TitlesFilterInput>;
  titlesTitleId?: InputMaybe<UuidOperationFilterInput>;
};

export type Episodes = {
  __typename?: 'Episodes';
  episodeId: Scalars['UUID']['output'];
  episodeNumber: Scalars['Int']['output'];
  seasonNumber: Scalars['Int']['output'];
  titleIdChild: Scalars['UUID']['output'];
  titleIdChildNavigation?: Maybe<Titles>;
  titleIdParent: Scalars['UUID']['output'];
  titleIdParentNavigation?: Maybe<Titles>;
};

export type EpisodesFilterInput = {
  and?: InputMaybe<Array<EpisodesFilterInput>>;
  episodeId?: InputMaybe<UuidOperationFilterInput>;
  episodeNumber?: InputMaybe<IntOperationFilterInput>;
  or?: InputMaybe<Array<EpisodesFilterInput>>;
  seasonNumber?: InputMaybe<IntOperationFilterInput>;
  titleIdChild?: InputMaybe<UuidOperationFilterInput>;
  titleIdChildNavigation?: InputMaybe<TitlesFilterInput>;
  titleIdParent?: InputMaybe<UuidOperationFilterInput>;
  titleIdParentNavigation?: InputMaybe<TitlesFilterInput>;
};

export type Error = {
  message: Scalars['String']['output'];
};

export type FloatOperationFilterInput = {
  eq?: InputMaybe<Scalars['Float']['input']>;
  gt?: InputMaybe<Scalars['Float']['input']>;
  gte?: InputMaybe<Scalars['Float']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Float']['input']>>>;
  lt?: InputMaybe<Scalars['Float']['input']>;
  lte?: InputMaybe<Scalars['Float']['input']>;
  neq?: InputMaybe<Scalars['Float']['input']>;
  ngt?: InputMaybe<Scalars['Float']['input']>;
  ngte?: InputMaybe<Scalars['Float']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Float']['input']>>>;
  nlt?: InputMaybe<Scalars['Float']['input']>;
  nlte?: InputMaybe<Scalars['Float']['input']>;
};

export type Genres = {
  __typename?: 'Genres';
  genre: Scalars['String']['output'];
  genreId: Scalars['UUID']['output'];
  titlesTitle?: Maybe<Array<Maybe<Titles>>>;
};

export type GenresDtoInput = {
  genre: Scalars['String']['input'];
};

export type GenresFilterInput = {
  and?: InputMaybe<Array<GenresFilterInput>>;
  genre?: InputMaybe<StringOperationFilterInput>;
  genreId?: InputMaybe<UuidOperationFilterInput>;
  or?: InputMaybe<Array<GenresFilterInput>>;
  titlesTitle?: InputMaybe<ListFilterInputTypeOfTitlesFilterInput>;
};

export type GenresSortInput = {
  genre?: InputMaybe<SortEnumType>;
  genreId?: InputMaybe<SortEnumType>;
};

export type IntOperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']['input']>;
  gt?: InputMaybe<Scalars['Int']['input']>;
  gte?: InputMaybe<Scalars['Int']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  lt?: InputMaybe<Scalars['Int']['input']>;
  lte?: InputMaybe<Scalars['Int']['input']>;
  neq?: InputMaybe<Scalars['Int']['input']>;
  ngt?: InputMaybe<Scalars['Int']['input']>;
  ngte?: InputMaybe<Scalars['Int']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  nlt?: InputMaybe<Scalars['Int']['input']>;
  nlte?: InputMaybe<Scalars['Int']['input']>;
};

export type InvalidOperationError = Error & {
  __typename?: 'InvalidOperationError';
  message: Scalars['String']['output'];
};

export type KnownFor = {
  __typename?: 'KnownFor';
  knownForId: Scalars['UUID']['output'];
  personsPerson?: Maybe<Persons>;
  personsPersonId: Scalars['UUID']['output'];
  titlesTitle?: Maybe<Titles>;
  titlesTitleId: Scalars['UUID']['output'];
};

export type KnownForFilterInput = {
  and?: InputMaybe<Array<KnownForFilterInput>>;
  knownForId?: InputMaybe<UuidOperationFilterInput>;
  or?: InputMaybe<Array<KnownForFilterInput>>;
  personsPerson?: InputMaybe<PersonsFilterInput>;
  personsPersonId?: InputMaybe<UuidOperationFilterInput>;
  titlesTitle?: InputMaybe<TitlesFilterInput>;
  titlesTitleId?: InputMaybe<UuidOperationFilterInput>;
};

export type ListFilterInputTypeOfActorsFilterInput = {
  all?: InputMaybe<ActorsFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<ActorsFilterInput>;
  some?: InputMaybe<ActorsFilterInput>;
};

export type ListFilterInputTypeOfAliasesFilterInput = {
  all?: InputMaybe<AliasesFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<AliasesFilterInput>;
  some?: InputMaybe<AliasesFilterInput>;
};

export type ListFilterInputTypeOfAttributesFilterInput = {
  all?: InputMaybe<AttributesFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<AttributesFilterInput>;
  some?: InputMaybe<AttributesFilterInput>;
};

export type ListFilterInputTypeOfCommentsFilterInput = {
  all?: InputMaybe<CommentsFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<CommentsFilterInput>;
  some?: InputMaybe<CommentsFilterInput>;
};

export type ListFilterInputTypeOfDirectorsFilterInput = {
  all?: InputMaybe<DirectorsFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<DirectorsFilterInput>;
  some?: InputMaybe<DirectorsFilterInput>;
};

export type ListFilterInputTypeOfEpisodesFilterInput = {
  all?: InputMaybe<EpisodesFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<EpisodesFilterInput>;
  some?: InputMaybe<EpisodesFilterInput>;
};

export type ListFilterInputTypeOfGenresFilterInput = {
  all?: InputMaybe<GenresFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<GenresFilterInput>;
  some?: InputMaybe<GenresFilterInput>;
};

export type ListFilterInputTypeOfKnownForFilterInput = {
  all?: InputMaybe<KnownForFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<KnownForFilterInput>;
  some?: InputMaybe<KnownForFilterInput>;
};

export type ListFilterInputTypeOfProfessionsFilterInput = {
  all?: InputMaybe<ProfessionsFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<ProfessionsFilterInput>;
  some?: InputMaybe<ProfessionsFilterInput>;
};

export type ListFilterInputTypeOfRatingsFilterInput = {
  all?: InputMaybe<RatingsFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<RatingsFilterInput>;
  some?: InputMaybe<RatingsFilterInput>;
};

export type ListFilterInputTypeOfTitlesFilterInput = {
  all?: InputMaybe<TitlesFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<TitlesFilterInput>;
  some?: InputMaybe<TitlesFilterInput>;
};

export type ListFilterInputTypeOfTypesFilterInput = {
  all?: InputMaybe<TypesFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<TypesFilterInput>;
  some?: InputMaybe<TypesFilterInput>;
};

export type ListFilterInputTypeOfWritersFilterInput = {
  all?: InputMaybe<WritersFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<WritersFilterInput>;
  some?: InputMaybe<WritersFilterInput>;
};

export type Mutation = {
  __typename?: 'Mutation';
  addMysqlGenre: AddMysqlGenrePayload;
  createMysqlAlias: CreateMysqlAliasPayload;
  createMysqlGenre: CreateMysqlGenrePayload;
  createMysqlPerson: CreateMysqlPersonPayload;
  createMysqlTitle: CreateMysqlTitlePayload;
  deleteMysqlAlias: DeleteMysqlAliasPayload;
  deleteMysqlGenre: DeleteMysqlGenrePayload;
  deleteMysqlPerson: DeleteMysqlPersonPayload;
  deleteMysqlTitle: DeleteMysqlTitlePayload;
  removeMysqlGenre: RemoveMysqlGenrePayload;
  updateMysqlAlias: UpdateMysqlAliasPayload;
  updateMysqlPerson: UpdateMysqlPersonPayload;
  updateMysqlTitle: UpdateMysqlTitlePayload;
};


export type MutationAddMysqlGenreArgs = {
  input: AddMysqlGenreInput;
};


export type MutationCreateMysqlAliasArgs = {
  input: CreateMysqlAliasInput;
};


export type MutationCreateMysqlGenreArgs = {
  input: CreateMysqlGenreInput;
};


export type MutationCreateMysqlPersonArgs = {
  input: CreateMysqlPersonInput;
};


export type MutationCreateMysqlTitleArgs = {
  input: CreateMysqlTitleInput;
};


export type MutationDeleteMysqlAliasArgs = {
  input: DeleteMysqlAliasInput;
};


export type MutationDeleteMysqlGenreArgs = {
  input: DeleteMysqlGenreInput;
};


export type MutationDeleteMysqlPersonArgs = {
  input: DeleteMysqlPersonInput;
};


export type MutationDeleteMysqlTitleArgs = {
  input: DeleteMysqlTitleInput;
};


export type MutationRemoveMysqlGenreArgs = {
  input: RemoveMysqlGenreInput;
};


export type MutationUpdateMysqlAliasArgs = {
  input: UpdateMysqlAliasInput;
};


export type MutationUpdateMysqlPersonArgs = {
  input: UpdateMysqlPersonInput;
};


export type MutationUpdateMysqlTitleArgs = {
  input: UpdateMysqlTitleInput;
};

/** A segment of a collection. */
export type MysqlGenresCollectionSegment = {
  __typename?: 'MysqlGenresCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Genres>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

/** A segment of a collection. */
export type MysqlPersonsCollectionSegment = {
  __typename?: 'MysqlPersonsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Persons>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

/** A segment of a collection. */
export type MysqlTitlesCollectionSegment = {
  __typename?: 'MysqlTitlesCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Titles>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export type Persons = {
  __typename?: 'Persons';
  actors?: Maybe<Array<Maybe<Actors>>>;
  birthYear: Scalars['Int']['output'];
  directors?: Maybe<Array<Maybe<Directors>>>;
  endYear?: Maybe<Scalars['Int']['output']>;
  knownFor?: Maybe<Array<Maybe<KnownFor>>>;
  name: Scalars['String']['output'];
  personId: Scalars['UUID']['output'];
  professions?: Maybe<Array<Maybe<Professions>>>;
  writers?: Maybe<Array<Maybe<Writers>>>;
};

export type PersonsDtoInput = {
  birthYear: Scalars['Int']['input'];
  endYear?: InputMaybe<Scalars['Int']['input']>;
  name: Scalars['String']['input'];
};

export type PersonsFilterInput = {
  actors?: InputMaybe<ListFilterInputTypeOfActorsFilterInput>;
  and?: InputMaybe<Array<PersonsFilterInput>>;
  birthYear?: InputMaybe<IntOperationFilterInput>;
  directors?: InputMaybe<ListFilterInputTypeOfDirectorsFilterInput>;
  endYear?: InputMaybe<IntOperationFilterInput>;
  knownFor?: InputMaybe<ListFilterInputTypeOfKnownForFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PersonsFilterInput>>;
  personId?: InputMaybe<UuidOperationFilterInput>;
  professions?: InputMaybe<ListFilterInputTypeOfProfessionsFilterInput>;
  writers?: InputMaybe<ListFilterInputTypeOfWritersFilterInput>;
};

export type PersonsSortInput = {
  birthYear?: InputMaybe<SortEnumType>;
  endYear?: InputMaybe<SortEnumType>;
  name?: InputMaybe<SortEnumType>;
  personId?: InputMaybe<SortEnumType>;
};

export type Professions = {
  __typename?: 'Professions';
  person?: Maybe<Persons>;
  personId: Scalars['UUID']['output'];
  profession: Scalars['String']['output'];
  professionId: Scalars['UUID']['output'];
};

export type ProfessionsFilterInput = {
  and?: InputMaybe<Array<ProfessionsFilterInput>>;
  or?: InputMaybe<Array<ProfessionsFilterInput>>;
  person?: InputMaybe<PersonsFilterInput>;
  personId?: InputMaybe<UuidOperationFilterInput>;
  profession?: InputMaybe<StringOperationFilterInput>;
  professionId?: InputMaybe<UuidOperationFilterInput>;
};

export type Query = {
  __typename?: 'Query';
  mysqlGenres?: Maybe<MysqlGenresCollectionSegment>;
  mysqlPerson?: Maybe<Persons>;
  mysqlPersons?: Maybe<MysqlPersonsCollectionSegment>;
  mysqlTitles?: Maybe<MysqlTitlesCollectionSegment>;
};


export type QueryMysqlGenresArgs = {
  order?: InputMaybe<Array<GenresSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<GenresFilterInput>;
};


export type QueryMysqlPersonArgs = {
  id: Scalars['UUID']['input'];
};


export type QueryMysqlPersonsArgs = {
  order?: InputMaybe<Array<PersonsSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<PersonsFilterInput>;
};


export type QueryMysqlTitlesArgs = {
  order?: InputMaybe<Array<TitlesSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<TitlesFilterInput>;
};

export type Ratings = {
  __typename?: 'Ratings';
  averageRating: Scalars['Float']['output'];
  numVotes: Scalars['Int']['output'];
  ratingId: Scalars['UUID']['output'];
  title?: Maybe<Titles>;
  titleId: Scalars['UUID']['output'];
};

export type RatingsFilterInput = {
  and?: InputMaybe<Array<RatingsFilterInput>>;
  averageRating?: InputMaybe<FloatOperationFilterInput>;
  numVotes?: InputMaybe<IntOperationFilterInput>;
  or?: InputMaybe<Array<RatingsFilterInput>>;
  ratingId?: InputMaybe<UuidOperationFilterInput>;
  title?: InputMaybe<TitlesFilterInput>;
  titleId?: InputMaybe<UuidOperationFilterInput>;
};

export type RemoveMysqlGenreError = ValidationError;

export type RemoveMysqlGenreInput = {
  genreId: Scalars['UUID']['input'];
  titleId: Scalars['UUID']['input'];
};

export type RemoveMysqlGenrePayload = {
  __typename?: 'RemoveMysqlGenrePayload';
  errors?: Maybe<Array<RemoveMysqlGenreError>>;
  genres?: Maybe<Genres>;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']['input']>;
  endsWith?: InputMaybe<Scalars['String']['input']>;
  eq?: InputMaybe<Scalars['String']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  ncontains?: InputMaybe<Scalars['String']['input']>;
  nendsWith?: InputMaybe<Scalars['String']['input']>;
  neq?: InputMaybe<Scalars['String']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  nstartsWith?: InputMaybe<Scalars['String']['input']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']['input']>;
};

export type Titles = {
  __typename?: 'Titles';
  actors?: Maybe<Array<Maybe<Actors>>>;
  aliases?: Maybe<Array<Maybe<Aliases>>>;
  comments?: Maybe<Array<Maybe<Comments>>>;
  directors?: Maybe<Array<Maybe<Directors>>>;
  endYear?: Maybe<Scalars['Int']['output']>;
  episodesTitleIdChildNavigation?: Maybe<Array<Maybe<Episodes>>>;
  episodesTitleIdParentNavigation?: Maybe<Array<Maybe<Episodes>>>;
  genresGenre?: Maybe<Array<Maybe<Genres>>>;
  isAdult?: Scalars['Boolean']['output'];
  knownFor?: Maybe<Array<Maybe<KnownFor>>>;
  originalTitle?: Scalars['String']['output'];
  primaryTitle?: Scalars['String']['output'];
  ratings?: Maybe<Array<Maybe<Ratings>>>;
  runtimeMinutes?: Maybe<Scalars['Int']['output']>;
  startYear?: Scalars['Int']['output'];
  titleId?: Scalars['UUID']['output'];
  titleType?: Scalars['String']['output'];
  writers?: Maybe<Array<Maybe<Writers>>>;
};

export type TitlesDtoInput = {
  endYear?: InputMaybe<Scalars['Int']['input']>;
  isAdult: Scalars['Boolean']['input'];
  originalTitle: Scalars['String']['input'];
  primaryTitle: Scalars['String']['input'];
  runtimeMinutes?: InputMaybe<Scalars['Int']['input']>;
  startYear: Scalars['Int']['input'];
  titleType: Scalars['String']['input'];
};

export type TitlesFilterInput = {
  actors?: InputMaybe<ListFilterInputTypeOfActorsFilterInput>;
  aliases?: InputMaybe<ListFilterInputTypeOfAliasesFilterInput>;
  and?: InputMaybe<Array<TitlesFilterInput>>;
  comments?: InputMaybe<ListFilterInputTypeOfCommentsFilterInput>;
  directors?: InputMaybe<ListFilterInputTypeOfDirectorsFilterInput>;
  endYear?: InputMaybe<IntOperationFilterInput>;
  episodesTitleIdChildNavigation?: InputMaybe<ListFilterInputTypeOfEpisodesFilterInput>;
  episodesTitleIdParentNavigation?: InputMaybe<ListFilterInputTypeOfEpisodesFilterInput>;
  genresGenre?: InputMaybe<ListFilterInputTypeOfGenresFilterInput>;
  isAdult?: InputMaybe<BooleanOperationFilterInput>;
  knownFor?: InputMaybe<ListFilterInputTypeOfKnownForFilterInput>;
  or?: InputMaybe<Array<TitlesFilterInput>>;
  originalTitle?: InputMaybe<StringOperationFilterInput>;
  primaryTitle?: InputMaybe<StringOperationFilterInput>;
  ratings?: InputMaybe<ListFilterInputTypeOfRatingsFilterInput>;
  runtimeMinutes?: InputMaybe<IntOperationFilterInput>;
  startYear?: InputMaybe<IntOperationFilterInput>;
  titleId?: InputMaybe<UuidOperationFilterInput>;
  titleType?: InputMaybe<StringOperationFilterInput>;
  writers?: InputMaybe<ListFilterInputTypeOfWritersFilterInput>;
};

export type TitlesSortInput = {
  endYear?: InputMaybe<SortEnumType>;
  isAdult?: InputMaybe<SortEnumType>;
  originalTitle?: InputMaybe<SortEnumType>;
  primaryTitle?: InputMaybe<SortEnumType>;
  runtimeMinutes?: InputMaybe<SortEnumType>;
  startYear?: InputMaybe<SortEnumType>;
  titleId?: InputMaybe<SortEnumType>;
  titleType?: InputMaybe<SortEnumType>;
};

export type Types = {
  __typename?: 'Types';
  aliasesAlias?: Maybe<Array<Maybe<Aliases>>>;
  type: Scalars['String']['output'];
  typeId: Scalars['UUID']['output'];
};

export type TypesFilterInput = {
  aliasesAlias?: InputMaybe<ListFilterInputTypeOfAliasesFilterInput>;
  and?: InputMaybe<Array<TypesFilterInput>>;
  or?: InputMaybe<Array<TypesFilterInput>>;
  type?: InputMaybe<StringOperationFilterInput>;
  typeId?: InputMaybe<UuidOperationFilterInput>;
};

export type UpdateMysqlAliasError = ValidationError;

export type UpdateMysqlAliasInput = {
  alias: AliasesDtoInput;
  aliasId: Scalars['UUID']['input'];
};

export type UpdateMysqlAliasPayload = {
  __typename?: 'UpdateMysqlAliasPayload';
  aliases?: Maybe<Aliases>;
  errors?: Maybe<Array<UpdateMysqlAliasError>>;
};

export type UpdateMysqlPersonError = ValidationError;

export type UpdateMysqlPersonInput = {
  id: Scalars['UUID']['input'];
  person: PersonsDtoInput;
};

export type UpdateMysqlPersonPayload = {
  __typename?: 'UpdateMysqlPersonPayload';
  errors?: Maybe<Array<UpdateMysqlPersonError>>;
  persons?: Maybe<Persons>;
};

export type UpdateMysqlTitleError = ArgumentError | InvalidOperationError;

export type UpdateMysqlTitleInput = {
  id: Scalars['UUID']['input'];
  title: TitlesDtoInput;
};

export type UpdateMysqlTitlePayload = {
  __typename?: 'UpdateMysqlTitlePayload';
  errors?: Maybe<Array<UpdateMysqlTitleError>>;
  titles?: Maybe<Titles>;
};

export type UuidOperationFilterInput = {
  eq?: InputMaybe<Scalars['UUID']['input']>;
  gt?: InputMaybe<Scalars['UUID']['input']>;
  gte?: InputMaybe<Scalars['UUID']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['UUID']['input']>>>;
  lt?: InputMaybe<Scalars['UUID']['input']>;
  lte?: InputMaybe<Scalars['UUID']['input']>;
  neq?: InputMaybe<Scalars['UUID']['input']>;
  ngt?: InputMaybe<Scalars['UUID']['input']>;
  ngte?: InputMaybe<Scalars['UUID']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['UUID']['input']>>>;
  nlt?: InputMaybe<Scalars['UUID']['input']>;
  nlte?: InputMaybe<Scalars['UUID']['input']>;
};

export type ValidationAttribute = {
  __typename?: 'ValidationAttribute';
  errorMessage?: Maybe<Scalars['String']['output']>;
  errorMessageResourceName?: Maybe<Scalars['String']['output']>;
  formatErrorMessage: Scalars['String']['output'];
  isDefaultAttribute: Scalars['Boolean']['output'];
  requiresValidationContext: Scalars['Boolean']['output'];
};


export type ValidationAttributeFormatErrorMessageArgs = {
  name: Scalars['String']['input'];
};

export type ValidationError = Error & {
  __typename?: 'ValidationError';
  message: Scalars['String']['output'];
  validationAttribute?: Maybe<ValidationAttribute>;
  validationResult: ValidationResult;
};

export type ValidationResult = {
  __typename?: 'ValidationResult';
  errorMessage?: Maybe<Scalars['String']['output']>;
  memberNames: Array<Scalars['String']['output']>;
};

export type Writers = {
  __typename?: 'Writers';
  personsPerson?: Maybe<Persons>;
  personsPersonId: Scalars['UUID']['output'];
  titlesTitle?: Maybe<Titles>;
  titlesTitleId: Scalars['UUID']['output'];
  writersId: Scalars['UUID']['output'];
};

export type WritersFilterInput = {
  and?: InputMaybe<Array<WritersFilterInput>>;
  or?: InputMaybe<Array<WritersFilterInput>>;
  personsPerson?: InputMaybe<PersonsFilterInput>;
  personsPersonId?: InputMaybe<UuidOperationFilterInput>;
  titlesTitle?: InputMaybe<TitlesFilterInput>;
  titlesTitleId?: InputMaybe<UuidOperationFilterInput>;
  writersId?: InputMaybe<UuidOperationFilterInput>;
};

export type GetTitlesQueryVariables = Exact<{
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
}>;


export type GetTitlesQuery = { __typename?: 'Query', mysqlTitles?: { __typename?: 'MysqlTitlesCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Titles', titleId: string, primaryTitle: string, originalTitle: string, startYear: number }> | null } | null };

export type GetTitleQueryVariables = Exact<{
  id?: InputMaybe<Scalars['UUID']['input']>;
}>;


export type GetTitleQuery = { __typename?: 'Query', mysqlTitles?: { __typename?: 'MysqlTitlesCollectionSegment', items?: Array<{ __typename?: 'Titles', titleId: string, primaryTitle: string, originalTitle: string, isAdult: boolean, startYear: number, endYear?: number | null, runtimeMinutes?: number | null, titleType: string, genresGenre?: Array<{ __typename?: 'Genres', genreId: string, genre: string } | null> | null }> | null } | null };


export const GetTitlesDocument = { "kind": "Document", "definitions": [{ "kind": "OperationDefinition", "operation": "query", "name": { "kind": "Name", "value": "GetTitles" }, "variableDefinitions": [{ "kind": "VariableDefinition", "variable": { "kind": "Variable", "name": { "kind": "Name", "value": "skip" } }, "type": { "kind": "NamedType", "name": { "kind": "Name", "value": "Int" } } }, { "kind": "VariableDefinition", "variable": { "kind": "Variable", "name": { "kind": "Name", "value": "take" } }, "type": { "kind": "NamedType", "name": { "kind": "Name", "value": "Int" } } }], "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "mysqlTitles" }, "arguments": [{ "kind": "Argument", "name": { "kind": "Name", "value": "skip" }, "value": { "kind": "Variable", "name": { "kind": "Name", "value": "skip" } } }, { "kind": "Argument", "name": { "kind": "Name", "value": "take" }, "value": { "kind": "Variable", "name": { "kind": "Name", "value": "take" } } }, { "kind": "Argument", "name": { "kind": "Name", "value": "order" }, "value": { "kind": "ObjectValue", "fields": [{ "kind": "ObjectField", "name": { "kind": "Name", "value": "startYear" }, "value": { "kind": "EnumValue", "value": "DESC" } }] } }], "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "totalCount" } }, { "kind": "Field", "name": { "kind": "Name", "value": "items" }, "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "titleId" } }, { "kind": "Field", "name": { "kind": "Name", "value": "primaryTitle" } }, { "kind": "Field", "name": { "kind": "Name", "value": "originalTitle" } }, { "kind": "Field", "name": { "kind": "Name", "value": "startYear" } }] } }] } }] } }] } as unknown as DocumentNode<GetTitlesQuery, GetTitlesQueryVariables>;
export const GetTitleDocument = { "kind": "Document", "definitions": [{ "kind": "OperationDefinition", "operation": "query", "name": { "kind": "Name", "value": "GetTitle" }, "variableDefinitions": [{ "kind": "VariableDefinition", "variable": { "kind": "Variable", "name": { "kind": "Name", "value": "id" } }, "type": { "kind": "NamedType", "name": { "kind": "Name", "value": "UUID" } } }], "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "mysqlTitles" }, "arguments": [{ "kind": "Argument", "name": { "kind": "Name", "value": "where" }, "value": { "kind": "ObjectValue", "fields": [{ "kind": "ObjectField", "name": { "kind": "Name", "value": "titleId" }, "value": { "kind": "ObjectValue", "fields": [{ "kind": "ObjectField", "name": { "kind": "Name", "value": "eq" }, "value": { "kind": "Variable", "name": { "kind": "Name", "value": "id" } } }] } }] } }], "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "items" }, "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "titleId" } }, { "kind": "Field", "name": { "kind": "Name", "value": "primaryTitle" } }, { "kind": "Field", "name": { "kind": "Name", "value": "originalTitle" } }, { "kind": "Field", "name": { "kind": "Name", "value": "isAdult" } }, { "kind": "Field", "name": { "kind": "Name", "value": "startYear" } }, { "kind": "Field", "name": { "kind": "Name", "value": "endYear" } }, { "kind": "Field", "name": { "kind": "Name", "value": "runtimeMinutes" } }, { "kind": "Field", "name": { "kind": "Name", "value": "titleType" } }, { "kind": "Field", "name": { "kind": "Name", "value": "genresGenre" }, "selectionSet": { "kind": "SelectionSet", "selections": [{ "kind": "Field", "name": { "kind": "Name", "value": "genreId" } }, { "kind": "Field", "name": { "kind": "Name", "value": "genre" } }] } }] } }] } }] } }] } as unknown as DocumentNode<GetTitleQuery, GetTitleQueryVariables>;