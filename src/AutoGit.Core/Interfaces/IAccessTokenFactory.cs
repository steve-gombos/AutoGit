namespace AutoGit.Core.Interfaces
{
    public interface IAccessTokenFactory
    {
        string Create(string appIdentifier, string privateKey);
    }
}