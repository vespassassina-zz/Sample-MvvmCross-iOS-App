

namespace Test.Core.Configuration
{


	public interface IConfiguration
	{
		string ApiUploadEndpoint{ get; }

		string WebUrl { get; }
	}

	public class BasicConfiguration:IConfiguration
	{
		public string ApiUploadEndpoint { 
			get { 
				return "http://localhost:9292/test"; 
			} 
		}

		public string WebUrl { 
			get { 
				return "http://localhost:9292/index.html"; 
			} 
		}
	}
}

