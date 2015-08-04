
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.BindingContext;
using Test.Core.Data;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace Test.iOS
{
	public partial class ProjectCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName("ProjectCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString("ProjectCell");

		public ProjectCell(IntPtr handle)
			: base(handle)
		{

			this.DelayBind(() => {
				var set = this.CreateBindingSet<ProjectCell, Project>();
				set.Bind(ProjectName).To(x => x.Name);
				set.Apply();
			});
		}

		public static ProjectCell Create()
		{
			return (ProjectCell)Nib.Instantiate(null, null)[0];
		}
	}
}

