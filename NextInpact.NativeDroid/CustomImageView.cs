using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace NextInpact.NativeDroid
{
    [Register("com.nextInpact.ui.widgets.CustomImageView")]
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