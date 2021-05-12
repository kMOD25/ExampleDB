using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelEntity.Model
{
    public class QuestionsAnswers
    {
        [Key]
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Answer { get; set; }
    }
}
