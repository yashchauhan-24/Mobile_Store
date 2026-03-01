// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Mobile_Store Frontend JavaScript

document.addEventListener('DOMContentLoaded', function() {
    // Auto-dismiss alerts after 5 seconds
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        setTimeout(() => {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });

    // Add to cart animation feedback
    const addToCartButtons = document.querySelectorAll('button[type="submit"]');
    addToCartButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            const icon = this.querySelector('i.fa-cart-plus');
            if (icon) {
                icon.classList.add('fa-bounce');
                setTimeout(() => {
                    icon.classList.remove('fa-bounce');
                }, 500);
            }
        });
    });

    // Smooth scroll for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
});
