using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public static String LastRefreshDateText
        {
            get
            {
                if (LastRefreshDate>0)
                {
                    return lastRefreshDateText + new DateTime(LastRefreshDate).ToString(Constantes.FORMAT_DATE_DERNIER_REFRESH);
                }

                return lastRefreshDateText + "Jamais synchronisé";
            }          
        }
    }
}
