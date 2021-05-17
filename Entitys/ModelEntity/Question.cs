using System.ComponentModel.DataAnnotations;

namespace ModelEntity.Model
{
   public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int LevelsId { get; set; }
        //public virtual QuestionsDescriptors QuestionDescriptor { get; set; }
        //public virtual QuestionsAnswers QuestionAnswer { get; set; }

    }
}
