//-----------------------------------------------------------------------------+
// jQuery call AJAX Page Method                                                |
//-----------------------------------------------------------------------------+
function PageMethod(fn, paramArray, successFn, errorFn) {
    //var pagePath = 'http://www.bioappsystem.com/bioappmalay/WebService.asmx';
    var pagePath = 'http://localhost/bioappmalay/WebService.asmx';
    //var pagePath = window.location.pathname; //only applicable for page in webapplication
    //-------------------------------------------------------------------------+
    // Create list of parameters in the form:                                  |
    // {"paramName1":"paramValue1","paramName2":"paramValue2"}                 |
    //-------------------------------------------------------------------------+
    var paramList = '';

    if (paramArray.length > 0) {
        for (var i = 0; i < paramArray.length; i += 2) {
            if (paramList.length > 0) paramList += ',';
            paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
        }
    }
    paramList = '{' + paramList + '}';
    //Call the page method
    $.ajax({
        type: "POST",
        url: pagePath + "/" + fn,
        contentType: "application/json; charset=utf-8",
        data: paramList,
        dataType: "json",
        timeout: 600000,
        success: successFn,
        error: errorFn
    });
}

function PageMethod(fn, paramArray, successFn, errorFn, asyncFn) {
    //var pagePath = 'http://www.bioappsystem.com/bioappmalay/WebService.asmx';
    var pagePath = 'http://localhost/bioappmalay/WebService.asmx';
    //var pagePath = window.location.pathname; //only applicable for page in webapplication
    //-------------------------------------------------------------------------+
    // Create list of parameters in the form:                                  |
    // {"paramName1":"paramValue1","paramName2":"paramValue2"}                 |
    //-------------------------------------------------------------------------+
    var paramList = '';

    if (paramArray.length > 0) {
        for (var i = 0; i < paramArray.length; i += 2) {
            if (paramList.length > 0) paramList += ',';
            paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
        }
    }
    paramList = '{' + paramList + '}';
    //Call the page method
    $.ajax({
        type: "POST",
        url: pagePath + "/" + fn,
        contentType: "application/json; charset=utf-8",
        data: paramList,
        dataType: "json",
        success: successFn,
        timeout: 600000,
        error: errorFn,
        async: asyncFn
    });
}
