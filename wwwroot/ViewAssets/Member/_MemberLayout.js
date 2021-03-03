function memberOnload() {
    var navLinks = document.querySelectorAll("a.nav-link");
    navLinks.forEach(link => {
        if (link.href == window.location.origin + window.location.pathname) link.classList.add("active");
    });
}