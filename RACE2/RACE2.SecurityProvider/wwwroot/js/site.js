function getButtonText(btnId)
{
    var button = document.getElementById(btnId);
    var text = button.innerHTML;
    var password = document.getElementById('govGatePasswordConfirm');
    if (text == "Show") {
        button.innerHTML = "Hide";
        password.setAttribute("type", "text");
    }
    else {
        button.innerHTML = "Show";
        password.setAttribute("type", "password");
    }
}

