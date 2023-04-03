    $('.bc').each(function() {
        $(this).css('background-color', $(this).data('color'));
    });
    $('.fc').each(function() {
        $(this).css('color', $(this).data('color'));
    });
    document.execCommand('styleWithCSS',true,null);
    document.execCommand('enableObjectResizing',false,"");
    $('#b').click(function(){
        document.execCommand('bold',false,true);
    });
    $('#i').click(function(){
        document.execCommand('italic',false,true);
    });
    $('#u').click(function(){
        document.execCommand('underline',false,true);
    });
    $('#sub').click(function(){
        document.execCommand('subscript',false,true);
    });
    $('#sup').click(function(){
        document.execCommand('superscript',false,true);
    });
    $('#a_left').click(function(){
        document.execCommand('justifyLeft',false,true);
    });
    $('#a_center').click(function(){
        document.execCommand('justifyCenter',false,true);
    });
    $('#a_right').click(function(){
        document.execCommand('justifyRight',false,true);
    });
    $('#a_justify').click(function(){
        document.execCommand('justifyFull',false,true);
    });    
    $('#h1').click(function(){
        document.execCommand('formatBlock',false,'<h1>');
    });
    $('#h2').click(function(){
        document.execCommand('formatBlock',false,'<h2>');
    });
    $('#h3').click(function(){
        document.execCommand('formatBlock',false,'<h3>');
    });
    $('#h4').click(function(){
        document.execCommand('formatBlock',false,'<h4>');
    });
    $('#h5').click(function(){
        document.execCommand('formatBlock',false,'<h5>');
    });
    $('#h6').click(function(){
        document.execCommand('formatBlock',false,'<h6>');
    });
    $('#redo').click(function(){
        document.execCommand('redo',false,true);
    });
    $('#undo').click(function(){
        document.execCommand('undo',false,true);
    });
    $('#btn_link').click(function(){
        document.execCommand('createLink',false,$('#txt_link').val());
    });
    $('#rmv_link').click(function(){
        document.execCommand('unlink',false,true);
    });
    $('#list_u').click(function(){
        document.execCommand('insertUnorderedList',false,true);
    });
    $('#list_o').click(function(){
        document.execCommand('insertOrderedList',false,true);
    });
    $('#add_img').click(function(){
        $("#file").trigger('click');
    });
    $('#clear_all_styles').click(function(){
        document.execCommand('removeFormat',false,true);
    });
    $('.bc').click(function(){
        document.execCommand('backColor',false,$(this).data('color'));
    });
    $('.fc').click(function(){
        document.execCommand('foreColor',false,$(this).data('color'));
    });
    $('.fntSize').click(function(){
        document.execCommand('fontSize',false,$(this).data('size'));
    });
    $('.fnt').click(function(){
        document.execCommand( "fontName", false, $(this).data('font') );
    });




function loadImage() {
  const preview = document.querySelector('img');
  const file = document.querySelector('input[type=file]').files[0];
  const reader = new FileReader();

  reader.addEventListener("load", function () {
    // convert image file to base64 string
    //preview.src = reader.result;
      //document.execCommand('insertImage',false,reader.result);
      $('#editor').append('<img hspace="8" vspace="8" src="' + reader.result + '">')
  }, false);

  if (file) {
    reader.readAsDataURL(file);
  }
}