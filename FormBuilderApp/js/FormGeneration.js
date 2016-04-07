var formData = {
    name: null,
    fields: []
}

//Search through the DOM for any elements that match the
//provided selector and create a new jQuery object that 
//references each element
var $createBtn = $('#create');
var $preview = $('#preview');
var $fieldForm = $('form[name=builder]');
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
var optionsTmpl = _.template($('#optionsTmpl').text());
var optionField = _.template($('#optionFieldTmpl').text());

// Field Builder submit handler
$fieldForm.on('submit', function (event) {
    event.preventDefault();

    var fieldType = $typeInput.val();
    var fieldName = $nameInput.val();
    var isRequired = $requiredInput.prop('checked');
    var placeholder = $placeholderInput.val();
    var options = $.map($('.field-option'), function (el) {
        return $(el).val()
    });

    var fieldData = {
        type: fieldType,
        name: fieldName,
        required: isRequired,
        placeholder: placeholder,
        options: options
    }

    formData.fields.push(fieldData);

    console.log(formData);


    resetFieldBuilder();
    renderFormPreview();
});

function resetFieldBuilder() {
    $fieldForm[0].reset();
    $('.placeholder-container').removeClass('hidden');
    $('#field-options').empty();
}

// Listen to changes on type selection
$typeInput.on('change', function (e) {
    var selectedType = $(this).val();
    var hasPlaceholder = true;
    var hasOptions = false;

    var isSelect = selectedType === 'select';
    var isRadio = selectedType === 'radio';
    var isCheckbox = selectedType === 'checkbox';

    // Render options field in field builder if type is select
    if (isSelect || isRadio || isCheckbox) {
        hasPlaceholder = false;
        hasOptions = true;
        renderOptionsField()
    }

    $('.placeholder-container').toggleClass('hidden', !hasPlaceholder);
    if (!hasOptions) {
        $('#field-options').empty();
    }
});

function renderOptionsField() {
    $('#field-options').html(optionsTmpl())

    var $addBtn = $('#add-option');
    var $list = $('#option-list');

    // Add new option input when 'Add' button is clicked
    $addBtn.on('click', function (e) {
        var id = $('.field-option').length;
        $list.append(optionField({ id: id }))

        $('.delete-option').on('click', function (e) {
            $('.field-option-container[data-field-id=' + $(this).data('field-id') + ']').remove()
        })
    })
}

function renderFormPreview() {
    var $formContent = $(formTmpl(formData));
    var $formFields = $formContent.find('#fields');
    formData.fields.forEach(function (field) {
        $formFields.append(renderField(field));
    })
    $preview.html($formContent);
    console.log(formData);
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

function submit(e) {
    var url = '/admin/createform'
    var postData = $('#preview').html().toString();
    var json = JSON.stringify($("#myForm").serializeArray());
    var token = $('[name=__RequestVerificationToken]').val();
    var values = [$('#formnameInput').val(), $('FormID').val(), postData, json];
    $.post(url, { __RequestVerificationToken: token, jsonData: values }, function (data) {
        if (data.error) {
            alert('Error saving form. Try again later.');
        } else {
            alert('Form saved successfully!');
        }
    })
}

$createBtn.on('click', submit);
