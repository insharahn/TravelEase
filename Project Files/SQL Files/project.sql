select * from Package
select * from Operator
select * from Accommodation
select * from Ride
select * from Request
select * from Hotel
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

select * from Destination