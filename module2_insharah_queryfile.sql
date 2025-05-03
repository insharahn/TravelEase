
use [Project Data]


sp_help Traveler




----------------------------------------------------------------------------------
-- DOB instead of Age in traveler
ALTER TABLE Traveler ADD DateOfBirth DATE;
UPDATE Traveler
SET DateOfBirth = DATEADD(YEAR, -Age, GETDATE())
WHERE Age IS NOT NULL;
ALTER TABLE Traveler DROP CK__Traveler__Age__55009F39 --drop dependency first
ALTER TABLE Traveler DROP COLUMN Age;
----------------------------------------------------------------------------------



----------------------------------------------------------------------------------
-- new reviews table
drop table review
CREATE TABLE Review (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    TravelerID INT NOT NULL,
    AccommodationID INT NULL,
    OperatorID INT NULL,
    GuideID INT NULL,
    RideID INT NULL,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5) NOT NULL,
    Comment VARCHAR(1000),
    ReviewDate DATETIME NOT NULL,
    ModerationStatus VARCHAR(20) CHECK (ModerationStatus IN ('Approved', 'Pending', 'Rejected')) NOT NULL,
    FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID),
    FOREIGN KEY (AccommodationID) REFERENCES Accommodation(AccomodationID),
    FOREIGN KEY (OperatorID) REFERENCES Operator(OperatorID),
    FOREIGN KEY (GuideID) REFERENCES Guide(GuideID),
    FOREIGN KEY (RideID) REFERENCES Ride(RideID),
    CONSTRAINT CHK_OneTarget CHECK (
        (AccommodationID IS NOT NULL AND OperatorID IS NULL AND GuideID IS NULL AND RideID IS NULL) OR
        (AccommodationID IS NULL AND OperatorID IS NOT NULL AND GuideID IS NULL AND RideID IS NULL) OR
        (AccommodationID IS NULL AND OperatorID IS NULL AND GuideID IS NOT NULL AND RideID IS NULL) OR
        (AccommodationID IS NULL AND OperatorID IS NULL AND GuideID IS NULL AND RideID IS NOT NULL)
    )
);
INSERT INTO Review (TravelerID, AccommodationID, OperatorID, GuideID, RideID, Rating, Comment, ReviewDate, ModerationStatus) VALUES
-- Accommodation Reviews (15 rows)
(1, 1, NULL, NULL, NULL, 5, 'Excellent hotel stay, very comfortable!', '2024-01-10 09:00:00', 'Approved'),
(2, 5, NULL, NULL, NULL, 4, 'Great location, but rooms were noisy.', '2024-02-15 14:30:00', 'Approved'),
(3, 10, NULL, NULL, NULL, 3, 'Decent stay, amenities need improvement.', '2024-03-20 11:45:00', 'Pending'),
(4, 15, NULL, NULL, NULL, 5, 'Wonderful hotel experience!', '2024-04-25 16:20:00', 'Approved'),
(5, 20, NULL, NULL, NULL, 4, 'Clean and cozy, good value.', '2024-05-30 10:10:00', 'Approved'),
(6, 25, NULL, NULL, NULL, 2, 'Rooms too small for the price.', '2024-06-12 13:00:00', 'Approved'),
(7, 30, NULL, NULL, NULL, 5, 'Outstanding hotel service!', '2024-07-18 08:50:00', 'Approved'),
(8, 35, NULL, NULL, NULL, 4, 'Good stay, but food was average.', '2024-08-22 12:15:00', 'Approved'),
(9, 40, NULL, NULL, NULL, 3, 'Average experience, Wi-Fi issues.', '2024-09-10 15:30:00', 'Pending'),
(10, 45, NULL, NULL, NULL, 5, 'Luxurious hotel, highly recommend!', '2024-10-05 09:40:00', 'Approved'),
(11, 50, NULL, NULL, NULL, 4, 'Comfortable stay, attentive staff.', '2024-11-15 14:00:00', 'Approved'),
(12, 55, NULL, NULL, NULL, 3, 'Okay stay, expected better quality.', '2024-12-20 11:25:00', 'Approved'),
(13, 60, NULL, NULL, NULL, 1, 'Terrible service, staff were rude idiots!', '2025-01-08 17:10:00', 'Rejected'),
(14, 65, NULL, NULL, NULL, 5, 'Perfect hotel experience!', '2025-02-12 10:50:00', 'Approved'),
(15, 66, NULL, NULL, NULL, 4, 'Great location, friendly service.', '2025-03-25 13:30:00', 'Approved'),

