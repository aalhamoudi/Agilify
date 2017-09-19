using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.SfBusyIndicator.XForms;
using Xamarin.Forms;

namespace Agilify.Views.ApplicationPages
{
    class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            SfBusyIndicator indicator = new SfBusyIndicator
            {
                Title = "Loading...",
                TextColor = Color.Black,
                TextSize = 20,
                ViewBoxWidth = 200,
                ViewBoxHeight = 200,
                AnimationType = AnimationTypes.DoubleCircle,
            };
            Content = indicator;
        }
    }
}
