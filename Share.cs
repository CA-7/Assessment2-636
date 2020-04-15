using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Android.Support.Design.Widget;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace Task3
{
    [Activity(Label = "Share")]
    public class Share : Activity
    {
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            CreateNotificationChannel();

            Button btn1 = FindViewById<Button>(Resource.Id.button1);
            

            Button share = FindViewById<Button>(Resource.Id.button2);
            share.Click += Btn2_Click;

            Button btn3 = FindViewById<Button>(Resource.Id.button3);
            btn3.Click += Btn3_Click;
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private void Btn1_Click(object sender, EventArgs e, object Seller1)
        {
            TextView tv2 = FindViewById<TextView>(Resource.Id.button1);
            tv2.Text = "Hello" + Seller1;
            tv2.Visibility = ViewStates.Visible;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.item1)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }

        private static Task RequestAsync(ShareTextRequest shareTextRequest)
        {
            throw new NotImplementedException();
        }

        private async void Btn2_Click(object sender, EventArgs e)
        {
            EditText et2 = FindViewById<EditText>(Resource.Id.share);
            await ShareText("Hello" + et2.Text);
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = "https://docs.microsoft.com/en-us/xamarin/essentials/share?context=xamarin%2Fandroid&tabs=android",
                Title = "Share Web Link"
            });
        }

        private async void Btn3_Click(object sender, EventArgs e)
        {
            
            EditText et2 = FindViewById<EditText>(Resource.Id.SMS);
            String messageText = "Hello" + et2.Text;
            String recipient = "0211645518";
            try
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                   .SetAutoCancel(false)
                   .SetSmallIcon(Resource.Drawable.abc_ic_star_black_16dp)
                   .SetContentTitle("Notification Title")
                   .SetContentText("This is a sample notification");
                var notificationManager = NotificationManagerCompat.From(this);
                notificationManager.Notify(NOTIFICATION_ID, builder.Build());

                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                // Sms is not supported on this device.
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetMessage("SMS is not supported on this phone").
                SetTitle("Feature Not Supported");
                Android.App.AlertDialog dialog = builder.Create();
            }
            catch (Exception)
            {
                // Other error has occurred.
            }

            // Create your application here
        }
    }
}