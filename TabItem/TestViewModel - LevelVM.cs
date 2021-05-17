using System;
using TabItem.ViewModelInterfaces;

namespace TabItem
{
    public partial class TestViewModel
    {
        class LevelVM : ILevelVM
        {
            private readonly Func<int, string> getDescriptor;
            private string _descriptor;

            public int Id { get; }
            public string Title { get; }

            public LevelVM(int id, string title, Func<int, string> getDescriptor)
            {
                Id = id;
                Title = title;
                this.getDescriptor = getDescriptor;
            }

            public string Descriptor => _descriptor
                ?? (_descriptor = getDescriptor(Id));
        }
    }
}