-- Operator Reviews (13 rows)
(16, NULL, 1, NULL, NULL, 5, 'Fantastic tour, well-organized!', '2024-01-25 08:00:00', 'Approved'),
(17, NULL, 5, NULL, NULL, 4, 'Enjoyable tour, but a bit rushed.', '2024-02-28 15:45:00', 'Approved'),
(18, NULL, 10, NULL, NULL, 3, 'Decent tour, lacked engagement.', '2024-03-15 10:20:00', 'Pending'),
(19, NULL, 15, NULL, NULL, 5, 'Exceptional tour experience!', '2024-04-30 12:30:00', 'Approved'),
(20, NULL, 20, NULL, NULL, 4, 'Informative tour, great guide.', '2024-05-25 14:10:00', 'Approved'),
(21, NULL, 25, NULL, NULL, 2, 'Check out my website for deals!', '2024-06-20 09:50:00', 'Rejected'),
(22, NULL, 30, NULL, NULL, 5, 'Highly recommend this tour!', '2024-07-30 11:40:00', 'Approved'),
(23, NULL, 35, NULL, NULL, 4, 'Good tour, but needed better planning.', '2024-08-15 16:00:00', 'Approved'),
(24, NULL, 40, NULL, NULL, 3, 'Average tour, nothing special.', '2024-09-20 13:20:00', 'Pending'),
(25, NULL, 45, NULL, NULL, 5, 'Best tour I’ve experienced!', '2024-10-25 10:30:00', 'Approved'),
(26, NULL, 50, NULL, NULL, 4, 'Enjoyable tour, tight schedule.', '2024-11-30 12:45:00', 'Approved'),
(27, NULL, 51, NULL, NULL, 3, 'Okay tour, could be improved.', '2025-01-15 15:15:00', 'Pending'),
(28, NULL, 3, NULL, NULL, 4, 'Solid tour experience.', '2025-02-20 09:00:00', 'Approved'),

-- Guide Reviews (12 rows)
(29, NULL, NULL, 1, NULL, 5, 'Amazing guide, very knowledgeable!', '2024-02-05 09:00:00', 'Approved'),
(30, NULL, NULL, 5, NULL, 4, 'Great guide, made the tour fun.', '2024-03-10 14:30:00', 'Approved'),
(31, NULL, NULL, 10, NULL, 3, 'Guide was okay, lacked enthusiasm.', '2024-04-15 11:45:00', 'Pending'),
(32, NULL, NULL, 15, NULL, 5, 'Outstanding guide, very engaging!', '2024-05-20 16:20:00', 'Approved'),
(33, NULL, NULL, 20, NULL, 4, 'Informative guide, great experience.', '2024-06-25 10:10:00', 'Approved'),
(34, NULL, NULL, 25, NULL, 2, 'Worst guide, totally unprofessional!', '2024-07-10 13:00:00', 'Rejected'),
(35, NULL, NULL, 30, NULL, 5, 'Fantastic guide, learned a lot!', '2024-08-20 08:50:00', 'Approved'),
(36, NULL, NULL, 35, NULL, 4, 'Good guide, but tour was rushed.', '2024-09-15 12:15:00', 'Approved'),
(37, NULL, NULL, 40, NULL, 3, 'Average guide performance.', '2024-10-20 15:30:00', 'Pending'),
(38, NULL, NULL, 45, NULL, 5, 'Best guide I’ve had!', '2024-11-25 09:40:00', 'Approved'),
(39, NULL, NULL, 50, NULL, 4, 'Very knowledgeable guide.', '2024-12-30 14:00:00', 'Approved'),
(40, NULL, NULL, 3, NULL, 3, 'Guide was decent, could improve.', '2025-02-05 11:25:00', 'Pending'),

-- Ride Reviews (10 rows)
(41, NULL, NULL, NULL, 1, 5, 'Smooth and comfortable ride!', '2024-03-05 09:00:00', 'Approved'),
(42, NULL, NULL, NULL, 5, 4, 'Clean transport, friendly driver.', '2024-04-10 14:30:00', 'Approved'),
(43, NULL, NULL, NULL, 10, 3, 'Ride was okay, but delayed.', '2024-05-15 11:45:00', 'Pending'),
(44, NULL, NULL, NULL, 15, 5, 'Really fun transportation experience!', '2024-06-20 16:20:00', 'Approved'),
(45, NULL, NULL, NULL, 20, 4, 'Comfortable ride, good service.', '2024-07-25 10:10:00', 'Approved'),
(46, NULL, NULL, NULL, 25, 1, 'Horrible driver, unsafe ride!', '2024-08-30 13:00:00', 'Rejected'),
(47, NULL, NULL, NULL, 30, 5, 'Excellent transportation, on time!', '2024-09-25 08:50:00', 'Approved'),
(48, NULL, NULL, NULL, 35, 4, 'Good ride, but could be cleaner.', '2024-10-30 12:15:00', 'Approved'),
(49, NULL, NULL, NULL, 40, 3, 'Average ride experience.', '2024-12-05 15:30:00', 'Pending'),
(50, NULL, NULL, NULL, 45, 5, 'Fantastic transportation service!', '2025-01-20 09:40:00', 'Approved');
----------------------------------------------------------------------------------


-- request altered
----------------------------------------------------------------------------------
ALTER TABLE Request
ADD AccomodationPaidStatus VARCHAR(20) NOT NULL DEFAULT 'Unpaid'
CONSTRAINT CHK_PaidStatus CHECK (AccomodationPaidStatus IN ('Paid', 'Unpaid'));

