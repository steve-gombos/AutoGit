using AutoGit.Jobs.Enums;

namespace AutoGit.Jobs.Attributes
{
    public class RecurringJobAttribute : StandardJobAttribute
    {
        public RecurringJobAttribute(string cronExpression, TimeZoneSetting timeZoneSetting = TimeZoneSetting.Utc, bool runOnStart = false) :
            base(runOnStart)
        {
            CronExpression = cronExpression;
            TimeZone = timeZoneSetting;
        }

        public string CronExpression { get; }
        public TimeZoneSetting TimeZone { get; }
    }
}