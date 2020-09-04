# Software Requirements

## Vision

Scorcher is an Android mobile application that will allow users to rate how Spicy dishes are from restaraunts and
view the overall ratings by dish or by restaraunt. Currently there is no application that allows users to
find local restaraunts that fit their Spicyness needs. Scorcher allows users rate dishes based off of how Spicy the restaraunt
claims it is compared to how Spicy it actually is.

## Scope

### In
* User friendly in the UI/UX Design
* The Android app will show the user restaraunts within a 10 mile radius of their location
* The android app will allow users input on the Spicyness of individual dishes.
* The Android App will display overall ratings of dishes and restaraunts
* The Android app will store searches in the user cache to improve loading times

### Out
* The Android app will not be overloaded with ads for monetization
* The application will not be available on IOS
* Will not allow users the ability to rate anything outside of the Spicyness ex: customer service, food quality etc
* The Android app will not have an admin dashboard

## Minimal Viable product
* Able to browse restaurants and dishes, including the overall rating
* Able to rate the relative spiciness of a dish

## Stretch Goals
* Able to give a description of your rating, and other users are able to browse those ratings with descriptions
* Able to register and log in
* Able to change the radius of the search with a sliding bar
* Users able to put in a request for dish and/or restaurant ratings, so that other users can see those requests and fulfill them
Map display of restaurants
* Able to search restaurants in other zip codes, or by restaurant name
* Refactor from using Xamarin Forms Local Database to making calls to an API with a Azure hosted DB
* Web front-end

## Functional Requirements

* A user can rate different dishes
* A user can view restaraunts within a 10 mile radius
* A user can view overall rating for dishes and restaraunts

### Data Flow

User downloads application and then opens application. They will then be directed to the home page that will display restaraunts.
The user will then be able to click on the restaraunt and be redirected to the restaraunts page. This page will display all of the
restaraunt information including but not limited to Name, Address, Overall rating, phone number, list of dishes, dish name and
dish rating. The user will be able to review and submit their rating on the dish. The user will be able to click on the dish and
be redirected to the dish page. this page will display name, restaraunt name, overall rating, price, and all reviews.

## Non Functional Requirements

### Performance
* Uses server-side rendering to show the user a completed app page upon load. This is accomplished through use of xaml
and storing recent searches into the database. It will provide a more professional experience for the user by not showing a 
flash of incomplete content upon load and increase the speed of which it loads after each search.

### Usability
* Users will have a fluid flow from page to page that is responsive. Will have faster load times after the initial search.
Everything will be very clearly display in its purpose and functionality to increase readability and usability.
