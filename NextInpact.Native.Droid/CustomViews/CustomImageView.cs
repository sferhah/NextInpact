using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace NextInpact.Native.Droid.CustomViews
{
    [Register("com.nextinpact.ui.widgets.CustomImageView")]
    public class CustomImageView : ImageView
    {   

        public CustomImageView(Context context, IAttributeSet s) : base(context, s)
        {

        }

        public byte[] ImageData
        {
            set
            {
                if(value!=null)
                {
                    Bitmap bm = BitmapFactory.DecodeByteArray(value, 0, value.Length);
                    DisplayMetrics dm = new DisplayMetrics();
                    SetImageBitmap(bm);
                }
            }
            get
            {
                return null;
            }
        }
    }
}