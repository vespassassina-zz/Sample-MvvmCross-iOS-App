using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Test.Core.Data
{




	public interface ITemplateProvider: IBaseProvider<Template>, IDisposable
	{
		List<Template> Templates();

		void InsertOrUpdate(Template item);
	}

	public class TemplateProvider:BaseProvider<Template>, ITemplateProvider
	{
		public List<Template> Templates()
		{
			var data = this.AsList();
			if (data != null && data.Count > 0) {
				data = data.OrderBy(o => o.Name).ToList();
			}

			return data;
		}

		public void InsertOrUpdate(Template item)
		{
			if (this.Exists(x => x.Id == item.Id)) {
				Update(item);
				return;
			}

			Insert(item);
		}
	}
}
