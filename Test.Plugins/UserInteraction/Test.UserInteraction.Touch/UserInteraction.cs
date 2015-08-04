using System;
using CoreGraphics;
using UIKit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Foundation;
using System.Linq;

namespace Plugins.UserInteraction.Touch
{

	public class UserInteraction : IUserInteraction
	{
		static UIAlertView alertView = null;
		static UIViewController modalViewController = null;
		static UIAlertController alertController;


		private void DismissAll()
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() => {
				if (alertView != null) {
					alertView.DismissWithClickedButtonIndex(-1, false);
					alertView.Dispose();
					alertView = null;
				}
				if (modalViewController != null) {
					modalViewController.DismissViewController(false, null);
					modalViewController.Dispose();
					modalViewController = null;
				}
				if (alertController != null) {
					alertController.DismissViewController(false, null);
					alertController.Dispose();
					alertController = null;
				}

			});

		}


		public Task<SelectionItem> SelectAsync(List<SelectionItem> options, string title = "", string cancelButton = "Cancel")
		{
			var tcs = new TaskCompletionSource<SelectionItem>();

			Select(tcs.SetResult, options, title, cancelButton);

			return tcs.Task;
		}

		public Task<InputResponse> InputAsync(string message, string placeholder = null, string title = null, string okButton = "OK", string cancelButton = "Cancel", bool password = false)
		{
			var tcs = new TaskCompletionSource<InputResponse>();
			Input(message, (ok, text) => tcs.SetResult(new InputResponse { Ok = ok, Text = text }),	placeholder, title, okButton, cancelButton, password);
			return tcs.Task;
		}

		public Task<SelectionItem> InputActionSheetAsync(List<SelectionItem> options, string cancelButton = "Cancel")
		{
			var tcs = new TaskCompletionSource<SelectionItem>();
			InputActionSheet(tcs.SetResult,	options, cancelButton);
			return tcs.Task;
		}

		void InputActionSheet(Action<SelectionItem> selection, List<SelectionItem> options, string cancelButton = "Cancel")
		{
			DismissAll();

			UIApplication.SharedApplication.InvokeOnMainThread(() => {

				alertController = new UIAlertController();
				//alertController.ModalPresentationStylePreferredStyle = UIAlertControllerStyle.ActionSheet;
				foreach (var option in options) {
					var action = UIAlertAction.Create(option.Title, UIAlertActionStyle.Default, (a) => {
						selection(options.FirstOrDefault(x => x.Title == a.Title));
					});
					alertController.AddAction(action);
				}

				//cancel
				var cancel = UIAlertAction.Create(cancelButton, UIAlertActionStyle.Default, (a) => {
					selection(null);
				});
				alertController.AddAction(cancel);

				var topViewController = TopViewControllerWithRootViewController(UIApplication.SharedApplication.KeyWindow.RootViewController); 
				topViewController.PresentViewController(alertController, true, null);
			});
		}

		void Input(string message, Action<bool, string> answer, string placeholder = null, string title = null, string okButton = "OK", string cancelButton = "Cancel", bool password = false)
		{
			DismissAll();

			UIApplication.SharedApplication.InvokeOnMainThread(() => {

				alertView = new UIAlertView(title ?? string.Empty, message, null, cancelButton, okButton);
				alertView.AlertViewStyle = password ? UIAlertViewStyle.SecureTextInput : UIAlertViewStyle.PlainTextInput;
				var textField = alertView.GetTextField(0);
				textField.Placeholder = placeholder;
				if (answer != null) {
					alertView.Clicked += (sender, args) => {
						answer(alertView.CancelButtonIndex != args.ButtonIndex, textField.Text);					
					};
				}
				alertView.Show();

			});
		}

		void Select(Action<SelectionItem> selectedItem, List<SelectionItem> options, string title = null, string cancelButton = "Cancel")
		{
			DismissAll();

			UIApplication.SharedApplication.InvokeOnMainThread(() => {

				var table = new OptionTableViewController(options);
				UINavigationController navigationController = new UINavigationController(table);

				table.NavigationItem.Title = title;
				table.NavigationController.NavigationBarHidden = string.IsNullOrEmpty(title);
				table.OptionSelected += ( sender, e) => {
					modalViewController.DismissViewController(true, null);
					Task.Delay(300).Wait();
					modalViewController.Dispose();
					modalViewController = null;

					if (e.SelectionAction != null) {
						e.SelectionAction.Invoke();
					}

					selectedItem(e);
				}; 


				modalViewController = navigationController;
				modalViewController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;

				var topViewController = TopViewControllerWithRootViewController(UIApplication.SharedApplication.KeyWindow.RootViewController); 
				topViewController.PresentViewController(modalViewController, true, null);
			});
		}

		UIViewController TopViewControllerWithRootViewController(UIViewController rootViewController)
		{

			if (rootViewController is UITabBarController) {

				var tabBarController = rootViewController as UITabBarController;
				return TopViewControllerWithRootViewController(tabBarController.SelectedViewController);

			} else if (rootViewController is UINavigationController) {

				var navigationController = rootViewController as UINavigationController;
				return TopViewControllerWithRootViewController(navigationController.VisibleViewController);

			} else if (rootViewController.PresentedViewController != null) {

				var presentedViewController = rootViewController.PresentedViewController;
				return TopViewControllerWithRootViewController(presentedViewController);

			} else {

				foreach (UIView view in rootViewController.View.Subviews) {
					if (view.NextResponder != null) {
						UIViewController subViewController = view.NextResponder as UIViewController;
						if (subViewController != null) {
							return TopViewControllerWithRootViewController(subViewController);
						}
					}
				}

				return rootViewController;
			}

		}
	}
}

