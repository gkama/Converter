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
            Spinner fromSpinnerDeg = FindViewById<Spinner>(Resource.Id.fromSpinnerDeg);
            Spinner toSpinnerDeg = FindViewById<Spinner>(Resource.Id.toSpinnerDeg);

            fromSpinnerDeg.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinnerDeg.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.from_arrayd, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinnerDeg.Adapter = adapter;
            toSpinnerDeg.Adapter = adapter;
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
                    if (fromSpinnerDeg.SelectedItem.ToString() == "Radians" && toSpinnerDeg.SelectedItem.ToString() == "Degrees")
                        resultDeg.Text = (Convert.ToDouble(valueDeg.Text.ToString()) * (180 / PI)).ToString("#.000");
                    else if (fromSpinnerDeg.SelectedItem.ToString() == "Degrees" && toSpinnerDeg.SelectedItem.ToString() == "Radians")
                        resultDeg.Text = (Convert.ToDouble(valueDeg.Text.ToString()) * (PI / 180)).ToString("#.000");
                    else if (fromSpinnerDeg.SelectedItem.ToString() == "Radians" && toSpinnerDeg.SelectedItem.ToString() == "Radians")
                        resultDeg.Text = Convert.ToDouble(valueDeg.Text.ToString()).ToString("#.000");
                    else if (fromSpinnerDeg.SelectedItem.ToString() == "Degrees" && toSpinnerDeg.SelectedItem.ToString() == "Degrees")
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

