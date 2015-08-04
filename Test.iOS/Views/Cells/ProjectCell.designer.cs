// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Test.iOS
{
	[Register ("ProjectCell")]
	partial class ProjectCell
	{
		[Outlet]
		UIKit.UILabel ProjectName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProjectName != null) {
				ProjectName.Dispose ();
				ProjectName = null;
			}
		}
	}
}
