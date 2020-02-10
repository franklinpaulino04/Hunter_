$.noConflict();

jQuery(document).ready(function($) {

	"use strict";

	[].slice.call( document.querySelectorAll( 'select.cs-select' ) ).forEach( function(el) {
		new SelectFx(el);
	});

	jQuery('.selectpicker').selectpicker;


	

	$('.search-trigger').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').addClass('open');
	});

	$('.search-close').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').removeClass('open');
	});

	$('.equal-height').matchHeight({
		property: 'max-height'
	});

	// var chartsheight = $('.flotRealtime2').height();
	// $('.traffic-chart').css('height', chartsheight-122);


	// Counter Number
	$('.count').each(function () {
		$(this).prop('Counter',0).animate({
			Counter: $(this).text()
		}, {
			duration: 3000,
			easing: 'swing',
			step: function (now) {
				$(this).text(Math.ceil(now));
			}
		});
	});


	 
	 
	// Menu Trigger
	$('#menuToggle').on('click', function(event) {
		var windowWidth = $(window).width();   		 
		if (windowWidth<1010) { 
			$('body').removeClass('open'); 
			if (windowWidth<760){ 
				$('#left-panel').slideToggle(); 
			} else {
				$('#left-panel').toggleClass('open-menu');  
			} 
		} else {
			$('body').toggleClass('open');
			$('#left-panel').removeClass('open-menu');  
		} 
			 
	}); 

	 
	$(".menu-item-has-children.dropdown").each(function() {
		$(this).on('click', function() {
			var $temp_text = $(this).children('.dropdown-toggle').html();
			$(this).children('.sub-menu').prepend('<li class="subtitle">' + $temp_text + '</li>'); 
		});
	});


	// Load Resize 
	$(window).on("load resize", function(event) { 
		var windowWidth = $(window).width();  		 
		if (windowWidth<1010) {
			$('body').addClass('small-device'); 
		} else {
			$('body').removeClass('small-device');  
		} 
		
	});
  
    $(document).ready(function () {
        if ($('.date').length > 0) { $('.date').daterangepicker({ singleDatePicker: true, showDropdowns: true, locale: { format: 'YYYY-MM-DD' } }); }

        $(document).on('click', '.modal_trigger_delete', function (e) {
            e.preventDefault();

            var thisSelector = $(this).data(),
                url = thisSelector.url,
                title = 'Atencion',
                text = 'Esta Seguro que Desea Eliminar este Documento?',
                type = 'warning',
                confirmButtonColor = '#3085d6',
                cancelButtonColor = '#d33',
                confirmButtonText = 'OK',
                cancelButtonText = 'Cancelar';

            swal({
                title: title,
                text: text,
                type: type,
                showCancelButton: true,
                confirmButtonColor: confirmButtonColor,
                cancelButtonColor: cancelButtonColor,
                confirmButtonText: confirmButtonText,
                cancelButtonText: cancelButtonText,
            }).then((result) => {
                if (result.value) {
                    $.ajax({ type: 'get', url: url }).done(function (data) {

                        if (data.response.result == true) {
                            //$(table).DataTable().ajax.reload();
                            location.reload();
                        }
                    });
                }
            });
        });

        $('[data-toggle="tooltip"]').tooltip();

        $(document).on('click', '#submit', function (e) {
            e.preventDefault();

            var selector = $(this).data(),
                form = $('#form'),
                url = form.attr('action'),
                data = form.serialize();
            var formData = new FormData(form[0]);

            $.ajax({ type: 'post', url: url, data: formData, contentType: false, mimeType: "multipart/form-data", processData: false }).done(function (response) {
                
                if (response.result == 1) {
                    window.Location = selector.url;
                } else {
                    $('.response').html(response.msg);
                }

            });
        });
    });
});