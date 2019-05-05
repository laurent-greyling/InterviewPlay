using SurveyDeserialise.SurveyDetails;
using System;

namespace SurveyDeserialise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start process");
            
            using (var context = new SurveyDbContext())
            {
                var surveyDetailsRepository = new SurveyDetailsRepository(context);
                surveyDetailsRepository.UpdateSurveyDetails();
            }            

            Console.WriteLine("End Process");
            Console.ReadKey();
        }
    }
}
