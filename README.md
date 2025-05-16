# TravelEase
Database Systems final project files. Incomplete README.

## Traveler

### sign up form & login
- enter all info AND inserts in table so be careful
- age is now DOB
- validation checks for email format, password length, username uniqueness, contact number all digits [all of our ddl commands]
- preferences are concatenated and inserted comma seperated
- location is inserted like city (country) based on existing location ids -> i do plan on adding more
- in the nationality dropdown, typing the city actually brings that result to the top
- really ugly formatting im sorry guys i cant make the form any longer its not letting me and i have no space
- added features like password show/hide

### home
- displays 10 packages at a time
- i will have to figure out the pages thing
- adds to wishlist when u click wishlist (if u logged in), but you cant see the wislist seperately yet

## Hotel & Transport
### Login and Signup
- Enter required info and insertions handled in table accordingly
- Destination is dropdown: destination name, city (does autofill if you type in the first few characters but kinda wonky)
- Entry is first added to ServiceProvider table then to Hotel table
- ProviderID picked from the Service Provider and inserted automatically, HotelID assigned here
- Validation checks for everything: email, password, numRooms, AvgPrices
- If you just signed up, you can't actually login after that (admin hasn't approved you yet)

### Hotel Dashboard Features
- Service Integration Tab: Displays pending accommodation assignments (room description, capacity, price in USD) in a DataGridView that you can approve or reject.
- Service Listing Tab: Lists all hotel accommodations (room details, price in USD, destination) in a DataGridView, you can edit room Description and Price
- Booking Management Tab: Shows booking details (request ID, booking date, total cost in USD, payment status) in a DataGridView, can confirm and cancel pending confirmations (occupied rooms updated accordingly).
- Performance Reports Tab: Visualizes metrics via charts (pie for occupancy, bar for ratings, line for revenue), with a ComboBox to switch chart types.
- Exit and Logout Buttons
  
### Transport Dashboard Features
- Trip Integration Tab: Displays pending ride requests (origin, destination, price in USD) in a DataGridView that you can accept or reject.
- Trip Listing Tab: Lists all transport rides (origin, destination, price in USD, arrival time) in a DataGridView, you can edit arrival time and price.
- Booking Management Tab: Shows booking details (request ID, booking date, total cost in USD, payment status) in a DataGridView, can confirm and cancel payments (occupied seats updated accordingly).
- Performance Reports Tab: Visualizes metrics via charts (pie for seat occupancy and on-time arrivals, bar for ratings, line for revenue), with a ComboBox to switch chart types.
- Exit and Logout Buttons


# Contributers
  - <a href=https://github.com/AabiaAli>Aabia Ali</a>
  - <a href=https://github.com/insharahn>Insharah Irfan Nazir</a>
  - <a href=https://github.com/ZaraHEREhehe>Zara Noor Qazi</a>



