export function download(options) {
    var bytes = options.byteArray.arrayBuffer();
    var blob = new Blob([bytes]);
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = options.fileName;
    link.click();
    link.remove();
}