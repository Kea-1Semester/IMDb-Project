# Software Requirements Specification (SRS) for IMDb Project

## SRS

The application, named **IMDb Explorer**, will consist of a **backend** for data handling and a **frontend** that provides an intuitive user interface for searching and viewing movie and TV show details.

The **backend** will manage the **IMDb dataset**, handling user authentication, data retrieval, and storage, ensuring secure access to all data. It will include a column named `is_adult` to indicate whether a title is intended for adults only.

The **frontend** will allow users to input search queries via a dedicated search bar, displaying a list of matching movie and TV show titles. When a user selects a title from the search results, the application will show detailed information, including:

- Title
- Genres
- Episodes
- Aliases
- Attributes
- Comments
- Types
- Ratings
- Professions
- Persons

If a user selects a title, the detailed information will be presented on the same screen. Users will be able to submit their comments and rates directly below the detailed information section.

The application aims to create an engaging user experience while ensuring robust data handling and security, providing fast response times for search queries and minimal loading times for detailed displays.

## Requirements

The data entry will require the user to input information according to the following template:

- Title
  - TitleType: Min. 5 Max. 25 char and does not allow any special characters
  - PrimaryTitle: Min. 5, Max. 255 char and dash allowed
  - OriginalTitle: Min. 5, Max. 255 char and dash allowed
  - StartYear: Max. 4 numeric digits positive and Range 1888-newest year
  - EndYear: 4 numeric digits positive or ``NULL`` and greater than StartYear
  - RuntimeMinutes: Min. 60 Min Max. 1440 minutes positive or ``NULL``
- Genres
  - Genre: Min. 6 Max. 50 char and does not allow any special characters
- Episodes
  - EpisodeNumber: Min. 1 Max. three numeric digits positive between 1 and 999 and does not allow 0
  - SeasonNumber: Min. 1 Max. 2 numeric digits positive between 1 and 99
- Aliases
  - Region: Max. 100 char and not allow any special characters
  - Language: Max. 5 char and dash allowed
  - Title: Max. 255 char and dash allowed
- Attributes
  - Attribute: Max. 100 char and does not allow any special characters
- Comments
  - Comment: Max. 255 char and allow (-, (, ), *, / )
- Types
  - Type: Max. 100 char and does not allow any special characters
- Ratings
  - AverageRating: Max. 2 numeric digits positive
  - NumVotes: int Max positive
- Professions
  - Profession: Max. 45 char and does not allow any special characters
- Persons
  - Name: Max. 255 char and does not allow any special characters
  - BirthYear: Max. 4 numeric digits positive
  - EndYear: Max. 4 numeric digits positive or ``NULL``

## Navigation

- [review-of-SRS.md](review-of-SRS.md)
