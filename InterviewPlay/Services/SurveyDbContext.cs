using Microsoft.EntityFrameworkCore;

namespace InterviewPlay.Services
{
    public class SurveyDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add sql server connection string here
            //If this was not internal tool/play project I would move this to keyvault or something, this could cause possible security issues
            optionsBuilder.UseSqlServer("Server=tcp:surveyplay.database.windows.net,1433;Initial Catalog=SurveyDetails;Persist Security Info=False;User ID=laurent;Password=survey@LK1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
