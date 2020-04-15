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
    [Activity(Label = "MainMenu")]
    public class MainMenu : Activity
    {
        Button Item1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainMenu);

            Item1 = FindViewById<Button>(Resource.Id.item1);

            Item1.Click += (Sender, e) =>
            {
                Intent ItemIntent1 = new Intent(this, typeof(ButtonItem1));
                StartActivity(ItemIntent1);
            };
        }
    }
}