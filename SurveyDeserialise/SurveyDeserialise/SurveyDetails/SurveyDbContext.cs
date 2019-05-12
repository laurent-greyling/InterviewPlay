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

        public virtual string _connectionString { get; set; }

        /// <summary>
        /// For testing
        /// </summary>
        public SurveyDbContext()
        {        
        }

        public SurveyDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);            
        }
    }
}
