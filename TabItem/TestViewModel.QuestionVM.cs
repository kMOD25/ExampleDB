using Simplified;
using System;
using TabItem.ViewModelInterfaces;

namespace TabItem
{
    public partial class TestViewModel
    {
        class QuestionVM : BaseInpc, IQuestionVM
        {
            private readonly Func<int, string> getDescriptor;
            private string _descriptor;
            private string _answer;

            public int Id { get; }
            public string Title { get; }

            public QuestionVM(int id, string title, Func<int, string> getDescriptor)
            {
                Id = id;
                Title = title;
                this.getDescriptor = getDescriptor;
            }

            public string Descriptor => _descriptor
                ?? (_descriptor = getDescriptor(Id));
            public string Answer { get => _answer; set => Set(ref _answer, value); }
        }
    }
}
