CREATE DATABASE TravelEase;
Use TravelEase

CREATE TABLE Admin (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(15) NOT NULL CHECK (LEN(Password) >= 8),
    Email VARCHAR(100) UNIQUE NOT NULL CHECK (Email LIKE '%_@__%.__%' AND (NOT (Email LIKE '%[ ,;]%')))
);

CREATE TABLE Traveler (
    TravelerID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Contact VARCHAR(15) NOT NULL CHECK (Contact NOT LIKE '%[^0-9]%'),   -- need to add check on these or not?
    Email VARCHAR(100) UNIQUE NOT NULL CHECK (Email LIKE '%_@__%.__%' AND (NOT (Email LIKE '%[ ,;]%'))),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(15) NOT NULL CHECK (LEN(Password) >= 8),
    Nationality INT,
    Age INT CHECK (Age >= 0),
    RegistrationDate DATE NOT NULL,
    Preferences VARCHAR(255),
    FOREIGN KEY (Nationality) REFERENCES Location(LocationID)
);

CREATE TABLE Operator (
    OperatorID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL CHECK (Email LIKE '%_@__%.__%' AND (NOT (Email LIKE '%[ ,;]%'))),
	ContactNo VARCHAR(15) NOT NULL CHECK (ContactNo NOT LIKE '%[^0-9]%'),     --------------------------------------
    Password VARCHAR(255) NOT NULL CHECK (LEN(Password) >= 8),
    RegistrationDate DATE NOT NULL,
    OpStatus VARCHAR(20) CHECK (OpStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL
);

CREATE TABLE ServiceProvider (
    ProviderId INT IDENTITY(1,1) PRIMARY KEY,
    ProviderName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL CHECK (Email LIKE '%_@__%.__%' AND (NOT (Email LIKE '%[ ,;]%'))),
    Password VARCHAR(15) NOT NULL CHECK (LEN(Password) >= 8),
    RegistrationDate DATE NOT NULL,
    SpStatus VARCHAR(20) CHECK (SpStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL,
    DestinationID INT NOT NULL,
	ProviderType INT NOT NULL CHECK (ProviderType IN (1, 2, 3)),
    FOREIGN KEY (DestinationID) REFERENCES Destination(DestinationID)
);

CREATE TABLE Hotel (
    HotelID INT IDENTITY(1,1) PRIMARY KEY,
    ProviderID INT UNIQUE NOT NULL,
    TotalRooms INT CHECK (TotalRooms >= 0),
    OccupiedRooms INT CHECK (OccupiedRooms >= 0),
    AvgPrice MONEY CHECK (AvgPrice >= 0),
    HotelType VARCHAR(50) CHECK (HotelType IN ('Resort', 'Budget', 'Luxury', 'Eco-Hotel')),
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderId),
);

CREATE TABLE Accommodation (
    AccomodationID INT IDENTITY(1,1) PRIMARY KEY,
    HotelID INT NOT NULL,
    MaxCapacity INT CHECK (MaxCapacity > 0),
    PricePerNight MONEY CHECK (PricePerNight >= 0),
    RoomDesc VARCHAR(255),
    FOREIGN KEY (HotelID) REFERENCES Hotel(HotelID)
);

CREATE TABLE TransportService (
    TransportID INT IDENTITY(1,1) PRIMARY KEY,
	ProviderID INT UNIQUE NOT NULL,
    Type VARCHAR(50) CHECK (Type IN ('Bus', 'Flight', 'Taxi', 'Shuttle', 'Bike', 'Train')),
    TotalSeats INT CHECK (TotalSeats >= 0),
    OccupiedSeats INT CHECK (OccupiedSeats >= 0 ),
	FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderId),
);

CREATE TABLE Ride (
    RideID INT IDENTITY(1,1) PRIMARY KEY,
    TransportID INT NOT NULL,
    OriginID INT NOT NULL,
    DestinationID INT NOT NULL,
    Price MONEY CHECK (Price >= 0),
    TimeRequested DATETIME,
    ArrivalTime DATETIME,
    FOREIGN KEY (TransportID) REFERENCES TransportService(TransportID),
    FOREIGN KEY (OriginID) REFERENCES Location(LocationID),
    FOREIGN KEY (DestinationID) REFERENCES Destination(DestinationID)
);

