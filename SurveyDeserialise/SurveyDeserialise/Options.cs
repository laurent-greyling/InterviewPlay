using CommandLine;

namespace SurveyDeserialise
{
    internal class Options
    {
        [Option('c', "connectionstring", HelpText = "db connection string.", Required = true)]
        public string StorageConnectionString { get; set; }
    }
}
