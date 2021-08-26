INSERT IGNORE INTO ##DATABASE##.logos (
    content_id,
    user_id,
    file_name,
    image
)
VALUES (
    @ContentId,
    @UserId,
    @FileName,
    @Image
);