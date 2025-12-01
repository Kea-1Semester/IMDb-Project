namespace GraphQL.Handler
{
    public static class GetEnvironmentVariable
    {
        public static string GetEnvVar(string varName) =>
            Environment.GetEnvironmentVariable(varName)
            ?? throw new ArgumentNullException($"Environment variable '{varName}' is not set.");

    }
}
