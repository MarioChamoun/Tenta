CREATE PROCEDURE AddTerritory
	@TerritoryDescription nvarchar(50),
	@RegionID int,
	@TerritoryID int
AS
BEGIN
	INSERT INTO Territories
           ([TerritoryID]
           ,[TerritoryDescription]
           ,[RegionID])
     VALUES
           (@TerritoryID
           ,@TerritoryDescription
           ,@RegionID)
END
