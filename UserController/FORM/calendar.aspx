<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<META HTTP-EQUIV="expires" CONTENT="0">
		<TITLE>Calendar</TITLE>
		<style>
            td {font-family:arial;color:black; font-size:8pt; border:1pt solid gainsboro;text-align:center}
            th {font-family:arial; background-color:steelblue; color:white; font-size:8pt}
            td#today {background-color:lightsteelblue}
            select {font-family:arial; font-size:8pt;}
        </style>
		<script language="JScript">
            function doMouseOver(e){
            e.style.border = "1pt solid steelblue"
            }
            function doMouseOut(e){
            e.style.border = "1pt solid gainsboro"
            }
		</script>
		<script language="jscript">
var dateFormat, fieldValue, monthNames, currentYear, currentMonth, dayNames
////////////////////////////////////////////////////////////////////////////////////////////////////
function initialise()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	var a = 'January,February,March,April,May,June,July,Augustus,September,October,November,December';
	var b = 'Sun,Mon,Tues,Wed,Thur,Fri,Sat';
	//fieldValue = parent.document.form[0].txtDate //window.dialogArguments.fieldValue;	
	fieldValue = eval('opener.document.forms[0].<%=Request("parent")%>').value;						
	
	dateFormat = 'D/M/YYYY' //window.dialogArguments.dateFormat;
	monthNames =  a.split(',') //window.dialogArguments.monthNames.split(',')
	dayNames = b.split(',') //window.dialogArguments.dayNames.split(',')

	dialogWidth = "190px"
	dialogHeight = "185px"
	dialogTop = window.event.screenY + "px"
	dialogLeft = window.event.screenX + "px"

	header.innerHTML = createHeader()

	if (isDate(fieldValue))
	{
		currentMonth = datePart('m',fieldValue)
		currentYear = datePart('y',fieldValue)
	}
	else
	{
		/*var d = new Date()
		currentMonth = d.getMonth()+1
		currentYear = d.getFullYear()				
		*/
		currentMonth = <%=Month(CDATE(Request("sesBusinessDate")))%>;
		currentYear = <%=Year(CDATE(Request("sesBusinessDate")))%>
		//d.getFullYear()		
	}
	createCalendar()
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function finalise()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	//window.returnValue = fieldValue;
	eval('opener.document.forms[0].<%=Request("parent")%>').focus();
	eval('opener.document.forms[0].<%=Request("parent")%>').value = fieldValue;
}

selectMonth

////////////////////////////////////////////////////////////////////////////////////////////////////
function createHeader()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
		var html = "<table cellspacing=	0 bgcolor=gainsboro border=0>"
		html += "<tr>"
		html += "<td align=center><button onclick=changeMonth(-1)>&lt;</button></td>"
		html += "<td align=center colspan=5 nowrap>" + monthOptions() + yearOptions() + "</td>"
		html += "<td align=center><button onclick=changeMonth(1)>&gt;</button></td>"
		html += "</tr></table>"
		return html
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function monthOptions()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	var html = ''
	for (var i=1;i<=12;i++)
		html += "<option value=" + i + ">" + monthNames[i-1]
	return "<select id=monthSelect onchange=selectMonth(this)>" + html + "</select>"
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function yearOptions()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	var html = ''
	for (var i=1920;i<2020;i++)
		html += "<option value=" + i + ">" + i
	return "<select id=yearSelect onchange=selectYear(this)>" + html + "</select>"
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function selectDate(cell)
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	dateArray = new Array(3)

	var dateSequence = dateFormat.toLowerCase().replace(/[^ymd]/g,'')

	dateArray[dateSequence.indexOf('d')] = cell.innerText
	dateArray[dateSequence.indexOf('m')] = currentMonth
	dateArray[dateSequence.indexOf('y')] = currentYear

	var seperator = dateFormat.toLowerCase().replace(/[ymd]/g,'').substr(0,1)

	fieldValue = dateArray[0] + seperator + dateArray[1] + seperator + dateArray[2]	
	<% if Request("postback") then %>	
		//opener.document.forms[0].submit();	
	<%	end if %>
	eval('opener.document.forms[0].<%=Request("parent")%>').focus();
	
	window.close()
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function selectMonth()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	currentMonth = parseInt(monthSelect.value,10)
	createCalendar()
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function selectYear()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	currentYear = parseInt(yearSelect.value)
	createCalendar()
}

