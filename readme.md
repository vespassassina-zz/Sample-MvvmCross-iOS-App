Website Creator Test App
========================

this is a sample app to show some Xamarin + MVVMCross in action.
it contains all the base components of much more complex apps:

- multi project setup
- view-viewmodel binding
- decoupled structure via dependency injection and interface resolution
- mvvmcross official plugins
- custom plugins (User Interaction)
- sql-lite code-first orm
- VM->VM navigation
- user interaction via async/await plugins
- platform filesystem integration
- bindable custom controls

**server instructions**
- install ruby + sinatra, if not locally available
- cd Server
- rackup
- the server should be running on http://localhost:9292
-if address is different reconfig the mobile app (/Core/Test.Core.Configuration/Configuration.cs) 

**app instructions**
The app comes preloaded with nuget packages, so no download is required.
- install xamarin
- open the solution file
- run the main project located in Platform/Test.iOS 

**other**
- Core/Test.Core contains all the view models
- /Core/Test.Core.Configuration/Configuration.cs contains the configuration for the app.
- If the server is running on a different address, configure it in Core/Test.Core.Configuration
- The custom plugin is inspired/derived from cheesebaron's mvvmcross userinteraction plugin.

Creating and running new platforms at this point would be just a matter of creating the 
platform UI and the platform implementation for the custom plugin
