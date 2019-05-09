using System.ComponentModel.DataAnnotations;

namespace InterviewPlay.Entities
{
    public class RespondentFinishedEntity
    {
        [Key]
        public string RespondentId { get; set; }
        public bool IsFinished { get; set; }
    }
}
