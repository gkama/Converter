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
using Android.Views;

namespace Converter
{
    [Activity(Icon = "@drawable/arrow_left_black", Theme = "@android:style/Theme.Holo.Light")]
    public class ConversionDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ConversionDetails);

            this.ActionBar.SetHomeButtonEnabled(true);

            //Custom font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");

            //Set text view fonts
            TextView txtMnuTextLengthConversion = FindViewById<TextView>(Resource.Id.txtMnuTextLengthConversion);
            TextView txtMnuTextWeightConversion = FindViewById<TextView>(Resource.Id.txtMnuTextWeightConversion);
            TextView txtMnuTextDegreesConversion = FindViewById<TextView>(Resource.Id.txtMnuTextDegreesConversion);
            TextView txtMnuTextRadiansDegreesConversion = FindViewById<TextView>(Resource.Id.txtMnuTextRadiansDegreesConversion);

            txtMnuTextLengthConversion.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextWeightConversion.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextDegreesConversion.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            txtMnuTextRadiansDegreesConversion.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            LinearLayout layoutLengthConversion = FindViewById<LinearLayout>(Resource.Id.layoutLengthConversion);
            LinearLayout layoutWeightConversion = FindViewById<LinearLayout>(Resource.Id.layoutWeightConversion);
            LinearLayout layoutDegreesConversion = FindViewById<LinearLayout>(Resource.Id.layoutDegreesConversion);
            LinearLayout layoutRadiansDegreesConversion = FindViewById<LinearLayout>(Resource.Id.layoutRadiansDegreesConversion);


            layoutLengthConversion.Click += LayoutLengthConversion_Click;
            layoutWeightConversion.Click += LayoutWeightConversion_Click;
            layoutDegreesConversion.Click += LayoutDegreesConversion_Click;
            layoutRadiansDegreesConversion.Click += LayoutRadiansDegreesConversion_Click;
        }

        private void LayoutLengthConversion_Click(object sender, EventArgs e)
        {
            ShowLengthDialog();
        }
        private void LayoutWeightConversion_Click(object sender, EventArgs e)
        {
            ShowWeightDialog();
        }
        private void LayoutDegreesConversion_Click(object sender, EventArgs e)
        {
            ShowDegreesDialog();
        }
        private void LayoutRadiansDegreesConversion_Click(object sender, EventArgs e)
        {
            ShowRadiansDegreesDialog();
        }

        //Dialog functions
        public void ShowLengthDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new LengthFormulasFragment();
            dialogFragment.Show(transaction, "length_formulas_fragment");
        }

        public void ShowWeightDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new WeightFormulasFragment();
            dialogFragment.Show(transaction, "weight_formulas_fragment");
        }
        public void ShowDegreesDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new DegreesFormulasFragment();
            dialogFragment.Show(transaction, "degrees_formulas_fragment");
        }
        public void ShowRadiansDegreesDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new RadiansDegreesFormulasFragment();
            dialogFragment.Show(transaction, "radiasns_degrees_formulas_fragment");
        }


        //Hhome button clicked
        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            Intent mainIntent = new Intent(this, typeof(Main));
            StartActivity(mainIntent);
            OverridePendingTransition(Resource.Animation.in_from_left, Resource.Animation.out_to_right);

            return base.OnMenuItemSelected(featureId, item);
        }
    }
}