namespace GraphQL.Handler
{
    public static class GetEnvironmentVariable
    {
        public static string GetEnvVar(string varName)
        {
            return Environment.GetEnvironmentVariable(varName) ?? string.Empty;
        }
    }
}
