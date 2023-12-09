<!-- This script and many more are available free online at -->

<!-- The JavaScript Source!! http://javascript.internet.com -->

<!-- Original:  KK Chan -->

<!-- Begin

var timerID ;



function tzone(tz, os, ds, cl)

{

	this.ct = new Date(0) ;		// datetime

	this.tz = tz ;		// code

	this.os = os ;		// GMT offset

	this.ds = ds ;		// has daylight savings

	this.cl = cl ;		// font color

}



function UpdateClocks()

{

	// www.timeanddate.com/worldclock

	var ct = new Array(

		new tzone('SFO: ', -8, 1, 'lime'),

		new tzone('TYO: ', +9, 0, 'violet'),

		new tzone('HKG: ', +8, 0, 'cyan'),

		new tzone('BKK: ', +7, 0, 'yellow'),

		new tzone('NYC: ', -5, 1, '#FFAA00'),

		new tzone('LON: ',  0, 1, 'silver'),

		new tzone('SVO: ', +3, 0, 'pink'),

		new tzone('KTM: ', +5.75, 0, 'red')

	) ;



	var dt = new Date() ;	// [GMT] time according to machine clock



	var startDST = new Date(dt.getFullYear(), 3, 1) ;

	while (startDST.getDay() != 0)

		startDST.setDate(startDST.getDate() + 1) ;



	var endDST = new Date(dt.getFullYear(), 9, 31) ;

	while (endDST.getDay() != 0)

		endDST.setDate(endDST.getDate() - 1) ;



	var ds_active ;		// DS currently active

	if (startDST < dt && dt < endDST)

		ds_active = 1 ;

	else

		ds_active = 0 ;



	// Adjust each clock offset if that clock has DS and in DS.

	for(n=0 ; n<ct.length ; n++)

		if (ct[n].ds == 1 && ds_active == 1) ct[n].os++ ;



	// compensate time zones

	gmdt = new Date() ;

	for (n=0 ; n<ct.length ; n++)

		ct[n].ct = new Date(gmdt.getTime() + ct[n].os * 3600 * 1000) ;



	document.all.Clock0.innerHTML =

		'<font color="' + ct[0].cl + '">' + ct[0].tz + ClockString(ct[0].ct) + '</font>' ;



	document.all.Clock1.innerHTML =

		'<font color="' + ct[1].cl + '">' + ct[1].tz + ClockString(ct[1].ct) + '</font>' ;



	document.all.Clock2.innerHTML =

		'<font color="' + ct[2].cl + '">' + ct[2].tz + ClockString(ct[2].ct) + '</font>' ;



	document.all.Clock3.innerHTML =

		'<font color="' + ct[3].cl + '">' + ct[3].tz + ClockString(ct[3].ct) + '</font>' ;



	document.all.Clock4.innerHTML =

		'<font color="' + ct[4].cl + '">' + ct[4].tz + ClockString(ct[4].ct) + '</font>' ;



	document.all.Clock5.innerHTML =

		'<font color="' + ct[5].cl + '">' + ct[5].tz + ClockString(ct[5].ct) + '</font>' ;



	document.all.Clock6.innerHTML =

		'<font color="' + ct[6].cl + '">' + ct[6].tz + ClockString(ct[6].ct) + '</font>' ;



	document.all.Clock7.innerHTML =

		'<font color="' + ct[7].cl + '">' + ct[7].tz + ClockString(ct[7].ct) + '</font>' ;





	timerID = window.setTimeout("UpdateClocks()", 1001) ;

}



function ClockString(dt)

{

	var stemp, ampm ;



	var dt_year = dt.getUTCFullYear() ;

	var dt_month = dt.getUTCMonth() + 1 ;

	var dt_day = dt.getUTCDate() ;

	var dt_hour = dt.getUTCHours() ;

	var dt_minute = dt.getUTCMinutes() ;

	var dt_second = dt.getUTCSeconds() ;



	dt_year = dt_year.toString() ;

	if (0 <= dt_hour && dt_hour < 12)

	{

		ampm = 'AM' ;

		if (dt_hour == 0) dt_hour = 12 ;

	} else {

		ampm = 'PM' ;

		dt_hour = dt_hour - 12 ;

		if (dt_hour == 0) dt_hour = 12 ;

	}



	if (dt_minute < 10)

		dt_minute = '0' + dt_minute ;



	if (dt_second < 10)

		dt_second = '0' + dt_second ;



	stemp = dt_month + '/' + dt_day + '/' + dt_year.substr(2,2) ;

	stemp = stemp + ' ' + dt_hour + ":" + dt_minute + ":" + dt_second + ' ' + ampm ;

	return stemp ;

}

//  End -->

function Go() {
/* original by Tim Van Goethem, tim@uophetnet.com */
nu = new Date();
hr = nu.getHours();
min = nu.getMinutes();
sec = nu.getSeconds();
ext = ".gif";
if(hr<="9"){hr = "0" + hr;} else {hr = "" + hr;}
if(min<="9"){min = "0" + min;} else {min = "" + min;}
if(sec<="9"){sec = "0" + sec;} else {sec = "" + sec;}
hrc = hr.substring(0,1);
hrd = hr.substring(1,2);
document.images.hra.src = "" + hrc + ext;
document.images.hrb.src = "" + hrd + ext;
minc = min.substring(0,1);
mind = min.substring(1,2);
document.images.mina.src = "" + minc + ext;
document.images.minb.src = "" + mind + ext;
secc = sec.substring(0,1);
secd = sec.substring(1,2);
document.images.seca.src = "" + secc + ext;
document.images.secb.src = "" + secd + ext;
setTimeout("Go()", 20);
}