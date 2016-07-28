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
    public class LiquidFrag : Fragment
    {
        //Pints
        public double PINT_TO_PINT = 1.00000;
        public double PINT_TO_CM = 0.00047;
        public double PINT_TO_QT = 0.50000;
        public double PINT_TO_GL = 0.12500;
        public double PINT_TO_OZ = 16.00000;
        public double PINT_TO_CUPS = 2.00000;

        //Cubic Meters
        public double CM_TO_PINT = 2113.37630;
        public double CM_TO_CM = 1.00000;
        public double CM_TO_QT = 1056.68815;
        public double CM_TO_GL = 264.17204;
        public double CM_TO_OZ = 33814.02077;
        public double CM_TO_CUPS = 4226.75260;

        //Quarts
        public double QT_TO_PINT = 2.00000;
        public double QT_TO_CM = 0.00095;
        public double QT_TO_QT = 1.00000;
        public double QT_TO_GL = 0.25000;
        public double QT_TO_OZ = 32.00000;
        public double QT_TO_CUPS = 4.00000;

        //Gallons
        public double GL_TO_PINT = 8.00000;
        public double GL_TO_CM = 0.00379;
        public double GL_TO_QT = 4.00000;
        public double GL_TO_GL = 1.00000;
        public double GL_TO_OZ = 128.00000;
        public double GL_TO_CUPS = 16.00000;

        //Ounces
        public double OZ_TO_PINT = 0.06250;
        public double OZ_TO_CM = 0.00003;
        public double OZ_TO_QT = 0.03125;
        public double OZ_TO_GL = 0.00781;
        public double OZ_TO_OZ = 1.00000;
        public double OZ_TO_CUPS = 0.12500;

        //Cups
        public double CUPS_TO_PINT = 0.50000;
        public double CUPS_TO_CM = 0.00024;
        public double CUPS_TO_QT = 0.25000;
        public double CUPS_TO_GL = 0.06250;
        public double CUPS_TO_OZ = 8.00000;
        public double CUPS_TO_CUPS = 1.00000;

        public static Context currentLiquidMainActivityContext;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.Liquid, container, false);

            //Custom font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");
            
            Button buttonLiquid = view.FindViewById<Button>(Resource.Id.MyButtonLiquid);
            
            EditText valueLiquid = view.FindViewById<EditText>(Resource.Id.valueLiquid);
            TextView resultLiquid = view.FindViewById<TextView>(Resource.Id.resultLiquid);
            TextView valueTextViewLiquid = view.FindViewById<TextView>(Resource.Id.valueTextViewLiquid);
            TextView fromTextViewLiquid = view.FindViewById<TextView>(Resource.Id.fromTextViewLiquid);
            TextView toTextViewLiquid = view.FindViewById<TextView>(Resource.Id.toTextViewLiquid);

            valueLiquid.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            resultLiquid.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            valueTextViewLiquid.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            fromTextViewLiquid.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            toTextViewLiquid.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            valueLiquid.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal);

            //When value edit text view is not focused
            valueLiquid.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
            {
                if (!e.HasFocus)
                {
                    //Hide keyboard
                    InputMethodManager imm = (InputMethodManager)currentLiquidMainActivityContext.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(valueLiquid.WindowToken, 0);
                }
            };
            
            //Spinners
            Spinner fromSpinnerLiquid = view.FindViewById<Spinner>(Resource.Id.fromSpinnerLiquid);
            Spinner toSpinnerLiquid = view.FindViewById<Spinner>(Resource.Id.toSpinnerLiquid);

            fromSpinnerLiquid.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinnerLiquid.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var LiquidArrayPulled = Resources.GetStringArray(Resource.Array.liquid_array);

            MySpinnerAdapter adapter = new MySpinnerAdapter(view.Context, Android.Resource.Layout.SimpleSpinnerItem, LiquidArrayPulled);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinnerLiquid.Adapter = adapter;
            toSpinnerLiquid.Adapter = adapter;
            //End Spinners


            //Calculation
            buttonLiquid.Click += delegate
            {
                //Disable keyboard
                valueLiquid.ClearFocus();

                //Error checking
                if (string.IsNullOrEmpty(valueLiquid.Text.ToString().Trim()) || IsNumber(valueLiquid.Text.ToString().Trim()) == false)
                    Toast.MakeText(view.Context, "Please insert a valid Value!", ToastLength.Long).Show();
                else
                {
                    string conversionStr = fromSpinnerLiquid.SelectedItem.ToString().Trim() + toSpinnerLiquid.SelectedItem.ToString().Trim();
                    resultLiquid.Text = (convertLiquid(Convert.ToDouble(valueLiquid.Text.ToString().Trim()), conversionStr)).ToString("#.00000");
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
        private double convertLiquid(double Value, string conversionStr)
        {
            switch (conversionStr)
            {
                //Pints
                case "PintsPints":
                    return Value * PINT_TO_PINT;
                case "PintsCubic Meters":
                    return Value * PINT_TO_CM;
                case "PintsQuarts":
                    return Value * PINT_TO_QT;
                case "PintsGallons":
                    return Value * PINT_TO_GL;
                case "PintsOunces":
                    return Value * PINT_TO_OZ;
                case "PintsCups":
                    return Value * PINT_TO_CUPS;

                //Cubic Meters
                case "Cubic MetersPints":
                    return Value * CM_TO_PINT;
                case "Cubic MetersCubic Meters":
                    return Value * CM_TO_CM;
                case "Cubic MetersQuarts":
                    return Value * CM_TO_QT;
                case "Cubic MetersGallons":
                    return Value * CM_TO_GL;
                case "Cubic MetersOunces":
                    return Value * CM_TO_OZ;
                case "Cubic MetersCups":
                    return Value * CM_TO_CUPS;

                //Quarts
                case "QuartsPints":
                    return Value * QT_TO_PINT;
                case "QuartsCubic Meters":
                    return Value * QT_TO_CM;
                case "QuartsQuarts":
                    return Value * QT_TO_QT;
                case "QuartsGallons":
                    return Value * QT_TO_GL;
                case "QuartsOunces":
                    return Value * QT_TO_OZ;
                case "QuartsCups":
                    return Value * QT_TO_CUPS;

                //Gallons
                case "GallonsPints":
                    return Value * GL_TO_PINT;
                case "GallonsCubic Meters":
                    return Value * GL_TO_CM;
                case "GallonsQuarts":
                    return Value * GL_TO_QT;
                case "GallonsGallons":
                    return Value * GL_TO_GL;
                case "GallonsOunces":
                    return Value * GL_TO_OZ;
                case "GallonsCups":
                    return Value * GL_TO_CUPS;

                //Ounces
                case "OuncesPints":
                    return Value * OZ_TO_PINT;
                case "OuncesCubic Meters":
                    return Value * OZ_TO_CM;
                case "OuncesQuarts":
                    return Value * OZ_TO_QT;
                case "OuncesGallons":
                    return Value * OZ_TO_GL;
                case "OuncesOunces":
                    return Value * OZ_TO_OZ;
                case "OuncesCups":
                    return Value * OZ_TO_CUPS;

                //Cups
                case "CupsPints":
                    return Value * CUPS_TO_PINT;
                case "CupsCubic Meters":
                    return Value * CUPS_TO_CM;
                case "CupsQuarts":
                    return Value * CUPS_TO_QT;
                case "CupsGallons":
                    return Value * CUPS_TO_GL;
                case "CupsOunces":
                    return Value * CUPS_TO_OZ;
                case "CupsCups":
                    return Value * CUPS_TO_CUPS;

                //Default
                default:
                    return 0;
            }
        }
    }
}

