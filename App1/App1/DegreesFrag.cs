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

namespace Converter
{
    public class DegreesFrag : Fragment
    {
        //Values
        public double KELVIN_CONST = 273.15;
        public double KELVIN_CONST2 = 459.67;

        public static Context currentDFMainActivityContext;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.Degrees, container, false);

            //Custome font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");
            
            Button buttonDeg = view.FindViewById<Button>(Resource.Id.MyButtonDeg);
            Button degreesFormulasButton = view.FindViewById<Button>(Resource.Id.degreesFormulasButton);

            EditText valueDeg = view.FindViewById<EditText>(Resource.Id.valueDeg);
            TextView resultDeg = view.FindViewById<TextView>(Resource.Id.resultDeg);
            TextView valueTextViewDegrees = view.FindViewById<TextView>(Resource.Id.valueTextViewDegrees);
            TextView fromTextViewDegreest = view.FindViewById<TextView>(Resource.Id.fromTextViewDegrees);
            TextView toTextViewDegrees = view.FindViewById<TextView>(Resource.Id.toTextViewDegrees);

            valueDeg.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            resultDeg.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            degreesFormulasButton.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            valueTextViewDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            fromTextViewDegreest.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            toTextViewDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            valueDeg.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal);

            //When value edit text view is not focused
            valueDeg.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
            {
                if (!e.HasFocus)
                {
                    //Hide keyboard
                    InputMethodManager imm = (InputMethodManager)currentDFMainActivityContext.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(valueDeg.WindowToken, 0);
                }
            };
            
            //Spinners
            Spinner fromSpinnerDeg = view.FindViewById<Spinner>(Resource.Id.fromSpinnerDeg);
            Spinner toSpinnerDeg = view.FindViewById<Spinner>(Resource.Id.toSpinnerDeg);
            
            fromSpinnerDeg.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinnerDeg.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var degreesArrayPulled = Resources.GetStringArray(Resource.Array.degrees_array);

            MySpinnerAdapter adapter = new MySpinnerAdapter(view.Context, Android.Resource.Layout.SimpleSpinnerItem, degreesArrayPulled);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinnerDeg.Adapter = adapter;
            toSpinnerDeg.Adapter = adapter;
            //End Spinners

            //Calculation
            buttonDeg.Click += delegate
            {
                //Disable keyboard
                valueDeg.ClearFocus();

                //Error checking
                if (string.IsNullOrEmpty(valueDeg.Text.ToString().Trim()) || IsNumber(valueDeg.Text.ToString().Trim()) == false)
                    Toast.MakeText(view.Context, "Please insert a valid Value!", ToastLength.Long).Show();
                else
                {
                    if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Celcius" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Fahrenheit")
                        resultDeg.Text = ((Convert.ToDouble(valueDeg.Text.ToString().Trim()) * 9/5) + 32).ToString("#.000");

                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Fahrenheit" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Celcius")
                        resultDeg.Text = ((Convert.ToDouble(valueDeg.Text.ToString().Trim()) - 32) * 5/9).ToString("#.000");

                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Celcius" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Kelvin")
                        resultDeg.Text = (Convert.ToDouble(valueDeg.Text.ToString().Trim()) + KELVIN_CONST).ToString("#.000");

                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Kelvin" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Celcius")
                        resultDeg.Text = (Convert.ToDouble(valueDeg.Text.ToString().Trim()) - KELVIN_CONST).ToString("#.000");

                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Fahrenheit" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Kelvin")
                        resultDeg.Text = ((Convert.ToDouble(valueDeg.Text.ToString().Trim()) + KELVIN_CONST2) * 5/9).ToString("#.000");

                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Kelvin" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Fahrenheit")
                        resultDeg.Text = ((Convert.ToDouble(valueDeg.Text.ToString().Trim()) * 9/5) + KELVIN_CONST2).ToString("#.000");

                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Fahrenheit" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Fahrenheit")
                        resultDeg.Text = Convert.ToDouble(valueDeg.Text.ToString().Trim()).ToString("#.000");
                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Celcius" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Celcius")
                        resultDeg.Text = Convert.ToDouble(valueDeg.Text.ToString().Trim()).ToString("#.000");
                    else if (fromSpinnerDeg.SelectedItem.ToString().Trim() == "Kelvin" && toSpinnerDeg.SelectedItem.ToString().Trim() == "Kelvin")
                        resultDeg.Text = Convert.ToDouble(valueDeg.Text.ToString().Trim()).ToString("#.000");
                }
            };

            //
            degreesFormulasButton.Click += delegate
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
            var dialogFragment = new DegreesFormulasFragment();
            dialogFragment.Show(transaction, "degrees_formulas_fragment");
        }

        //Check if string is a number
        bool IsNumber(string s)
        {
            double d;
            return double.TryParse(s, out d);
        }
    }
}

