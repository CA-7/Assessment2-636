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
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        Button buttonRegister;
        EditText editFirstName;
        EditText editLastName;
        EditText editPhoneNumber;
        EditText editAddress;
        EditText editCountry;
        EditText editUserName;
        EditText editPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register);

            buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);
            editFirstName = FindViewById<EditText>(Resource.Id.editFirstName);
            editLastName = FindViewById<EditText>(Resource.Id.editLastName);
            editPhoneNumber = FindViewById<EditText>(Resource.Id.editPhoneNumber);
            editAddress = FindViewById<EditText>(Resource.Id.editAddress);
            editCountry = FindViewById<EditText>(Resource.Id.editCountry);
            editUserName = FindViewById<EditText>(Resource.Id.editUserName);
            editPassword = FindViewById<EditText>(Resource.Id.editPassword);


           

            // Create your application here
        }
    }
}