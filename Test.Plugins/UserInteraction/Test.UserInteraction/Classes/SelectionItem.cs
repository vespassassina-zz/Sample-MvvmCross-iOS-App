namespace Plugins.UserInteraction
{
	using System;

	public class SelectionItem
	{
		public SelectionItem(string command = null, string title = null, string icon = null, Action selectionAction = null, string description = null, bool isChecked = false)
		{
			this.Command = command;
			this.Title = title;
			this.Description = description;
			this.Icon = icon;
			this.Checked = isChecked;
			this.SelectionAction = selectionAction;
		}

		public string Title { get; set; }

		public string Description { get; set; }

		public string Command { get; set; }

		public string Icon { get; set; }


		public bool Checked { get; set; }


		public Action SelectionAction { get; set; }
	}
}