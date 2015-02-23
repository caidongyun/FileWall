<?php
	$required_vars = array('OSVer', 'ErrorDate', 'DriverVer', 'ServiceVer', 'ClientVer', 'ErrorID', 'ExceptionDetails');

	//  Check all input variables.
	foreach($required_vars as $req_var)
		if(empty($_POST[$req_var]))
			die(__LINE__ . " INTERNAL ERROR: $req_var is empty");
			
	// Connect to database
	$link = mysql_connect('localhost', 'root', '');
	if (!$link)
	    die(__LINE__ . ' INTERNAL ERROR: Could not connect: ' . mysql_error());

	$OSVer 				= mysql_real_escape_string($_POST['OSVer']);
	$ErrorDate			= mysql_real_escape_string($_POST['ErrorDate']);
	$DriverVer			= mysql_real_escape_string($_POST['DriverVer']);
	$ServiceVer			= mysql_real_escape_string($_POST['ServiceVer']);
	$ClientVer			= mysql_real_escape_string($_POST['ClientVer']);
	$ErrorID			= mysql_real_escape_string($_POST['ErrorID']);
	$ExceptionDetails	= mysql_real_escape_string($_POST['ExceptionDetails']);
	$UserActions		= mysql_real_escape_string($_POST['UserActions']);
	$email				= mysql_real_escape_string($_POST['email']);

	$result = mysql_select_db('bugreports');
	if (!$result)
		die(__LINE__ . ' INTERNAL ERROR: ' . mysql_error());

	// Try to insert to reports table
	//printf("INSERT INTO reports (ErrorDate, DriverVer, ServiceVer, ClientVer, ErrorID, ExceptionDetails, UserActions, email) values ($ErrorDate, '$DriverVer', '$ServiceVer', '$ClientVer', '$ErrorID', '$ExceptionDetails', '$UserActions', '$email')");
	$result = mysql_query("INSERT INTO reports (OSVer, ErrorDate, DriverVer, ServiceVer, ClientVer, ErrorID, ExceptionDetails, UserActions, email) values ('$OSVer', '$ErrorDate', '$DriverVer', '$ServiceVer', '$ClientVer', '$ErrorID', '$ExceptionDetails', '$UserActions', '$email')");
	if (!$result)
		die(__LINE__ . ' INTERNAL ERROR: ' . mysql_error());

	$ReportID = mysql_insert_id();
		
	for ($i = 0; $i < 10; $i++)
	{
		if(!empty($_POST['Event' . $i]))
		{
			$Message = mysql_real_escape_string($_POST['Event' . $i]);
			$result = mysql_query("INSERT INTO logdetails (Message, ReportID) values ('$Message', $ReportID)");
			if (!$result)
				die(__LINE__ . ' INTERNAL ERROR: ' . mysql_error());
		}
	}
?>