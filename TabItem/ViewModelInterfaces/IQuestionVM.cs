namespace TabItem.ViewModelInterfaces
{
    public interface IQuestionVM
    {
        int Id { get; }
        string Title { get; }
        string Descriptor { get; }
        string Answer { get; set; }
    }

}
