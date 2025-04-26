use [Project Data]

select * from Traveler
select distinct preferences from traveler
select * from Location
where CountryName = 'Pakistan'

sp_help Traveler

-- DOB instead of Age in traveler
ALTER TABLE Traveler ADD DateOfBirth DATE;
UPDATE Traveler
SET DateOfBirth = DATEADD(YEAR, -Age, GETDATE())
WHERE Age IS NOT NULL;
ALTER TABLE Traveler DROP CK__Traveler__Age__55009F39 --drop dependency first
ALTER TABLE Traveler DROP COLUMN Age;

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
