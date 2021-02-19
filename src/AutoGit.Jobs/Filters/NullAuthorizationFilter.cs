using Hangfire.Dashboard;

namespace AutoGit.Jobs.Filters
{
    public class NullAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}