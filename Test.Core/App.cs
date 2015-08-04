using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using Test.Core.Data;
using Test.Core.Network;
using Test.Core.Composer;
using Test.Core.Configuration;
using Test.Core.ImageStorage;
using System;

namespace Test.Core
{
	public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
			//anything with a suffix of Services will be a singleton
			CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			//register providers for dependency injection and interface resolution 
			Mvx.RegisterType<IProjectProvider, ProjectProvider>();
			Mvx.RegisterType<ITemplateProvider, TemplateProvider>();
			Mvx.RegisterType<IApiConnection, ApiConnection>();
			Mvx.RegisterType<IComposer, HtmlComposer>();
			Mvx.RegisterType<ISiteStorage, LocalSiteStorage>();

			//singletons
			Mvx.LazyConstructAndRegisterSingleton<IConfiguration, BasicConfiguration>();
				
			//first view model to be loaded
			RegisterAppStart<ViewModels.ProjectsViewModel>();

			SeedDatabase();
		}

		private void SeedDatabase()
		{
			using (var provider = Mvx.Resolve<ITemplateProvider>()) {

				#if DEBUG
				provider.DropTable();
				#endif

				if (provider.Templates().Count <= 0) {
					
					provider.Insert(new Template() {
						Id = 1,
						Name = "Basic",
						ImageTemplate = "<div class='image'><img src='{0}' title='{1}'/></div>",
						BodyTemplate = "<html><body bgcolor='grey'><div class='page'>{0}</div></body></html>"
					});

					provider.Insert(new Template() {
						Id = 2,
						Name = "Tabled",
						ImageTemplate = "<tr><td><img src='{0}' title='{1}'/></td><td>{1}</td></tr>",
						BodyTemplate = "<html><body bgcolor='grey'><table>{0}</table></body></html>"
					});

					provider.Insert(new Template() {
						Id = 3,
						Name = "Blue",
						ImageTemplate = "<span class='image'><img src='{0}' title='{1}'/><br /><span class='caption'>{1}</span></span>",
						BodyTemplate = "<html><body bgcolor='blue'><div class='page'>{0}</div></body></html>"
					});

				}

			}
		}
	}
}