SELECT
  i.content_id          as 'ContentId',
  i.user_id             as 'UserId',
  i.file_name           as 'FileName',
  i.image               as 'Image'
FROM ##DATABASE##.logos i
WHERE i.content_id = @ContentId;