CREATE TABLE Guide (
    GuideID INT IDENTITY(1,1) PRIMARY KEY,
    ProviderID INT UNIQUE NOT NULL,
    SignLanguageFlag BIT NOT NULL,
    Price MONEY CHECK (Price >= 0),
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderId)
);

CREATE TABLE Activity (
    ActivityID INT IDENTITY(1,1) PRIMARY KEY,
    ActivityName VARCHAR(100) CHECK (ActivityName IN('Hiking', 'Luxury Tours', 'Cultural Tours','Cruise', 'Shopping', 'Museum Visit', 'Historical Site Tour', 'Food Street', 'Local Market Visit' )),
    GuideID INT NOT NULL,
    StartTime DateTime,
    EndTime DateTime,
    CapacityLimit INT CHECK (CapacityLimit > 0),
    Price MONEY CHECK (Price >= 0),
    FOREIGN KEY (GuideID) REFERENCES Guide(GuideID),
);

CREATE TABLE Accessibility (
    ProviderID INT NOT NULL,
    AccessibilityName VARCHAR(50) CHECK (AccessibilityName IN ('Wheelchair Availability', 'Wheelchair Access', 'Gluten Free Meals', 'Vegan Meals', 'Nut-Free Meals', 'Sign Language')) NOT NULL,
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderId),
	PRIMARY KEY (ProviderID, AccessibilityName)
);

CREATE TABLE Wishlist (
    TravelerID INT NOT NULL,
    PackageID INT NOT NULL,
    AddedDate DATETIME,
    PRIMARY KEY (TravelerID, PackageID),
    FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID),
    FOREIGN KEY (PackageID) REFERENCES Package(PackageID)
);

CREATE TABLE Location (
    CityName VARCHAR(100) NOT NULL,
    CountryName VARCHAR(100)  NOT NULL,
    LocationID INT IDENTITY(1,1),
    PRIMARY KEY (LocationID)
);

CREATE TABLE Destination (
    DestinationID INT IDENTITY(1,1) PRIMARY KEY,
    LocationID INT NOT NULL FOREIGN KEY REFERENCES Location(LocationID),
    Name VARCHAR(100),
    Description TEXT,
    Image VARCHAR(255) -- Assuming this is a URL or image path

);

CREATE TABLE Package (
    PackageID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Description TEXT,
    DestinationID INT NOT NULL,
    AccommodationID INT,
    AccommodationStatusFlag BIT NOT NULL,
    RideID INT,
    RideStatusFlag BIT NOT NULL,
    ActivityStatusFlag BIT NOT NULL,
    TripType VARCHAR(50) CHECK (TripType IN ('Leisure', 'Adventure', 'Education', 'Business', 'Spiritual/Religious', 'Cultural', 'Other')),
    CapacityType VARCHAR(20) CHECK (CapacityType IN ('Solo', 'Group')) NOT NULL Default 'Solo',
    GroupSize INT CHECK (GroupSize >= 1) default 1,
    Duration INT CHECK (Duration > 0), -- no. of days
    BasePrice Money CHECK (BasePrice >= 0),
    SustainabilityScore INT CHECK (SustainabilityScore >= 0 AND SustainabilityScore <=5),
    FOREIGN KEY (DestinationID) REFERENCES Destination(DestinationID),
    FOREIGN KEY (AccommodationID) REFERENCES Accommodation(AccomodationID),
    FOREIGN KEY (RideID) REFERENCES Ride(RideID),
);

CREATE TABLE PackageActivities (
    PackageID INT NOT NULL,
    ActivityID INT NOT NULL,
    FOREIGN KEY (PackageID) REFERENCES Package(PackageID),
    FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID),
    PRIMARY KEY (PackageID, ActivityID)
);

