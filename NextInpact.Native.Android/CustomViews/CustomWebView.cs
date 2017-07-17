using Android.Content;
using Android.Runtime;
using Android.Graphics;
using Android.Util;
using Android.Webkit;
using static Android.Webkit.WebSettings;

namespace NextInpact.Native.Droid.CustomViews
{
    [Register("com.nextinpact.ui.widgets.CustomWebView")]
    public class CustomWebView : WebView
    {   

        public CustomWebView(Context context, IAttributeSet s) : base(context, s)
        {

        }

        public string HtmlString
        {
            set
            {
                if(value!=null)
                {
                    this.Settings.SetLayoutAlgorithm(LayoutAlgorithm.SingleColumn);
                    this.LoadDataWithBaseURL(null, value, "text/html", "utf-8", null);
                }
            }
            get
            {
                return null;
            }
        }
    }
}