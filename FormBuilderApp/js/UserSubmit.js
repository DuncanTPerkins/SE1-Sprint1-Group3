var $fieldForm = $('form[name=genform]');
var $submitBtn = $('#completed');
var $draftBtn = $('#later');
var $formID = $('#FormID');
$fieldForm.on('submit', function (event) {
    event.preventDefault();
});
function submit(e) {
    var url = '/user/fillout'
    var json = JSON.stringify($fieldForm.serializeArray());
    console.log(json);
    var token = $('[name=__RequestVerificationToken]').val();
    var values = [$formID.val(), json];
    $.post(url, { __RequestVerificationToken: token, jsonData: values }, function (data) {
        if (data.error) {
            alert('Error saving form. Try again later.');
        } else {
            alert('Form saved successfully!');
        }
    })
}

$submitBtn.on('click', submit);