CREATE TABLE CustomTrip (
    CustomTripID INT IDENTITY(1,1) PRIMARY KEY,
    TravelerID INT NOT NULL,
    AccommodationID INT,
    AccommodationStatusFlag BIT NOT NULL,
    ActivitiesStatusFlag BIT NOT NULL,
    TripType VARCHAR(50) CHECK (TripType IN ('Leisure', 'Adventure', 'Education', 'Business', 'Spiritual/Religious', 'Cultural', 'Other')),
    CapacityType VARCHAR(20) CHECK (CapacityType IN ('Solo', 'Group')) NOT NULL DEFAULT 'Solo',
    GroupSize INT CHECK (GroupSize >= 1) DEFAULT 1,
    Duration INT CHECK (Duration > 0),  -- no. of days
    BasePrice DECIMAL(10,2) CHECK (BasePrice >= 0),
    SustainabilityScore INT CHECK(SustainabilityScore >= 0 and SustainabilityScore <= 5 ),
    DestinationID INT,
    RideID INT,
    RideStatusFlag BIT NOT NULL,
    FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID),
    FOREIGN KEY (AccommodationID) REFERENCES Accommodation(AccomodationID),
    FOREIGN KEY (DestinationID) REFERENCES Destination(DestinationID),
    FOREIGN KEY (RideID) REFERENCES Ride(RideID)
);

CREATE TABLE CustomTripActivities (
    CustomTripID INT NOT NULL,
    ActivityID INT NOT NULL,
    FOREIGN KEY (CustomTripID) REFERENCES CustomTrip(CustomTripID),
    FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID),
	Primary Key(CustomTripID, ActivityID)
);

CREATE TABLE Request (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    OperatorID INT NOT NULL,
    TripSourceType VARCHAR(20) CHECK (TripSourceType IN ('Package', 'Custom')) NOT NULL,
    PackageID INT,
    CustomTripID INT,
    DateRequested DATETIME NOT NULL,
    RequestStatus VARCHAR(20) CHECK (RequestStatus IN ('Pending', 'Accepted', 'Rejected')) NOT NULL,
    PreferredStartDate DATE,
    CONSTRAINT CK_OneTripSource CHECK ((PackageID IS NOT NULL AND CustomTripID IS NULL) OR (PackageID IS NULL AND CustomTripID IS NOT NULL)),
	-- Ensures that exactly one of PackageID or CustomTripID has a value (not NULL) in every row of the Request table, but never both or neither.
    FOREIGN KEY (OperatorID) REFERENCES Operator(OperatorID),
    FOREIGN KEY (PackageID) REFERENCES Package(PackageID),
    FOREIGN KEY (CustomTripID) REFERENCES CustomTrip(CustomTripID)
);

CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    RequestID INT NOT NULL,
    TotalCost Money CHECK (TotalCost >= 0), -- Total cost (hotel rooms*price, transportation, guide, activityprice*person sum)
    BookingDate DATETIME NOT NULL,
    BookingStatus VARCHAR(20) CHECK (BookingStatus IN ('Confirmed', 'Pending', 'Rejected', 'Cancelled')) NOT NULL,
    CancellationReason VARCHAR(50) CHECK (CancellationReason IN ('High Price', 'Change of Plans', 'Poor Service / Quality', 'Technical Issues', 'Found a Better Deal', 'Schedule Conflict', 'Insufficient Information', 'Other', NULL)),   
    FOREIGN KEY (RequestID) REFERENCES Request(RequestID),
);

CREATE TABLE Payment (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentMethod VARCHAR(50) CHECK (PaymentMethod IN ('Credit Card', 'PayPal', 'Bank Transfer', 'Online Transfer')),
    PaymentStatus VARCHAR(20) CHECK (PaymentStatus IN ('Success', 'Failed', 'Chargeback')) NOT NULL,
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);

CREATE TABLE Review (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    TravelerID INT NOT NULL,
    DestinationID INT,
    AccommodationID INT,
    OperatorID INT,
    GuideID INT,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5) NOT NULL,
    Comment VARCHAR(1000),
    ReviewDate DATETIME NOT NULL,
    ModerationStatus VARCHAR(20) CHECK (ModerationStatus IN ('Approved', 'Pending', 'Rejected')) NOT NULL,
    FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID),
    FOREIGN KEY (DestinationID) REFERENCES Destination(DestinationID),
    FOREIGN KEY (AccommodationID) REFERENCES Accommodation(AccomodationID),
    FOREIGN KEY (OperatorID) REFERENCES Operator(OperatorID),
    FOREIGN KEY (GuideID) REFERENCES Guide(GuideID)
);

