using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace Task3
{
    [Activity(Label = "NavigationBar")]
    public class NavigationBar : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.item1);
            NavigationBar navigationBar = this;
           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [System.Obsolete]
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            TestFragment testFragment = new TestFragment();

            FrameLayout fragCon = FindViewById<FrameLayout>(Resource.Id.item1);
            FragmentTransaction transaction;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
             

                    return true;
                case Resource.Id.navigation_shop:
                 

                    return true;
                case Resource.Id.navigation_map:
                    
                    return true;
            }
            return false;
        }
    }
}