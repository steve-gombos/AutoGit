namespace AutoGit.Jobs.Attributes
{
    public class RecurringJobAttribute : JobAttribute
    {
        public string CronExpression { get; set; }

        public RecurringJobAttribute(string cronExpression, bool runOnStart = false) : base(runOnStart)
        {
            CronExpression = cronExpression;
        }
    }
}
