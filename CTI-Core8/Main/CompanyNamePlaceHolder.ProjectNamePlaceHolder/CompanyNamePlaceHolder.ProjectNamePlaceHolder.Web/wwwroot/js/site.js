// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    (function () {
        "use strict"; // Start of use strict

        var sidebar = document.querySelector('.sidebar');
        var sidebarToggles = document.querySelectorAll('#sidebarToggle, #sidebarToggleTop');

        if (sidebar) {

            var collapseEl = sidebar.querySelector('.collapse');
            var collapseElementList = [].slice.call(document.querySelectorAll('.sidebar .collapse'))
            var sidebarCollapseList = collapseElementList.map(function (collapseEl) {
                return new bootstrap.Collapse(collapseEl, { toggle: false });
            });

            for (var toggle of sidebarToggles) {

                // Toggle the side navigation
                toggle.addEventListener('click', function (e) {
                    document.body.classList.toggle('sidebar-toggled');
                    sidebar.classList.toggle('toggled');

                    if (sidebar.classList.contains('toggled')) {
                        for (var bsCollapse of sidebarCollapseList) {
                            bsCollapse.hide();
                        }
                    };
                });
            }

            // Close any open menu accordions when window is resized below 768px
            window.addEventListener('resize', function () {
                var vw = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);

                if (vw < 768) {
                    for (var bsCollapse of sidebarCollapseList) {
                        bsCollapse.hide();
                    }
                };
            });
        }

        // Prevent the content wrapper from scrolling when the fixed side navigation hovered over

        var fixedNaigation = document.querySelector('body.fixed-nav .sidebar');

        if (fixedNaigation) {
            fixedNaigation.on('mousewheel DOMMouseScroll wheel', function (e) {
                var vw = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);

                if (vw > 768) {
                    var e0 = e.originalEvent,
                        delta = e0.wheelDelta || -e0.detail;
                    this.scrollTop += (delta < 0 ? 1 : -1) * 30;
                    e.preventDefault();
                }
            });
        }

        var scrollToTop = document.querySelector('.scroll-to-top');

        if (scrollToTop) {

            // Scroll to top button appear
            window.addEventListener('scroll', function () {
                var scrollDistance = window.pageYOffset;

                //check if user is scrolling up
                if (scrollDistance > 100) {
                    scrollToTop.style.display = 'block';
                } else {
                    scrollToTop.style.display = 'none';
                }
            });
        }

    })(); // End of use strict

    $('#logoutLink').click(function () {
        $('#logoutForm').submit();
    });

    var toggleSwitch = $('#themeSwitch')[0];
    var mainHeader = $('.main-header')[0];
    var currentTheme = localStorage.getItem('theme');

    if (currentTheme) {
        if (currentTheme === 'dark') {
            if (!document.body.classList.contains('dark-mode')) {
                document.body.classList.add("dark-mode");
            }           
            toggleSwitch.checked = true;
        }
    }

    function switchTheme(e) {
        if (e.target.checked) {
            if (!document.body.classList.contains('dark-mode')) {
                document.body.classList.add("dark-mode");
            }            
            localStorage.setItem('theme', 'dark');
        } else {
            if (document.body.classList.contains('dark-mode')) {
                document.body.classList.remove("dark-mode");
            }           
            localStorage.setItem('theme', 'light');
        }
    }

    toggleSwitch.addEventListener('change', switchTheme, false);

    $('[data-toggle="tooltip"]').tooltip();

    $(".select2").select2({
        placeholder: "Select one",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });

    $.fn.ajaxSelect = function (options) {
        var settings = $.extend(true, {
            placeholder: "Select one",
            allowClear: true,
            ajax: {
                delay: 250,
            }
        }, options);
        this.filter("select").each(function () {
            $(this).select2(settings);
        });
        return this;
    };

    $.fn.onChangeClear = function (targetSelect) {
        $(this).on('select2:select', function (e) {
            $(targetSelect).val(null).trigger('change');
            $(targetSelect).trigger('select2:unselect');
        });
        $(this).on('select2:unselect', function (e) {
            $(targetSelect).val(null).trigger('change');
            $(targetSelect).trigger('select2:unselect');
        });
    };

    $.fn.onChangeToggle = function (targetSelect) {
        $(this).on('select2:select', function (e) {
            $(targetSelect).prop("disabled", false);
            $(targetSelect).val(null).trigger('change');
            $(targetSelect).trigger('select2:unselect');
        });
        $(this).on('select2:unselect', function (e) {
            $(targetSelect).prop("disabled", true);
            $(targetSelect).val(null).trigger('change');
            $(targetSelect).trigger('select2:unselect');
        });
    };

    openFormModal = (url, title, callback) => {
        var placeholderElement = $('#modal-placeholder');
        $.get(url).done(function (data) {
            placeholderElement.find('.modal-body').html(data);
            placeholderElement.find('.modal-title').html(title);
            placeholderElement.find('.modal').modal('show');
            placeholderElement
                .off('click')
                .on('click', '[data-save="modal"]', function (event) {
                    var form = $('#modal-placeholder').find('form');
                    $.validator.unobtrusive.parse(form);
                    form.validate().form();
                    if (form.valid()) {
                        var actionUrl = form.prop('action');
                        var dataToSend = new FormData(form.get(0));
                        $.ajax({
                            url: actionUrl,
                            method: 'post',
                            data: dataToSend,
                            processData: false,
                            contentType: false
                        }).done(function (data) {
                            $('#modal-placeholder').find('.modal-body').html(data);
                            var shouldClose = $(data).find('[name="ShouldClose"]').val() == 'True';
                            if (shouldClose) {
                                $('#modal-placeholder').find('.modal').modal('hide');
                                callback();
                            }
                        });
                    }
                });
        });
    }
    openModal = (modalId, url, title, callback) => {
        var placeholderElement = $('#' + modalId);
        $.get(url).done(function (data) {
            placeholderElement.find('.modal-body').html(data);
            placeholderElement.find('.modal-title').html(title);
            placeholderElement.find('.modal').modal('show');
            // Event handler for the close button
            placeholderElement.find('.modal-footer .btnClose').click(function () {
                placeholderElement.find('.modal').modal('hide');
            });

            // Event handler for the 'x' button
            placeholderElement.find('.modal-header .close').click(function () {
                placeholderElement.find('.modal').modal('hide');
            });
            if (callback != null) { callback(); }
        });
    }
	setTimeout(function () {
        $('body').addClass('loaded');
    }, 200);
	
	$.initializeFormAction = function (action, handler, triggerElements, elementContainer, form, initializeFormFunction) {
        var triggerElementString = "";
        for (let i = 0; i < triggerElements.length; i++) {
            if (triggerElementString != "") { triggerElementString += ", "; }
            triggerElementString += triggerElements[i];
        }
        $(triggerElementString).bind(action, function () {
            $.triggerPageForm(handler, elementContainer, form, initializeFormFunction);
        });
    }
  
	$.triggerPageForm = function (handler, elementContainer, form, initializeFormFunction, skipValidation) {
        $('body').removeClass('loaded');
        var url = '?handler=' + handler;
        $.post(url, $(form).serialize(),
            function (data) {
                $(elementContainer).html(data);
                initializeFormFunction();
                $('body').addClass('loaded');
                if (!skipValidation) {     
                    if ($(form).valid() == false) { }
                }              
            })
            .fail(function () { });
    }
	
	$.showAjaxLoaderOnClick = function (triggerElements)
    {      
        var triggerElementString = "";
        for (let i = 0; i < triggerElements.length; i++) {
            if (triggerElementString != "") { triggerElementString += ", "; }
            triggerElementString += triggerElements[i];
        }
        $(triggerElementString).bind('click', function () {
            $('body').removeClass('loaded');
        });
    }
    $('.level-one-nav').click(function () {      
        if ($(this).hasClass("menu-open") == true) {
            $(this).removeClass('menu-open');
            $(this).find('.level-one-nav-icon').removeClass('fa-angle-down');
            $(this).find('.level-one-nav-icon').addClass('fa-angle-left');
        }
        else {
            $(this).addClass('menu-open');
            $(this).find('.level-one-nav-icon').removeClass('fa-angle-left');
            $(this).find('.level-one-nav-icon').addClass('fa-angle-down');
        }
    });
    $(".level-one-nav").map(function () {       
        if ($(this).hasClass("menu-open") == true) {
            $(this).addClass('menu-open');
            $(this).find('.level-one-nav-icon').removeClass('fa-angle-left');
            $(this).find('.level-one-nav-icon').addClass('fa-angle-down');
           
        }
        else {
            $(this).removeClass('menu-open');
            $(this).find('.level-one-nav-icon').removeClass('fa-angle-down');
            $(this).find('.level-one-nav-icon').addClass('fa-angle-left');
        }
    });
    function GetElementTopPosition(elem) {
        var top = 0;

        do {
            top += elem.offsetTop - elem.scrollTop;
        } while (elem = elem.offsetParent);

        return top;
    }
    if ($('#toolbar-container').length)
    {
        var toolbarTopPosition = GetElementTopPosition(document.getElementById('toolbar-container'));
        RefreshToolBarPosition();
        $(window).scroll(function () {
            RefreshToolBarPosition();
        });
        $(window).resize(function () {
            RefreshToolBarPosition();
        });
        function RefreshToolBarPosition() {
            if ($(window).height() < (toolbarTopPosition - $(document).scrollTop() + 110)) {
                $('#toolbar-container').css({ "position": "fixed", "bottom": "20px", "right": "75px" });
            }
            else {
                $('#toolbar-container').css({ "position": "relative", "bottom": "0px", "right": "0px" });
            }
        }
    }
	function confirmSubmit(transactionName, formElement, buttonElement, promptMessage) {
        // Create the modal
        const name = $(buttonElement).attr("name");
        const value = $(buttonElement).attr("value");
        const href = $(buttonElement).attr("href");
        var modal = $('<div id="modal-' + transactionName + '-placeholder">' +
            '<div class="modal" tabindex="-1" role="dialog">' +
            '<div class="modal-dialog" role="document">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<h5 class="modal-title">Confirmation</h5>' +
            '<button type="button" class="close btnCloseModalIcon" data-dismiss="modal" aria-label="Close" id="btnCloseIcon' + transactionName + '">' +
            '<span aria-hidden="true">&times;</span>' +
            '</button>' +
            '</div>' +
            '<div class="modal-body">' +
            '<p>' + (promptMessage != null ? promptMessage : 'Are you sure you want to ' + transactionName + ' the record?') + '</p>' +
            '</div>' +
            '<div class="modal-footer">' +
            (href != null ? '<a href="' + href + '" class="btn btn-primary" title="Yes">Yes</a>' : '<button type="submit" class="btn btn-primary" name="' + name + '" value="' + value + '">Yes</button>') +
            '<button type="button" class="btn btn-secondary" id="btnCloseButton' + transactionName + '">No</button>' +        
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>');

        // Attach the modal to the body and display it
        modal.appendTo($(formElement)).modal();

        // When the modal is hidden, remove it from the DOM
        modal.on('hidden.bs.modal', function () {
            modal.remove();
        });

        $('#btnCloseButton' + transactionName + ', #btnCloseIcon' + transactionName).on('click', function () {
            var placeholderElement = $('#modal-' + transactionName + '-placeholder');
            placeholderElement.find('.modal').modal('hide');
        });

        // Return false to prevent the form from submitting automatically
        return false;
    }
	$.bindCancelConfirmationModal = function (transactionName, buttonElement, formElement, promptMessage = null) {
        $(buttonElement).on('click', function (event) {
            event.preventDefault();
            confirmSubmit(transactionName, formElement, buttonElement, promptMessage);
            var placeholderElement = $('#modal-' + transactionName + '-placeholder');
            placeholderElement.find('.modal').modal('show');            
        });
    }
    $.bindSaveConfirmationModal = function (transactionName, buttonElement, formElement, promptMessage = null) {
        $(buttonElement).on('click', function () {
            var elements = document.querySelectorAll('[data-attribute-datatype="amount"]');
            for (let i = 0; i < elements.length; i++) {            
                elements[i].value = elements[i].value.replace(/,/g, '');               
            }
            if ($(formElement).valid()) {
                confirmSubmit(transactionName, formElement, buttonElement, promptMessage);
                var placeholderElement = $('#modal-' + transactionName + '-placeholder');
                placeholderElement.find('.modal').modal('show');
            }
        });        
    }
    function confirmWithRemarksSubmit(transactionName, formElement, buttonElement, remarksNameAttribute, promptMessage) {
        // Create the modal
        const name = $(buttonElement).attr("name");
        const value = $(buttonElement).attr("value");
        const href = $(buttonElement).attr("href");
        var modal = $('<div id="modal-' + transactionName + '-placeholder">' +
            '<div class="modal" tabindex="-1" role="dialog">' +
            '<div class="modal-dialog" role="document">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<h5 class="modal-title">Confirmation</h5>' +
            '<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="background:transparent;border:none;" id="btnCloseIcon' + transactionName + '">' +
            '<span aria-hidden="true">&times;</span>' +
            '</button>' +
            '</div>' +
            '<div class="modal-body">' +
            '<p>' + (promptMessage != null ? promptMessage : 'Are you sure you want to ' + transactionName + ' the record?') + '</p>' +
            '<div class="col">' +
            '<div class="mb-3">' +
            '<label class="form-label">Remarks :</strong></label>' +
            '<textarea name="' + remarksNameAttribute + '" class="form-control"></textarea>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="modal-footer">' +
            (href != null ? '<a href="' + href + '" class="btn btn-primary" title="Yes">Yes</a>' : '<button type="submit" class="btn btn-primary" name="' + name + '" value="' + value + '">Yes</button>') +
            '<button type="button" class="btn btn-secondary" id="btnCloseButton' + transactionName + '">No</button>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>');

        // Attach the modal to the body and display it
        modal.appendTo($(formElement)).modal();

        // When the modal is hidden, remove it from the DOM
        modal.on('hidden.bs.modal', function () {
            modal.remove();
        });

        $('#btnCloseButton' + transactionName + ', #btnCloseIcon' + transactionName).on('click', function () {
            var placeholderElement = $('#modal-' + transactionName + '-placeholder');
            placeholderElement.find('.modal').modal('hide');
        });

        // Return false to prevent the form from submitting automatically
        return false;
    }
    $.bindSaveWithRemarksConfirmationModal = function (transactionName, buttonElement, formElement, remarksNameAttribute, promptMessage = null) {
        $(buttonElement).on('click', function () {
            var elements = document.querySelectorAll('[data-attribute-datatype="amount"]');
            for (let i = 0; i < elements.length; i++) {
                elements[i].value = elements[i].value.replace(/,/g, '');
            }
            if ($(formElement).valid()) {
                confirmWithRemarksSubmit(transactionName, formElement, buttonElement, remarksNameAttribute, promptMessage);
                var placeholderElement = $('#modal-' + transactionName + '-placeholder');
                placeholderElement.find('.modal').modal('show');
            }
        });
    }
});