ALTER TABLE Request
ADD RidePaidStatus VARCHAR(20) NOT NULL DEFAULT 'Unpaid'
CONSTRAINT CHK_RidePaidStatus CHECK (RidePaidStatus IN ('Paid', 'Unpaid'));

ALTER TABLE Request
ADD ActivityPaidStatus VARCHAR(20) NOT NULL DEFAULT 'Unpaid'
CONSTRAINT CHK_ActivityPaidStatus CHECK (ActivityPaidStatus IN ('Paid', 'Unpaid'));
----------------------------------------------------------------------------------


----------------------------------------------------------------------------------
-- ADDING TRAVELERID BECAUSE....? WE FORGOT THAT SOMEHOW?
ALTER TABLE Request
ADD TravelerID INT
FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID) ON DELETE NO ACTION; -- no action as cycles are forming w/ cascade

-- FILL DATA IN
UPDATE Request
SET TravelerID = ABS(CHECKSUM(NEWID())) % 55 + 1 --random IDS within the traveler range
WHERE TravelerID IS NULL;
----------------------------------------------------------------------------------


---- ------------------------- UPDATING BASE PRICE AND ADDING TO TOTAL COST CALCULATIONS --------------------
UPDATE CustomTrip
SET BasePrice = 1000

select * from CustomTrip


-------------------------------------------
---		Total Cost Calculations        ----
-------------------------------------------
-- UPDATED TO INCLUDE BASE PRICE

SELECT * FROM Booking

-- 1) PACKAGE BASED BOOKINGS
UPDATE b
SET TotalCost =
      p.BasePrice
    + p.Duration * ISNULL(a.PricePerNight, 0)
    + ISNULL(
        (
          -- ride price * group size
          (SELECT rv.Price
           FROM   Ride rv
           WHERE  rv.RideID = p.RideID) * p.GroupSize
        ), 0)
    + ISNULL(
        (
          -- sum of activity prices, then multiply by group size
          p.GroupSize * (
            SELECT SUM(act.Price)
            FROM   PackageActivities pa
            JOIN   Activity act ON pa.ActivityID = act.ActivityID
            WHERE  pa.PackageID = p.PackageID
          )
        ), 0)
FROM   Booking b
JOIN   Request r ON b.RequestID = r.RequestID
JOIN   Package p ON r.PackageID = p.PackageID
LEFT JOIN Accommodation a ON p.AccommodationID = a.AccomodationID
WHERE  r.TripSourceType = 'Package';


-- 2) CUSTOM?TRIP?BASED BOOKINGS
UPDATE b
SET TotalCost =
      ct.BasePrice
    + ct.Duration * ISNULL(a.PricePerNight, 0)
    + ISNULL(
        (
          -- ride price * group size
          (SELECT rv.Price
           FROM   Ride rv
           WHERE  rv.RideID = ct.RideID) * ct.GroupSize
        ), 0)
    + ISNULL(
        (
          -- sum of activity prices, then multiply by group size
          ct.GroupSize * (
            SELECT SUM(act.Price)
            FROM   CustomTripActivities cta
            JOIN   Activity act ON cta.ActivityID = act.ActivityID
            WHERE  cta.CustomTripID = ct.CustomTripID
          )
        ), 0)
FROM   Booking b
JOIN   Request r ON b.RequestID = r.RequestID
JOIN   CustomTrip ct ON r.CustomTripID = ct.CustomTripID
LEFT JOIN Accommodation a ON ct.AccommodationID = a.AccomodationID
WHERE  r.TripSourceType = 'Custom';



-----------------------------------------------------------------------------------------
-------------------------------MODIFYING BOOKING COLUMNS SO THEY R NULLABLE
ALTER TABLE Booking
ALTER COLUMN BookingDate DATETIME NULL;

ALTER TABLE Booking
ALTER COLUMN BookingStatus VARCHAR(20) NULL;
-----------------------------------------------------------------------------------------



