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
using Newtonsoft.Json;

namespace Task3
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {

        Button buttonLogin;
        Button buttonRegister;
        EditText editPsw;
        EditText editUserName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            editUserName = FindViewById<EditText>(Resource.Id.editUserName);
            editPsw = FindViewById<EditText>(Resource.Id.editPsw);
            buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);

            buttonLogin.Click += (Sender, e) =>
            {
                
                Intent MainMenuIntent = new Intent(this, typeof(MainMenu));
                StartActivity(MainMenuIntent);
            };


            buttonRegister.Click += (Sender, e) =>
            {
                Intent RegisterIntent = new Intent(this, typeof(Register));
                StartActivity(RegisterIntent);
            };
        }

       
    }
}