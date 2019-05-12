using CommandLine;
using SurveyDeserialise.SurveyDetails;
using System;

namespace SurveyDeserialise
{
    /// <summary>
    /// Naming should change, but this is just a silly little prject to setup my lookup tables
    /// As the respondent data will only be saved as Id values, we need tables to lookup the values of these Ids.
    /// For example a respondent will answer "I love my job", this will have an id of say 32. Only 32 will be saved into surveydata. 
    /// In analysis we would want to know what 32 equals to to show client. this can then be matched to the lookup table.
    /// Why, well because the id never has to change, but the wording may change in the future, for simple reasons such as spelling errors or client want to reword a category.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => 
                {
                    Console.WriteLine("Start process");

                    using (var context = new SurveyDbContext(opts.StorageConnectionString))
                    {
                        var surveyDetailsRepository = new SurveyDetailsRepository(context);
                        surveyDetailsRepository.UpdateSurveyDetails();
                    }

                    Console.WriteLine("End Process");
                }).WithNotParsed(err => Console.WriteLine(err));
            
            Console.ReadKey();
        }
    }
}