-----------------------------------------------------------------------------------------
---------------------- TRIGGER FOR UPDATING COST WHENEVER SMTH IS INSERTED INTO BOOKINGS!
CREATE TRIGGER trg_UpdateTotalCost
ON Booking
AFTER INSERT
AS
BEGIN
    -- PACKAGE TRIPS
    UPDATE b
    SET TotalCost =
          p.BasePrice
        + p.Duration * ISNULL(a.PricePerNight, 0)
        + ISNULL((SELECT rv.Price FROM Ride rv WHERE rv.RideID = p.RideID) * p.GroupSize, 0)
        + ISNULL(
            p.GroupSize * (
                SELECT SUM(act.Price)
                FROM PackageActivities pa
                JOIN Activity act ON pa.ActivityID = act.ActivityID
                WHERE pa.PackageID = p.PackageID
            ), 0)
    FROM Booking b
    JOIN inserted i ON b.BookingID = i.BookingID
    JOIN Request r ON b.RequestID = r.RequestID
    JOIN Package p ON r.PackageID = p.PackageID
    LEFT JOIN Accommodation a ON p.AccommodationID = a.AccomodationID
    WHERE r.TripSourceType = 'Package';

    -- CUSTOM TRIPS
    UPDATE b
    SET TotalCost =
          ct.BasePrice
        + ct.Duration * ISNULL(a.PricePerNight, 0)
        + ISNULL((SELECT rv.Price FROM Ride rv WHERE rv.RideID = ct.RideID) * ct.GroupSize, 0)
        + ISNULL(
            ct.GroupSize * (
                SELECT SUM(act.Price)
                FROM CustomTripActivities cta
                JOIN Activity act ON cta.ActivityID = act.ActivityID
                WHERE cta.CustomTripID = ct.CustomTripID
            ), 0)
    FROM Booking b
    JOIN inserted i ON b.BookingID = i.BookingID
    JOIN Request r ON b.RequestID = r.RequestID
    JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
    LEFT JOIN Accommodation a ON ct.AccommodationID = a.AccomodationID
    WHERE r.TripSourceType = 'Custom';
END;
-----------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------
----------------------------------- ZARA'S QUERY FOR PACKAGES: INCLUDING OPERATOR------------------------------------
  ALTER TABLE Package
  ADD OperatorID INT NULL;

  ALTER TABLE Package
  ADD CONSTRAINT FK_Package_Operator
    FOREIGN KEY (OperatorID) REFERENCES Operator(OperatorID);

  WITH RandomPick AS (
  SELECT
    p.PackageID,
    o.OperatorID,
    ROW_NUMBER() OVER (
      PARTITION BY p.PackageID 
      ORDER BY NEWID()
    ) AS rn
  FROM dbo.Package AS p
  CROSS JOIN dbo.[Operator] AS o
)

UPDATE p
SET p.OperatorID = rp.OperatorID
FROM dbo.Package AS p
JOIN RandomPick AS rp
  ON p.PackageID = rp.PackageID
WHERE rp.rn = 1
   -- remove this line if you want to overwrite existing values
-----------------------------------------------------------------------------------------------------

----------------------------- EXTREMELY IMPORTANT----------------------------------------------------
------------------ PREF START DATE CANT BE NUKL-------------------------------------------

select * from request where PreferredStartDate IS NULL
update Request
set PreferredStartDate = '2025-08-12'
 where PreferredStartDate IS NULL
 --------------------------------------------------------------------------------------------------------
 ALTER TABLE Request
ALTER COLUMN PreferredStartDate DATE NOT NULL;


---------------------------------------------------------------------------------------
-- ADD STATUS TO TRAVELER 
ALTER TABLE Traveler
ADD TravelerStatus VARCHAR(20) NOT NULL DEFAULT 'Pending';
-- ADD CHECK CONSTRAINT
ALTER TABLE Traveler
ADD CONSTRAINT CK_Traveler_TravelerStatus
CHECK (TravelerStatus IN ('Approved', 'Pending', 'Rejected')); 
----- ASSIGN RANDOM VALUES
UPDATE Traveler
SET TravelerStatus = CASE ABS(CHECKSUM(NEWID())) % 3
    WHEN 0 THEN 'Approved'
    WHEN 1 THEN 'Pending'
    ELSE 'Rejected'
END;
---------------------------------------------------------------------------------------

----- AABIAS QUERIES CHAPAFIED

