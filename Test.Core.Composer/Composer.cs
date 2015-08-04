using System;
using System.Collections.Generic;
using Test.Core.Data;
using System.Text;
using System.IO;
using System.Linq;

namespace Test.Core.Composer
{
	public class ImagePayload
	{
		public string Path{ get; set; }

		public string Caption{ get; set; }

		public ImagePayload(string path, string caption)
		{
			Path = path;
			Caption = caption;
		}
	}

	public interface IComposer
	{
		string ComposeHtml(Template template, List<ImagePayload> images);

		string ComposeHtml(Template template, List<string> imagePaths);
	}

	public class HtmlComposer: IComposer
	{
		public string ComposeHtml(Template template, List<ImagePayload> images)
		{

			StringBuilder imageCorpus = new StringBuilder();
			foreach (var image in images) {
				imageCorpus.AppendFormat(template.ImageTemplate, image.Path, image.Caption);
			}

			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(template.BodyTemplate, imageCorpus);

			return builder.ToString();
		}

		public string ComposeHtml(Template template, List<string> imagePaths)
		{
			var payloads = imagePaths.Select(path => new ImagePayload(path, "")).ToList();
			return ComposeHtml(template, payloads);
		}
	}
}

