using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using Test.Core.Data;
using Cirrious.CrossCore;
using System.Windows.Input;
using System;
using Test.Core.Composer;
using Test.Core.ImageStorage;
using Plugins.UserInteraction;
using System.Linq;
using Cirrious.MvvmCross.Plugins.PictureChooser;
using System.IO;
using Test.Core.Network;

namespace Test.Core.ViewModels
{
	public class ProjectViewModel : BaseViewModel
	{
		
		public string Name {
			get {
				return currentProject.Name;
			}
			set {
				currentProject.Name = value;
				projectProvider.InsertOrUpdate(currentProject);

				RaisePropertyChanged(() => Name);
			}
		}

		public int Template {
			get {
				return currentProject.TemplateId;
			}
			set {
				currentProject.TemplateId = value;
				projectProvider.InsertOrUpdate(currentProject);
				RaisePropertyChanged(() => Template);
			}
		}


		public string Html {
			get {
				return currentProject.Html;
			}
			set {
				currentProject.Html = value;
				projectProvider.InsertOrUpdate(currentProject);
				siteStorage.StoreHomePage(value, Folder);
				RaisePropertyChanged(() => Html);
			}
		}


		public string Folder {
			get {
				return currentProject.LocalFolder;
			}
		}


		public List<string> Images {
			get {
				return siteStorage.ImagesInFolder(currentProject.LocalFolder);
			}
		}

		//commands

		private MvxCommand uploadProjectCommand;

		public ICommand UploadProjectCommand {
			get {
				return this.uploadProjectCommand = this.uploadProjectCommand ?? new MvxCommand(async () => {
					IsLoading = true;
					Html = RegenerateHtml();

					var api = Mvx.Resolve<IApiConnection>();
					var result = await api.UploadWebsite(Folder);

					IsLoading = false;
				});
			}
		}

		private MvxCommand deleteProjectCommand;

		public ICommand DeleteProjectCommand {
			get {
				return this.deleteProjectCommand = this.deleteProjectCommand ?? new MvxCommand(() => {
					using (var provider = Mvx.Resolve<IProjectProvider>()) {
						provider.Delete(currentProject);
						siteStorage.RemoveFolder(Folder);
						this.Close(this);
					}
				});
			}
		}

		private MvxCommand saveProjectCommand;

		public ICommand SaveProjectCommand {
			get {
				return this.saveProjectCommand = this.saveProjectCommand ?? new MvxCommand(() => {
					using (var provider = Mvx.Resolve<IProjectProvider>()) {
						IsLoading = true;
						Html = RegenerateHtml();
						provider.InsertOrUpdate(currentProject);
						IsLoading = false;
					}
				});
			}
		}

	
		private MvxCommand addImageCommand;

		public ICommand AddImageCommand {
			get {
				return this.addImageCommand = this.addImageCommand ?? new MvxCommand(async () => {

					var userInteraction = Mvx.Resolve<IUserInteraction>();

					var selection = await userInteraction.InputActionSheetAsync(new List<SelectionItem>() {
						new SelectionItem("L", "From Library"),
						new SelectionItem("C", "From Camera")
					});

					if (selection != null) {
						var choser = Mvx.Resolve<IMvxPictureChooserTask>();
						Stream imageStream = null;
						if (selection.Command == "L") {
							imageStream = await choser.ChoosePictureFromLibrary(200, 75);
						}
						if (selection.Command == "C") {
							imageStream = await choser.TakePicture(200, 75);
						}

						if (imageStream != null) {
							siteStorage.StoreImage(imageStream, Folder, "jpg");
						}
					}

					Html = RegenerateHtml();
				});
			}
		}

		private MvxCommand selectTemplateCommand;

		public ICommand SelectTemplateCommand {
			get {
				return this.selectTemplateCommand = this.selectTemplateCommand ?? new MvxCommand(async () => {

					var userInteraction = Mvx.Resolve<IUserInteraction>();

					var availableTemplates = templateProvider.Templates().Select(template => new SelectionItem(template.Id.ToString(), template.Name, null, null, null, template.Id == currentProject.TemplateId)).ToList();
					var selectedItem = await userInteraction.SelectAsync(availableTemplates);

					if (selectedItem != null) {
						int selectedid = int.Parse(selectedItem.Command);
						Template = selectedid;
						Html = RegenerateHtml();
					}
				});
			}
		}

		//implementation

		private Project currentProject;

		private readonly ISiteStorage siteStorage;

		private readonly IProjectProvider projectProvider;

		private readonly ITemplateProvider templateProvider;

		private readonly IComposer composer;


		public ProjectViewModel(ISiteStorage siteStorage, IProjectProvider projectProvider, ITemplateProvider templateProvider, IComposer composer)
		{
			this.siteStorage = siteStorage;
			this.projectProvider = projectProvider;
			this.templateProvider = templateProvider;
			this.composer = composer;
		}

		public void Init(int projectId)
		{
			currentProject = projectProvider.ProjectById(projectId);
			if (currentProject == null) {
				this.Close(this);
				return;
			}
		}

		public override void ViewReady()
		{
			base.ViewReady();
			Html = RegenerateHtml();
		}

		private string RegenerateHtml()
		{
			if (Template > 0) {
				var template = templateProvider.Find(x => x.Id == Template);
				if (template != null) {
					var html = composer.ComposeHtml(template, siteStorage.ImagesInFolder(Folder));
					return html;
				}			
			}

			return null;
		}

	}
	
}
