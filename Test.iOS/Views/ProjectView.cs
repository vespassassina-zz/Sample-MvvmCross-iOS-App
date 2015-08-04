
using System;

using Foundation;
using UIKit;
using Test.iOS.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using WebKit;

namespace Test.iOS
{
	public partial class ProjectView : BaseView
	{
		UIToolbar toolbar;
		CustomWebView webView;
		UIView activityView;

		public ProjectView()
			: base("ProjectView", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UIBarButtonItem saveButton = new UIBarButtonItem(UIBarButtonSystemItem.Save);
			NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{ saveButton };

			AddViews();
			UIBarButtonItem addImageButton = new UIBarButtonItem(UIBarButtonSystemItem.Camera);
			UIBarButtonItem selectTemplateButton = new UIBarButtonItem(UIBarButtonSystemItem.Compose);
			UIBarButtonItem uploadButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh);
			UIBarButtonItem spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
			UIBarButtonItem deleteProjectButton = new UIBarButtonItem(UIBarButtonSystemItem.Trash);
			toolbar.Items = new UIBarButtonItem[] {
				selectTemplateButton,
				addImageButton,
				uploadButton,
				spacer,
				deleteProjectButton
			};

			var set = this.CreateBindingSet<ProjectView, Core.ViewModels.ProjectViewModel>();

			set.Bind(deleteProjectButton).To(vm => vm.DeleteProjectCommand);
			set.Bind(saveButton).To(vm => vm.SaveProjectCommand);
			set.Bind(addImageButton).To(vm => vm.AddImageCommand);
			set.Bind(uploadButton).To(vm => vm.UploadProjectCommand);
			set.Bind(selectTemplateButton).To(vm => vm.SelectTemplateCommand);

			set.Bind(NavigationItem).For(ui => ui.Title).To(vm => vm.Name);
			set.Bind(activityView).For(ui => ui.Hidden).To(vm => vm.IsLoading).WithConversion("InvertBoolean");

			set.Bind(webView).For(ui => ui.HtmlContent).To(vm => vm.Html);
			set.Bind(webView).For(ui => ui.Folder).To(vm => vm.Folder);

			set.Apply();
		}


		void AddViews()
		{
			if (toolbar == null) {
				toolbar = new UIToolbar();
				View.AddSubview(toolbar);

				toolbar.TranslatesAutoresizingMaskIntoConstraints = false;
				View.AddConstraint(NSLayoutConstraint.Create(toolbar, NSLayoutAttribute.LeadingMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Leading, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(toolbar, NSLayoutAttribute.TrailingMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Trailing, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(toolbar, NSLayoutAttribute.BottomMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(toolbar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.Height, 1, 44));
			}

			if (webView == null) {
				webView = new CustomWebView();
				View.AddSubview(webView);

				webView.TranslatesAutoresizingMaskIntoConstraints = false;
				View.AddConstraint(NSLayoutConstraint.Create(webView, NSLayoutAttribute.LeadingMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Leading, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(webView, NSLayoutAttribute.TrailingMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Trailing, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(webView, NSLayoutAttribute.BottomMargin, NSLayoutRelation.Equal, toolbar, NSLayoutAttribute.Top, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(webView, NSLayoutAttribute.TopMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0));
			}

			if (activityView == null) {
				activityView = new UIView();
				activityView.Hidden = true;
				activityView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.2f);
				View.AddSubview(activityView);

				activityView.TranslatesAutoresizingMaskIntoConstraints = false;
				View.AddConstraint(NSLayoutConstraint.Create(activityView, NSLayoutAttribute.LeadingMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Leading, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(activityView, NSLayoutAttribute.TrailingMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Trailing, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(activityView, NSLayoutAttribute.BottomMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0));
				View.AddConstraint(NSLayoutConstraint.Create(activityView, NSLayoutAttribute.TopMargin, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0));

				UIActivityIndicatorView indicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
				indicator.StartAnimating();
				activityView.AddSubview(indicator);

				indicator.TranslatesAutoresizingMaskIntoConstraints = false;
				activityView.AddConstraint(NSLayoutConstraint.Create(indicator, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, activityView, NSLayoutAttribute.CenterX, 1, 0));
				activityView.AddConstraint(NSLayoutConstraint.Create(indicator, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, activityView, NSLayoutAttribute.CenterY, 1, 0));
				//activityView.AddConstraint(NSLayoutConstraint.Create(indicator, NSLayoutAttribute.Width, NSLayoutRelation.Equal, indicator, NSLayoutAttribute.Width, 1, 80));
				//activityView.AddConstraint(NSLayoutConstraint.Create(indicator, NSLayoutAttribute.Height, NSLayoutRelation.Equal, indicator, NSLayoutAttribute.Height, 1, 80));
			}
		}
	}
}

