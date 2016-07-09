using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;

namespace Converter
{
    [Activity(Label = "Converter",  Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light")]
    public class Degrees : Activity
    {
        //Values
        const double PI = 3.1416;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Degrees);

            Button button = FindViewById<Button>(Resource.Id.MyButtonDeg);

            EditText valueDeg = FindViewById<EditText>(Resource.Id.valueDeg);
            TextView resultDeg = FindViewById<TextView>(Resource.Id.resultDeg);


            //Spinners
            Spinner fromSpinner = FindViewById<Spinner>(Resource.Id.fromSpinnerDeg);
            Spinner toSpinner = FindViewById<Spinner>(Resource.Id.toSpinnerDeg);

            string fromSpinnerID = fromSpinner.Id.ToString();
            string toSpinnerID = toSpinner.Id.ToString();


            fromSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.from_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinner.Adapter = adapter;
            toSpinner.Adapter = adapter;
            //End Spinners

            //Calculation
            button.Click += delegate {
                //Disable keyboard
                valueDeg.ClearFocus();
                InputMethodManager closeKeyboard = (InputMethodManager)GetSystemService(Context.InputMethodService);
                closeKeyboard.HideSoftInputFromWindow(valueDeg.WindowToken, 0);

                //Error checking
                if (string.IsNullOrEmpty(valueDeg.Text.ToString().Trim()))
                    Toast.MakeText(this, "Invalid Input! Try Again", ToastLength.Long).Show();
                else
                {
                    if (fromSpinner.SelectedItem.ToString() == "Radians" && toSpinner.SelectedItem.ToString() == "Degrees")
                        resultDeg.Text = (Convert.ToDouble(valueDeg.Text.ToString()) * (180 / PI)).ToString("#.000");
                    else if (fromSpinner.SelectedItem.ToString() == "Degrees" && toSpinner.SelectedItem.ToString() == "Radians")
                        resultDeg.Text = (Convert.ToDouble(valueDeg.Text.ToString()) * (PI / 180)).ToString("#.000");
                    else if (fromSpinner.SelectedItem.ToString() == "Radians" && toSpinner.SelectedItem.ToString() == "Radians")
                        resultDeg.Text = Convert.ToDouble(valueDeg.Text.ToString()).ToString("#.000");
                    else if (fromSpinner.SelectedItem.ToString() == "Degrees" && toSpinner.SelectedItem.ToString() == "Degrees")
                        resultDeg.Text = Convert.ToDouble(valueDeg.Text.ToString()).ToString("#.000");
                }
            };
        }

       

        //Spinner Item Selected
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            
        }
    }
}

