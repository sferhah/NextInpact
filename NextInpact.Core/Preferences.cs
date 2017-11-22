using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace NextInpact.Core
{

    public class Preferences
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static System.Int64 LastRefreshDate
        {
            get => AppSettings.GetValueOrDefault(nameof(LastRefreshDate), default(System.Int64));
            set => AppSettings.AddOrUpdateValue(nameof(LastRefreshDate), value);
        }

        private const string lastRefreshDateText = "Dernière synchronisation : ";

        public static string LastRefreshDateText
        {
            get
            {
                if (LastRefreshDate>0)
                {
                    return lastRefreshDateText + new DateTime(LastRefreshDate).ToString(Constants.FORMAT_DATE_LAST_REFRESH);
                }

                return lastRefreshDateText + "Jamais synchronisé";
            }          
        }
    }
}