////////////////////////////////////////////////////////////////////////////////////////////////////
function changeMonth(adj)
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	currentMonth += adj
	if (currentMonth == 13)
	{
		currentMonth = 1
		currentYear += 1
	}
	if (currentMonth == 0)
	{
		currentMonth = 12
		currentYear -= 1
	}
	createCalendar()
}


////////////////////////////////////////////////////////////////////////////////////////////////////
function createCalendar()
////////////////////////////////////////////////////////////////////////////////////////////////////
{
	var month = currentMonth
	var year = currentYear
	var d = new Date()
	var totalDay = 31
	d.setDate(31);
	if (month==2) {
		if ((year%4)==0)
		{ d.setDate(29)		
		  totalDay = 29
		} else {
	  	 d.setDate(28)		
	     totalDay = 28
		}
	} 
	else {
			if (month==4 || month==6 || month== 9 || month==11)
			{	d.setDate(30);
				totalDay = 30 }
		}
	d.setYear(year)
	d.setMonth(month-1)


	var firstDay
	var monthArray = new Array(6)
	for (var i=0;i<monthArray.length;i++)
		monthArray[i] = new Array(7)
	
	for (var day=1;day<=totalDay;day++)
	{		
		d.setDate(day);
		if (day == 1)
			firstDay = d.getDay()
	  	var weekNo = parseInt(((( firstDay + (day-1)) /7 )*1))
  	
		monthArray[weekNo][d.getDay()] = d.getDate()	
	}

	var html = '<table cellspacing=0>'
	html += "<tr>"
	for (var i=0;i<dayNames.length;i++)
		html += '<th style="width:14%">' + dayNames[i] + '</th>'

	html += "</tr>"

	for (var i=0;i<monthArray.length;i++)
	{
		html += '<tr>'
		for (var j=0;j<monthArray[i].length;j++)
		{
			if (typeof(monthArray[i][j]) == 'undefined')
				html += '<td></td>'
			else
				html += "<td class=datecell onclick=selectDate(this) onmouseover=doMouseOver(this) onmouseout=doMouseOut(this)>" + monthArray[i][j] + '</td>'
		}
		html += '</tr>'
	}

	html += '</table>'

	monthSelect.value = month
	yearSelect.value = year

	calcell.innerHTML = html
}


//////////////////////////////////////////////////////////////////////////////////////////////
function isDate(strValue)
//////////////////////////////////////////////////////////////////////////////////////////////
{
	var dateSequence = dateFormat.toLowerCase().replace(/[^ymd]/g,'')

	var objRegExp = /^\d{1,4}( |-|\/|\.)\d{1,4}\1\d{1,4}$/
	if(!objRegExp.test(strValue))
		return false

	var arrayDate = strValue.split(RegExp.$1); //split date into month, day, year

	var intDay = parseInt(arrayDate[dateSequence.indexOf('d')],10);
	var intMonth = parseInt(arrayDate[dateSequence.indexOf('m')],10);
	var intYear = parseInt(arrayDate[dateSequence.indexOf('y')],10);

	if (intYear < 30)
		intYear+=2000
	if (intYear < 100)
		intYear+=1900

	var d = new Date( intYear, intMonth-1, intDay )

	if ( (d.getFullYear() != intYear) ||  (d.getMonth() != intMonth-1)  || (d.getDate() != intDay) || (intYear < 1000) )
		return false

	return true
}

//////////////////////////////////////////////////////////////////////////////////////////////
function datePart(part, strValue)
//////////////////////////////////////////////////////////////////////////////////////////////
{
	var dateSequence = dateFormat.toLowerCase().replace(/[^ymd]/g,'')

	var objRegExp = /^\d{1,4}( |-|\/|\.)\d{1,4}\1\d{1,4}$/
	objRegExp.test(strValue)
	var arrayDate = strValue.split(RegExp.$1); //split date into month, day, year

	return parseInt(arrayDate[dateSequence.indexOf(part)],10)
}


		</script>
	</head>
	<BODY onload="initialise()" onunload="finalise()" bgcolor="gainsboro">
		<center>
			<table border="0" cellspacing="0" ID="Table1">
				<tr>
					<td valign="middle" align="center" id="header">&nbsp;
					</td>
				</tr>
				<tr>
					<td valign="middle" align="center" id="calcell">&nbsp;
					</td>
				</tr>
			</table>
		</center>
	</BODY>
</html>
