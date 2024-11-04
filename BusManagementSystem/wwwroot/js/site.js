// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const errorMessage = document.querySelector(".alert-danger");
    if (errorMessage) {
        setTimeout(function () {
            errorMessage.style.display = "none";
        }, 2000); // Hide after 2 seconds
    }
});