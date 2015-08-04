using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Plugins.UserInteraction.Touch
{
	public class Plugin : IMvxPlugin
	{
		public void Load() 
		{
			Mvx.RegisterType<IUserInteraction, UserInteraction>();
		}
	}
}

