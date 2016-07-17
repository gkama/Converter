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


namespace Converter
{
    public class LengthFrag : Fragment
    {
        //Centimeters
        public double CM_TO_CM = 1.00000;
        public double CM_TO_M = 0.01000;
        public double CM_TO_FT = 0.03281;
        public double CM_TO_YD = 0.01094;
        public double CM_TO_KM = 0.00001;
        public double CM_TO_MI = 0.00001;
        public double CM_TO_IN = 0.39370;
        //Meters
        public double M_TO_CM = 100.00000;
        public double M_TO_M = 1.00000;
        public double M_TO_FT = 3.28084;
        public double M_TO_YD = 1.09361;
        public double M_TO_KM = 0.00100;
        public double M_TO_MI = 0.00062;
        public double M_TO_IN = 39.37008;
        //Feet
        public double FT_TO_CM = 30.48000;
        public double FT_TO_M = 0.30480;
        public double FT_TO_FT = 1.00000;
        public double FT_TO_YD = 0.33333;
        public double FT_TO_KM = 0.00030;
        public double FT_TO_MI = 0.00019;
        public double FT_TO_IN = 12.00000;
        //Yards
        public double YD_TO_CM = 91.43999;
        public double YD_TO_M = 0.91440;
        public double YD_TO_FT = 3.00000;
        public double YD_TO_YD = 1.00000;
        public double YD_TO_KM = 0.00091;
        public double YD_TO_MI = 0.00057;
        public double YD_TO_IN = 36.00000;
        //Kilometers
        public double KM_TO_CM = 100000.00000;
        public double KM_TO_M = 1000.00000;
        public double KM_TO_FT = 3280.83976;
        public double KM_TO_YD = 1093.61339;
        public double KM_TO_KM = 1.00000;
        public double KM_TO_MI = 0.62137;
        public double KM_TO_IN = 39370.07874;
        //Miles
        public double MI_TO_CM = 160934.68839;
        public double MI_TO_M = 1609.34688;
        public double MI_TO_FT = 5280.00925;
        public double MI_TO_YD = 1760.00329;
        public double MI_TO_KM = 1.60935;
        public double MI_TO_MI = 1.00000;
        public double MI_TO_IN = 63360.11354;
        //Inchhes
        public double IN_TO_CM = 2.54000;
        public double IN_TO_M = 0.02540;
        public double IN_TO_FT = 0.08333;
        public double IN_TO_YD = 0.02778;
        public double IN_TO_KM = 0.00003;
        public double IN_TO_MI = 0.00002;
        public double IN_TO_IN = 1.00000;

        public static Context currentLengthMainActivityContext;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.Length, container, false);

            //Custome font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");
            
            Button buttonLength = view.FindViewById<Button>(Resource.Id.MyButtonLength);
            Button lengthFormulasButton = view.FindViewById<Button>(Resource.Id.lengthFormulasButton);

            EditText valueLength = view.FindViewById<EditText>(Resource.Id.valueLength);
            TextView resultLength = view.FindViewById<TextView>(Resource.Id.resultLength);

