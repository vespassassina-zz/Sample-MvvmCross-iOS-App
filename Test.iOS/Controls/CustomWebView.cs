using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.IO;

namespace Test.iOS
{
	[Register("CustomWebView")]
	public class CustomWebView : UIWebView
	{
		string content;

		public string HtmlContent {
			get {
				return content;
			}
			set {
				if (value == null || (content != null && content.Equals(value, StringComparison.InvariantCultureIgnoreCase))) {
					return;
				}
				content = value;
				LoadContent();
			}
		}


		string folder;

		public string Folder {
			get {
				return folder;
			}
			set {
				if (value == null || (folder != null && folder.Equals(value, StringComparison.InvariantCultureIgnoreCase))) {
					return;
				}
				folder = value;
				LoadContent();
			}
		}

		public CustomWebView()
		{
		}

		public CustomWebView(NSCoder coder)
			: base(coder)
		{
		}

		public CustomWebView(IntPtr p)
			: base(p)
		{
		}

		public CustomWebView(CGRect frame)
			: base(frame)
		{
		}

		void LoadContent()
		{
			if (content != null && folder != null) {
				string contentDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);
				this.LoadHtmlString(content, new NSUrl(contentDirectoryPath, true));
			} else {
				//this.LoadHtmlString(null, null);
			}
		}
	}
}

