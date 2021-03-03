document.addEventListener("DOMContentLoaded", () => {
    var a = document.querySelectorAll("[data-onload]");
    a.forEach(element => {
        eval(element.getAttribute("data-onload"));
    });
});
