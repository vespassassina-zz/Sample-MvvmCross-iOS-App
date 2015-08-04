
using System;

using Foundation;
using UIKit;
using Test.iOS.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Test.Core.Data;

namespace Test.iOS
{
	public partial class ProjectsView : BaseView
	{
		public ProjectsView()
			: base("ProjectsView", null)
		{
		}



		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UIBarButtonItem newProjectButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
			NavigationItem.RightBarButtonItem = newProjectButton;

			var source = new MvxStandardTableViewSource(TableView, UITableViewCellStyle.Default, new NSString("cell"), "TitleText Name");
			TableView.Source = source;

			var set = this.CreateBindingSet<ProjectsView, Core.ViewModels.ProjectsViewModel>();

			set.Bind(newProjectButton).To(vm => vm.CreateProjectCommand);
			set.Bind(source).To(vm => vm.Projects);
			set.Bind(source).For(ui => ui.SelectionChangedCommand).To(vm => vm.SelectProjectCommand);

			set.Apply();
		}
			
	}
}

