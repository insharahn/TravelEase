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
----------------------------------------
-- Add payment status columns to Request
----------------------------------------

-- I'm sorry guys I had to

ALTER TABLE Request
ADD AccomodationPaidStatus VARCHAR(20) NOT NULL DEFAULT 'Unpaid'
CONSTRAINT CHK_PaidStatus CHECK (AccomodationPaidStatus IN ('Paid', 'Unpaid'));

ALTER TABLE Request
ADD RidePaidStatus VARCHAR(20) NOT NULL DEFAULT 'Unpaid'
CONSTRAINT CHK_RidePaidStatus CHECK (RidePaidStatus IN ('Paid', 'Unpaid'));

ALTER TABLE Request
ADD ActivityPaidStatus VARCHAR(20) NOT NULL DEFAULT 'Unpaid'
CONSTRAINT CHK_ActivityPaidStatus CHECK (ActivityPaidStatus IN ('Paid', 'Unpaid'));
