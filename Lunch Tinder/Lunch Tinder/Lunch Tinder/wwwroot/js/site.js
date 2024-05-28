// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
///makes the Error and sucess messages dissapear
//$(document).ready(function () {
//    // Hide the error and success messages after 5 seconds
//    setTimeout(function () {
//        $('.error-message, .success-message').fadeOut();
//    }, 3000);
//});

function validateForm() {
    var emailInput = document.getElementById("email").value;
    var submittedEmails = []; // Keep track of submitted emails

    // Fetch the previously submitted emails (you will need to implement this)
    // Example: submittedEmails = getSubmittedEmails();

    // Check if the email has already been submitted
    if (submittedEmails.includes(emailInput)) {
        alert("This email has already been submitted.");
        return false; // Prevent form submission
    }

    // Add the current email to the submitted emails list (you will need to implement this)
    // Example: addSubmittedEmail(emailInput);
    submittedEmails.push(emailInput);

    // Disable the submit button to prevent multiple submissions
    document.getElementById("submitButton").disabled = true;

    return true; // Allow form submission
}

function validateEmail() {
    const emailInput = document.getElementById("emailInput");
    const emailValidationMessage = document.getElementById("emailValidationMessage");
    let button = document.getElementById("RegisterBttn");

    const email = emailInput.value;

    fetch(`/User/CheckEmail?email=${email}`)
        .then(response => response.json())
        .then(data => {
            if (data.isEmailTaken) {
                emailValidationMessage.textContent = "Invalid email. Please try again.";
                button.disabled = true;
            } else {
                emailValidationMessage.textContent = "";
                button.disabled = false;
            }
        })
        .catch(error => {
            console.error("Error occurred while checking email:", error);
        });
}

function validateUsername() {
    const usernameInput = document.getElementById("usernameInput");
    const usernameValidationMessage = document.getElementById("usernameValidationMessage");
    let button = document.getElementById("RegisterBttn");

    usernameInput.addEventListener("input", () => {
        const username = usernameInput.value;

        if (username == null) {
            usernameInput.style.outline = "none";
            usernameInput.style.borderColor = "red";
            usernameValidationMessage.textContent = "Username is empty";
            usernameInput.setCustomValidity("Username is empty");
            button.disabled = true;
        }

        fetch(`/User/CheckUsername?username=${username}`)
            .then(response => response.json())
            .then(data => {
                if (data.isUsernameTaken) { 
                    usernameInput.style.outline = "none";
                    usernameInput.style.borderColor = "red";
                    usernameValidationMessage.textContent = "This username is already in use.";
                    usernameInput.setCustomValidity("Username already in use");
                    button.disabled = true;
                } else {
                    usernameInput.style.outline = "none";
                    usernameInput.style.borderColor = "green";
                    usernameValidationMessage.textContent = "";
                    usernameInput.setCustomValidity("");
                    button.disabled = false;
                }
            })
            .catch(error => {
                console.error("Error occurred while checking username:", error);
            });
    });
}

function validatePasswords() {
    document.getElementById("confirmpassword").addEventListener("blur", function () {
        let password = document.getElementById("passwordInput");
        let confirmPassword = document.getElementById("confirmpassword");
        let passwordValidationMessage = document.getElementById("passwordValidationMessage");
        let button = document.getElementById("RegisterBttn");

        if (password.value !== confirmPassword.value) {
            passwordValidationMessage.textContent = "Passwords do not match";
            password.style.outline = "none";
            password.style.borderColor = "red";
            confirmPassword.style.outline = "none";
            confirmPassword.style.borderColor = "red";
            button.disabled = true;
        }
        else {
            passwordValidationMessage.textContent = ""; // Clear the validation message
            password.style.outline = "none";
            password.style.borderColor = "green";
            confirmPassword.style.outline = "none";
            confirmPassword.style.borderColor = "green";
            button.disabled = false;
        }
    });
}

