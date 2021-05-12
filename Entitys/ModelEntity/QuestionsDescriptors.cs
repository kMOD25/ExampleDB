using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelEntity.Model
{
   public class QuestionsDescriptors
    {
        [Key]
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Descriptor { get; set; }
    }
}
