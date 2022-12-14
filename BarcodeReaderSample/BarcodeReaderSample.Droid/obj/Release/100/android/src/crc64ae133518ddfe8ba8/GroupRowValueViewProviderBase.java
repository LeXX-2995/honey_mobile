package crc64ae133518ddfe8ba8;


public abstract class GroupRowValueViewProviderBase
	extends crc64ae133518ddfe8ba8.ViewProviderBase
	implements
		mono.android.IGCUserPeer,
		com.devexpress.dxgrid.providers.GroupRowValueViewProvider,
		com.devexpress.dxgrid.providers.GroupRowValueViewProviderBase
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_canUpdate:(I)Z:GetCanUpdate_IHandler:DevExpress.XamarinAndroid.Grid.IGroupRowValueViewProviderInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"n_getView:(Landroid/content/Context;I)Landroid/view/View;:GetGetView_Landroid_content_Context_IHandler:DevExpress.XamarinAndroid.Grid.IGroupRowValueViewProviderBaseInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"n_updateView:(Landroid/content/Context;Landroid/view/View;I)V:GetUpdateView_Landroid_content_Context_Landroid_view_View_IHandler:DevExpress.XamarinAndroid.Grid.IGroupRowValueViewProviderBaseInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"";
		mono.android.Runtime.register ("DevExpress.XamarinForms.DataGrid.Android.GroupRowValueViewProviderBase, DevExpress.XamarinForms.Grid.Android", GroupRowValueViewProviderBase.class, __md_methods);
	}


	public GroupRowValueViewProviderBase ()
	{
		super ();
		if (getClass () == GroupRowValueViewProviderBase.class) {
			mono.android.TypeManager.Activate ("DevExpress.XamarinForms.DataGrid.Android.GroupRowValueViewProviderBase, DevExpress.XamarinForms.Grid.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public boolean canUpdate (int p0)
	{
		return n_canUpdate (p0);
	}

	private native boolean n_canUpdate (int p0);


	public android.view.View getView (android.content.Context p0, int p1)
	{
		return n_getView (p0, p1);
	}

	private native android.view.View n_getView (android.content.Context p0, int p1);


	public void updateView (android.content.Context p0, android.view.View p1, int p2)
	{
		n_updateView (p0, p1, p2);
	}

	private native void n_updateView (android.content.Context p0, android.view.View p1, int p2);

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
