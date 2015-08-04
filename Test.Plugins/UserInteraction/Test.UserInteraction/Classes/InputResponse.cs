namespace Plugins.UserInteraction
{
	public class InputResponse
	{
		public bool Ok { get; set; }

		public string Text { get; set; }

		public InputResponse()
		{
			
		}

		public InputResponse(string text)
		{
			Text = text;
			Ok = true;
		}

		public InputResponse(string text, bool ok)
		{
			Text = text;
			Ok = ok;
		}
	}
}

