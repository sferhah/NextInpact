using NextInpact.Forms;
using NextInpact.Forms.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyProgressRing), typeof(MyProgressRingRenderer))]
namespace NextInpact.Forms.UWP
{
    public class MyProgressRingRenderer : ViewRenderer<MyProgressRing, ProgressRing>
    {
        ProgressRing ring;
        protected override void OnElementChanged(ElementChangedEventArgs<MyProgressRing> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                ring = new ProgressRing()
                {
                    IsActive = true,
                    Visibility = Windows.UI.Xaml.Visibility.Visible,
                    IsEnabled = true
                };
                SetNativeControl(ring);
            }
        }
    }
}
