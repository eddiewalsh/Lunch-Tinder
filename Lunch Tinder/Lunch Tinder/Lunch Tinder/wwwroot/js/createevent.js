function updateSelectedVenues() {
    var selectedVenues = Array.from(document.querySelectorAll("#venueTableBody td")).map(function (td) {
        return td.textContent.trim();
    });
    document.getElementById("selectedVenues").value = selectedVenues.join(",");
}


function checkEventTime() {
    let eventDateInput = document.getElementById('EventStartDate');
    let eventTimeInput = document.getElementById('EventStartTime');
    let timeValidationMessage = document.getElementById('timeValidationMessage');

    let selectedDate = new Date(eventDateInput.value);
    let currentDate = new Date();

    if (selectedDate.toDateString() === currentDate.toDateString()) {
        let currentHour = currentDate.getHours();
        let selectedHour = parseInt(eventTimeInput.value.split(':')[0]);

        if (selectedHour <= currentHour + 1) {
            timeValidationMessage.textContent = "You can't select a date and time that is less than 2 hours in the future.";
        } else {
            timeValidationMessage.textContent = "";
        }
    } else {
        timeValidationMessage.textContent = "";
    }
}

let selectedVenues = []; // Array to store selected venues

function addVenueToTable() {
    let selectedVenue = document.getElementById('restaurant').value;
    if (selectedVenue !== '') {
        let tableBody = document.getElementById('venueTableBody');
        let VotingCloseTime = document.getElementById('VotingCloseTime');
        let newRow = document.createElement('tr');
        let newCell = document.createElement('td');
        newCell.textContent = selectedVenue;
        newRow.appendChild(newCell);
        tableBody.appendChild(newRow);
        document.getElementById('restaurant').value = '';

        // Show the venue table
        let venueTable = document.getElementById('venueTable');
        venueTable.style.display = 'block';

        // Reset the dropdown menu
        let restaurantDropdown = document.getElementById('restaurant');
        restaurantDropdown.selectedIndex = 0;

        // Enable the plus button if a venue is selected
        let plusButton = document.getElementById('plusButton');
        plusButton.disabled = true;

        // Add the selected venue to the array
        selectedVenues.push(selectedVenue);

        var text = VotingCloseTime.options[VotingCloseTime.selectedIndex].text;
   
        //if the table contents are greater than 0 then enable the submit button
        if (tableBody.childElementCount > 0 && text !== 'Select a time') {
            document.getElementById("submitButton").disabled = false; // Enable the submit button
        } else {
            if (tableBody.childElementCount== 0) {
                RestaurantValidationMessage.textContent = "Invalid SELECT STUFF FROM TABLE. ";
            }
            if (text === 'Select a time') {
                VoteTimeValidationMessage.textContent = "Invalid Time, please select a Time ";
            }
                document.getElementById("submitButton").disabled = true;
            }
    }
    updateSelectedVenues();
}

function enablePlusButton() {
    let selectedVenue = document.getElementById('restaurant').value;
    let plusButton = document.getElementById('plusButton');
    plusButton.disabled = selectedVenue === '';
}

function compareEventTimeAndVotingCloseTime() {
   
    let eventDateInput = document.getElementById('EventStartDate');
    let eventTimeInput = document.getElementById('EventStartTime');
    let votingCloseDateInput = document.getElementById('VotingEndDate');
    let votingCloseTimeInput = document.getElementById('VotingCloseTime');
    let timeValidationMessage = document.getElementById('VoteTimeValidationMessage');
    let tableBody = document.getElementById('venueTableBody');

    let eventDate = new Date(eventDateInput.value);
    let eventTime = eventTimeInput.value;
    let votingCloseDate = new Date(votingCloseDateInput.value);
    let votingCloseTime = votingCloseTimeInput.value;

    let eventDateTime = new Date(eventDate.getFullYear(), eventDate.getMonth(), eventDate.getDate(), eventTime.split(':')[0], eventTime.split(':')[1]);
    let votingCloseDateTime = new Date(votingCloseDate.getFullYear(), votingCloseDate.getMonth(), votingCloseDate.getDate(), votingCloseTime.split(':')[0], votingCloseTime.split(':')[1]);

    eventDateTime.setHours(0, 0, 0, 0);
    votingCloseDateTime.setHours(0, 0, 0, 0);

    if (votingCloseDateTime > eventDateTime) {
        timeValidationMessage.textContent = "Invalid date or time. Cannot select a date after the event date.";
        document.getElementById("submitButton").disabled = true; // Disable the submit button
    } else if (votingCloseDate > eventDate) {
        timeValidationMessage.textContent = "Invalid date or time. Cannot select a voting end date that ends after the event begins.";
        document.getElementById("submitButton").disabled = true; // Disable the submit button
    } else if (eventDateTime.getTime() - new Date().getTime() < votingCloseTime) {
        timeValidationMessage.textContent = "Invalid Time. Cannot schedule the event less than 2 hours in the future.";
        document.getElementById("submitButton").disabled = true; // Disable the submit button
    } else {
        const eventTimeInput = document.getElementById('EventStartTime');
        const votingCloseTimeInput = document.getElementById('VotingCloseTime');

        const eventTimeParts = eventTimeInput.value.split(':');
        const eventTimeHours = parseInt(eventTimeParts[0]);
        const eventTimeMinutes = parseInt(eventTimeParts[1]);

        const eventTimeInMinutes = eventTimeHours * 60 + eventTimeMinutes;

        const votingCloseTimeParts = votingCloseTimeInput.value.split(':');
        const votingCloseTimeHours = parseInt(votingCloseTimeParts[0]);
        const votingCloseTimeMinutes = parseInt(votingCloseTimeParts[1]);

        const votingCloseTimeInMinutes = votingCloseTimeHours * 60 + votingCloseTimeMinutes;
        const timeDifferenceMinutes = eventTimeInMinutes - votingCloseTimeInMinutes;

        let isValidTimeDifference = timeDifferenceMinutes >= 30 && tableBody.childElementCount > 0;
        let isValidTableBody = timeDifferenceMinutes <= 30 && tableBody.childElementCount === 0;


        if (timeDifferenceMinutes >= 30) {
            timeValidationMessage.textContent = "";
        
        } else {
            timeValidationMessage.textContent = "Invalid Time. Cannot enter a time less than 30 minutes before the event time.";
        }

    }
}
function cancelEvent() {
    // Clear the input fields
    let eventDateInput = document.getElementById('EventStartDate');
    let eventTimeInput = document.getElementById('EventStartTime');
    let votingCloseDateInput = document.getElementById('VotingEndDate');
    let votingCloseTimeInput = document.getElementById('VotingCloseTime');
    let votingvalidationmessage = document.getElementById('VoteTimeValidationMessage');
    let selectedVenue = document.getElementById('restaurant');

    eventDateInput.value = '';
    eventTimeInput.value = '';
    votingCloseDateInput.value = '';
    votingCloseTimeInput.value = '';
    votingvalidationmessage.value = '';
    selectedVenue.selectedIndex = 0;

    // Clear the table
    let venueTableBody = document.getElementById('venueTableBody');
    venueTableBody.innerHTML = '';
}

