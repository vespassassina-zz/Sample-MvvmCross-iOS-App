using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugins.UserInteraction
{
	public interface IUserInteraction
	{
		Task<InputResponse> InputAsync(string message, string placeholder = null, string title = null, string okButton = "OK", string cancelButton = "Cancel", bool password = false);

		Task<SelectionItem> SelectAsync(List<SelectionItem> options, string title = "", string cancelButton = "Cancel");

		Task<SelectionItem> InputActionSheetAsync(List<SelectionItem> options, string cancelButton = "Cancel");
	}
}

