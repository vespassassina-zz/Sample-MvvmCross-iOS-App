using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using CoreImage;
using System.Runtime.Remoting.Messaging;

namespace Plugins.UserInteraction.Touch
{

	public class OptionTableViewController : UITableViewController
	{
		int totalHeight = 0;
		int headerheight = 30;
		int rowheight = 44;

		public List<SelectionItem> Options{ get; set; }

		public Dictionary<string, List<SelectionItem>> OptionDictionary{ get; set; }

		public event EventHandler<SelectionItem> OptionSelected;

		public int TableTotalHeight {
			get {
				return totalHeight;
			}
		}

		public OptionTableViewController(List<SelectionItem> options, UITableViewStyle style = UITableViewStyle.Plain)
			: base(style)
		{
			int items = 0;
			int headers = 0;

			Options = options;
			OptionDictionary = new Dictionary<string, List<SelectionItem>>();

			List<SelectionItem> temp = new List<SelectionItem>();
			string previousBlockName = "";

			foreach (var item in options) {
				if (temp != null) {
					items++;
					temp.Add(item);
				}
			}

			if (temp != null && temp.Count > 0) {
				headers++;
				OptionDictionary.Add(previousBlockName, temp);
			}

			if (headers > 1) {
				totalHeight = totalHeight + (headers * headerheight);
			}
			totalHeight = totalHeight + (items * rowheight);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();			

			TableView.Source = new OptionTableViewControllerSource(OptionDictionary);
			((OptionTableViewControllerSource)TableView.Source).OptionSelected += (object sender, SelectionItem e) => {
				if (OptionSelected != null) {
					OptionSelected(this, e);
				}
			};
		}
	}

}
