function comparePW() {
    if (document.getElementById("newPassword").value == document.getElementById("verify").value) alterProfile();
    else {
        var message = document.getElementById("message");
        message.style.color = "#eb7373";
        message.innerHTML = "兩次密碼輸入必須相同";
        setTimeout(() => { document.getElementById("message").innerHTML = ""; }, 3000);
    }

    return false;
}

function alterProfile() {
    var xhr = new XMLHttpRequest();
    var data = {
        "currentPassword": document.getElementById("currentPassword").value,
        "newPassword": document.getElementById("newPassword").value,
        "verify": document.getElementById("verify").value
    };

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.responseText);
            var message = document.getElementById("message");

            response.result ? message.style.color = "#52ca58" : message.style.color = "#eb7373";
            message.innerHTML = response.message;
            setTimeout(() => { document.getElementById("message").innerHTML = ""; }, 3000);

            document.getElementById("currentPassword").value = document.getElementById("newPassword").value = document.getElementById("verify").value = "";
        }
    };

    xhr.open("POST", "/member/changepassword", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(data));
}