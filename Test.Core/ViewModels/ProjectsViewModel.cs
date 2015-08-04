using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using Test.Core.Data;
using Cirrious.CrossCore;
using System.Windows.Input;
using System;
using System.Collections;
using Plugins.UserInteraction;

namespace Test.Core.ViewModels
{

	public class ProjectsViewModel : BaseViewModel
	{

		//properties

		List<Project> projects = new List<Project>();

		public List<Project> Projects {
			get {
				return projects;
			}
			set {
				projects = value;
				RaisePropertyChanged(() => Projects);
			}
		}

		//commands

		private MvxCommand<Project> selectProjectCommand;

		public ICommand SelectProjectCommand {
			get {
				return this.selectProjectCommand = this.selectProjectCommand ?? new MvxCommand<Project>(async item => {
					this.ShowViewModel<ProjectViewModel>(new Dictionary<string, string> { { "projectId", item.Id.ToString() } });
				});
			}
		}


		private MvxCommand createProjectCommand;

		public ICommand CreateProjectCommand {
			get {
				return this.createProjectCommand = this.createProjectCommand ?? new MvxCommand(async () => {
					
					var userInteraction = Mvx.Resolve<IUserInteraction>();
					var response = await userInteraction.InputAsync(null, "Name", "New Project", "Create", "Cancel");
					if (response.Ok) {
						using (var provider = Mvx.Resolve<IProjectProvider>()) {
						
							var project = new Project();
							project.Name = response.Text;
							project.Date = DateTime.Now;
							project.TemplateId = 1;
							project.LocalFolder = Guid.NewGuid().ToString("N");
							provider.InsertOrUpdate(project);

							//refresh the list
							Projects = provider.Projects();

							this.ShowViewModel<ProjectViewModel>(new Dictionary<string, string> { { "projectId", project.Id.ToString() } });
						}
					}
				});
			}
		}
	
		//implementation

		public override void ViewReady()
		{
			base.ViewReady();

			using (var provider = Mvx.Resolve<IProjectProvider>()) {
				Projects = provider.Projects();
			}
		}
		 
	}
}
