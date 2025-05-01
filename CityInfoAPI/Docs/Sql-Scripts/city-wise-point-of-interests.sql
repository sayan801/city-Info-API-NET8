SELECT 
    C.[Id] AS CityId,
    C.[Name] AS CityName,
    C.[Description] AS CityDescription,
    P.[Id] AS PointId,
    P.[Name] AS PointName,
    P.[Description] AS PointDescription
FROM 
    [dbo].[Cities] AS C
    LEFT JOIN [dbo].[PointsOfInterest] AS P ON C.[Id] = P.[CityId]
ORDER BY 
    C.[Name], P.[Name]