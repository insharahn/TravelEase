# TravelEase - Comprehensive Travel Management Platform

## Project Overview
TravelEase is a centralized platform designed for planning, booking, and managing travel experiences. It connects travelers, tour operators, hotels, local guides, and transport providers, offering robust functionality for trip customization, booking management, resource coordination, payments, and reviews. The platform supports diverse travel types (adventure, cultural, leisure, etc.) and caters to solo travelers, groups, and more.

## Features

### 1. Traveler Interface
- **Registration and Login**: Secure sign-up with personal details and authentication.
- **Trip Search and Booking With Advanced Filters**: Filter trips by destination, date, price, activity type, and group size. Book trips or accommodations seamlessly.
- **Trip Dashboard**: View upcoming trips and itineraries by navigating paginated results with highlighted keywords and detailed trip cards (destination image, title, cost, ratings, availability, and action buttons like "Book Now" and "Save to Wishlist").
- **Custom Trip Creation**: Create custom trip packages according to personal needs with detailed transport, accommodation, and itinerary booking.
- **Digital Travel Pass**: Access e-tickets, hotel vouchers, and activity passes.
- **Reviews and Ratings**: Rate trips, accommodations, and services.
- **Profile Management**: Update preferences and view travel history.
- **Wishlist**: View saved trips and book them quickly if need be.

### 2. Tour Operator Interface
- **Operator Registration and Login**: Create and manage company profiles.
- **Trip Creation and Management**: Design, update, and delete trips with details like itineraries, pricing, inclusions, duration, and capacity.
- **Trips Listing**: View and edit all existing trips created by the operator.
- **Resource Coordination**: Assign services (hotels, guides, transport) to travelers.
- **Booking Management**: Track reservations, send reminders, and handle cancellations/refunds.
- **Performance Analytics**: View booking rates, revenue, and review summaries.

### 3. Admin Interface
- **User and Operator Management**: Approve or reject registrations for travelers and operators.
- **Tour Categories Management**: Oversee and manage tour categories.
- **Platform Analytics**: Monitor user traffic, booking trends, and revenue.
- **Review Moderation**: Filter inappropriate reviews to maintain platform integrity.

### 4. Tour Guide Interface
- **Guide Registration and Login**: Create and manage profiles with qualifications and availability using ASP.NET Identity.
- **Service Assignment**: Accept or reject guide assignments for specific trips from tour operators.
- **Schedule Management**: View and manage assigned trips, including dates, locations, and traveler details.
- **Feedback and Ratings**: Access traveler reviews and ratings specific to guide services.
- **Performance Reports**: Analyze performance metrics, such as average ratings and number of trips guided.

### 5 & 6. Hotel & Transport Interfaces
#### Login and Signup
- Enter required info and insertions handled in table accordingly
- Destination is dropdown: destination name, city (does autofill if you type in the first few characters but kinda wonky)
- Entry is first added to ServiceProvider table then to Hotel table
- ProviderID picked from the Service Provider and inserted automatically, HotelID assigned here
- Validation checks for everything: email, password, numRooms, AvgPrices
- If you just signed up, you can't actually login after that (admin hasn't approved you yet)

#### Hotel Dashboard Features
- Service Integration Tab: Displays pending accommodation assignments (room description, capacity, price in USD) in a DataGridView that you can approve or reject.
- Service Listing Tab: Lists all hotel accommodations (room details, price in USD, destination) in a DataGridView, you can edit room Description and Price
- Booking Management Tab: Shows booking details (request ID, booking date, total cost in USD, payment status) in a DataGridView, can confirm and cancel pending confirmations (occupied rooms updated accordingly).
- Performance Reports Tab: Visualizes metrics via charts (pie for occupancy, bar for ratings, line for revenue), with a ComboBox to switch chart types.
- Exit and Logout Buttons
  
#### Transport Dashboard Features
- Trip Integration Tab: Displays pending ride requests (origin, destination, price in USD) in a DataGridView that you can accept or reject.
- Trip Listing Tab: Lists all transport rides (origin, destination, price in USD, arrival time) in a DataGridView, you can edit arrival time and price.
- Booking Management Tab: Shows booking details (request ID, booking date, total cost in USD, payment status) in a DataGridView, can confirm and cancel payments (occupied seats updated accordingly).
- Performance Reports Tab: Visualizes metrics via charts (pie for seat occupancy and on-time arrivals, bar for ratings, line for revenue), with a ComboBox to switch chart types.
- Exit and Logout Buttons

## Reports
The platform includes eight comprehensive reports with visualizations (bar/pie charts) to provide insights into system performance:
1. **Trip Booking and Revenue Report**: Tracks booking trends, revenue by trip type, cancellation rates, peak periods, and average booking value.
2. **Traveler Demographics and Preferences Report**: Analyzes user demographics, preferred trip types, destinations, and spending habits.
3. **Tour Operator Performance Report**: Evaluates operators based on ratings, revenue, and response times.
4. **Service Provider Efficiency Report**: Measures hotel occupancy, guide ratings, and transport punctuality.
5. **Destination Popularity Report**: Highlights trending destinations, seasonal trends, satisfaction scores, and emerging locations.
6. **Abandoned Booking Analysis Report**: Investigates incomplete bookings, reasons for abandonment, recovery rates, and potential revenue loss.
7. **Platform Growth Report**: Monitors user registrations, active users, partnership growth, and regional expansion.
8. **Payment Transaction and Fraud Report**: Tracks payment success/failure rates and chargeback rates.

These reports have been integrated into the relevant interfaces e.g. travelers can view the Traveler Demographics and Preferences Report while admins monitor the Platform Growth Report.

All of the reports' data and information are **dynamically fetched** and hence updated upon every insertion, update, or deletion.

## Tech Stack
- **Frontend**: WinForms (.NET Framework)
- **Backend**: C# with .NET Framework
- **Database**: MS SQL Server 2019
- **Development Environment**: Visual Studio 2019 or above
- **Reporting**: RDLC reports 

# Contributers
  - <a href=https://github.com/AabiaAli>Aabia Ali</a>
  - <a href=https://github.com/insharahn>Insharah Irfan Nazir</a>
  - <a href=https://github.com/ZaraHEREhehe>Zara Noor Qazi</a>

