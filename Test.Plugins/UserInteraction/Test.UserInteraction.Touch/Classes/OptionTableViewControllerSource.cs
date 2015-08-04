using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Plugins.UserInteraction.Touch
{

	public class OptionTableViewControllerSource:UITableViewSource
	{
		public Dictionary<string, List<SelectionItem>> Options{ get; set; }

		List<string> keyList;

		public event EventHandler<SelectionItem> OptionSelected;

		public OptionTableViewControllerSource(Dictionary<string, List<SelectionItem>> options)
		{
			Options = options;
			keyList = new List<string>(Options.Keys);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return keyList.Count;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			var key = keyList[(int)section];
			var list = Options[key];

			return list.Count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			if (keyList.Count <= 1) {
				return null;
			}

			var key = keyList[(int)section];
			return key;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			if (keyList.Count <= 1) {
				return 0.01f;
			}

			return 30;
		}

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
		{
			return 0.01f;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 44;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			//the data
			var key = keyList[indexPath.Section];
			var list = Options[key];

			SelectionItem option = list[indexPath.Row];

			var cell = tableView.DequeueReusableCell("OptionTableViewControllerCell");
			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "OptionTableViewControllerCell");
			}

			//text
			cell.TextLabel.Text = option.Title;
			cell.TextLabel.AdjustsFontSizeToFitWidth = true;

			//checkmark
			if (option.Checked) {
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			}

			//description
			cell.DetailTextLabel.Text = option.Description;
			cell.DetailTextLabel.TextColor = UIColor.DarkGray;
			cell.DetailTextLabel.AdjustsFontSizeToFitWidth = true;

			//image
			if (!string.IsNullOrEmpty(option.Icon)) {
				var image = UIImage.FromBundle(option.Icon);
				if (image != null) {
					cell.ImageView.Image = image;
				}
			}
				
			cell.BackgroundColor = UIColor.White;
			return cell;

		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var key = keyList[indexPath.Section];
			var list = Options[key];

			SelectionItem option = list[indexPath.Row];

			if (OptionSelected != null) {
				OptionSelected(this, option);
			}
		}
	}

}