CREATE TABLE DigitalPasses (
    PassID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    PassType VARCHAR(50) CHECK (PassType IN ('Hotel Voucher', 'E-Ticket', 'Activity Pass')) NOT NULL,
    IssueDate DATETIME NOT NULL,
    ExpiryDate DATETIME,
    PassStatus VARCHAR(20) CHECK (PassStatus IN ('Active', 'Used', 'Expired')) NOT NULL,
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
);




-------------------------------------------
---		Total Cost Calculations        ----
-------------------------------------------



-- 1) PACKAGE?BASED BOOKINGS
UPDATE b
SET TotalCost =
      -- accommodation
      p.Duration * a.PricePerNight
    -- + rides
    + ISNULL(
        (
          SELECT SUM(rv.Price * p.GroupSize)
          FROM   Package pa
          JOIN   Ride rv ON pa.RideID = rv.RideID
          WHERE  pa.PackageID = p.PackageID
        )
      , 0)
    -- + activities
    + ISNULL(
        (
          SELECT SUM(act.Price * p.GroupSize)
          FROM   PackageActivities pa
          JOIN   Activity act ON pa.ActivityID = act.ActivityID
          WHERE  pa.PackageID = p.PackageID
        )
      , 0)
FROM   Booking    b
JOIN   Request    r ON b.RequestID       = r.RequestID
JOIN   Package    p ON r.PackageID       = p.PackageID
JOIN   Accommodation a ON p.AccommodationID = a.AccomodationID
WHERE  r.TripSourceType = 'Package';

-- 2) CUSTOM?TRIP?BASED BOOKINGS
UPDATE b
SET TotalCost =
      -- accommodation
      ct.Duration * a.PricePerNight
    -- + rides
    + ISNULL(
        (
          SELECT SUM(rv.Price * ct.GroupSize)
          FROM   CustomTripActivities cta
          JOIN   Ride              rv ON cta.RideID = rv.RideID
          WHERE  cta.CustomTripID = ct.CustomTripID
        )
      , 0)
    -- + activities
    + ISNULL(
        (
          SELECT SUM(act.Price * ct.GroupSize)
          FROM   CustomTripActivities cta
          JOIN   Activity         act ON cta.ActivityID = act.ActivityID
          WHERE  cta.CustomTripID = ct.CustomTripID
        )
      , 0)
FROM   Booking       b
JOIN   Request       r  ON b.RequestID         = r.RequestID
JOIN   CustomTrip    ct ON r.CustomTripID      = ct.CustomTripID
JOIN   Accommodation a  ON ct.AccommodationID = a.AccomodationID
WHERE  r.TripSourceType = 'Custom';

SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'CustomTripActivities';



----------------------------------------------------------------------
-- Adding Constraints for Foreign Keys : On delete set null / cascade 
----------------------------------------------------------------------

select * from Accessibility

SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Accessibility';

ALTER TABLE Accessibility
DROP CONSTRAINT FK__Accessibi__Provi__01142BA1; 

ALTER TABLE Accessibility
ADD CONSTRAINT FK__Accessibi__Provi__01142BA2
FOREIGN KEY (ProviderID)
REFERENCES ServiceProvider(ProviderID)
ON DELETE CASCADE; 


select * from Accommodation
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Accommodation';

ALTER TABLE Accommodation
DROP CONSTRAINT FK__Accommoda__Hotel__02084FDA; 

ALTER TABLE Accommodation
ADD CONSTRAINT FK__Accommoda__Hotel__02084FDB
FOREIGN KEY (HotelID)
REFERENCES Hotel(HotelID)
ON DELETE CASCADE; 


select * from Activity
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Activity';

ALTER TABLE Activity
DROP CONSTRAINT FK__Activity__GuideI__02FC7413; 