function convertingDateTimeValuesToUTC(dateinputOneId, dateinputTwoId, timeInputOne, timeInputTwo) {
    var eventstartdatetime = convertToUtc(dateinputOneId, timeInputOne);
    var voteenddatetime = convertToUtc(dateinputTwoId, timeInputTwo);
    GetLocalDateTimeNowLocal();
    console.log("Values are added");
    document.getElementById("EventStartDateTime").value = eventstartdatetime;
    document.getElementById("VoteEndDateTime").value = voteenddatetime;

    console.log(document.getElementById("EventStartDateTime").value);
    console.log(document.getElementById("VoteEndDateTime").value);

}

function convertToUtc(dateInputId, timeInputId) {
    // Get the date and time input values
    var dateInput = document.getElementById(dateInputId).value;
    var timeInput = document.getElementById(timeInputId).value;

    // Split the date into day, month, and year components
    var dateComponents = dateInput.split("-");
    var year = parseInt(dateComponents[0], 10);
    var month = parseInt(dateComponents[1], 10) - 1; // Months in JavaScript are zero-based
    var day = parseInt(dateComponents[2], 10);

    // Split the time into hour and minute components
    var timeComponents = timeInput.split(":");
    var hour = parseInt(timeComponents[0], 10);
    var minute = parseInt(timeComponents[1], 10);

    // Perform basic validation
    if (
        isNaN(day) ||
        isNaN(month) ||
        isNaN(year) ||
        isNaN(hour) ||
        isNaN(minute)
    ) {
        console.log("Invalid date or time format");
        return;
    }

    // Create a new JavaScript Date object using the local date and time components
    var localDateTime = new Date(year, month, day, hour, minute);

    // Check if the created Date object is valid
    if (isNaN(localDateTime)) {
        console.log("Invalid date or time");
        return;
    }

    // Convert the local date and time to UTC
    var utcDateTime = localDateTime.toISOString();

    // Get the offset in minutes
    var offsetMinutes = localDateTime.getTimezoneOffset();

    // Calculate the offset in hours and minutes
    var offsetHours = Math.floor(Math.abs(offsetMinutes) / 60);
    var offsetMinutesRemainder = Math.abs(offsetMinutes) % 60;

    // Format the offset string (e.g., +03:30 or -02:00)
    var offsetString =
        (offsetMinutes >= 0 ? "-" : "+") +
        ("0" + offsetHours).slice(-2) +
        ":" +
        ("0" + offsetMinutesRemainder).slice(-2);

    // Append the offset to the UTC date and time string
    utcDateTime = utcDateTime.slice(0, -1) + offsetString;

    console.log("UTC Date Time with Offset: " + utcDateTime);


    return utcDateTime;
}


function GetLocalDateTimeNowLocal() {
    const currentDate = new Date();

    let localYear = currentDate.getFullYear();
    let localMonth = currentDate.getMonth() - 1;
    let localDay = currentDate.getDate();
    let localHour = currentDate.getHours();
    let localMinute = currentDate.getMinutes();

    var localDateTime = new Date(localYear, localMonth, localDay, localHour, localMinute);

    // Check if the created Date object is valid
    if (isNaN(localDateTime)) {
        console.log("Invalid date or time");
        return;
    }

    // Convert the local date and time to UTC
    var utcDateTime = localDateTime.toISOString();

    // Get the offset in minutes
    var offsetMinutes = localDateTime.getTimezoneOffset();

    // Calculate the offset in hours and minutes
    var offsetHours = Math.floor(Math.abs(offsetMinutes) / 60);
    var offsetMinutesRemainder = Math.abs(offsetMinutes) % 60;

    // Format the offset string (e.g., +03:30 or -02:00)
    var offsetString =
        (offsetMinutes >= 0 ? "-" : "+") +
        ("0" + offsetHours).slice(-2) +
        ":" +
        ("0" + offsetMinutesRemainder).slice(-2);

    // Append the offset to the UTC date and time string
    utcDateTime = utcDateTime.slice(0, -1) + offsetString;

    console.log("UTC Date Time with Offset: " + utcDateTime);

    document.getElementById("VotingStartTime").value = utcDateTime;
}