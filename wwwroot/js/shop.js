﻿document.addEventListener("DOMContentLoaded", function () {
    const container = document.getElementById("itemContainer");
    const shopSelector = document.querySelector(".shopBar");

    shopSelector.querySelectorAll(".nav-link").forEach(function (navItem) {
        navItem.addEventListener("click", function (event) {
            event.preventDefault();

            document.querySelectorAll(".nav-link").forEach(function (item) {
                item.classList.remove("active");
            });

            this.classList.add("active");

            const itemType = this.getAttribute("data-type");

            if (itemType != null) {
                if (itemType != "All") {
                    container.querySelectorAll(".itemDiv").forEach(function (row) {
                        if (row.id === itemType) {
                            row.style.display = "block";
                        } else {
                            row.style.display = "none";
                        }
                    });
                }
                else {
                    container.querySelectorAll(".itemDiv").forEach(function (row) {
                        row.style.display = "block";
                    });
                }
            }
        });
    });

    const sortHighToLow = document.getElementById("sortHighToLow");
    const sortLowToHigh = document.getElementById("sortLowToHigh");

    sortHighToLow.addEventListener("click", function () {
        var urlParams = new URLSearchParams(window.location.search);
        urlParams.set('order', 'desc');
        var newUrl = window.location.pathname + '?' + urlParams.toString();

        history.pushState({}, '', newUrl);

        // Refresh the page
        window.location.reload();
    });

    sortLowToHigh.addEventListener("click", function () {
        var urlParams = new URLSearchParams(window.location.search);
        urlParams.set('order', 'asc');
        var newUrl = window.location.pathname + '?' + urlParams.toString();

        history.pushState({}, '', newUrl);

        // Refresh the page
        window.location.reload();
    });

    var addToCartButtons = document.querySelectorAll('.add-to-cart');

    addToCartButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var itemId = button.getAttribute('data-item-id');

            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/Home/AddToCart');
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            xhr.onreadystatechange = function () {
                if (xhr.readyState === XMLHttpRequest.DONE) {
                    if (xhr.status === 200) {
                        // Handle success response if needed
                        alert('Item added to cart successfully!');
                    } else {
                        // Handle error response if needed
                        console.error('Error:', xhr.responseText);
                    }
                }
            };
            xhr.send('itemId=' + encodeURIComponent(itemId));
        });
    });
});