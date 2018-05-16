README
======
To provide some means to manage our DB2 for z/OS services, we need an application that can:

1. Connect to DB2 and itemise the services that we are allowed to see
2. Create new services
3. Drop services
4. Display information about a service
5. Run the service, supplying any parms required

wDB2REST has been written to try and do all of this. It has proved to be an interesting journey, as I use VB.NET infrequently, so finding out how to 
form a REST GET and PUT request, how to interact with certificates in Windows and how to dynamically build forms to show parameter lists has all been 
quite interesting.

If you see anything that looks especially clever, Stackoverflow is almost certainly the source. If, on the other hand, you see something utterly 
boneheaded, then that will be down to me, or my rather old MSDN reference!

Some notes:

a) Whilst almost all of this is written with vanilla VB.NET - as available in Microsoft Visual Studio Express 2013 for Windows Desktop - I couldn't
   overcome my frustration with JSON handling, and like almost everyone else who's talking about this on the internet, have opted to use Newtonsoft's
   (www.newtonsoft.com) JSON plugin. This can be installed (also under MIT licensing) into Visual Studio by:
   * In Visual Studio
   * Tools menu
   * NuGet Package Manager -> NuGet console
   * At the PM> prompt
   * Install-Package Newtonsoft.Json

b) The builds were done on Windows 10 using VB.NET 4.5

c) I haven't done anything clever to obfuscate userids and passwords stored in the registry, but as it's the users section of the registry, this 
   should provide some cover. So much better if you're using client certificate authentication over HTTPS...

James Gill - May 2018