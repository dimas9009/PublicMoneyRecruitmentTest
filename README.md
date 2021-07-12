# About test task and refactoring
## Author:  Dmitrii Bakun

Email: dimas9009@gmail.com

## Realization
I have refactored a lot of legacy code in Booking and Rental controller, have created a multilevel hierarchy. This means that the controllers call the services where the main logic is located, the services call the repositories when it is necessary to add/get data in storage.

## An error encountered in legacy logic
Steps to reproduce:
1. Create a rental with 2 units
2. Create a booking with Start = 1.7.2021 and 7 nights
3. Create a booking with Start = 10.07.2021 and 10 nights
4. Create a booking with Start = 5.07.2021 and 10 nights
Expected result: I should success create a booking because in real life we can reserve the 1st and the 2nd bookings into the 1st unit and put the 3rd booking into the 2nd unit
Actual result: There is an Exception

## Realization feature Extra cleaning
I have moved the booking logic to special validator classes. In the future, if we need more restrictions we can just a new validator class

## Covering unit test
I have not completely covered this code with unit tests, but I am ready to implement them if it is critical to assess the quality of my work.