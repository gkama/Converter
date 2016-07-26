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
    public class WeightFrag : Fragment
    {
        //Pounds
        public double LB_TO_LB = 1.00000;
        public double LB_TO_KG = 0.45359;
        public double LB_TO_OZ = 16.00000;
        public double LB_TO_G = 453.59232;
        public double LB_TO_T = 0.00050;
        //Kilograms
        public double KG_TO_LB = 2.20462;
        public double KG_TO_KG = 1.00000;
        public double KG_TO_OZ = 35.27397;
        public double KG_TO_G = 1000.00000;
        public double KG_TO_T = 0.00110;
        //Ounces
        public double OZ_TO_LB = 0.06250;
        public double OZ_TO_KG = 0.02835;
        public double OZ_TO_OZ = 1.00000;
        public double OZ_TO_G = 28.34952;
        public double OZ_TO_T = 0.00003;
        //Grams
        public double G_TO_LB = 0.00220;
        public double G_TO_KG = 0.00100;
        public double G_TO_OZ = 0.03527;
        public double G_TO_G = 1.00000;
        public double G_TO_T = 0.00000;
        //US Tons
        public double T_TO_LB = 2000.00000;
        public double T_TO_KG = 907.18464;
        public double T_TO_OZ = 32000.00000;
        public double T_TO_G = 907184.64000;
        public double T_TO_T = 1.00000;

        public static Context currentWeightMainActivityContext;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.Weight, container, false);

            //Custom font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");
            
            Button buttonWeight = view.FindViewById<Button>(Resource.Id.MyButtonWeight);
            
            EditText valueWeight = view.FindViewById<EditText>(Resource.Id.valueWeight);
            TextView resultWeight = view.FindViewById<TextView>(Resource.Id.resultWeight);
            TextView valueTextViewWeight = view.FindViewById<TextView>(Resource.Id.valueTextViewWeight);
            TextView fromTextViewWeight = view.FindViewById<TextView>(Resource.Id.fromTextViewWeight);
            TextView toTextViewWeight = view.FindViewById<TextView>(Resource.Id.toTextViewWeight);

            valueWeight.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            resultWeight.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            valueTextViewWeight.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            fromTextViewWeight.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            toTextViewWeight.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            valueWeight.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal);

            //When value edit text view is not focused
            valueWeight.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
            {
                if (!e.HasFocus)
                {
                    //Hide keyboard
                    InputMethodManager imm = (InputMethodManager)currentWeightMainActivityContext.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(valueWeight.WindowToken, 0);
                }
            };
            
            //Spinners
            Spinner fromSpinnerWeight = view.FindViewById<Spinner>(Resource.Id.fromSpinnerWeight);
            Spinner toSpinnerWeight = view.FindViewById<Spinner>(Resource.Id.toSpinnerWeight);

            fromSpinnerWeight.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinnerWeight.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var weightArrayPulled = Resources.GetStringArray(Resource.Array.weight_array);

            MySpinnerAdapter adapter = new MySpinnerAdapter(view.Context, Android.Resource.Layout.SimpleSpinnerItem, weightArrayPulled);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinnerWeight.Adapter = adapter;
            toSpinnerWeight.Adapter = adapter;
            //End Spinners


            //Calculation
            buttonWeight.Click += delegate
            {
                //Disable keyboard
                valueWeight.ClearFocus();

                //Error checking
                if (string.IsNullOrEmpty(valueWeight.Text.ToString().Trim()) || IsNumber(valueWeight.Text.ToString().Trim()) == false)
                    Toast.MakeText(view.Context, "Please insert a valid Value!", ToastLength.Long).Show();
                else
                {
                    string conversionStr = fromSpinnerWeight.SelectedItem.ToString().Trim() + toSpinnerWeight.SelectedItem.ToString().Trim();
                    resultWeight.Text = (convertWeight(Convert.ToDouble(valueWeight.Text.ToString().Trim()), conversionStr)).ToString("#.00000");
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
        private double convertWeight(double Value, string conversionStr)
        {
            switch (conversionStr)
            {
                //Pounds
                case "PoundsPounds":
                        return Value * LB_TO_LB;
                case "PoundsKilograms":
                        return Value * LB_TO_KG;
                case "PoundsOunces":
                        return Value * LB_TO_OZ;
                case "PoundsGrams":
                        return Value * LB_TO_G;
                case "PoundsUS Tons":
                        return Value * LB_TO_T;

                //Kilograms
                case "KilogramsPounds":
                    return Value * KG_TO_LB;
                case "KilogramsKilograms":
                    return Value * KG_TO_KG;
                case "KilogramsOunces":
                    return Value * KG_TO_OZ;
                case "KilogramsGrams":
                    return Value * KG_TO_G;
                case "KilogramsUS Tons":
                    return Value * KG_TO_T;

                //Ounces
                case "OuncesPounds":
                    return Value * OZ_TO_LB;
                case "OuncesKilograms":
                    return Value * OZ_TO_KG;
                case "OuncesOunces":
                    return Value * OZ_TO_OZ;
                case "OuncesGrams":
                    return Value * OZ_TO_G;
                case "OuncesUS Tons":
                    return Value * OZ_TO_T;

                //Grams
                case "GramsPounds":
                    return Value * G_TO_LB;
                case "GramsKilograms":
                    return Value * G_TO_KG;
                case "GramsOunces":
                    return Value * G_TO_OZ;
                case "GramsGrams":
                    return Value * G_TO_G;
                case "GramsUS Tons":
                    return Value * G_TO_T;

                //US Tons
                case "US TonsPounds":
                    return Value * T_TO_LB;
                case "US TonsKilograms":
                    return Value * T_TO_KG;
                case "US TonsOunces":
                    return Value * T_TO_OZ;
                case "US TonsGrams":
                    return Value * T_TO_G;
                case "US TonsUS Tons":
                    return Value * T_TO_T;

                //Default
                default:
                    return 0;
            }
        }
    }
}

