UPDATE [companies].[Companies]
SET Address_Coordinates_Point = GEOGRAPHY::Point(c.Address_Coordinates_Latitude, c.Address_Coordinates_Longitude, 4326)
FROM [companies].[Companies] as c
WHERE c.Address_Coordinates_Latitude IS NOT NULL AND c.Address_Coordinates_Longitude IS NOT NULL