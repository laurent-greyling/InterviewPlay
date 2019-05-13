using InterviewPlay.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewPlay.Services
{
    public class ISurveyDbContext : DbContext
    {
        /// <summary>
        /// Entity that contain the current state of a respondent isFinsihed true/false
        /// </summary>
        public virtual DbSet<RespondentFinishedEntity> RespondentFinalState { get; set; }
    }
}
