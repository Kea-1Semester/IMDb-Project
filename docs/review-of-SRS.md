# **Review**

## Issues

| Issues                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ``1ˢᵗ par``. The application, named **IMDb Explorer**, will consist of a **backend** for data handling and a **frontend** that provides an intuitive user interface <code>**[intuitive user interface is unclear: unmeasurable criteria for usability.]**</code> for searching and viewing movie and TV show details. <br></br> ``2ⁿᵈ par``. The **backend** will manage the **IMDb dataset**, handling user authentication, data retrieval, and storage, ensuring secure access to all data <code>**[secure access unclear: Does this imply encryption, hashing, role-based access control e.g. admin, user etc...]**</code>. It will include a column named `is_adult` to indicate whether a title is intended for adults only <code>**[is_adult: What happens if the title is marked as adult? does it blocked, filtered, or shown with a warning?]**</code>. <br></br> ``3ʳᵈ par`` The **frontend** will allow users to input search queries via a dedicated search bar, displaying a list of matching movie and TV show titles <code>**[which fields are searchable]**</code> <code>**[Do we use pagination or infinite scrolling?]**</code>. When a user selects a title from the search results, the application will show detailed information,  including: </br> ``4ᵗʰ par`` </br> - Title</br> - Genres</br> - Episodes</br> - Aliases</br> - Attributes</br> - Comments</br> - Types</br> - Ratings</br>- Professions  </br> - Persons </br><code>**[Types is undefined. Attributes: which attributes? Runtime? Language? Comments are unclear: User-generated comments or IMDB metadata comments? Types: Types of what? Title types? Person types? Genre types? ]**</code> <br></br> ``5ᵗʰ par`` If a user selects a title, the detailed information will be presented on the same screen. Users will be able to submit their comments and rates directly below the detailed information section <code>**[Is login required for commenting, or are anonymous comments allowed? Can a user submit multiple ratings for the same title, or only one? </br> What is the rating scale? 1-5? 1-10? using Percentage?]**</code>. </br></br> ``6ᵗʰ par`` The application aims to create an engaging user experience while ensuring robust data handling and security, providing fast response times for search queries and minimal loading times for detailed displays <code>**["engaging user experience": not measurable. "fast response times": Undefined. What counts as fast? <img 200 ms? < 1 second? "minimal loading times": not quantified ]**</code>. |

## Review table

| ID | Name                     | Location            | priority                                      | Description                                                                                      |
|----|--------------------------|---------------------|-----------------------------------------------|--------------------------------------------------------------------------------------------------|
| 2  | Security                 | 2<sup>nd</sup> par. | <span style="color:red;">Very High</span>     | need to specify the security level and implementation for the system within the access control.  |
| 6  | Types                    | 4<sup>th</sup> par. | <span style="color:orangered;">High</span>    | The types are not defined - What is a type and what properties does it include                   |
| 7  | Attributes               | 4<sup>th</sup> par. | <span style="color:orangered;">High</span>    | The attributes are not defined - What is an attribute and what properties does it include        |
| 13 | Ratings submission       | 5<sup>th</sup> par. | <span style="color:orangered;">High</span>    | The rating submission process is not defined - can users submit multiple ratings or just one?    |
| 14 | Login requirement        | 5<sup>th</sup> par. | <span style="color:#E0912B;">Near High</span> | It is not defined whether login is required for commenting or if anonymous comments are allowed. |
| 4  | filterable frontend      | 3<sup>rd</sup> par. | <span style="color:gold;">Medium</span>       | Need to specify the filterable fields and data                                                   |
| 5  | Pagination               | 3<sup>rd</sup> par. | <span style="color:gold;">Medium</span>       | Defining the pagination page size e.g. 20 movies per. page                                       |
| 9  | Ratings system           | 4<sup>th</sup> par. | <span style="color:gold;">Medium</span>       | The rating system is not defined - is it 1-5, 1-10 or percentage based.                          |
| 11 | Fast response times      | 6<sup>th</sup> par. | <span style="color:gold;">Medium</span>       | The fast response times is not defined - what is considered fast?                                |
| 12 | Minimal loading times    | 6<sup>th</sup> par. | <span style="color:gold;">Medium</span>       | The minimal loading times is not defined - what is considered minimal?                           |
| 1  | Intuitive user interface | 1<sup>st</sup> par. | <span style="color:limegreen;">Low</span>     | intuitive is ambiguous.                                                                          |
| 3  | Is_adult                 | 2<sup>nd</sup> par. | <span style="color:limegreen;">Low</span>     | What should happen if a title is marked as adult.                                                |
| 8  | Comments                 | 4<sup>th</sup> par. | <span style="color:limegreen;">low</span>     | The comments is unclear - is it user comment or metadata for IMDB comments.                      |
| 10 | Engaging user experience | 6<sup>th</sup> par. | <span style="color:limegreen;">low</span>     | The engaging user experience is not defined - what makes it engaging?                            |

## Requirements

The data entry will require the user to input information according to the following template:

- Title
  - TitleType: Max. 100 char and dash allowed
  - PrimaryTitle: Max. 255 char and dash allowed
  - OriginalTitle: Max. 255 char and dash allowed
  - StartYear: Max. 4 numeric digits positive
  - EndYear: Max. 4 numeric digits positive or ``NULL``
  - RuntimeMinutes: Max. 1440 minutes positive or ``NULL``
- Genres
  - Genre: Max. 100 char and not allow any special characters
- Episodes
  - EpisodeNumber: Max. three numeric digits positive between 01 and 999
  - SeasonNumber: Max. 2 numeric digits positive between 01 and 99
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
| 3 | is_adult | 2<sup>nd</sup> par. | <span style="color:limegreen;">Low</span> | W. |
| 4 | filterable frontend | 3<sup>rd</sup> par. | <span style="color:gold;">Medium</span> | need to specify the security level and implementation for the system within the access control. |
is marked as adul
