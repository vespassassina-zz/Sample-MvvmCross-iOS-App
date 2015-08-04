using System;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace Test.Core.Data
{
	public class Project
	{
		[AutoIncrement, PrimaryKey]
		public int Id { get; set; }

		public string Name{ get; set; }

		public string LocalFolder{ get; set; }

		public DateTime Date { get; set; }

		[Indexed]
		public int TemplateId{ get; set; }

		public string Html{ get; set; }
	}
	
}