ALTER TABLE Activity
ADD CONSTRAINT FK__Activity__GuideI__02FC7414
FOREIGN KEY (GuideID)
REFERENCES Guide(GuideID)
ON DELETE CASCADE; 

SELECT * FROM Booking

SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Booking';

ALTER TABLE Booking
DROP CONSTRAINT FK__Booking__Request__03F0984C; 

ALTER TABLE Booking
ADD CONSTRAINT FK__Booking__Request__03F0984C
FOREIGN KEY (RequestID)
REFERENCES Request(RequestID)
ON DELETE CASCADE; 

SELECT * FROM CustomTrip

SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'CustomTrip';

--DESTINATION
ALTER TABLE CustomTrip
DROP CONSTRAINT FK_CustomTrip_Destination; 
ALTER TABLE CustomTrip
ADD CONSTRAINT FK_CustomTrip_Destination1
FOREIGN KEY (DestinationID)
REFERENCES Destination(DestinationID)
ON DELETE CASCADE; 
-- ACCOMMODATION
ALTER TABLE CustomTrip
DROP CONSTRAINT FK__CustomTri__Accom__05D8E0BE; 
ALTER TABLE CustomTrip
ADD CONSTRAINT FK__CustomTri__Accom__05D8E0BF
FOREIGN KEY (AccommodationID)
REFERENCES Accommodation(AccomodationID)
ON DELETE SET NULL; 
-- RIDE
ALTER TABLE CustomTrip
DROP CONSTRAINT FK_CustomTrip_Ride; 
ALTER TABLE CustomTrip
ADD CONSTRAINT FK_CustomTrip_Ride1
FOREIGN KEY (RideID)
REFERENCES Ride(RideID)
ON DELETE SET NULL; 
--TRAVELER
ALTER TABLE CustomTrip
DROP CONSTRAINT FK__CustomTri__Trave__04E4BC85; 
ALTER TABLE CustomTrip
ADD CONSTRAINT FK__CustomTri__Trave__04E4BC86
FOREIGN KEY (TravelerID)
REFERENCES Traveler(TravelerID)
ON DELETE CASCADE; 

SELECT * FROM CustomTripActivities

SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'CustomTripActivities';

-- ACTIVITY
ALTER TABLE CustomTripActivities
DROP CONSTRAINT FK__CustomTri__Activ__08B54D69; 
ALTER TABLE CustomTripActivities
ADD CONSTRAINT FK__CustomTri__Activ__08B54D6A
FOREIGN KEY (ActivityID)
REFERENCES Activity(ActivityID)
ON DELETE CASCADE; 
-- CUSTOM TRIP ID
ALTER TABLE CustomTripActivities
DROP CONSTRAINT FK__CustomTri__Custo__09A971A2; 
ALTER TABLE CustomTripActivities
ADD CONSTRAINT FK__CustomTri__Custo__09A971A3
FOREIGN KEY (CustomTripID)
REFERENCES CustomTrip(CustomTripID)
ON DELETE CASCADE;

SELECT * FROM Destination
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Destination';
ALTER TABLE Destination
DROP CONSTRAINT FK__Destinati__Locat__0A9D95DB; 
ALTER TABLE Destination
ADD CONSTRAINT FK__Destinati__Locat__0A9D95DC
FOREIGN KEY (LocationID)
REFERENCES Location(LocationID)
ON DELETE CASCADE;


SELECT * FROM DigitalPasses
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'DigitalPasses';
ALTER TABLE DigitalPasses
DROP CONSTRAINT FK__DigitalPa__Booki__0B91BA14; 
ALTER TABLE DigitalPasses
ADD CONSTRAINT FK__DigitalPa__Booki__0B91BA15
FOREIGN KEY (BookingID)
REFERENCES Booking(BookingID)
ON DELETE CASCADE;

SELECT * FROM Guide
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Guide';
ALTER TABLE Guide
DROP CONSTRAINT FK__Guide__ProviderI__0C85DE4D; 
ALTER TABLE Guide
ADD CONSTRAINT FK__Guide__ProviderI__0C85DE4E
FOREIGN KEY (ProviderID)
REFERENCES ServiceProvider(ProviderID)
ON DELETE CASCADE;

