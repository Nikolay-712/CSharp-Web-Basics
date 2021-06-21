namespace CarShop.Services.Issues
{
    using CarShop.ViewModels.Issues;

    public interface IIssuesService
    {
        void CreateIssue(InputIssueViewModel input);

        void DeleteIssue(string Id);

        void FixIssue(string Id);
    }
}
