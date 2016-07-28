using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using Android.Widget;

namespace Converter
{
    [Activity(Label = "Converter", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light")]
    public class Main : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Main);

            //Custom font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");

            //Set text view fonts
            TextView txtMnuTextLength = FindViewById<TextView>(Resource.Id.txtMnuTextLength);
            TextView txtMnuTextWeight = FindViewById<TextView>(Resource.Id.txtMnuTextWeight);
            TextView txtMnuTextDegrees = FindViewById<TextView>(Resource.Id.txtMnuTextDegrees);
            TextView txtMnuTextRadiansDegrees = FindViewById<TextView>(Resource.Id.txtMnuTextRadiansDegrees);
            TextView txtMnuTextArea = FindViewById<TextView>(Resource.Id.txtMnuTextArea);
            TextView txtMnuTextLiquid = FindViewById<TextView>(Resource.Id.txtMnuTextLiquid);
            TextView txtMnuTextConversionDetails = FindViewById<TextView>(Resource.Id.txtMnuTextConversionDetails);
            

            txtMnuTextLength.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextWeight.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextRadiansDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextArea.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextLiquid.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextConversionDetails.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);


            LinearLayout layoutLength = FindViewById<LinearLayout>(Resource.Id.layoutLength);
            LinearLayout layoutWeight = FindViewById<LinearLayout>(Resource.Id.layoutWeight);
            LinearLayout layoutDegrees= FindViewById<LinearLayout>(Resource.Id.layoutDegrees);
            LinearLayout layoutRadiansDegrees = FindViewById<LinearLayout>(Resource.Id.layoutRadiansDegrees);
            LinearLayout layoutArea = FindViewById<LinearLayout>(Resource.Id.layoutArea);
            LinearLayout layoutLiquid = FindViewById<LinearLayout>(Resource.Id.layoutLiquid);
            LinearLayout layoutConversionDetails = FindViewById<LinearLayout>(Resource.Id.layoutConversionDetails);

            layoutLength.Click += LayoutLength_Click;
            layoutWeight.Click += LayoutWeight_Click;
            layoutDegrees.Click += LayoutDegrees_Click;
            layoutRadiansDegrees.Click += LayoutRadiansDegrees_Click;
            layoutArea.Click += LayoutArea_Click;
            layoutLiquid.Click += LayoutLiquid_Click;
            layoutConversionDetails.Click += LayoutConversionDetails_Click;
        }

        private void LayoutLength_Click(object sender, EventArgs e)
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainLayoutActivity));
            mainActivityIntent.PutExtra("CameFrom", "Length");
            StartActivity(mainActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
        private void LayoutWeight_Click(object sender, EventArgs e)
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainLayoutActivity));
            mainActivityIntent.PutExtra("CameFrom", "Weight");
            StartActivity(mainActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
        private void LayoutDegrees_Click(object sender, EventArgs e)
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainLayoutActivity));
            mainActivityIntent.PutExtra("CameFrom", "Degrees");
            StartActivity(mainActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
        private void LayoutRadiansDegrees_Click(object sender, EventArgs e)
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainLayoutActivity));
            mainActivityIntent.PutExtra("CameFrom", "RadiansDegrees");
            StartActivity(mainActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
        private void LayoutArea_Click(object sender, EventArgs e)
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainLayoutActivity));
            mainActivityIntent.PutExtra("CameFrom", "Area");
            StartActivity(mainActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
        private void LayoutLiquid_Click(object sender, EventArgs e)
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainLayoutActivity));
            mainActivityIntent.PutExtra("CameFrom", "Liquid");
            StartActivity(mainActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
        private void LayoutConversionDetails_Click(object sender, EventArgs e)
        {
            Intent conversionActivityIntent = new Intent(this, typeof(ConversionDetailsActivity));
            StartActivity(conversionActivityIntent);
            OverridePendingTransition(Resource.Animation.in_from_right, Resource.Animation.out_to_left);
        }
    }
}