SELECT * FROM Hotel
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Hotel';
ALTER TABLE Hotel
DROP CONSTRAINT FK__Hotel__ProviderI__0D7A0286; 
ALTER TABLE Hotel
ADD CONSTRAINT FK__Hotel__ProviderI__0D7A0287
FOREIGN KEY (ProviderID)
REFERENCES ServiceProvider(ProviderID)
ON DELETE CASCADE;

SELECT * FROM Package
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Package';
-- accommodation
ALTER TABLE Package
DROP CONSTRAINT FK__Package__Accommo__0F624AF8; 
ALTER TABLE Package
ADD CONSTRAINT FK__Package__Accommo__0F624AF9
FOREIGN KEY (AccommodationID)
REFERENCES Accommodation(AccomodationID)
ON DELETE CASCADE;
--destination
ALTER TABLE Package
DROP CONSTRAINT FK__Package__Destina__0E6E26BF; 
ALTER TABLE Package
ADD CONSTRAINT FK__Package__Destina__0E6E26BE
FOREIGN KEY (DestinationID)
REFERENCES Destination(DestinationID)
ON DELETE CASCADE;
--ride
ALTER TABLE Package
DROP CONSTRAINT FK__Package__RideID__10566F31; 
ALTER TABLE Package
ADD CONSTRAINT FK__Package__RideID__10566F32
FOREIGN KEY (RideID)
REFERENCES Ride(RideID)
ON DELETE SET NULL;

SELECT * FROM Request
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Request';
--custom trip
ALTER TABLE Request
DROP CONSTRAINT FK__Request__CustomT__160F4887; 
ALTER TABLE Request
ADD CONSTRAINT FK__Request__CustomT__160F4888
FOREIGN KEY (CustomTripID)
REFERENCES CustomTrip(CustomTripID)
ON DELETE CASCADE;
--operator
ALTER TABLE Request
DROP CONSTRAINT FK__Request__Operato__14270015; 
ALTER TABLE Request
ADD CONSTRAINT FK__Request__Operato__14270016
FOREIGN KEY (Operatorid)
REFERENCES Operator(OperatorID)
ON DELETE CASCADE;
--package
ALTER TABLE Request
DROP CONSTRAINT FK__Request__Package__151B244E; 
ALTER TABLE Request
ADD CONSTRAINT FK__Request__Package__151B244F
FOREIGN KEY (Package)
REFERENCES Package(PackageID)
ON DELETE CASCADE;

SELECT * FROM Review
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Review';
-- SETTING EVERYTHING TO NULL SO THE DELETION OF ONE DOESNT DELETE EVERYONES REVIEW
-- EXCEPT FOR DESTINATION + TRAVELER, IF THAT DELETES THEN REVIEW DELETES
--accommodation
ALTER TABLE Review
DROP CONSTRAINT FK__Review__Accommod__18EBB532; 
ALTER TABLE Review
ADD CONSTRAINT FK__Review__Accommod__18EBB533
FOREIGN KEY (AccommodationID)
REFERENCES Accommodation(AccomodationID)
ON DELETE SET NULL;
--destination
ALTER TABLE Review
DROP CONSTRAINT FK__Review__Destinat__17F790F9; 
ALTER TABLE Review
ADD CONSTRAINT FK__Review__Destinat__17F790FA
FOREIGN KEY (DestinationID)
REFERENCES Destination(DestinationID)
ON DELETE CASCADE;
--guide
ALTER TABLE Review
DROP CONSTRAINT FK__Review__GuideID__1AD3FDA4; 
ALTER TABLE Review
ADD CONSTRAINT FK__Review__GuideID__1AD3FDA5
FOREIGN KEY (GuideID)
REFERENCES Guide(GuideID)
ON DELETE NO ACTION; -- DOESNT WORK WITH SET NULL?
--operator
ALTER TABLE Review
DROP CONSTRAINT FK__Review__Operator__19DFD96B; 
ALTER TABLE Review
ADD CONSTRAINT FK__Review__Operator__19DFD96C
FOREIGN KEY (OperatorID)
REFERENCES Operator(OperatorID)
ON DELETE SET NULL;
--traveler
ALTER TABLE Review
DROP CONSTRAINT FK__Review__Traveler__17036CC0; 
ALTER TABLE Review
ADD CONSTRAINT FK__Review__Traveler__17036CC1
FOREIGN KEY (CustomTripID)
REFERENCES CustomTrip(CustomTripID)
ON DELETE CASCADE;

