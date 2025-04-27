---------------------------------
-- Add status columns to services
---------------------------------

ALTER TABLE Accommodation
ADD AccommodationStatus VARCHAR(20) CHECK (AccommodationStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL DEFAULT 'Pending';

ALTER TABLE Ride
ADD RideStatus VARCHAR(20) CHECK (RideStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL DEFAULT 'Pending';

ALTER TABLE Activity
ADD ActivityStatus VARCHAR(20) CHECK (ActivityStatus IN ('Pending', 'Approved', 'Rejected')) NOT NULL DEFAULT 'Pending';

-------------------------------------------------------------------------------------------------------------------------------------
