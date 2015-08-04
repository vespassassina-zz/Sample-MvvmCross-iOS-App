Website Creator Test App
========================

this is a sample app to show some Xamarin + MVVMCross in action.
it contains all the base components of much more complex apps:

- multi project setup
- view-viewmodel binding
- mvvmcross official plugins
- custom plugins and plugin loading
- sql-lite code first orm
- VM->VM navigation
- user interaction via async/await plugins
- file system integration
- bindable custom controls

**instructions**
The app comes preloaded with nuget packages, so no download is required.
- install xamarin
- open the solution file
- run the server (should be running on http://localhost:9292)
- run the main project located in Platform/Test.iOS 

Creating and running new platforms at this point would be just a matter of creating the 
platform UI and the platform implementation for the custom plugin

**other**
- Core/Test.Core contains all the view models
- If the server is running on a different address, configure it in Core/Test.Core.Configuration
- The custom plugin is inspired/derived from cheesebaron's mvvmcross userinteraction plugin.
