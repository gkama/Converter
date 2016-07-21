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
    public class MySpinnerAdapter : ArrayAdapter<string>
    {
        // Initialise custom font:
        private Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");

        //Constructor
        public MySpinnerAdapter(Context context, int resource, string[] items) : base(context, resource, items)
        {
            
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView view = (TextView)base.GetView(position, convertView, parent);
            view.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            return view;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            TextView view = (TextView)base.GetDropDownView(position, convertView, parent);
            view.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            return view;
        }

        public override void SetDropDownViewResource(int resource)
        {
            base.SetDropDownViewResource(resource);
        }

        public override int Count
        {
            get
            {
                return base.Count;
            }
        }
    }
}