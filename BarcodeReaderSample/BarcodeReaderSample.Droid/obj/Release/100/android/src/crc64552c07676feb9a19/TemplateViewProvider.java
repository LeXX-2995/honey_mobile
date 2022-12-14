package crc64552c07676feb9a19;


public class TemplateViewProvider
	extends crc64ae133518ddfe8ba8.ViewProviderBase
	implements
		mono.android.IGCUserPeer,
		com.devexpress.dxgrid.providers.ViewProvider
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_requestView:(Landroid/content/Context;I)Landroid/view/View;:GetRequestView_Landroid_content_Context_IHandler:DevExpress.XamarinAndroid.Grid.IViewProviderInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"n_storeAsFree:(Landroid/view/View;)V:GetStoreAsFree_Landroid_view_View_Handler:DevExpress.XamarinAndroid.Grid.IViewProviderInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"n_updateAppearance:(Landroid/view/View;Lcom/devexpress/dxgrid/models/columns/GridColumnModel;Lcom/devexpress/dxgrid/models/appearance/AppearanceBase;I)V:GetUpdateAppearance_Landroid_view_View_Lcom_devexpress_dxgrid_models_columns_GridColumnModel_Lcom_devexpress_dxgrid_models_appearance_AppearanceBase_IHandler:DevExpress.XamarinAndroid.Grid.IViewProviderInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"n_updateContent:(Landroid/view/View;Lcom/devexpress/dxgrid/providers/DataProvider;Ljava/lang/String;I)V:GetUpdateContent_Landroid_view_View_Lcom_devexpress_dxgrid_providers_DataProvider_Ljava_lang_String_IHandler:DevExpress.XamarinAndroid.Grid.IViewProviderInvoker, DevExpress.Xamarin.Android.Grid\n" +
			"";
		mono.android.Runtime.register ("DevExpress.XamarinForms.DataGrid.Android.Internal.TemplateViewProvider, DevExpress.XamarinForms.Grid.Android", TemplateViewProvider.class, __md_methods);
	}


	public TemplateViewProvider ()
	{
		super ();
		if (getClass () == TemplateViewProvider.class) {
			mono.android.TypeManager.Activate ("DevExpress.XamarinForms.DataGrid.Android.Internal.TemplateViewProvider, DevExpress.XamarinForms.Grid.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public android.view.View requestView (android.content.Context p0, int p1)
	{
		return n_requestView (p0, p1);
	}

	private native android.view.View n_requestView (android.content.Context p0, int p1);


	public void storeAsFree (android.view.View p0)
	{
		n_storeAsFree (p0);
	}

	private native void n_storeAsFree (android.view.View p0);


	public void updateAppearance (android.view.View p0, com.devexpress.dxgrid.models.columns.GridColumnModel p1, com.devexpress.dxgrid.models.appearance.AppearanceBase p2, int p3)
	{
		n_updateAppearance (p0, p1, p2, p3);
	}

	private native void n_updateAppearance (android.view.View p0, com.devexpress.dxgrid.models.columns.GridColumnModel p1, com.devexpress.dxgrid.models.appearance.AppearanceBase p2, int p3);


	public void updateContent (android.view.View p0, com.devexpress.dxgrid.providers.DataProvider p1, java.lang.String p2, int p3)
	{
		n_updateContent (p0, p1, p2, p3);
	}

	private native void n_updateContent (android.view.View p0, com.devexpress.dxgrid.providers.DataProvider p1, java.lang.String p2, int p3);

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