SELECT * FROM Ride
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Ride';
--destination
ALTER TABLE Ride
DROP CONSTRAINT FK__Ride__Destinatio__1DB06A4F; 

---------------
--------------
-----------
ALTER TABLE Ride
ADD CONSTRAINT FK__Ride__Destinatio__1DB06A50
FOREIGN KEY (DestinationID)
REFERENCES Destination(DestinationID)
ON DELETE NO ACTION;
--------------
--------------
--------------

--LOCATION
ALTER TABLE Ride
DROP CONSTRAINT FK__Ride__OriginID__1CBC4616; 
ALTER TABLE Ride
ADD CONSTRAINT FK__Ride__OriginID__1CBC4616
FOREIGN KEY (OriginID)
REFERENCES Location(LocationID)
ON DELETE CASCADE;
--transport
ALTER TABLE Ride
DROP CONSTRAINT FK__Ride__TransportI__1BC821DD; 
ALTER TABLE Ride
ADD CONSTRAINT FK__Ride__TransportI__1BC821DE
FOREIGN KEY (TransportID)
REFERENCES TransportService(TransportID)
ON DELETE CASCADE;



SELECT * FROM ServiceProvider
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'ServiceProvider';
--destination
--------------------
------------------
--------------------------
ALTER TABLE ServiceProvider
DROP CONSTRAINT FK__ServicePr__Desti__1EA48E88; 
ALTER TABLE ServiceProvider
ADD CONSTRAINT FK__ServicePr__Desti__1EA48E89
FOREIGN KEY (DestinationID)
REFERENCES Destination(DestinationID)
ON DELETE NO ACTION;
-----------
--------------------
------------


SELECT * FROM TransportService
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'TransportService';
ALTER TABLE TransportService
DROP CONSTRAINT FK__Transport__Provi__1F98B2C1; 
ALTER TABLE TransportService
ADD CONSTRAINT FK__Transport__Provi__1F98B2C1
FOREIGN KEY (ProviderID)
REFERENCES ServiceProvider(ProviderID)
ON DELETE CASCADE;


SELECT * FROM Traveler
SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Traveler';
ALTER TABLE Traveler
DROP CONSTRAINT FK__Traveler__Nation__208CD6FA; 
ALTER TABLE Traveler
ADD CONSTRAINT FK__Traveler__Nation__208CD6FB
FOREIGN KEY (Nationality)
REFERENCES Location(LocationID)
ON DELETE SET NULL; --NOT VERY IMPORTANT SO WE SET NULL

SELECT
    fk.name AS constraint_name,
    tp.name AS parent_table,
    cp.name AS parent_column,
    tr.name AS referenced_table,
    cr.name AS referenced_column
FROM 
    sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND cp.object_id = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND cr.object_id = tr.object_id
WHERE tp.name = 'Wishlist';
-- PACKAGE
ALTER TABLE Wishlist
DROP CONSTRAINT FK__Wishlist__Packag__22751F6C; 
ALTER TABLE Wishlist
ADD CONSTRAINT FK__Wishlist__Packag__22751F6D
FOREIGN KEY (PackageID)
REFERENCES Package(PackageID)
ON DELETE CASCADE;
-- TRAVELER
ALTER TABLE Wishlist
DROP CONSTRAINT FK__Wishlist__Travel__2180FB33; 
ALTER TABLE Wishlist
ADD CONSTRAINT FK__Wishlist__Travel__2180FB34
FOREIGN KEY (TravelerID)
REFERENCES Traveler(TravelerID)
ON DELETE CASCADE;