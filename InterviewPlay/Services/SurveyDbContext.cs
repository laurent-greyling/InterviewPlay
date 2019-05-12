using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InterviewPlay.Services
{
    public class SurveyDbContext: ISurveyDbContext
    {
        IConfiguration _config;
        public SurveyDbContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //would like this to rather be in dotnet secrets for local development and keyvault for production. 
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SurveyDataBase"));
        }
    }
}
