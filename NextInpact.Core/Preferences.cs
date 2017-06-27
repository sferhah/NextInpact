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
        public static long LastRefreshDate
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(nameof(LastRefreshDate)))
                {
                    var refreshDate = (Application.Current.Properties[nameof(LastRefreshDate)] as Nullable<long>);

                    if (refreshDate == null)
                    {
                        refreshDate = 0;
                    }

                    return refreshDate.Value;
                }

                return 0;
            }
            set
            {
                Application.Current.Properties[nameof(LastRefreshDate)] = value;
            }
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