function checkPasswordRequirements() {
    // Get the password input element
    let passwordInput = document.getElementById("passwordInput");
    let passwordRequirements = document.querySelector(".password-requirements");

    // Get the password requirements elements
    let requirement1 = document.getElementById("requirement1");
    let requirement2 = document.getElementById("requirement2");
    let requirement3 = document.getElementById("requirement3");
    let requirement4 = document.getElementById("requirement4");
    let requirement5 = document.getElementById("requirement5");

    // Event handler when the password input gains focus
    passwordInput.addEventListener("focus", function () {
        // Show the password requirements
        passwordRequirements.style.display = "flex";
        passwordRequirements.style.flexDirection = "column";
        passwordRequirements.style.paddingBottom = "20px";

    });

    // Event handler when the password input loses focus
    passwordInput.addEventListener("blur", function () {
        // Hide the password requirements
        passwordRequirements.style.display = "none";
    });

    // Event handler on password input keyup
    passwordInput.addEventListener("keyup", function () {
        // Get the entered password value
        var password = passwordInput.value;

        // Check if the password meets the requirements and update the requirements elements
        requirement1.style.color = /[a-z]/.test(password) ? "green" : "red";
        requirement2.style.color = /[A-Z]/.test(password) ? "green" : "red";
        requirement3.style.color = /\d/.test(password) ? "green" : "red";
        requirement4.style.color = /[^a-zA-Z0-9]/.test(password) ? "green" : "red";
        requirement5.style.color = password.length >= 6 && password.length <= 20 ? "green" : "red";
    });
}

function checkUsernameRequirements() {
    // Get the username input element
    let usernameInput = document.getElementById("usernameInput");
    let usernameRequirements = document.querySelector(".username-requirements");

    // Get the username requirements elements
    let requirement1 = document.getElementById("namerequirement1");
    let requirement4 = document.getElementById("namerequirement4");

    // Get the register button element
    let button = document.getElementById("RegisterBttn");

    // Event handler when the username input gains focus
    usernameInput.addEventListener("focus", function () {
        // Show the username requirements
        usernameRequirements.style.display = "flex";
        usernameRequirements.style.flexDirection = "column";
        usernameRequirements.style.paddingBottom = "20px";
    });

    // Event handler on username input blur
    usernameInput.addEventListener("blur", function () { 
        usernameRequirements.style.display = "none";
    });

    // Event handler on username input keyup
    usernameInput.addEventListener("keyup", function () {
        // Get the entered username value
        var username = usernameInput.value.trim(); 

        usernameInput.style.outline = "none";
        requirement1.style.color = (username.length >= 6 && username.length <= 20) ? "green" : "red";
        requirement4.style.color = /^[a-zA-Z0-9]+$/.test(username) ? "green" : "red";

        let isValidLength = (username.length >= 6 && username.length <= 20);
        let isAlphanumeric = /^[a-zA-Z0-9]+$/.test(username);

        usernameInput.style.borderColor = (isValidLength && isAlphanumeric) ? "green" : "red";
        button.disabled = !(isValidLength && isAlphanumeric);
    });
}

function clearModal() {
    // Clear the input fields in the modal
    document.getElementById('groupName').value = '';
    document.getElementById('Description').value = '';

    // Close the modal
    $('#createGroupModal').modal('hide');
}

function toggleSaveButton() {
    let descriptionTextArea = document.getElementById("groupDescription");
    let saveButton = document.getElementById("saveButton");

    descriptionTextArea.addEventListener("input", function () {
        let description = descriptionTextArea.value.trim();
        saveButton.disabled = description.length === 0;
    });
}

function convertUtcToLocal(utcDateTimeString) {
    
    var dateParts = utcDateTimeString.split(' ')[0].split('/');
    var timeParts = utcDateTimeString.split(' ')[1].split(':');

    var day = parseInt(dateParts[1]);
    var month = parseInt(dateParts[0]) - 1; // Months in JavaScript are zero-based
    var year = parseInt(dateParts[2]);
    var hours = parseInt(timeParts[0]);
    var minutes = parseInt(timeParts[1]);
    var meridian = utcDateTimeString.substring(utcDateTimeString.length - 2); // Handle cases without AM/PM

    // Convert 12-hour format to 24-hour format
    if (meridian === 'PM' && hours !== 12) {
        hours += 12;
    } else if (meridian === 'AM' && hours === 12) {
        hours = 12;
    }

    var convertedDateTime = new Date(Date.UTC(year, month, day, hours, minutes));

    var options = {
        year: '2-digit',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        hour12: true,
        timeZone: 'Europe/Dublin'
    };

    var localdateTime = convertedDateTime.toLocaleString('en-IE',options);

    return localdateTime;
}