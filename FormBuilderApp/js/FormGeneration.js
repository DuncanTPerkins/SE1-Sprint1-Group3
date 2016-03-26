var formData = {
    name: null,
    fields: []
}

//Search through the DOM for any elements that match the
//provided selector and create a new jQuery object that 
//references each element
var $preview = $('#preview');
var $fieldForm = $('form[name=builder]')
var $typeInput = $('#typeInput');
var $nameInput = $('#nameInput');
var $requiredInput = $('#requiredInput');
var $placeholderInput = $('#placeholderInput');

// Template Functions
var formTmpl = _.template($('#formTmpl').text());
var inputTmpl = _.template($('#inputTmpl').text());
var emailTmpl = _.template($('#emailTmpl').text());
var passwordTmpl = _.template($('#passwordTmpl').text());
var phoneTmpl = _.template($('#phoneTmpl').text());
var textAreaTmpl = _.template($('#textAreaTmpl').text());
var checkboxTmpl = _.template($('#checkboxTmpl').text());
var radioTmpl = _.template($('#radioTmpl').text());
var dropdownTmpl = _.template($('#dropdownTmpl').text());

// Field Builder submit handler
$fieldForm.on('submit', function (event) {
    event.preventDefault();

    var fieldType = $typeInput.val();
    var fieldName = $nameInput.val();
    var isRequired = $requiredInput.prop('checked');
    var placeholder = $placeholderInput.val();

    formData.fields.push({
        type: fieldType,
        name: fieldName,
        required: isRequired,
        placeholder: placeholder
    })

    renderFormPreview();
});

function renderFormPreview() {
    var $formContent = $(formTmpl(formData));
    var $formFields = $formContent.find('#fields');
    formData.fields.forEach(function (field) {
        $formFields.append(renderField(field));
    })
    $preview.html($formContent);
}

function renderField(fieldData) {
    switch (fieldData.type) {
        case 'input':
            return inputTmpl(fieldData)
        case 'email':
            return emailTmpl(fieldData)
        case 'password':
            return passwordTmpl(fieldData)
        case 'tel':
            return phoneTmpl(fieldData)
        case 'textarea':
            return textAreaTmpl(fieldData)
        case 'checkbox':
            return checkboxTmpl(fieldData)
        case 'radio':
            return radioTmpl(fieldData)
        case 'select':
            return dropdownTmpl(fieldData)
        default:
            return;
    }
}