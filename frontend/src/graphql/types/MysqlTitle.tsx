export type MysqlTitle = {
  titleId: string;
  primaryTitle: string;
  originalTitle: string;
  isAdult: boolean;
  startYear: number;
  endYear: number | null;
  runtimeMinutes: number | null;
  titleType: string;
};
