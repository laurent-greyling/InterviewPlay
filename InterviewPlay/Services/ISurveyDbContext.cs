using InterviewPlay.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewPlay.Services
{
    public class ISurveyDbContext : DbContext
    {
        public virtual DbSet<RespondentFinishedEntity> RespondentFinalState { get; set; }
    }
}
