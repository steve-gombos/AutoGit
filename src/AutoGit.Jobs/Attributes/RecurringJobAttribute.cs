namespace AutoGit.Jobs.Attributes
{
    public class RecurringJobAttribute : StandardJobAttribute
    {
        public RecurringJobAttribute(string cronExpression, bool runOnStart = false) : base(runOnStart)
        {
            CronExpression = cronExpression;
        }

        public string CronExpression { get; }
    }
}