ALTER TABLE Accommodation
ADD AccommodationStatus VARCHAR(20) CHECK (AccommodationStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL DEFAULT 'Pending';

ALTER TABLE Ride
ADD RideStatus VARCHAR(20) CHECK (RideStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL DEFAULT 'Pending';

ALTER TABLE Activity
ADD ActivityStatus VARCHAR(20) CHECK (ActivityStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL DEFAULT 'Pending';


ALTER TABLE Activity
ADD CurrentParticipants INT DEFAULT 0 NOT NULL;

UPDATE a
SET a.CurrentParticipants = COALESCE(
    (SELECT SUM(DISTINCT CASE 
        WHEN p.PackageID IS NOT NULL THEN p.GroupSize
        WHEN ct.CustomTripID IS NOT NULL THEN ct.GroupSize
        ELSE 0
    END)
     FROM Request r
     LEFT JOIN Package p ON r.PackageID = p.PackageID
     LEFT JOIN PackageActivities pa ON p.PackageID = pa.PackageID AND pa.ActivityID = a.ActivityID
     LEFT JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
     LEFT JOIN CustomTripActivities cta ON ct.CustomTripID = cta.CustomTripID AND cta.ActivityID = a.ActivityID
     WHERE (pa.ActivityID IS NOT NULL OR cta.ActivityID IS NOT NULL)), 0)
FROM Activity a;

UPDATE Activity
SET CapacityLimit = CapacityLimit + 40;


-----------------------------------------------------------------------------------------
-- MY USELESS QUERIES FOR WHEN I WAS MAKING FORMS
---- EVERYTHING BELOW RTHIS IS USELESS
----------- USELESS STARTS HERE -------------------------------------------------
----------------------------------------------------------------------------------
----------------------------------------------------------------------------------
----------------------------------------------------------------------------------
select * from Traveler
select distinct preferences from traveler
select * from Location
where CountryName = 'Pakistan'

DECLARE @Keyword VARCHAR(100) = 'Paris'
DECLARE @MinBudget INT = 0
DECLARE @MaxBudget INT = 100000
DECLARE @Duration INT = 5
DECLARE @GroupSize INT = 1
DECLARE @TripType VARCHAR(100) = 'Cultural'

SELECT 
    p.*, 
    sp_accom.ProviderName AS AccommodationProvider,
    sp_transport.ProviderName AS TransportProvider,
    l.CityName,
    l.CountryName
FROM Package p
INNER JOIN Accommodation ac ON p.AccommodationID = ac.AccomodationID
INNER JOIN Hotel h ON ac.HotelID = h.HotelID
INNER JOIN ServiceProvider sp_accom ON h.ProviderID = sp_accom.ProviderId
INNER JOIN Ride r on r.RideID = p.RideID
INNER JOIN TransportService ts ON r.RideID = ts.TransportID
INNER JOIN ServiceProvider sp_transport ON ts.ProviderID = sp_transport.ProviderId
INNER JOIN Destination d ON p.DestinationID = d.DestinationID
INNER JOIN Location l ON d.LocationID = l.LocationID
WHERE 
    (@Keyword = '' OR p.Title LIKE '%' + @Keyword + '%' OR l.CityName LIKE '%' + @Keyword + '%')
    AND p.BasePrice BETWEEN @MinBudget AND @MaxBudget
    AND (@Duration IS NULL OR (p.Duration = @Duration OR (@Duration = 10 AND p.Duration >= 10)))
    AND (@GroupSize IS NULL OR (p.GroupSize = @GroupSize OR (@GroupSize = 20 AND p.GroupSize >= 20)))
    AND (@TripType IS NULL OR p.TripType = @TripType)
ORDER BY p.PackageID

select p.Title, a.ActivityName
from Package p
inner join PackageActivities pa on pa.PackageID = p.PackageID
inner join Activity a on a.ActivityID = pa.ActivityID
where p.Title = 'Sydney Coastal Getaway'

select * from Wishlist



select * from Operator




SELECT 
    p.*,
    sp_accom.ProviderName AS AccommodationProvider,
    sp_transport.ProviderName AS TransportProvider,
    l.CityName,
    l.CountryName,
    STUFF((
        SELECT ', ' + a.ActivityName
        FROM PackageActivities pa
        INNER JOIN Activity a ON pa.ActivityID = a.ActivityID
        WHERE pa.PackageID = p.PackageID
        FOR XML PATH('')
    ), 1, 2, '') AS Activities
FROM Package p
INNER JOIN Accommodation ac ON p.AccommodationID = ac.AccomodationID
INNER JOIN Hotel h ON ac.HotelID = h.HotelID
INNER JOIN ServiceProvider sp_accom ON h.ProviderID = sp_accom.ProviderId
INNER JOIN Ride r on r.RideID = p.RideID
INNER JOIN TransportService ts ON r.RideID = ts.TransportID
INNER JOIN ServiceProvider sp_transport ON ts.ProviderID = sp_transport.ProviderId
INNER JOIN Destination d ON p.DestinationID = d.DestinationID
INNER JOIN Location l ON d.LocationID = l.LocationID
ORDER BY p.PackageID
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY

select * from package
select * from Request

select *  from Package
select OperatorID, CompanyName FROM Operator

select * from Package p
inner join Operator o on p.OperatorID = o.OperatorID
where o.CompanyName = --whatever was picked

select * from Accommodation
select * from Hotel
select * from ServiceProvider

select * from Ride
select * from PackageActivities
select * from Activity

select p.packageid, sp.ProviderName
from Package p
INNER JOIN Accommodation a ON p.AccommodationID = a.AccomodationID
INNER JOIN Hotel h ON a.HotelID = h.HotelID
INNER JOIN ServiceProvider sp ON h.ProviderID = sp.ProviderId

select p.packageid, l.CityName, l.CountryName
from Package p
inner join Destination d on p.DestinationID = d.DestinationID
inner join Location l on l.LocationID = d.LocationID

Select DISTINCT SustainabilityScore from package

-- package accessibilty join for filters
SELECT * FROM Package
SELECT * FROM Accessibility

select distinct AccessibilityName from Accessibility

--hotel accessibilty
SELECT p.PackageID, p.Title, acc.AccessibilityName
FROM Package p
INNER JOIN Accommodation a ON a.AccomodationID = p.AccommodationID
INNER JOIN Hotel h ON h.HotelID = a.HotelID
INNER JOIN ServiceProvider sp ON sp.ProviderId = h.ProviderID
INNER JOIN Accessibility acc on acc.ProviderID = sp.ProviderId
GROUP BY p.PackageID, p.title, acc.AccessibilityName

-- ride
SELECT p.PackageID, p.Title, acc.AccessibilityName
FROM Package p
INNER JOIN Ride r ON r.RideID = p.RideID
INNER JOIN TransportService t ON t.TransportID = r.TransportID
INNER JOIN ServiceProvider sp ON sp.ProviderId = t.ProviderID
INNER JOIN Accessibility acc on acc.ProviderID = sp.ProviderId
GROUP BY p.PackageID, p.title, acc.AccessibilityName

-- activity
SELECT * FROM PackageActivities
select * from Guide
SELECT * FROM Activity

-- guide has a sign lanaguage flag indicating if they know sign lanaguage or not
SELECT p.PackageID, p.Title, acc.AccessibilityName
FROM Package p
INNER JOIN PackageActivities pa ON pa.PackageID = p.PackageID
INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
INNER JOIN Guide g ON g.GuideID = a.GuideID
INNER JOIN ServiceProvider sp ON sp.ProviderId = g.ProviderID
INNER JOIN Accessibility acc ON acc.ProviderID = sp.ProviderId
GROUP BY p.PackageID, p.title, acc.AccessibilityName


-----------------------------------------------------------
SELECT p.PackageID, p.Title,  acc.AccessibilityName as HotelAccess, acc2.AccessibilityName as TransportAccess, acc3.AccessibilityName as ActivityAccess
FROM Package p
INNER JOIN Accommodation a ON a.AccomodationID = p.AccommodationID
INNER JOIN Hotel h ON h.HotelID = a.HotelID
INNER JOIN ServiceProvider sp1 ON sp1.ProviderId = h.ProviderID
INNER JOIN Accessibility acc on acc.ProviderID = sp1.ProviderId
INNER JOIN Ride r ON r.RideID = p.RideID
INNER JOIN TransportService t ON t.TransportID = r.TransportID
INNER JOIN ServiceProvider sp2 ON sp2.ProviderId = t.ProviderID
INNER JOIN Accessibility acc2 on acc2.ProviderID = sp2.ProviderId
INNER JOIN PackageActivities pa ON pa.PackageID = p.PackageID
INNER JOIN Activity aC ON aC.ActivityID = pa.ActivityID
INNER JOIN Guide g ON g.GuideID = aC.GuideID
INNER JOIN ServiceProvider sp3 ON sp3.ProviderId = g.ProviderID
INNER JOIN Accessibility acc3 ON acc3.ProviderID = sp3.ProviderId
GROUP BY p.PackageID, p.title, acc.AccessibilityName, acc2.AccessibilityName, acc3.AccessibilityName

---------- custom trip WAAAAA
select * from CustomTrip
select distinct duration from CustomTrip

select distinct ActivityName from Activity

select l.CityName, l.CountryName
from Destination d
inner join Location l on d.LocationID = l.LocationID

select * from Accommodation
select * from Hotel
select * from ServiceProvider

select l.CityName, l.CountryName
from Accommodation a
inner join Hotel h on h.HotelID = a.HotelID
inner join ServiceProvider s on s.ProviderId = h.ProviderID
inner join Destination d on s.DestinationID = d.DestinationID
inner join Location l on l.LocationID = d.LocationID


SELECT a.AccomodationID, s.ProviderName
                         FROM Accommodation a
                         INNER JOIN Hotel h ON h.HotelID = a.HotelID
                         INNER JOIN ServiceProvider s ON s.ProviderId = h.ProviderID
                         WHERE s.DestinationID = 1

SELECT r.RideID, sp.ProviderName 
FROM Ride r 
INNER JOIN TransportService t ON r.TransportID = t.TransportID
INNER JOIN ServiceProvider sp ON sp.ProviderID = t.ProviderID
WHERE sp.DestinationID = 1 OR r.DestinationID = 1 


SELECT a.AccomodationID, s.ProviderName
                         FROM Accommodation a
                         INNER JOIN Hotel h ON h.HotelID = a.HotelID
                         INNER JOIN ServiceProvider s ON s.ProviderId = h.ProviderID
                         WHERE s.DestinationID = 1

SELECT ct.AccommodationID, ct.RideID, p.SustainabilityScore
FROM Package p
INNER JOIN CustomTrip ct ON ct.AccommodationID = p.AccommodationID AND ct.RideID = p.RideID
GROUP BY ct.AccommodationID, ct.RideID, p.SustainabilityScore, p.Title

select * from CustomTrip
select * from CustomTripActivities

select * from Request
select * from Booking

SELECT * FROM Activity
select  DISTINCT ActivityName 
from activity

 SELECT A.ActivityID, A.ActivityName, A.StartTime, SP.ProviderName AS GuideName
 FROM Activity A
 INNER JOIN Guide G ON A.GuideID = G.GuideID
 INNER JOIN ServiceProvider SP on SP.ProviderId = G.GuideID
 WHERE SP.DestinationID = 1

 -- bookings wAAAAAAAAAA
 select * from Request
select * from Booking
select * from Payment
select * from DigitalPasses

-- package
SELECT l.CityName, l.CountryName, p.Title, r.PreferredStartDate, b.TotalCost
FROM Booking b
INNER JOIN Request r ON b.RequestID = r.RequestID
INNER JOIN Package p ON p.PackageID = r.PackageID
INNER JOIN Destination d ON p.DestinationID = d.DestinationID
INNER JOIN Location l ON l.LocationID = d.LocationID
WHERE r.TripSourceType = 'Package'

SELECT l.CityName, l.CountryName, r.PreferredStartDate, b.TotalCost
FROM Booking b
INNER JOIN Request r ON b.RequestID = r.RequestID
INNER JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID
INNER JOIN Destination d ON c.DestinationID = d.DestinationID
INNER JOIN Location l ON l.LocationID = d.LocationID
WHERE r.TripSourceType = 'Custom'

select * from Request
select * from Booking
select * from Payment

INSERT INTO Booking (RequestID, TotalCost, BookingDate, BookingStatus, CancellationReason)
            SELECT r.RequestID, 0, NULL, 'Pending', NULL
            FROM Request r
            LEFT JOIN Booking b ON r.RequestID = b.RequestID
            WHERE r.TravelerID = 1 AND b.RequestID IS NULL

select r.RequestID, t.Username, t.Password, r.PreferredStartDate
from Booking b
inner join Payment p on p.BookingID = b.BookingID
inner join Request r on r.RequestID = b.RequestID
inner join Traveler t on t.TravelerID = r.TravelerID
where b.BookingStatus = 'Confirmed' AND p.PaymentStatus = 'Success' AND r.PreferredStartDate < GETDATE()

select * from Review

-- UPDATING MY BOY THOMAS' SO I CAN SEE SOMETHING IN THE DAMN TRAVEL HISTORY
UPDATE Request
SET PreferredStartDate = DATEADD(YEAR, -1, GETDATE())
WHERE RequestID = 2

inner join Payment p on p.BookingID = b.BookingID
inner join Request r on r.RequestID = b.RequestID
inner join Traveler t on t.TravelerID = r.TravelerID
where b.BookingStatus = 'Confirmed' AND p.PaymentStatus = 'Success' AND r.PreferredStartDate < GETDATE()


select distinct CancellationReason
from Booking

select *
from Booking b
inner join Request r on r.RequestID = b.RequestID
inner join Traveler t on r.TravelerID = t.TravelerID
where t.Username = 'james_w'

select *
from Booking b
inner join Request r on r.RequestID = b.RequestID
inner join Traveler t on r.TravelerID = t.TravelerID
inner join DigitalPasses d on d.BookingID = b.BookingID
where t.Username = 'james_w'

select * from DigitalPasses
select * from Payment


select b.BookingID, b.BookingStatus, p.PaymentStatus, d.PassID, d.PassType, d.PassStatus
from Booking b
inner join Request r on r.RequestID = b.RequestID
inner join Traveler t on r.TravelerID = t.TravelerID
inner join DigitalPasses d on d.BookingID = b.BookingID
inner join Payment p on p.BookingID = b.BookingID
where b.BookingStatus <> 'Confirmed'

select * from Booking
select * from  Payment
select * from DigitalPasses

select * from Request r -- paid status for all of them
inner join CustomTrip ct on ct.CustomTripID = r.CustomTripID



select * from CustomTrip

select c.AccommodationStatusFlag, c.RideStatusFlag, c.ActivitiesStatusFlag
from Booking b
inner join Request r on r.RequestID = b.RequestID
inner join CustomTrip c on c.CustomTripID = r.CustomTripID
where r.TripSourceType = 'Custom'

select * from  Booking
select * from Package

select d.PassType, d.ExpiryDate
from Booking b
inner join DigitalPasses d on d.BookingID = b.BookingID
where b.BookingID = @BookingID

select * from Traveler where TravelerID = 26

SELECT PassType, ExpiryDate 
FROM DigitalPasses 
WHERE BookingID = 2


select * from Booking
select * from Payment

select * from Review

sp_help Review

select * from Traveler

SELECT a.AccomodationID, sp.ProviderName
FROM Booking b
INNER JOIN Request r ON b.RequestID = r.RequestID
INNER JOIN Payment pay ON pay.BookingID = b.BookingID
INNER JOIN Package p ON r.PackageID = p.PackageID
INNER JOIN Accommodation a ON p.AccommodationID = a.AccomodationID
INNER JOIN Hotel h ON h.HotelID = a.HotelID
INNER JOIN ServiceProvider sp ON sp.ProviderId = h.ProviderID
WHERE r.TripSourceType = 'Package'
  AND b.BookingStatus = 'Confirmed'
  AND r.PreferredStartDate < GETDATE()
  AND pay.PaymentStatus = 'Success' --paid, confirmed, in the past -> reviewable
  AND r.TravelerID = 26 -- thomas_n Quest$567


SELECT a.AccomodationID, sp.ProviderName
FROM Booking b
INNER JOIN Request r ON b.RequestID = r.RequestID
INNER JOIN Payment pay ON pay.BookingID = b.BookingID
INNER JOIN CustomTrip c ON r.CustomTripID = c.CustomTripID
INNER JOIN Accommodation a ON c.AccommodationID = a.AccomodationID
INNER JOIN Hotel h ON h.HotelID = a.HotelID
INNER JOIN ServiceProvider sp ON sp.ProviderId = h.ProviderID
WHERE r.TripSourceType = 'Custom'
  AND b.BookingStatus = 'Confirmed'
  AND r.PreferredStartDate < GETDATE()
  AND pay.PaymentStatus = 'Success' --paid, confirmed, in the past -> reviewable
  AND r.TravelerID = 26 -- thomas_n Quest$567


SELECT o.OperatorID, o.CompanyName
FROM Booking b
INNER JOIN Request r ON b.RequestID = r.RequestID
INNER JOIN Payment pay ON pay.BookingID = b.BookingID
INNER JOIN Operator o ON r.OperatorID = o.OperatorID
WHERE b.BookingStatus = 'Confirmed'
  AND r.PreferredStartDate < GETDATE()
  AND pay.PaymentStatus = 'Success' --paid, confirmed, in the past -> reviewable
  AND r.TravelerID = 26 -- thomas_n Quest$567

   SELECT r.RideID, sp.ProviderName
        FROM Booking b
        INNER JOIN Request req ON b.RequestID = req.RequestID
        INNER JOIN Payment pay ON pay.BookingID = b.BookingID
        LEFT JOIN CustomTrip c ON req.CustomTripID = c.CustomTripID
        LEFT JOIN Package p ON req.PackageID = p.PackageID
        INNER JOIN Ride r ON r.RideID = ISNULL(c.RideID, p.RideID)
		INNER JOIN TransportService t ON t.TransportID = r.TransportID
        INNER JOIN ServiceProvider sp ON sp.ProviderId = t.ProviderID
        WHERE b.BookingStatus = 'Confirmed'
          AND req.PreferredStartDate < GETDATE()
          AND pay.PaymentStatus = 'Success'
          AND req.TravelerID = 26

 
   SELECT g.GuideID, sp.ProviderName
        FROM Booking b
        INNER JOIN Request req ON b.RequestID = req.RequestID
		INNER JOIN CustomTrip c ON req.CustomTripID = c.CustomTripID
		INNER JOIN CustomTripActivities cta ON cta.CustomTripID = c.CustomTripID
		INNER JOIN Activity a ON a.ActivityID = cta.ActivityID
		INNER JOIN Guide g ON g.GuideID = a.GuideID
		INNER JOIN ServiceProvider sp ON sp.ProviderId = g.ProviderID
		INNER JOIN Payment p ON p.BookingID = b.BookingID
		WHERE req.TripSourceType = 'Custom' AND p.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed' AND req.PreferredStartDate < GETDATE()


		  SELECT DISTINCT g.GuideID, sp.ProviderName
 FROM Booking b
 INNER JOIN Request req ON b.RequestID = req.RequestID
 INNER JOIN Package pack ON req.PackageID = pack.PackageID
 INNER JOIN PackageActivities pa ON pa.PackageID = pack.PackageID
 INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
 INNER JOIN Guide g ON g.GuideID = a.GuideID
 INNER JOIN ServiceProvider sp ON sp.ProviderId = g.ProviderID
 INNER JOIN Payment p ON p.BookingID = b.BookingID
 WHERE req.TripSourceType = 'Package' 
   AND p.PaymentStatus = 'Success'
   AND b.BookingStatus = 'Confirmed'
   AND req.PreferredStartDate < GETDATE()
   AND req.TravelerID = 26


   SELECT r.Rating, r.Comment, r.ReviewDate,
    ISNULL(spAccommodation.ProviderName, 
        ISNULL(spRide.ProviderName,
            ISNULL(spGuide.ProviderName, o.CompanyName))) AS ReviewTarget
FROM Review r
LEFT JOIN Accommodation a ON r.AccommodationID = a.AccomodationID
LEFT JOIN Hotel h ON a.HotelID = h.HotelID
LEFT JOIN ServiceProvider spAccommodation ON spAccommodation.ProviderId = h.ProviderID

LEFT JOIN Ride ride ON r.RideID = ride.RideID
LEFT JOIN TransportService ts ON ride.TransportID = ts.TransportID
LEFT JOIN ServiceProvider spRide ON spRide.ProviderId = ts.ProviderID

LEFT JOIN Guide g ON r.GuideID = g.GuideID
LEFT JOIN ServiceProvider spGuide ON spGuide.ProviderId = g.ProviderID

LEFT JOIN Operator o ON r.OperatorID = o.OperatorID

WHERE r.TravelerID = 1 AND r.ModerationStatus = 'Approved'
ORDER BY r.ReviewDate DESC

select * from Review
UPDATE Review
SET ModerationStatus = 'Approved'
WHERE ReviewID > 51

select * from TransportService
select * from Ride

select * from Traveler
