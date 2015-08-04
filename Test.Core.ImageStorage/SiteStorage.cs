using System;
using System.Collections.Generic;
using System.Text;
using Cirrious.MvvmCross.Plugins.File;
using System.Linq;
using System.IO;

namespace Test.Core.ImageStorage
{


	public interface ISiteStorage
	{
		void StoreImage(byte[] data, string folder, string extension);

		void StoreImage(Stream data, string folder, string extension);

		void StoreHomePage(string html, string folder);

		void RemoveImage(string name, string folder);

		void RemoveImages(string folder);

		void RemoveFolder(string folder);

		List<string> ImagesInFolder(string folder);
	}

	public class LocalSiteStorage: ISiteStorage
	{
		readonly IMvxFileStore FileStore;

		public LocalSiteStorage(IMvxFileStore fileStore)
		{
			FileStore = fileStore;
		}


		public void StoreImage(byte[] data, string folder, string extension)
		{
			string name = GetNextAvailableFilename(folder, extension);
			string path = GetPathForWriting(name, folder);

			FileStore.WriteFile(path, data);
		}

		public void StoreImage(Stream data, string folder, string extension)
		{
			string name = GetNextAvailableFilename(folder, extension);
			string path = GetPathForWriting(name, folder);

			FileStore.WriteFile(path, data.CopyTo);
		}

		public void StoreHomePage(string html, string folder)
		{
			string path = GetPathForWriting("index.html", folder);
			FileStore.WriteFile(path, html);
		}

		public void RemoveImage(string name, string folder)
		{
			FileStore.EnsureFolderExists(folder);
			string path = Path.Combine(folder, name);

			if (FileStore.Exists(path)) {
				FileStore.DeleteFile(path);
			}
		}

		public void RemoveImages(string folder)
		{
			FileStore.EnsureFolderExists(folder);
			foreach (var item in ImagesInFolder(folder)) {
				FileStore.DeleteFile(item);
			}
		}

		public void RemoveFolder(string folder)
		{
			if (FileStore.FolderExists(folder)) {
				FileStore.DeleteFolder(folder, true);
			}
		}

		public List<string> ImagesInFolder(string folder)
		{
			FileStore.EnsureFolderExists(folder);
			return FileStore.GetFilesIn(folder)
				.Where(file => Path.GetExtension(file).ToLower().Contains("jpg"))
				.Select(file => Path.GetFileName(file)).ToList();
		}


		private string GetNextAvailableFilename(string folder, string extension)
		{
			var list = ImagesInFolder(folder);
			string name = string.Format("image_{0:0000}.{1}", list.Count + 1, extension);
			return name;
		}

		private string GetPathForWriting(string filename, string folder)
		{
			RemoveImage(filename, folder);

			return Path.Combine(folder, filename);
		}

	}
}

