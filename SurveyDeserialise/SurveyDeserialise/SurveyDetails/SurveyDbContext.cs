using Microsoft.EntityFrameworkCore;
using SurveyDeserialise.Entities;

namespace SurveyDeserialise.SurveyDetails
{
    /// <summary>
    /// Add Migration -> add-migration CreateSurveyDb
    /// Add Db -> update-database -verbose
    /// </summary>
    public class SurveyDbContext: DbContext
    {
        /// <summary>
        /// Virtual for unit tests
        /// </summary>
        public virtual DbSet<QuestionnaireEntity> Questionnaire { get; set; }
        public virtual DbSet<QuestionnaireItemEntity> QuestionnaireItem { get; set; }

        public virtual DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add sql server connection string here
            //If this was not internal tool/play project I would move this to keyvault or something, this could cause possible security issues
            optionsBuilder.UseSqlServer("");            
        }
    }
}
