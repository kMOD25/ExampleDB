﻿using Entitys.Model;
using Simplified;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabItem.ViewModelInterfaces;

namespace TabItem
{
    public partial class TestViewModel : BaseInpc, ITestVM
    {

        private readonly TestModel model;
        private ILevelVM _selectedLevel;
        private int _totalCount;
        private int _rightCount;
        private ICommand _getQuestionsCommand;
        private ICommand _getResultCommand;
        private ICommand _startSelectLevelCommand;
        private readonly ObservableCollection<IQuestionVM> questions = new ObservableCollection<IQuestionVM>();
        private readonly ObservableCollection<ILevelVM> levels = new ObservableCollection<ILevelVM>();

        public TestViewModel(TestModel model)
        {
            this.model = model;
        }

        public IEnumerable<ILevelVM> Levels => levels;
        public ICommand GetQuestionsCommand => _getQuestionsCommand
            ?? (_getQuestionsCommand = new RelayCommand(GetQuestionsExecute, GetQuestionsCanExecute));

        private bool GetQuestionsCanExecute()
            => SelectedLevel != null;

        private void GetQuestionsExecute()
        {
            questions.Clear();
            foreach (var question in model.GetLevelQuestions(SelectedLevel.Id))
            {
                questions.Add(new QuestionVM(question.id, question.title, GetQuestionDescriptor));
            }
        }

        private string GetQuestionDescriptor(int questionId)
            => model.GeQuestionDescriptor(questionId);

        public IEnumerable<IQuestionVM> Questions => questions;
        public ICommand GetResultCommand => _getResultCommand
            ?? (_getResultCommand = new RelayCommand(GetResultExecute));

        private void GetResultExecute(object parameter)
        {
            var result = model.RateAnswers(SelectedLevel.Id, questions.Select(qst => (qst.Id, qst.Answer)));
            TotalCount = result.totalCount;
            RightCount = result.rightCount;
        }

        public int TotalCount { get => _totalCount; private set => Set(ref _totalCount, value); }
        public int RightCount { get => _rightCount; private set => Set(ref _rightCount, value); }
        public ICommand StartSelectLevelCommand => _startSelectLevelCommand
            ?? (_startSelectLevelCommand = new RelayCommand(StartSelectLevelExecute));

        private void StartSelectLevelExecute()
        {
            levels.Clear();
            foreach (var level in model.GetAllLevels())
            {
                levels.Add(new LevelVM(level.id, level.title, GetQuestionDescriptor));
            }
        }

        private string GetLevelDescriptor(int levelId)
            => model.GeLevelDescriptor(levelId);


        public ILevelVM SelectedLevel { get => _selectedLevel; set => Set(ref _selectedLevel, value); }
    }
}
