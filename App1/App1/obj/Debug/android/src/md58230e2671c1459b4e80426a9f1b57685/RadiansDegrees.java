package md58230e2671c1459b4e80426a9f1b57685;


public class RadiansDegrees
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Converter.RadiansDegrees, Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RadiansDegrees.class, __md_methods);
	}


	public RadiansDegrees () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RadiansDegrees.class)
			mono.android.TypeManager.Activate ("Converter.RadiansDegrees, Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
