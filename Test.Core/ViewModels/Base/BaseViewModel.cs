using System;
using Cirrious.MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Test.Core
{
	public class BaseViewModel 	: MvxViewModel
	{

		bool isLoading = false;

		public bool IsLoading {
			get {
				return isLoading;
			}
			set {
				isLoading = value;
				RaisePropertyChanged(() => IsLoading);
			}
		}

		public virtual void ViewReady()
		{
		}

		public virtual void ViewCreated()
		{
		}


	}
		
}

