using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Converter
{
    public class LiquidFormulasFragment : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Dialog dialog = base.OnCreateDialog(savedInstanceState);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            
            return dialog;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.LiquidFormulas, container, false);

            //Custome font - Century Gothic
            Typeface centuryGothicFont = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/century_gothic_font.TTF");

            TableLayout tableLiquidFormulas = view.FindViewById<TableLayout>(Resource.Id.tableLiquidFormulas);
            Button dismissBtn = view.FindViewById<Button>(Resource.Id.dialogLiquidDismissBtn);

            //Iterate through every textView in table and set the font
            for (int k = 0; k < tableLiquidFormulas.ChildCount; k++)
            {
                View v = tableLiquidFormulas.GetChildAt(k);
                if (v.GetType().Equals(typeof(TableRow)))
                {
                    TableRow tr = (TableRow) v;
                    for(int a = 0; a < tr.ChildCount; a++)
                    {
                        TextView tv = (TextView) tr.GetChildAt(a);
                        tv.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);
                    } 
                }
            }

            //Set font
            dismissBtn.SetTypeface(centuryGothicFont, TypefaceStyle.Normal);

            dismissBtn.Click += (sender, args) => Dismiss();
            return view;
        }

        
    }
}