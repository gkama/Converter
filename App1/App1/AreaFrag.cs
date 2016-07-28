using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Views.InputMethods;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Converter
{
    public class AreaFrag : Fragment
    {
        //Square Inches
        public double SQI_TO_SQIN = 1.00000;
        public double SQI_TO_SQKM = 0.00000;
        public double SQI_TO_SQMI = 0.00000;
        public double SQI_TO_SQM = 0.00065;
        public double SQI_TO_SQFT = 0.00694;
        public double SQI_TO_SQCM = 6.45160;
        public double SQI_TO_SQYD = 0.00077;

        //Square Kilometers
        public double SQKM_TO_SQIN = 1550387596.89922;
        public double SQKM_TO_SQKM = 1.00000;
        public double SQKM_TO_SQMI = 0.38605;
        public double SQKM_TO_SQM = 1000248.06202;
        public double SQKM_TO_SQFT = 10766579.84496;
        public double SQKM_TO_SQCM = 10002480620.15500;
        public double SQKM_TO_SQYD = 1196286.82171;

        //Square Miles
        public double SQMI_TO_SQIN = 4016064257.02811;
        public double SQMI_TO_SQKM = 2.59036;
        public double SQMI_TO_SQMI = 1.00000;
        public double SQMI_TO_SQM = 2591004.01606;
        public double SQMI_TO_SQFT = 27889333.33333;
        public double SQMI_TO_SQCM = 25910040160.64260;
        public double SQMI_TO_SQYD = 3098815.26104;

        //Square Meters
        public double SQM_TO_SQIN = 1550.00310;
        public double SQM_TO_SQKM = 0.00000;
        public double SQM_TO_SQMI = 0.00000;
        public double SQM_TO_SQM = 1.00000;
        public double SQM_TO_SQFT = 10.76391;
        public double SQM_TO_SQCM = 10000.00000;
        public double SQM_TO_SQYD = 1.19599;

        //Square Feet
        public double SQFT_TO_SQIN = 144.00001;
        public double SQFT_TO_SQKM = 0.00000;
        public double SQFT_TO_SQMI = 0.00000;
        public double SQFT_TO_SQM = 0.09290;
        public double SQFT_TO_SQFT = 1.00000;
        public double SQFT_TO_SQCM = 929.03046;
        public double SQFT_TO_SQYD = 0.11111;

        //Square Feet
        public double SQCM_TO_SQIN = 0.15500;
        public double SQCM_TO_SQKM = 0.00000;
        public double SQCM_TO_SQMI = 0.00000;
        public double SQCM_TO_SQM = 0.00010;
        public double SQCM_TO_SQFT = 0.00108;
        public double SQCM_TO_SQCM = 1.00000;
        public double SQCM_TO_SQYD = 0.00012;

        //Square Yards
        public double SQYD_TO_SQIN = 1295.99990;
        public double SQYD_TO_SQKM = 0.00000;
        public double SQYD_TO_SQMI = 0.00000;
        public double SQYD_TO_SQM = 0.83613;
        public double SQYD_TO_SQFT = 9.00000;
        public double SQYD_TO_SQCM = 8361.27293;
        public double SQYD_TO_SQYD = 1.00000;

        public static Context currentAreaMainActivityContext;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.Area, container, false);

            //Custom font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");
            
            Button buttonArea = view.FindViewById<Button>(Resource.Id.MyButtonArea);
            
            EditText valueArea = view.FindViewById<EditText>(Resource.Id.valueArea);
            TextView resultArea = view.FindViewById<TextView>(Resource.Id.resultArea);
            TextView valueTextViewArea = view.FindViewById<TextView>(Resource.Id.valueTextViewArea);
            TextView fromTextViewArea = view.FindViewById<TextView>(Resource.Id.fromTextViewArea);
            TextView toTextViewArea = view.FindViewById<TextView>(Resource.Id.toTextViewArea);

            valueArea.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            resultArea.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            valueTextViewArea.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            fromTextViewArea.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            toTextViewArea.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            valueArea.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal);

            //When value edit text view is not focused
            valueArea.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
            {
                if (!e.HasFocus)
                {
                    //Hide keyboard
                    InputMethodManager imm = (InputMethodManager)currentAreaMainActivityContext.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(valueArea.WindowToken, 0);
                }
            };
            
            //Spinners
            Spinner fromSpinnerArea = view.FindViewById<Spinner>(Resource.Id.fromSpinnerArea);
            Spinner toSpinnerArea = view.FindViewById<Spinner>(Resource.Id.toSpinnerArea);

            fromSpinnerArea.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinnerArea.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var AreaArrayPulled = Resources.GetStringArray(Resource.Array.area_array);

            MySpinnerAdapter adapter = new MySpinnerAdapter(view.Context, Android.Resource.Layout.SimpleSpinnerItem, AreaArrayPulled);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinnerArea.Adapter = adapter;
            toSpinnerArea.Adapter = adapter;
            //End Spinners


            //Calculation
            buttonArea.Click += delegate
            {
                //Disable keyboard
                valueArea.ClearFocus();

                //Error checking
                if (string.IsNullOrEmpty(valueArea.Text.ToString().Trim()) || IsNumber(valueArea.Text.ToString().Trim()) == false)
                    Toast.MakeText(view.Context, "Please insert a valid Value!", ToastLength.Long).Show();
                else
                {
                    string conversionStr = fromSpinnerArea.SelectedItem.ToString().Trim() + toSpinnerArea.SelectedItem.ToString().Trim();
                    resultArea.Text = (convertArea(Convert.ToDouble(valueArea.Text.ToString().Trim()), conversionStr)).ToString("#.00000");
                }
            };

            return view;
        }

        //Spinner Item Selected
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }

        //Check if string is a number
        bool IsNumber(string s)
        {
            double d;
            return double.TryParse(s, out d);
        }

        //Conversion function
        private double convertArea(double Value, string conversionStr)
        {
            switch (conversionStr)
            {
                //Square Inches
                case "Square InchesSquare Inches":
                    return Value * SQI_TO_SQIN;
                case "Square InchesSquare Kilometers":
                    return Value * SQI_TO_SQKM;
                case "Square InchesSquare Miles":
                    return Value * SQI_TO_SQMI;
                case "Square InchesSquare Meters":
                    return Value * SQI_TO_SQM;
                case "Square InchesSquare Feet":
                    return Value * SQI_TO_SQFT;
                case "Square InchesSquare Centimeters":
                    return Value * SQI_TO_SQCM;
                case "Square InchesSquare Yards":
                    return Value * SQI_TO_SQYD;

                //Square Kilometers
                case "Square KilometersSquare Inches":
                    return Value * SQKM_TO_SQIN;
                case "Square KilometersSquare Kilometers":
                    return Value * SQKM_TO_SQKM;
                case "Square KilometersSquare Miles":
                    return Value * SQKM_TO_SQMI;
                case "Square KilometersSquare Meters":
                    return Value * SQKM_TO_SQM;
                case "Square KilometersSquare Feet":
                    return Value * SQKM_TO_SQFT;
                case "Square KilometersSquare Centimeters":
                    return Value * SQKM_TO_SQCM;
                case "Square KilometersSquare Yards":
                    return Value * SQKM_TO_SQYD;

                //Square Miles
                case "Square MilesSquare Inches":
                    return Value * SQMI_TO_SQIN;
                case "Square MilesSquare Kilometers":
                    return Value * SQMI_TO_SQKM;
                case "Square MilesSquare Miles":
                    return Value * SQMI_TO_SQMI;
                case "Square MilesSquare Meters":
                    return Value * SQMI_TO_SQM;
                case "Square MilesSquare Feet":
                    return Value * SQMI_TO_SQFT;
                case "Square MilesSquare Centimeters":
                    return Value * SQMI_TO_SQCM;
                case "Square MilesSquare Yards":
                    return Value * SQMI_TO_SQYD;

                //Square Meters
                case "Square MetersSquare Inches":
                    return Value * SQM_TO_SQIN;
                case "Square MetersSquare Kilometers":
                    return Value * SQM_TO_SQKM;
                case "Square MetersSquare Miles":
                    return Value * SQM_TO_SQMI;
                case "Square MetersSquare Meters":
                    return Value * SQM_TO_SQM;
                case "Square MetersSquare Feet":
                    return Value * SQM_TO_SQFT;
                case "Square MetersSquare Centimeters":
                    return Value * SQM_TO_SQCM;
                case "Square MetersSquare Yards":
                    return Value * SQM_TO_SQYD;

                //Square Feet
                case "Square FeetSquare Inches":
                    return Value * SQFT_TO_SQIN;
                case "Square FeetSquare Kilometers":
                    return Value * SQFT_TO_SQKM;
                case "Square FeetSquare Miles":
                    return Value * SQFT_TO_SQMI;
                case "Square FeetSquare Meters":
                    return Value * SQFT_TO_SQM;
                case "Square FeetSquare Feet":
                    return Value * SQFT_TO_SQFT;
                case "Square FeetSquare Centimeters":
                    return Value * SQFT_TO_SQCM;
                case "Square FeetSquare Yards":
                    return Value * SQFT_TO_SQYD;

                //Square Centimeters
                case "Square CentimetersSquare Inches":
                    return Value * SQCM_TO_SQIN;
                case "Square CentimetersSquare Kilometers":
                    return Value * SQCM_TO_SQKM;
                case "Square CentimetersSquare Miles":
                    return Value * SQCM_TO_SQMI;
                case "Square CentimetersSquare Meters":
                    return Value * SQCM_TO_SQM;
                case "Square CentimetersSquare Feet":
                    return Value * SQCM_TO_SQFT;
                case "Square CentimetersSquare Centimeters":
                    return Value * SQCM_TO_SQCM;
                case "Square CentimetersSquare Yards":
                    return Value * SQCM_TO_SQYD;

                //Square Yards
                case "Square YardsSquare Inches":
                    return Value * SQYD_TO_SQIN;
                case "Square YardsSquare Kilometers":
                    return Value * SQYD_TO_SQKM;
                case "Square YardsSquare Miles":
                    return Value * SQYD_TO_SQMI;
                case "Square YardsSquare Meters":
                    return Value * SQYD_TO_SQM;
                case "Square YardsSquare Feet":
                    return Value * SQYD_TO_SQFT;
                case "Square YardsSquare Centimeters":
                    return Value * SQYD_TO_SQCM;
                case "Square YardsSquare Yards":
                    return Value * SQYD_TO_SQYD;

                //Default
                default:
                    return 0;
            }
        }
    }
}

