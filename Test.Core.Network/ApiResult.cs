using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Cirrious.CrossCore;
using Test.Core.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Cirrious.MvvmCross.Plugins.File;

namespace Test.Core.Network
{

	public class ApiResult<T> where T:new()
	{
		public T Payload{ get; set; }

		public bool Success{ get; set; }

		public string Error{ get; set; }

		public ApiResult(T payload)
		{
			Payload = payload;
			Success = true;
		}

		public ApiResult(T payload, string error)
		{
			Payload = payload;
			Success = false;
			Error = error;
		}

		public ApiResult(Exception ex)
		{
			Payload = default(T);
			Success = false;
			Error = ex.Message;
		}
	}
	
}
