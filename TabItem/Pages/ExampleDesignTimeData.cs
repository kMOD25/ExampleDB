using System.Collections.Generic;
using System.Windows.Input;
using TabItem.ViewModelInterfaces;

namespace TabItem.Pages
{
    static class ExampleDesignTimeData
    {
        public class LevelsVM : ILevelsVM
        {
            public ICommand GetQuestionsCommand { get; }
            public ILevelVM SelectedLevel { get; set; }
            public IEnumerable<ILevelVM> Levels { get; }

            public LevelsVM(IEnumerable<ILevelVM> levels)
                => Levels = levels;
        }
        public class Level : ILevelVM
        {
            public int Id { get; }
            public string Title { get; }
            public string Descriptor { get; }

            private Level(int id, string title, string descriptor)
            {
                Id = id;
                Title = title;
                Descriptor = descriptor;
            }

            public static Level First { get; } = new Level(111, "Первый", "Тест первого уровня");
            public static Level Second { get; } = new Level(222, "Второй", "Тест второго уровня");
        }

        public static LevelsVM LevelsDD { get; }
            = new LevelsVM(new Level[] { Level.First, Level.Second })
            { SelectedLevel = Level.First };

        public class Question : IQuestionVM
        {
            public int Id { get; }
            public string Title { get; }
            public string Descriptor { get; }
            public string Answer { get; set; }

            public Question(int id, string title, string descriptor)
            {
                Id = id;
                Title = title;
                Descriptor = descriptor;
            }

            public static Question First { get; } = new Question(111, "Умножение", "Дважды два?");
            public static Question Second { get; } = new Question(222, "Сложение", "Три плюс четыре");

        }

        public class QuestionsVM : IQuestionsVM
        {
            public IEnumerable<IQuestionVM> Questions { get; }
            public ICommand GetResultCommand { get; }
            public ILevelVM SelectedLevel { get; set; }

            public QuestionsVM(IEnumerable<IQuestionVM> questions)
            {
                Questions = questions;
            }
        }

        public static QuestionsVM QuestionsDD { get; }
            = new QuestionsVM(new Question[] { Question.First, Question.Second })
            { SelectedLevel = Level.First };

        public class Result : IResultVM
        {
            public int TotalCount { get; }
            public int RightCount { get; }
            public ICommand StartSelectLevelCommand { get; }
            public ILevelVM SelectedLevel { get; set; }

            public Result(int totalCount, int rightCount)
            {
                TotalCount = totalCount;
                RightCount = rightCount;
            }
        }

        public static Result ResultDD { get; }
            = new Result(25, 15)
            { SelectedLevel = Level.First};
    }

}
