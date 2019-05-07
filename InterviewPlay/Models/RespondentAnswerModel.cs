namespace InterviewPlay.Models
{
    public class RespondentAnswerModel
    {
        public string RespondentId { get; set; }
        public int SurveyId { get; set; }
        public int SubjectId { get; set; }
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string OpenAnswer { get; set; }
    }
}
