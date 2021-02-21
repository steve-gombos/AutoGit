using AutoBogus;
using Octokit;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class PayloadFaker : AutoFaker<string>
    {
        public PayloadFaker()
        {
            UseSeed(Constants.DataSeed);
            
            string payload =
                "{{\"action\": \"{0}\",\"issue\": {{\"url\": \"https://api.github.com/repos/octocat/Hello-World/issues/1347\",\"number\": 1347}},\"repository\" : {{\"id\": 1296269,\"full_name\": \"octocat/Hello-World\",\"owner\": {{\"login\": \"octocat\",\"id\": 1}}}},\"sender\": {{\"login\": \"octocat\", \"id\": 1, \"type\": \"{1}\" }}}}";
            
            CustomInstantiator(f =>
                string.Format(payload, f.Random.ListItem(Constants.Actions), f.Random.Enum<AccountType>()));
        }
    }
}