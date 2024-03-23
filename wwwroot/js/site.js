// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    // Get the container element
    const container = document.getElementById("itemContainer");

    // Add event listener to each navbar item
    document.querySelectorAll(".nav-link").forEach(function (navItem) {
        navItem.addEventListener("click", function (event) {
            // Prevent default link behavior
            event.preventDefault();

            // Get the selected item type
            const itemType = this.getAttribute("data-type");

            // Show/hide rows based on the selected category
            container.querySelectorAll(".row").forEach(function (row) {
                if (row.id === itemType) {
                    row.style.display = "flex"; // Show the row
                } else {
                    row.style.display = "none"; // Hide other rows
                }
            });
        });
    });
});
