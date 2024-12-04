# FastPoll

## Features 
* Create Polls
* Vote and polls and view the results in real-time
* Little to no commitment, User accounts are not required
* Account System
* Can log in with google too
## Layout Updates
* Added bootstrap css to make the website look cleaner
* Added sorttable.js for more functionality and sort tables 

## Bonus
* Added Search Functionality for options and polls to find entries quickly

## Unit Tests
* Create_Get_ReturnsView: Verifies that the GET Create action successfully returns the view.
* Create_ValidModel_RedirectsToIndex: Ensures a valid poll submission redirects to the Index action.
* Create_EmptyQuestion_ReturnsView: Checks that a poll with an empty question returns the Create view with an error.
* Create_MissingCreatedBy_ReturnsView: Verifies that a poll missing the CreatedBy field returns the Create view with an error.
* Create_NullPoll_ReturnsBadRequest: Confirms that submitting a null poll object returns a BadRequestResult.
* Create_FutureCreatedAt_ReturnsView: Ensures a poll with a future CreatedAt date returns the Create view with an error.
* Create_DuplicateQuestion_ReturnsViewWithError: Verifies that a duplicate question submission returns the Create view with an error.
* Create_InvalidCharactersInQuestion_ReturnsViewWithError: Checks that a poll with invalid special characters in the question returns the Create view with an error.

## Website Link
[FastPoll](https://fastpolls-d7ehaqhed0fdbdeb.canadacentral-01.azurewebsites.net/)
