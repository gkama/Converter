package md58230e2671c1459b4e80426a9f1b57685;


public class MySpinnerAdapter
	extends android.widget.ArrayAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_getDropDownView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetDropDownView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_setDropDownViewResource:(I)V:GetSetDropDownViewResource_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"";
		mono.android.Runtime.register ("Converter.MySpinnerAdapter, Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MySpinnerAdapter.class, __md_methods);
	}


	public MySpinnerAdapter (android.content.Context p0, int p1, java.lang.Object[] p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == MySpinnerAdapter.class)
			mono.android.TypeManager.Activate ("Converter.MySpinnerAdapter, Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:T[], Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1, p2 });
	}

	public MySpinnerAdapter (android.content.Context p0, int p1, java.lang.String[] p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == MySpinnerAdapter.class)
			mono.android.TypeManager.Activate ("Converter.MySpinnerAdapter, Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String[], mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);


	public android.view.View getDropDownView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getDropDownView (p0, p1, p2);
	}

	private native android.view.View n_getDropDownView (int p0, android.view.View p1, android.view.ViewGroup p2);


	public void setDropDownViewResource (int p0)
	{
		n_setDropDownViewResource (p0);
	}

	private native void n_setDropDownViewResource (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
