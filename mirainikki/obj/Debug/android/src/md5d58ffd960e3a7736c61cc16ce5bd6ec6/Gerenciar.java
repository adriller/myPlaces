package md5d58ffd960e3a7736c61cc16ce5bd6ec6;


public class Gerenciar
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
		mono.android.Runtime.register ("mirainikki.Gerenciar, mirainikki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Gerenciar.class, __md_methods);
	}


	public Gerenciar () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Gerenciar.class)
			mono.android.TypeManager.Activate ("mirainikki.Gerenciar, mirainikki, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
