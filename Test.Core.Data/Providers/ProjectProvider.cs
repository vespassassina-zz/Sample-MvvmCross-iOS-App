using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Test.Core.Data
{

	public interface IProjectProvider:IBaseProvider<Project>, IDisposable
	{
		List<Project> Projects();

		Project ProjectByName(string name);

		Project ProjectById(int id);

		void InsertOrUpdate(Project item);

	}

	public class ProjectProvider:BaseProvider<Project>, IProjectProvider
	{
		public List<Project> Projects()
		{
			var data = this.AsList();
			if (data != null && data.Count > 0) {
				data = data.OrderBy(o => o.Name).ToList();
			}

			return data;
		}

		public Project ProjectByName(string name)
		{
			return Find(x => x.Name.Equals(name));
		}

		public Project ProjectById(int id)
		{
			return Find(x => x.Id == id);
		}

		public void InsertOrUpdate(Project item)
		{
			if (this.Exists(x => x.Id == item.Id)) {
				Update(item);
				return;
			}

			Insert(item);
		}
	}
	
}
