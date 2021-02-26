using AutoGit.Jobs.Enums;
using System;

namespace AutoGit.Jobs.Extensions
{
    public static class TimeZoneSettingExtensions
    {
        public static TimeZoneInfo GetTimeZoneInfo(this TimeZoneSetting timeZoneSetting)
        {
            switch (timeZoneSetting)
            {
                case TimeZoneSetting.Local:
                    return TimeZoneInfo.Local;
                default:
                    return TimeZoneInfo.Utc;
            }
        }
    }
}