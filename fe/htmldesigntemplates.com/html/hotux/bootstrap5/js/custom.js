// Header and Footer Loading
$(document).ready(function() {
    // Load header
    $("#header-container").load("header.html", function() {
        // Initialize any header-specific functionality after loading
    });

    // Load footer
    $("#footer-container").load("footer.html", function() {
        // Initialize any footer-specific functionality after loading
    });
});

// Back to top button functionality
$(window).scroll(function() {
    if ($(this).scrollTop() > 100) {
        $('#back-to-top').fadeIn();
    } else {
        $('#back-to-top').fadeOut();
    }
});

$('#back-to-top').click(function() {
    $('html, body').animate({scrollTop: 0}, 800);
    return false;
});

// Preloader
$(window).on('load', function() {
    $('#status').fadeOut();
    $('#preloader').delay(350).fadeOut('slow');
    $('body').delay(350).css({'overflow': 'visible'});
});

// Initialize tooltips
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

// Initialize popovers
$(function () {
    $('[data-toggle="popover"]').popover();
}); 