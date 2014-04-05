#SQM Import Export
This library enables you to import ArmA 2 & 3 *.sqm files so that you can analyze or modify it. When you're done you can export the file again, with your changes of course.

Since I haven't had access to any scheme that explains what SQM-files can contain, my implementation has been by trial and error on a lot of files, some properties might be missing. You should get an exception in that case which would tell you what line it cannot parse, please send me an email with the error and a copy of the file that failed.

#How to use
##Import
Use the Import function on the SqmImporter to import an *.sqm file. It takes a stream to read from, any kind of Stream works just don't forget to close it after! If the importer cannot read the file an SqmContentsInvalidException-exception is thrown, check the inner exception for more details.

	var importStream = new FileStream("c:\\mission.sqm", FileMode.Open);
	var importer = new SqmImporter();
	var sqmContents = importer.Import(importStream);
	importStream.Close();

##Export
Use the Export function on the SqmExporter to export an SqmContentsBase instance. The stream it takes in can be any kind of Stream just don't forget to close it after!

	var exportStream = new FileStream("c:\\mission_out.sqm", FileMode.Create);
	var exporter = new SqmExporter();
	exporter.Export(exportStream, sqmContents);
	exportStream.Close()

#Release Notes
Version 0.1
- Initial release