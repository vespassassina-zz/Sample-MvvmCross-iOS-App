using System;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace Test.Core.Data
{

	public class Template
	{
		[PrimaryKey]
		public int Id { get; set; }

		public string Name{ get; set; }

		public string BodyTemplate{ get; set; }

		public string ImageTemplate{ get; set; }

	}
}

