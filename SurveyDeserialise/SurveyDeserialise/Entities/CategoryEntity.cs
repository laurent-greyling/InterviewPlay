using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyDeserialise.Entities
{
    /// <summary>
    /// Category entity for adding data to the Categories Table
    /// </summary>
    public class CategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerId { get; set; }
        public int AnswerType { get; set; }
        public int OrderNumber { get; set; }
        public string CategoryTexts { get; set; }
        public int ItemType { get; set; }
    }
}
