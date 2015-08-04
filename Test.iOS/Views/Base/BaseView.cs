using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using Test.Core;
using System;

namespace Test.iOS.Views
{

	public class BaseView : MvxViewController
	{
		
		public BaseView(string nibName, NSBundle bundle)
			: base(nibName, bundle)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "";

			//ios 7 fix
			if (RespondsToSelector(new Selector("edgesForExtendedLayout"))) {
				EdgesForExtendedLayout = UIRectEdge.None;
			}

			var model = ViewModel as BaseViewModel;
			if (model != null) {
				model.ViewCreated();
			}
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			var model = ViewModel as BaseViewModel;
			if (model != null) {
				model.ViewReady();
			}
		}


		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}
}