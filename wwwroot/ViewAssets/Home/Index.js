function homeOnload() {
    var adList = document.getElementsByClassName("ad-list")[0];

    if (adList.children.length > 0) adInitializer(adList);

    document.body.onresize = () => { adSlider(-3); };
    document.getElementById("adPrev").onclick = () => { adSlider(-2); };
    document.getElementById("adNext").onclick = () => { adSlider(-1); };
}

function adSlider(target) {
    var adList = document.getElementsByClassName("ad-list")[0];
    var state = adList.getAttribute("data-state");

    if (state == "0" || target == -3) {
        var current = adList.getAttribute("data-current");
        var adWidth = adList.clientWidth / adList.children.length;
        var adSpots = document.getElementsByClassName("ad-spots")[0];

        switch (target) {
            case -3:    //resize
                adList.classList.remove("ad-list-transition");
                break;
            case -2:    //previous
                current = --current < 0 ? adList.children.length - 1 : current;
                break;
            case -1:    //next
                current = ++current >= adList.children.length ? 0 : current;
                break;
            default:
                current = target;
        }

        adList.setAttribute("data-current", current);
        adList.style.left = adWidth * -current + "px";
        adList.setAttribute("data-state", "1");
        [...adSpots.children].forEach((item, index) => {
            if (index == current)
                item.classList.add("ad-spot-conspicuous");
            else
                item.classList.remove("ad-spot-conspicuous");
        });

        setTimeout(() => {
            adList.setAttribute("data-state", "0");
            adList.classList.add("ad-list-transition");
        }, 1000);
    }
}

function adInitializer(adList) {
    var ul = document.createElement("ul");

    ul.classList.add("ad-spots");

    for (i = 0; i < adList.children.length; i++) {
        var li = document.createElement("li");
        li.setAttribute("data-number", i);
        li.addEventListener("click", (event) => { adSlider(event.target.getAttribute("data-number")); });
        if (i == 0) li.classList.add("ad-spot-conspicuous");
        ul.appendChild(li);
    }

    adList.parentNode.appendChild(ul);

    adList.style.width = adList.children.length * 100 + "%";
    [...adList.children].forEach(item => { item.style.width = 100 / adList.children.length + "%"; });

    setInterval(() => {
        adSlider(-1);
    }, 3000);
}

function redirectToSearch(categoryId) {
    var urlParams = new URLSearchParams(window.location.search);

    urlParams.set("categoryId", categoryId);
    window.location.href = "/product/search?" + urlParams.toString();
}

function redirectToDetail(productId) {
    window.location.href = "/product/detail/" + productId;
}