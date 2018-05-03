(function ($) {
    "use strict";
    jQuery(document).ready(function () {
        
        //prevent defult
        var prevent_a = $('a.search_btn');
        prevent_a.on('click', function (event) {
            event.preventDefault();
            $("<div>")
                .append("default " + event.type + " prevented")
                .appendTo("#log");
        });
        
        //header_search_bar
        $('a.search_btn').on('click', function () {
            $(' form.search_bar').toggleClass('active');
        });
        
        //Mobile menu plugin 
        if ($.fn.slicknav) {
            $('.main_menu').slicknav({
                prependTo: '.menu_bar',
                label: ''
            });
        };
        
        //slider plugin 
        if ($.fn.owlCarousel) {
            //==welcome_area==\\
            $('.welcome_slider').owlCarousel({
                items: 1,
                loop: true,
                smartSpeed: 500,
                autoplay: true,
                mouseDrag: false,
                touchDrag: false
            });
            
            //==welcome_area animation==\\
            $(".welcome_slider").on('translate.owl.carousel', function () {
                $('.welcome_content h1').removeClass('fadeInDown animated').hide();
                $('.welcome_content span').removeClass('fadeInDown animated').hide();
                $('.welcome_content a').removeClass('fadeInDown animated').hide();
            });
            $(".welcome_slider").on('translated.owl.carousel', function () {
                $('.owl-item.active .welcome_content h1').addClass('fadeInDown animated').show();
                $('.owl-item.active .welcome_content span').addClass('fadeInDown animated').show();
                $('.owl-item.active .welcome_content a').addClass('fadeInDown animated').show();
            });
            
            //special_food_slider 
            $('.special_food_slider').owlCarousel({
                loop: true,
                margin: 30,
                nav: true,
                navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                dot: false,
                responsive: {
                    0: {
                        items: 1
                    },
                    768: {
                        items: 2
                    },
                    992: {
                        items: 4
                    }
                }
            });
            
            // Testimonial SLide 
            $('.testimonial_slider').owlCarousel({
                items: 1,
                loop: true,
                dot: false,
                autoplay: true,
                autoplaySpeed: 1000
            });
        };
        
        //Wow Animation 
        new WOW().init();
        
        //Filter Menu 
        if ($.fn.isotope) {
            $('.our_menu_tabs li').on("click", function () {
                $(".our_menu_tabs li").removeClass("active");
                $(this).addClass("active");
                var selector = $(this).attr('data-name');
                $(".isotope").isotope({
                    filter: selector,
                    animationOptions: {
                        duration: 750,
                        easing: 'linear',
                        queue: false
                    }
                });
                return false;
            });
        };
        
        //Image popup Plugin 
        if ($.fn.magnificPopup) {
            $('.single_menu a').magnificPopup({
                type: 'image',
                mainClass: 'mfp-with-zoom', // this class is for CSS animation below
                gallery: {
                    enabled: true
                },
                zoom: {
                    enabled: true, // By default it's false, so don't forget to enable it
                    duration: 300, // duration of the effect, in milliseconds
                    easing: 'ease-in-out', // CSS transition easing function
                    // The "opener" function should return the element from which popup will be zoomed in
                    // and to which popup will be scaled down
                    // By defailt it looks for an image tag:
                    opener: function (openerElement) {
                        // openerElement is the element on which popup was initialized, in this case its <a> tag
                        // you don't need to add "opener" option if this code matches your needs, it's defailt one.
                        return openerElement.is('img') ? openerElement : openerElement.find('img');
                    }
                }
            });
        };
        
        //Select form plugin
        if ($.fn.niceSelect) {
            $('select').niceSelect();
        };
        
        //Chef double slider
        if ($.fn.slick) {
            $('.chef_details_slider').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: false,
                asNavFor: '.chef_team_slider',
                infinite: true
            });
            
            $('.chef_team_slider').slick({
                slidesToShow: 4,
                slidesToScroll: 1,
                asNavFor: '.chef_details_slider',
                focusOnSelect: true,
                arrows: true,
                prevArrow: '<i class="prev fa fa-angle-left" aria-hidden="true"></i>',
                nextArrow: '<i class="next fa fa-angle-right" aria-hidden="true"></i>',
                infinite: true,
                responsive: [
                    {
                        breakpoint: 992,
                        settings: {
                            slidesToShow: 3,
                            slidesToScroll: 3,
                            infinite: true
                        }
                    },
                    {
                        breakpoint: 768,
                        settings: {
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }
                ]
            });
        };
    });
    
    jQuery(window).on("load", function () {
        
        //Active isotope in container
        if ($.fn.isotope) {
            $(".isotope").isotope({
                itemSelector: '.single_menu',
                layoutMode: 'fitRows'
            });
        };
        
        //Custom Preloader
        $('.preloader').fadeOut(300);
    });
})(jQuery);