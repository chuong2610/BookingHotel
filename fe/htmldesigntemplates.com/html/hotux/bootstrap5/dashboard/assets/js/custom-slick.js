

 (function($) {
     "use strict";
     $('.item-slider').slick({
         infinite: true,
         slidesToShow: 1,
         slidesToScroll: 1,
         arrows: false,
         dots: false,
         autoplay: true,
         speed: 2000,
         rows:0,
         loop:true,
     });

     $('.slider-store').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        direction: 'vertical',
        arrows: false,
        dots: false,
        fade: true,
        autoplay: true,
        asNavFor: '.slider-thumbs'
    });
   

    $('.slider-thumbs').slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        asNavFor: '.slider-store',
        dots: false,
        arrows: false,
        autoplay: true,
        direction: 'vertical',
        centerMode: true,
        focusOnSelect: true,
        responsive: [{
            breakpoint: 800,
            settings: {
                arrows:false,
            }
        }]

    });


 })(jQuery);
