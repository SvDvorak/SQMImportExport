#SQM Import Export
This library enables you to import ArmA 2 & 3 *.sqm files so that you can analyze or modify it. When you're done you can export the file again, with your changes of course.

Since I haven't had access to any scheme that explains what SQM-files can contain, my implementation has been by trial and error on a lot of files, some properties might be missing. You should get an exception in that case which would tell you what line it cannot parse, please contact me with the error and a copy of the file that failed.

#How to use
##Import
Use the Import function on the SqmImporter to import an *.sqm file. It takes a stream to read from, any kind of Stream works just don't forget to close it after! Since ArmA 2 And ArmA 3 *.sqm-files are different and need to be handled differently I recommend you to use the [Visitor pattern](http://en.wikipedia.org/wiki/Visitor_pattern) with the return value but casting it to either type works as well. If the importer cannot read the file an SqmContentsInvalidException-exception is thrown, check the inner exception for more details.

	var importStream = new FileStream("c:\\mission.sqm", FileMode.Open);
	var importer = new SqmImporter();
	var sqmContents = importer.Import(importStream);
    _sqmContents.Accept(new ArmAContentsVisitor());

or

	var importStream = new FileStream("c:\\mission.sqm", FileMode.Open);
	var importer = new SqmImporter();
	var sqmContents = importer.Import(importStream);
	var arma2Contents = sqmContents as SQMImportExport.ArmA2.SqmContents;
	if (arma2Contents != null)
	{
	    // Do ArmA 2 stuff
	}
	else
	{
	    var arma3Contents = sqmContents as SQMImportExport.ArmA3.SqmContents;
	    // Do ArmA 3 stuff
	}

##Export
Use the Export function on the SqmExporter to export an SqmContentsBase instance (both ArmA 2 and ArmA 3 contents inherit from it). The stream it takes in can be any kind of Stream just don't forget to close it afterwards!

	var exportStream = new FileStream("c:\\mission_out.sqm", FileMode.Create);
	var exporter = new SqmExporter();
	exporter.Export(exportStream, sqmContents);
	exportStream.Close();

#Release Notes
Version 0.3
- Added CombatMode, Formation, Speed, Combat to Waypoint.

Version 0.2
- Added StartWindDir, StartGust, ForecastGust, ForecastWindDir to Intel and OffsetY, Special, ForceHeadlessClient and IsUAV to Vehicle.

Version 0.1
- Initial release
