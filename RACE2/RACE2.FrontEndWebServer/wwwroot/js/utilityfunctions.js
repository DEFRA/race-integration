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

window.CookieFunction = {
    acceptMessage: function (cookieString) {
        document.cookie = cookieString;
    }
};

//window.addEventListener('beforeunload', function (e) {
    //document.cookie = "username=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    //e.preventDefault();
    //e.returnValue = '';
    //const anchorElement = document.createElement('a');
    //anchorElement.href = "logout";
    //anchorElement.click();
    //anchorElement.remove();
//});
