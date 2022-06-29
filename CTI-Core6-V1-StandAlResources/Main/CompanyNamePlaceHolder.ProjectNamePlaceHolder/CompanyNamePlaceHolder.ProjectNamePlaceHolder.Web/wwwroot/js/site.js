// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
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
            //if (mainHeader.classList.contains('navbar-light')) {
            //    mainHeader.classList.add('navbar-dark');
            //    mainHeader.classList.remove('navbar-light');
            //}
            toggleSwitch.checked = true;
        }
    }

    function switchTheme(e) {
        if (e.target.checked) {
            if (!document.body.classList.contains('dark-mode')) {
                document.body.classList.add("dark-mode");
            }
            //if (mainHeader.classList.contains('navbar-light')) {
            //    mainHeader.classList.add('navbar-dark');
            //    mainHeader.classList.remove('navbar-light');
            //}
            localStorage.setItem('theme', 'dark');
        } else {
            if (document.body.classList.contains('dark-mode')) {
                document.body.classList.remove("dark-mode");
            }
            //if (mainHeader.classList.contains('navbar-dark')) {
            //    mainHeader.classList.add('navbar-light');
            //    mainHeader.classList.remove('navbar-dark');
            //}
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

    openModal = (url, title, callback) => {
        var placeholderElement = $('#modal-placeholder');
        $.get(url).done(function (data) {
            placeholderElement.find('.modal-body').html(data);
            placeholderElement.find('.modal-title').html(title);
            placeholderElement.find('.modal').modal('show');
            callback();
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
    $.triggerPageForm = function (handler, elementContainer, form, initializeFormFunction) {
        $('body').removeClass('loaded');
        var url = '?handler=' + handler;
        $.post(url, $(form).serialize(),
            function (data) {
                $(elementContainer).html(data);
                initializeFormFunction();
                $('body').addClass('loaded');
                if ($(form).valid() == false) { }
            })
            .fail(function () { });
    }
});
