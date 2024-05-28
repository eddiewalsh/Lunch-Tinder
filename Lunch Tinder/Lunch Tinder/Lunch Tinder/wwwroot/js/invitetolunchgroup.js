
function InviteUserSubmission() {
    var modalheader = document.getElementById("modalheader");
    var headerText = document.getElementById("AddJobLabel");

    modalheader.style.backgroundColor = 'green';
    headerText.style.color = 'white';
    headerText.textContent = "Invitation Sent";
}

const button = document.getElementById("InviteBttn");
let input = document.getElementById("userinput");

const NameEmailValidationMessage = document.getElementById("emailusernameValidationMessage");


input.addEventListener("input", () => {
    const emailusername = input.value;

    if (emailusername.trim() === "") {
        NameEmailValidationMessage.textContent = "";
        button.disabled = true;
        return;
    }

    fetch(`/Admin/CheckUsernameEmail?emailusername=${emailusername}`)
        .then(response => response.json())
        .then(data => {
            if (data.userExists) {
                NameEmailValidationMessage.textContent = "Email / Username cannot be found";
                button.disabled = true;
            } else {
                NameEmailValidationMessage.textContent = "";
                button.disabled = false;
            }
        })
        .catch(error => {
            console.error("Error occurred while checking email:", error);
        });
});

