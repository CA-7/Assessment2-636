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

namespace Task3
{
    [Activity(Label = "ButtonItem1")]
    public class ButtonItem1 : Activity
    {
        Button button2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.item1);

            button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += (sender, e) =>
            {
              Intent Backintent = new Intent(this, typeof(MainMenu));
                StartActivity(Backintent);
            };
        }
    }
}