package crc6439a491d224ce8c87;


public class XamarinDataFormAutoCompleteEdit
	extends crc6439a491d224ce8c87.XamarinDataFormEditorBase_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DevExpress.XamarinForms.Editors.Android.DataForm.Editors.XamarinDataFormAutoCompleteEdit, DevExpress.XamarinForms.Editors.Android", XamarinDataFormAutoCompleteEdit.class, __md_methods);
	}


	public XamarinDataFormAutoCompleteEdit (android.view.View p0)
	{
		super (p0);
		if (getClass () == XamarinDataFormAutoCompleteEdit.class)
			mono.android.TypeManager.Activate ("DevExpress.XamarinForms.Editors.Android.DataForm.Editors.XamarinDataFormAutoCompleteEdit, DevExpress.XamarinForms.Editors.Android", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public XamarinDataFormAutoCompleteEdit (android.view.View p0, boolean p1)
	{
		super (p0, p1);
		if (getClass () == XamarinDataFormAutoCompleteEdit.class)
			mono.android.TypeManager.Activate ("DevExpress.XamarinForms.Editors.Android.DataForm.Editors.XamarinDataFormAutoCompleteEdit, DevExpress.XamarinForms.Editors.Android", "Android.Views.View, Mono.Android:System.Boolean, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public XamarinDataFormAutoCompleteEdit (android.view.View p0, boolean p1, com.devexpress.editors.dataForm.protocols.DXDataFormEditorItemErrorProvider p2)
	{
		super (p0, p1, p2);
		if (getClass () == XamarinDataFormAutoCompleteEdit.class)
			mono.android.TypeManager.Activate ("DevExpress.XamarinForms.Editors.Android.DataForm.Editors.XamarinDataFormAutoCompleteEdit, DevExpress.XamarinForms.Editors.Android", "Android.Views.View, Mono.Android:System.Boolean, mscorlib:Com.Devexpress.Editors.DataForm.Protocols.DXDataFormEditorItemErrorProvider, DevExpress.Xamarin.Android.Editors", this, new java.lang.Object[] { p0, p1, p2 });
	}

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
