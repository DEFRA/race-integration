window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
};

//window.CookieFunction = {
//    acceptMessage: function (cookieString) {
//        document.cookie = cookieString;
//    }
//};

//window.addEventListener('beforeunload', function (e) {
    //document.cookie = "username=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    //e.preventDefault();
    //e.returnValue = '';
    //const anchorElement = document.createElement('a');
    //anchorElement.href = "logout";
    //anchorElement.click();
    //anchorElement.remove();
//});

function timeOutCall(dotnethelper) {
    document.onmousemove = resetTimeDelay;
    document.onkeypress = resetTimeDelay;

    function resetTimeDelay() {
        dotnethelper.invokeMethodAsync("TimerInterval");
    }
}

function initializeInactivityTimer(dotnetHelper) {
    var timer;
    document.onmousemove = resetTimer();
    document.onkeypress = resetTimer();

    function resetTimer() {
        clearTimeout(timer);
        //timer = setTimeOut(logout, 5000);
        timer = setTimeout(function () {
            dotnetHelper.invokeMethodAsync("PageTimedOut");
        }, 1*60*1000); 
    }    
}
