CREATE TABLE [dbo].[User]
(
	Id NVARCHAR(128) not null primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	EmailAddress nvarchar(256) not null,
	CreatedDate Datetime2 not null default getutcdate()
)
