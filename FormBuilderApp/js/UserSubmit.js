var $fieldForm = $('form[name=genform]');
var $submitBtn = $('#completed');
var $draftBtn = $('#later');
var $formID = $('#FormID');
$fieldForm.on('submit', function (event) {
    event.preventDefault();
});
$fieldForm.on('draft', function (event) {
    event.preventDefault();
});

if (window.formValues) {
    window.formValues.forEach(function (field) {
        $('[name=' + field.name + ']').val(field.value)
    })
}


var save = function (isDraft) {
    var postData = null;
    if(isDraft)
    {
        var status = 1;
    }
    else
    {
        var status = 0;
    }

        postData = $fieldForm.html().toString();

        var url = '/user/fillout'
        var json = JSON.stringify($fieldForm.serializeArray());
        var token = $('[name=__RequestVerificationToken]').val();
        var values = [$formID.val(), json, status, postData];
        console.log(values);
        $.post(url, { __RequestVerificationToken: token, jsonData: values }, function (data) {
            if (data.error) {
                alert('Error saving form. Try again later.');
            } else {
                alert('Form saved successfully!');
            }
        })
}

//function submit(e) {
//    var url = '/user/fillout'
//    var status = 0;
//    var postData = null;
//    var json = JSON.stringify($fieldForm.serializeArray());
//    console.log(json);
//    var token = $('[name=__RequestVerificationToken]').val();
//    var values = [$formID.val(), json, status, postData];
//    console.log(values);
//    $.post(url, { __RequestVerificationToken: token, jsonData: values }, function (data) {
//        if (data.error) {
//            alert('Error saving form. Try again later.');
//        } else {
//            alert('Form saved successfully!');
//        }
//    })
//}

//function draft(e) {
//    var url = '/user/fillout'
//    var status = 1;
//    var postData = $fieldForm.html().toString();
//    var json = JSON.stringify($fieldForm.serializeArray());
//    console.log(json);
//    var token = $('[name=__RequestVerificationToken]').val();
//    var values = [$formID.val(), json, status, postData];
//    console.log(values);
//    $.post(url, { __RequestVerificationToken: token, jsonData: values }, function (data) {
//        if (data.error) {
//            alert('Error saving form. Try again later.');
//        } else {
//            alert('Form saved successfully!');
//        }
//    })
//}

//$draftBtn.on('click', draft);
//$submitBtn.on('click', submit);


$draftBtn.click(function (e) { save(true) })
$submitBtn.click(function (e) { save(false) })