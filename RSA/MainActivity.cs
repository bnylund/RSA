using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Java.Math;
using Android.Util;
using System.Collections.Generic;

namespace RSA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindViewById<Button>(Resource.Id.factor).Click += (s, v) =>
            {
                try
                {
                    List<long> factors = Utils.FindFactors(long.Parse(FindViewById<EditText>(Resource.Id.input_n).Text));
                    FindViewById<EditText>(Resource.Id.input_p).Text = factors[0] + "";
                    FindViewById<EditText>(Resource.Id.input_q).Text = factors[1] + "";
                    FindViewById<EditText>(Resource.Id.input_phin).Text = Utils.PhiN(factors[0], factors[1]) + "";
                } catch(Exception ex)
                {
                    Toast.MakeText(this, "Couldn't factor n.", ToastLength.Long).Show();
                    Log.Error("Factor", ex.Message + "\n" + ex.StackTrace);
                }
            };

            FindViewById<Button>(Resource.Id.find_d).Click += (s, v) =>
            {
                try
                {
                    double e = double.Parse(FindViewById<EditText>(Resource.Id.input_e).Text);
                    double phin = double.Parse(FindViewById<EditText>(Resource.Id.input_phin).Text);
                    FindViewById<EditText>(Resource.Id.input_d).Text = Utils.D(e, phin) + "";
                } catch(Exception ex)
                {
                    Toast.MakeText(this, "Couldn't calculate d.", ToastLength.Long).Show();
                    Log.Error("Calculate D", ex.Message + "\n" + ex.StackTrace);
                }
            };

            FindViewById<Button>(Resource.Id.decrypt).Click += (s, v) =>
            {
                try
                {
                    FindViewById<LinearLayout>(Resource.Id.decryptLayout).RemoveAllViews();
                    TextView tv = new TextView(this);
                    tv.Text = "Message Text: ";
                    tv.Gravity = Android.Views.GravityFlags.Center;
                    TextView tv1 = new TextView(this);
                    tv1.Text = "Decrypted Message Text: ";
                    tv1.Gravity = Android.Views.GravityFlags.Center;
                    foreach (string input in FindViewById<EditText>(Resource.Id.input_ciphertext).Text.Split(' '))
                    {
                        double d = double.Parse(FindViewById<EditText>(Resource.Id.input_d).Text);
                        double n = double.Parse(FindViewById<EditText>(Resource.Id.input_n).Text);
                        BigInteger decryptDec = Utils.Decrypt(double.Parse(input), d, n);
                        tv.Text = tv.Text + decryptDec.IntValue() + " ";
                        tv1.Text = tv1.Text + Convert.ToChar(decryptDec.IntValue());
                    }
                    FindViewById<LinearLayout>(Resource.Id.decryptLayout).AddView(tv);
                    FindViewById<LinearLayout>(Resource.Id.decryptLayout).AddView(tv1);
                } catch(Exception ex)
                {
                    Toast.MakeText(this, "Couldn't decrypt the text.", ToastLength.Long).Show();
                    Log.Error("Decrypt", ex.Message + "\n" + ex.StackTrace);
                }
            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}