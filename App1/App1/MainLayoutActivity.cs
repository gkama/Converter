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
using Android.Graphics.Drawables;

namespace Converter
{
    [Activity(Icon = "@drawable/arrow_left_black", Theme = "@android:style/Theme.Holo.Light")]
    class MainLayoutActivity : Activity
    {
        private int LENGTHTAB_POS = 0;
        private int WEIGHTTAB_POS = 1;
        private int DEGREESTAB_POS = 2;
        private int RADIANSDEGREESTAB_POS = 3;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            this.ActionBar.SetHomeButtonEnabled(true);

            SetContentView(Resource.Layout.MainLayout);

            AddTab("length", new LengthFrag());
            AddTab("weight", new WeightFrag());
            AddTab("degrees", new DegreesFrag());
            AddTab("radians & degrees", new RadiansDegreesFrag());

            //Pass context
            RadiansDegreesFrag.currentRDFMainActivityContext = this.ApplicationContext;
            DegreesFrag.currentDFMainActivityContext = this.ApplicationContext;
            LengthFrag.currentLengthMainActivityContext = this.ApplicationContext;
            WeightFrag.currentWeightMainActivityContext = this.ApplicationContext;

            //See where it came from and set the selected tab
            string cameFrom = Intent.GetStringExtra("CameFrom");

            if (cameFrom == "Length")
                ActionBar.SetSelectedNavigationItem(LENGTHTAB_POS);
            else if (cameFrom == "Weight")
                ActionBar.SetSelectedNavigationItem(WEIGHTTAB_POS);
            else if (cameFrom == "Degrees")
                ActionBar.SetSelectedNavigationItem(DEGREESTAB_POS);
            else
                ActionBar.SetSelectedNavigationItem(RADIANSDEGREESTAB_POS);

            
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        //Add the tabs via this method
        void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            //tab.SetIcon(icon);
            
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

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            Intent mainIntent = new Intent(this, typeof(Main));
            StartActivity(mainIntent);
            OverridePendingTransition(Resource.Animation.in_from_left, Resource.Animation.out_to_right);

            return base.OnMenuItemSelected(featureId, item);
        }
    }
}