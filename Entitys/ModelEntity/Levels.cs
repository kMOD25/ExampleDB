using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelEntity.Model
{
   public class Levels
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public LevelsDescriptors LevelsDescriptor { get; set; }
        public virtual List<Questions> Questions { get; set; }
    }
}
