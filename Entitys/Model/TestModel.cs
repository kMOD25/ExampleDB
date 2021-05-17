using Entitys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitys.Model
{
    public class TestModel
    {
        //1) Метод возвращающий все уровни и их названия(Title);
        //2) Метод возвращающий описание(Descriptor) уровня по его Id;
        //3) Метод возвращающий все вопросы уровня с их названиями(Title);
        //4) Метод возвращающий описание(Descriptor) вопроса по его Id;
        //5) Метод получающий ответы на все вопросы уровня и возвращающий оценку.

        /// <summary>Возвращает все уровни.</summary>
        /// <returns>Последовательность кортежей id и title уровней.</returns>
        public IEnumerable<(int id, string title)> GetAllLevels()
        {
            using (var context = new ApplicationContext())
                return context.Levels
                    .ToList()
                    .Select(lvl => (id: lvl.Id, title: lvl.Title ?? string.Empty))
                    .ToList()
                    .AsReadOnly();
        }


        /// <summary>Возвращает все вопросы уровня.</summary>
        /// <param name="levelId">Id уровня.</param>
        /// <returns>Последовательность кортежей id и title вопросов.</returns>
        public IEnumerable<(int id, string title)> GetLevelQuestions(int levelId)
        {
            using (var context = new ApplicationContext())
                return context.Questions
                    .Where(qst => qst.LevelsId == levelId)
                    .ToList()
                    .Select(qst => (id: qst.Id, title: qst.Title ?? string.Empty))
                    .ToList()
                    .AsReadOnly();
        }

        /// <summary>Возвращает описание вопроса.</summary>
        /// <param name="questionId">Id вопроса.</param>
        /// <returns>Строку с описанием вопроса.
        /// Если оописание не найдено - возвращается <see cref="string.Empty"/>.</returns>
        public string GeQuestionDescriptor(int questionId)
        {
            using (var context = new ApplicationContext())
                return context.QuestionsDescriptors
                    .Find(questionId)
                    ?.Descriptor
                    ?? string.Empty;
        }


        /// <summary>Возвращает количество правильных ответов.</summary>
        /// <param name="levelId">Выбранный уровень.</param>
        /// <param name="answers">Последовательность кортежей id вопроса и ответа на него.</param>
        /// <returns>Возвращает кортеж: общее количество вопросов уровня и количество правильных ответов.</returns>
        /// <exception cref="Exception">Если в последовательности ответов есть Id вопроса не из этого уровня.</exception>
        public (int totalCount, int rightCount) RateAnswers(int levelId, IEnumerable<(int questionId, string answer)> answers)
        {
            using (var context = new ApplicationContext())
            {
                var questionsIds = context.Questions
                    .Where(qst => qst.LevelsId == levelId)
                    .Select(qst => qst.Id)
                    .ToHashSet();

                if (answers.Any(ans => !questionsIds.Contains(ans.questionId)))
                    throw new Exception("Дан ответ на вопрос не из этого уровня.");

                var answersquestionsIds = answers
                    .Select(ans => ans.questionId)
                    .ToHashSet();

                var rightAnswers = context.QuestionsAnswers
                    .Where(ans => answersquestionsIds.Contains(ans.Id))
                    .ToDictionary(ans => ans.Id);

                int total = 0;

                foreach (var ans in answers)
                {
                    if (!string.IsNullOrWhiteSpace(ans.answer) &&
                        rightAnswers.TryGetValue(ans.questionId, out var answer) &&
                        string.Equals(ans.answer, answer.Answer,StringComparison.CurrentCultureIgnoreCase))
                        total++;
                }

                return (questionsIds.Count, total);
            }

        }

    }
}
