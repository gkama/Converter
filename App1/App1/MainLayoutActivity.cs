using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;

namespace Converter
{
    [Activity(Label = "Converter", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light")]
    class MainLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            SetContentView(Resource.Layout.MainLayout);

            AddTab("Radians/Degrees", new RadiansDegreesFrag());
            AddTab("Degrees", new DegreesFrag());

            if (bundle != null)
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

            // must set event handler before adding tab
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e) {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }
    }

    //RadiansDegrees Fragment
    class RadiansDegreesFrag : Fragment
    {
        //Values
        const double PI = 3.1416;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.RadiansDegrees, container, false);

            ////
            Button button = view.FindViewById<Button>(Resource.Id.MyButton);

            EditText valueTxt = view.FindViewById<EditText>(Resource.Id.valueTxt);
            TextView resultTxt = view.FindViewById<TextView>(Resource.Id.resultTxt);


            //Spinners
            Spinner fromSpinner = view.FindViewById<Spinner>(Resource.Id.fromSpinner);
            Spinner toSpinner = view.FindViewById<Spinner>(Resource.Id.toSpinner);

            fromSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            toSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    view.Context, Resource.Array.from_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            fromSpinner.Adapter = adapter;
            toSpinner.Adapter = adapter;
            //End Spinners

            //Calculation
            button.Click += delegate {
                //Disable keyboard
                valueTxt.ClearFocus();

                //Error checking
                if (string.IsNullOrEmpty(valueTxt.Text.ToString().Trim()))
                    Toast.MakeText(view.Context, "Invalid Input! Try Again", ToastLength.Long).Show();
                else
                {
                    if (fromSpinner.SelectedItem.ToString() == "Radians" && toSpinner.SelectedItem.ToString() == "Degrees")
                        resultTxt.Text = (Convert.ToDouble(valueTxt.Text.ToString()) * (180 / PI)).ToString("#.000");
                    else if (fromSpinner.SelectedItem.ToString() == "Degrees" && toSpinner.SelectedItem.ToString() == "Radians")
                        resultTxt.Text = (Convert.ToDouble(valueTxt.Text.ToString()) * (PI / 180)).ToString("#.000");
                    else if (fromSpinner.SelectedItem.ToString() == "Radians" && toSpinner.SelectedItem.ToString() == "Radians")
                        resultTxt.Text = Convert.ToDouble(valueTxt.Text.ToString()).ToString("#.000");
                    else if (fromSpinner.SelectedItem.ToString() == "Degrees" && toSpinner.SelectedItem.ToString() == "Degrees")
                        resultTxt.Text = Convert.ToDouble(valueTxt.Text.ToString()).ToString("#.000");
                }
            };
            ////


            return view;
        }

        //Spinner Item Selected
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

        }
        
    }

    

    class DegreesFrag : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
            Resource.Layout.Degrees, container, false);

            return view;
        }
    }
}