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

