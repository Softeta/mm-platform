UPDATE [jobs].[Jobs]
SET [Company_Address_Coordinates_Point] = GEOGRAPHY::Point(j.Company_Address_Coordinates_Latitude, j.Company_Address_Coordinates_Longitude, 4326)
FROM [jobs].[Jobs] as j
WHERE j.Company_Address_Coordinates_Latitude IS NOT NULL AND j.Company_Address_Coordinates_Longitude IS NOT NULL
