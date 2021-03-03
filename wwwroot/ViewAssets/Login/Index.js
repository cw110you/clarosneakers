function login() {
    var xhr = new XMLHttpRequest();
    var data = { "account": document.getElementById("username").value, "password": document.getElementById("password").value };

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.responseText);
            if (response.isSuccess) {
                var urlParams = new URLSearchParams(window.location.search);
                var next = urlParams.get("next");

                if (next) {
                    urlParams.delete("next");
                    window.location.href = next + "?" + urlParams.toString();
                }
                else window.location.href = "/";
            }
            else document.getElementById("error-message").innerHTML = response.message;
        }
    };

    xhr.open("POST", "/login/login", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(data));

    return false;
}

function inheritQueryString(aTag) {
    aTag.href += "?" + new URLSearchParams(window.location.search).toString();
}