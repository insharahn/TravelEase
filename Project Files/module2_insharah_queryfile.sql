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

-- Modified the Review table, just drop the old one 


DROP table Review

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
Select * from Review
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

