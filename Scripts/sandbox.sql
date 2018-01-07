select * from People WHERE PersonId = 11

UPDATE People
SET 
    IsAuthorised = @IsAuthorised,
    IsEnabled = @IsEnabled,
    IsValid = @IsValid,
WHERE
    PersonId = @PersonId


INSERT INTO FavouriteColours(PersonId, ColourId) VALUES(@PersonId, @ColourId)
DELETE FROM FavouriteColours WHERE PersonId = @PersonId AND ColourId = @ColourId

select * from Colours
select * from FavouriteColours

SELECT
    p.*,
    c.ColourId,
    c.Name
FROM
    People p
LEFT JOIN
    FavouriteColours fc on p.PersonId = fc.PersonId
LEFT JOIN
    Colours c ON fc.ColourId = c.ColourId

SELECT 
    c.ColourId AS Id,
    c.Name,
    c.IsEnabled
FROM 
    Colours c 
INNER JOIN 
    FavouriteColours fc ON c.ColourId = fc.ColourId
WHERE 
    fc.PersonId = 11


SELECT DISTINCT
    c.*
FROM
    Colours c
LEFT JOIN
    FavouriteColours fc ON fc.ColourId = c.ColourId
WHERE
    fc.ColourId IS NOT NULL