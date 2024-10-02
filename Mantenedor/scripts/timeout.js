<!--

var tStart;

var timerID;

var session_timeout=60; //5 minutos

var url_loginlogout;

function startSessionTimeoutTimer()
{
	tStart = new Date();

	timerID = setTimeout("checkSessionTimeout()", 1000);
}

function checkSessionTimeout()
{
	if( timerID )
		clearTimeout(timerID);

	if(!tStart)
		tStart = new Date();

	var tDate = new Date();
	var tDiff = tDate.getTime() - tStart.getTime();

	tDate.setTime(tDiff);

	var hh = '0' + tDate.getMinutes() + '';
	var nn = '0' + tDate.getSeconds() + '';

	self.status = hh.substring(hh.length - 2, hh.length) + ':' + nn.substring(nn.length - 2, nn.length) + ' (elapsed time: ' + ((tDate.getMinutes() * 60) + tDate.getSeconds()) + ' seconds ; session timeout: ' + session_timeout + ' seconds)';

	if( ((tDate.getMinutes() * 60) + tDate.getSeconds()) >= session_timeout )
	{
		document.forms[0].submit();
		//window.close(this);
		return false;
	}

	timerID = setTimeout("checkSessionTimeout()", 1000);
}

//-->
