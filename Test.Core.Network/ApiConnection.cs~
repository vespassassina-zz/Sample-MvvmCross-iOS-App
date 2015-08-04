using System;
using System.Threading.Tasks;
using System.Net.Http;
using Cirrious.CrossCore;
using Test.Core.Configuration;
using System.IO;
using System.Net;
using Cirrious.MvvmCross.Plugins.File;

namespace Test.Core.Network
{
	public interface IApiConnection
	{
		Task<ApiResult<bool>> UploadWebsite(string localfolder);
	}

	public class ApiConnection:IApiConnection
	{
		public async Task<ApiResult<bool>> UploadWebsite(string localfolder)
		{
			try {
				string url = Mvx.Resolve<IConfiguration>().ApiUploadEndpoint;
				var filestore = Mvx.Resolve<IMvxFileStore>();
				foreach (var item in filestore.GetFilesIn(localfolder)) {
					byte[] buffer;
					if (filestore.TryReadBinaryFile(item, out buffer)) {
						var uploadresult = await UploadByte(url, buffer, Path.GetFileName(item));
						if (uploadresult.Success == false) {
							return new ApiResult<bool>(false, uploadresult.Error);
						}
					}
				}

				return new ApiResult<bool>(true);
			} catch (Exception ex) {
				return new ApiResult<bool>(ex);
			}
		}

	
		async Task<ApiResult<bool>> UploadByte(string url, byte[] file, string fileName)
		{
			try {
				using (var client = new HttpClient()) {
					var imageContent = new ByteArrayContent(file);

					var requestContent = new MultipartFormDataContent(); 
					requestContent.Add(imageContent, "file", fileName);

					var response = await client.PostAsync(url, requestContent);
					return new ApiResult<bool>(response.StatusCode == HttpStatusCode.OK);
				}
			} catch (Exception ex) {
				return new ApiResult<bool>(ex);
			}
		}
	}
}

