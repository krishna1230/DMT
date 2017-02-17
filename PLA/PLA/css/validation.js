$(document).ready(function () {
    $("#FRM,#FRM1,#FRM2,#FRM3,#FRM4,#FRM5,#FRM6,#FRM7,#FRM8,#FRM123").submit(function () {
        var errorFlag = false;

        $('.required').each(function () {

            if ($(this).val() == '' || $(this).val() == ' ') {

                if ($(this).prev().attr('class') != 'error') {
                    $(this).before('<span class="error"></span>');
                }

                $(this).css({
                    border: '1px solid red'
                });

                errorFlag = true;
            } else {
                if ($(this).prev().attr('class') == 'error') {
                    $(this).prev().remove();
                    $(this).removeAttr('style');

                }
            }
        });
      
        if (errorFlag == true) {
            return false;
        }
        else {
            return true;
        }
        return false;
    });

    $('.StopChar').bind('keypress', function (e) {
        return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
    })
    $('.StopNumber').keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                e.preventDefault();
            }
        }
    });

  
});