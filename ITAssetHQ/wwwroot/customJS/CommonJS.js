$(function () {
    $(".dateclass").datepicker({
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        minDate: new Date('01/01/1900'),
        maxDate: '+50Y'
    });


});


var isShift = false;
var seperator = "/";
window.onload = function () {
    //Reference the Table.
    var tblForm = document.getElementById("tblForm");

    //Reference all INPUT elements in the Table.
    var inputs = document.getElementsByTagName("input");

    //Loop through all INPUT elements.
    for (var i = 0; i < inputs.length; i++) {
        //Check whether the INPUT element is TextBox.
        if (inputs[i].type == "text") {
            //Check whether Date Format Validation is required.
            if (inputs[i].className.indexOf("date-format") > 0) {

                //Set Max Length.
                inputs[i].setAttribute("maxlength", 10);

                //Only allow Numeric Keys.
                inputs[i].onkeydown = function (e) {
                    return IsNumeric(this, e.keyCode);
                };

                //Validate Date as User types.
                inputs[i].onkeyup = function (e) {
                    ValidateDateFormat(this, e.keyCode);
                };
            }
        }
    }
};

function IsNumeric(input, keyCode) {
    if (keyCode == 16) {
        isShift = true;
    }
    //Allow only Numeric Keys.
    if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
        if ((input.value.length == 2 || input.value.length == 5) && keyCode != 8) {
            input.value += seperator;
        }

        return true;
    }
    else {
        return false;
    }
};

function ValidateDateFormat(input, keyCode) {
    var dateString = input.value;
    if (keyCode == 16) {
        isShift = false;
    }
    var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;

    //Check whether valid dd/MM/yyyy Date Format.
    if (regex.test(dateString) || dateString.length == 0) {
        ShowHideError(input, "none");
    } else {
        ShowHideError(input, "block");
    }
};

function ShowHideError(textbox, display) {
    var row = textbox.parentNode.parentNode;
    var errorMsg = row.getElementsByTagName("span")[0];
    if (errorMsg != null) {
        errorMsg.style.display = display;
    }
};







