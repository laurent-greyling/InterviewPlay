using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyDeserialise.Entities
{
    /// <summary>
    /// Questionnaire entity for adding data to the Questionnaire Table
    /// </summary>
    public class QuestionnaireEntity
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }
        public int OrderNumber { get; set; }
        public string QuestionnaireText { get; set; }
        public int ItemType { get; set; }
    }
}