            valueLength.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            resultLength.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);

            valueLength.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal);

            //When value edit text view is not focused
            valueLength.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
            {
                if (!e.HasFocus)
                {
                    //Hide keyboard
                    InputMethodManager imm = (InputMethodManager)currentLengthMainActivityContext.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(valueLength.WindowToken, 0);
                }
            };
            
            //Spinners
            Spinner fromSpinnerLength = view.FindViewById<Spinner>(Resource.Id.fromSpinnerLength);
            Spinner toSpinnerLength = view.FindViewById<Spinner>(Resource.Id.toSpinnerLength);
            
            fromSpinnerLength.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinnerLength.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var adapter = ArrayAdapter.CreateFromResource(
                    view.Context, Resource.Array.length_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinnerLength.Adapter = adapter;
            toSpinnerLength.Adapter = adapter;
            //End Spinners

            //Calculation
            buttonLength.Click += delegate
            {
                //Disable keyboard
                valueLength.ClearFocus();

                //Error checking
                if (string.IsNullOrEmpty(valueLength.Text.ToString().Trim()) || IsNumber(valueLength.Text.ToString().Trim()) == false)
                    Toast.MakeText(view.Context, "Please insert a valid Value!", ToastLength.Long).Show();
                else
                {
                    string conversionStr = fromSpinnerLength.SelectedItem.ToString().Trim() + toSpinnerLength.SelectedItem.ToString().Trim();
                    resultLength.Text = (convertLength(Convert.ToDouble(valueLength.Text.ToString().Trim()), conversionStr)).ToString("#.00000");
                }
            };

            //
            lengthFormulasButton.Click += delegate
            {
                ShowDialog();
            };

            return view;
        }

        //Spinner Item Selected
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }

        //Show dialog
        public void ShowDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new LengthFormulasFragment();
            dialogFragment.Show(transaction, "length_formulas_fragment");
        }

        //Check if string is a number
        bool IsNumber(string s)
        {
            double d;
            return double.TryParse(s, out d);
        }

        //Conversion function
        private double convertLength(double Value, string conversionStr)
        {
            switch (conversionStr)
            {
                //Centimeters
                case "CentimetersCentimeters":
                        return Value * CM_TO_CM;
                case "CentimetersMeters":
                        return Value * CM_TO_M;
                case "CentimetersFeet":
                        return Value * CM_TO_FT;
                case "CentimetersYards":
                        return Value * CM_TO_YD;
                case "CentimetersKilometers":
                        return Value * CM_TO_KM;
                case "CentimetersMiles":
                        return Value * CM_TO_MI;
                case "CentimetersInches":
                        return Value * CM_TO_IN;

                //Meters
                case "MetersCentimeters":
                    return Value * M_TO_CM;
                case "MetersMeters":
                    return Value * M_TO_M;
                case "MetersFeet":
                    return Value * M_TO_FT;
                case "MetersYards":
                    return Value * M_TO_YD;
                case "MetersKilometers":
                    return Value * M_TO_KM;
                case "MetersMiles":
                    return Value * M_TO_MI;
                case "MetersInches":
                    return Value * M_TO_IN;

                //Feet
                case "FeetCentimeters":
                    return Value * FT_TO_CM;
                case "FeetMeters":
                    return Value * FT_TO_M;
                case "FeetFeet":
                    return Value * FT_TO_FT;
                case "FeetYards":
                    return Value * FT_TO_YD;
                case "FeetKilometers":
                    return Value * FT_TO_KM;
                case "FeetMiles":
                    return Value * FT_TO_MI;
                case "FeetInches":
                    return Value * FT_TO_IN;

                //Yards
                case "YardsCentimeters":
                    return Value * YD_TO_CM;
                case "YardsMeters":
                    return Value * YD_TO_M;
                case "YardsFeet":
                    return Value * YD_TO_FT;
                case "YardsYards":
                    return Value * YD_TO_YD;
                case "YardsKilometers":
                    return Value * YD_TO_KM;
                case "YardsMiles":
                    return Value * YD_TO_MI;
                case "YardsInches":
                    return Value * YD_TO_IN;

                //Kilometers
                case "KilometersCentimeters":
                    return Value * KM_TO_CM;
                case "KilometersMeters":
                    return Value * KM_TO_M;
                case "KilometersFeet":
                    return Value * KM_TO_FT;
                case "KilometersYards":
                    return Value * KM_TO_YD;
                case "KilometersKilometers":
                    return Value * KM_TO_KM;
                case "KilometersMiles":
                    return Value * KM_TO_MI;
                case "KilometersInches":
                    return Value * KM_TO_IN;

                //Miles
                case "MilesCentimeters":
                    return Value * MI_TO_CM;
                case "MilesMeters":
                    return Value * MI_TO_M;
                case "MilesFeet":
                    return Value * MI_TO_FT;
                case "MilesYards":
                    return Value * MI_TO_YD;
                case "MilesKilometers":
                    return Value * MI_TO_KM;
                case "MilesMiles":
                    return Value * MI_TO_MI;
                case "MilesInches":
                    return Value * MI_TO_IN;

                //Inches
                case "InchesCentimeters":
                    return Value * IN_TO_CM;
                case "InchesMeters":
                    return Value * IN_TO_M;
                case "InchesFeet":
                    return Value * IN_TO_FT;
                case "InchesYards":
                    return Value * IN_TO_YD;
                case "InchesKilometers":
                    return Value * IN_TO_KM;
                case "InchesMiles":
                    return Value * IN_TO_MI;
                case "InchesInches":
                    return Value * IN_TO_IN;

                //Default
                default:
                    return 0;
            }
        }
    }
}

