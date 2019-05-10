using Microsoft.EntityFrameworkCore;

namespace InterviewPlay.Services
{
    public class SurveyDbContext: ISurveyDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add sql server connection string here
            //If this was not internal tool/play project I would move this to keyvault or something, this could cause possible security issues
            optionsBuilder.UseSqlServer("");
        }
    }
}
