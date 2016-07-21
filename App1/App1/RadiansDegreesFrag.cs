using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;


namespace Converter
{
    public class RadiansDegreesFrag : Fragment
    {
        //Values
        const double PI = 3.1416;
        public static Context currentRDFMainActivityContext;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.RadiansDegrees, container, false);

            //Custome font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");

            Button button = view.FindViewById<Button>(Resource.Id.MyButton);
            Button radiansDegreesFormulasButton = view.FindViewById<Button>(Resource.Id.radiansDegreesFormulasButton);

            EditText valueTxt = view.FindViewById<EditText>(Resource.Id.valueTxt);
            TextView resultTxt = view.FindViewById<TextView>(Resource.Id.resultTxt);
            TextView valueTextViewRadiansDegrees = view.FindViewById<TextView>(Resource.Id.valueTextViewRadiansDegrees);
            TextView fromTextViewRadiansDegrees = view.FindViewById<TextView>(Resource.Id.fromTextViewRadiansDegrees);
            TextView toTextViewRadiansDegrees = view.FindViewById<TextView>(Resource.Id.toTextViewRadiansDegrees);

            valueTxt.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            resultTxt.SetTypeface(centuryGothicFont, TypefaceStyle.Italic);
            radiansDegreesFormulasButton.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            valueTextViewRadiansDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            fromTextViewRadiansDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
            toTextViewRadiansDegrees.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            valueTxt.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal);

            //When value edit text view is not focused
            valueTxt.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
            {
                if(!e.HasFocus)
                { 
                    //Hide keyboard
                    InputMethodManager imm = (InputMethodManager)currentRDFMainActivityContext.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(valueTxt.WindowToken, 0);
                }
            };


            //Spinners
            Spinner fromSpinner = view.FindViewById<Spinner>(Resource.Id.fromSpinner);
            Spinner toSpinner = view.FindViewById<Spinner>(Resource.Id.toSpinner);

            fromSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
       
            var radiansDegreesArrayPulled = Resources.GetStringArray(Resource.Array.radiansDegrees_array);

            MySpinnerAdapter adapter = new MySpinnerAdapter(view.Context, Android.Resource.Layout.SimpleSpinnerItem, radiansDegreesArrayPulled);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinner.Adapter = adapter;
            toSpinner.Adapter = adapter;
            //End Spinners

            //Calculation
            button.Click += delegate {
                //Disable keyboard
                valueTxt.ClearFocus();


                //Error checking
                if (string.IsNullOrEmpty(valueTxt.Text.ToString().Trim()) || IsNumber(valueTxt.Text.ToString().Trim()) == false)
                    Toast.MakeText(view.Context, "Please insert a valid Value!", ToastLength.Long).Show();
                else
                { //Calculations
                    if (fromSpinner.SelectedItem.ToString().Trim() == "Radians" && toSpinner.SelectedItem.ToString().Trim() == "Degrees")
                        resultTxt.Text = (Convert.ToDouble(valueTxt.Text.ToString().Trim()) * (180 / PI)).ToString("#.000");

                    else if (fromSpinner.SelectedItem.ToString().Trim() == "Degrees" && toSpinner.SelectedItem.ToString().Trim() == "Radians")
                        resultTxt.Text = (Convert.ToDouble(valueTxt.Text.ToString().Trim()) * (PI / 180)).ToString("#.000");

                    else if (fromSpinner.SelectedItem.ToString().Trim() == "Radians" && toSpinner.SelectedItem.ToString().Trim() == "Radians")
                        resultTxt.Text = Convert.ToDouble(valueTxt.Text.ToString().Trim()).ToString("#.000");

                    else if (fromSpinner.SelectedItem.ToString().Trim() == "Degrees" && toSpinner.SelectedItem.ToString().Trim() == "Degrees")
                        resultTxt.Text = Convert.ToDouble(valueTxt.Text.ToString().Trim()).ToString("#.000");
                }
            };

            radiansDegreesFormulasButton.Click += delegate
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
            var dialogFragment = new RadiansDegreesFormulasFragment();
            dialogFragment.Show(transaction, "radiasns_degrees_formulas_fragment");
        }

        //Check if string is a number
        bool IsNumber(string s)
        {
            double d;
            return double.TryParse(s, out d);
        }
    }
}

