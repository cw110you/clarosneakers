function comparePW() {
    if (document.getElementById("password").value == document.getElementById("password-confirm").value) signup();
    else document.getElementById("error-message").innerHTML = "兩次密碼輸入必須相同";
    return false;
}

function signup() {
    var xhr = new XMLHttpRequest();
    var data = { "account": document.getElementById("username").value, "password": document.getElementById("password").value, "passwordConfirm": document.getElementById("password-confirm").value };

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.responseText);
            if (response.isSuccess) login();
            else document.getElementById("error-message").innerHTML = response.message;
        }
    };

    xhr.open("POST", "/login/signup", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(data));
}