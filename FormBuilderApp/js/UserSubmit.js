function submit(e) {
    var url = '/form/fillout'
    var postData = $('#preview').html()
    var postData = html2json(postData);
    var token = $('[name=__RequestVerificationToken]').val();
    $.post(url, { __RequestVerificationToken: token, jsonData: postData }, function (data) {
        if (data.error) {
            alert('Error saving form. Try again later.');
        } else {
            alert('Form saved successfully!');
        }
    })
}

$createBtn.on('click', submit);