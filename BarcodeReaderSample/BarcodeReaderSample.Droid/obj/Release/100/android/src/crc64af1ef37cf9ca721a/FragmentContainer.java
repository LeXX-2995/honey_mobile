package crc64af1ef37cf9ca721a;


public class FragmentContainer
	extends androidx.fragment.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"n_onDestroyView:()V:GetOnDestroyViewHandler\n" +
			"n_onHiddenChanged:(Z)V:GetOnHiddenChanged_ZHandler\n" +
			"";
		mono.android.Runtime.register ("Navigation.XF.Droid.FragmentContainer, DevExpress.XamarinForms.Navigation.Android", FragmentContainer.class, __md_methods);
	}


	public FragmentContainer ()
	{
		super ();
		if (getClass () == FragmentContainer.class) {
			mono.android.TypeManager.Activate ("Navigation.XF.Droid.FragmentContainer, DevExpress.XamarinForms.Navigation.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public FragmentContainer (int p0)
	{
		super (p0);
		if (getClass () == FragmentContainer.class) {
			mono.android.TypeManager.Activate ("Navigation.XF.Droid.FragmentContainer, DevExpress.XamarinForms.Navigation.Android", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
		}
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);


	public void onDestroyView ()
	{
		n_onDestroyView ();
	}

	private native void n_onDestroyView ();


	public void onHiddenChanged (boolean p0)
	{
		n_onHiddenChanged (p0);
	}

	private native void n_onHiddenChanged (boolean p0);

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
