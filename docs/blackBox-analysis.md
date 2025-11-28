# BackBox Analysis


## Black-box design techniques (manual analysis)

- Based on requirements, we are building Equivalence partitioning analysis and Boundary Value Analysis with 3 Boundary Values.

### Title Test Cases

| Partition Type           | Partition                         | Test Case Values                     |
|--------------------------|-----------------------------------|--------------------------------------|
| Equivalence Partitioning | Valid TitleType                   | 6-char, 7-char, 24-char              |
| Equivalence Partitioning | Invalid TitleType                 | movi, 50-char                        |
| Boundary Value Analysis  | TitleType Valid Lower Bound       | 5-char                               |
|                          | TitleType Valid Upper Bound       | 25-char                              |
|                          | TitleType InValid Lower Bound     | 1-char, 4-char                       |
|                          | TitleType Invalid Upper Bound     | 26-char                              |
| Edges                    | TitleType edges cases             | NULL, "", " ", #asbas-char etc.      |
| Equivalence Partitioning | Valid PrimaryTitle                | 6-char, 50-char, 254-char, star-wars |
| Equivalence Partitioning | Invalid PrimaryTitle              | 4-char, 400-char                     |
| Boundary Value Analysis  | PrimaryTitle Valid Lower Bound    | 5-char                               |
|                          | PrimaryTitle Valid Upper Bound    | 255-char                             |
|                          | PrimaryTitle Invalid Lower Bound  | 1-char , 4-char                      |
|                          | PrimaryTitle Invalid Upper Bound  | 256-char , char.max                  |
| Edges                    | PrimaryTitle edges cases          | NULL, "", " ", #asbas-char etc.      |
| Equivalence Partitioning | Valid OriginalTitle               | 6-char, 50-char. 254-char, star-wars |
| Equivalence Partitioning | Invalid OriginalTitle             | 4-char, 400-char                     |
| Boundary Value Analysis  | OriginalTitle Valid Lower Bound   | 5-char                               |
|                          | OriginalTitle Valid Upper Bound   | 255-char                             |
|                          | OriginalTitle Invalid Lower Bound | 1-char , 4-char                      |
|                          | OriginalTitle Invalid Upper Bound | 256-char , char.max                  |
| Edges                    | OriginalTitle edges cases         | NULL, "", " ", #asbas-char etc.      |
| Equivalence Partitioning | Valid StartYear                   | 1999, 2024                           |
| Equivalence Partitioning | Invalid StartYear                 | 999, -2020                           |
| Boundary Value Analysis  | StartYear Lower Bound             | 1888                                 |
|                          | StartYear Upper Bound             | newest year                          |
| Edges                    | StartYear edges cases             | 0000                                 |
| Equivalence Partitioning | Valid EndYear                     | 2005, 2023, 2026 > 2025, NULL        |
| Equivalence Partitioning | Invalid EndYear                   | 99, -2010                            |
| Boundary Value Analysis  | EndYear Lower Bound               | endYear 2025 = 2025 startYear        |
|                          | EndYear Upper Bound               | 9999                                 |
| Edges                    | EndYear edges cases               | 0000                                 |
| Equivalence Partitioning | Valid RuntimeMinutes              | 61, 1000, 1439, NULL                 |
| Equivalence Partitioning | Invalid RuntimeMinutes            | 50                                   |
|                          | Valid Lower Bound                 | 60                                   |
|                          | Valid Upper Bound                 | 1440                                 |
|                          | InValid Lower Bound               | 0                                    |
|                          | InValid Upper Bound               | 1441                                 |
| Edges                    | RuntimeMinutes edges cases        | -1, int.max                          |



- List of test cases:
  - **TitleType:**
    - Valid:
      - 5 characters (lower valid boundary)
      - 6 characters
      - 7 characters
      - 24 characters
      - 25 characters (upper valid boundary)
    - Invalid
      - 1 character 
      - 4 characters
      - 26 characters
      - 50 characters
    - Edge Cases:
      - NULL
      - ""
      - " "
      - "#5-char"
  - **PrimaryTitle & OriginalTitle:**
    - Valid:
      - 5-character 
      - 6-character 
      - 50-character 
      - 254-character 
      - 255-character 
    - Invalid:
      - 1-character 
      - 4-character 
      - 256-character 
      - 400-character 
      - char.max (maximum representable length)
    - Edge Cases:
      - NULL
      - ""
      - " "
      - "#5-char"
  - **StartYear:**
    - Valid:
      - 1999
      - 2024
      - new year = 2025 
    - Invalid:
      - 999
      - -2020
    - Edge Cases:
      - 0000
  - **EndYear:**
    - Valid:
      - -2005
      - 2026 > 2025 (endYear < startYear)
      - NULL
    - Invalid:
      - 99
      - -2010
    - Edge Cases:
      - 0000
  - **RuntimeMinutes:**
    - Valid:
      - 60 ``lower valid boundary``
      - 61
      - 1000
      - 1439
      - 1440 ``upper valid boundary``
      - NULL
    - Invalid:
      - 0 ``invalid lower boundary``
      - 50
      - 1441 ``invalid upper boundary``
    - Edge Cases:
      - -1
      - int.max
## Genres

| Partition Type           | Partition         | Test Case Values                                 |
|--------------------------|-------------------|--------------------------------------------------|
| Equivalence Partitioning | Valid Genre       | 3-char, 2.char, 10-char, 49-char, 50-char Sci-Fi |
|                          | Invalid Genre     | 1-char, 2-char, 51-char 60-char                  |
| Boundary Value Analysis  | Valid Lower Bound | 3-char                                           |
|                          | Valid Upper Bound | 50-char                                          |
| Edges                    | Genre edges       | NULL, "", " ", #5char                            |

- list of test cases:
- **Genre:**
    - Valid:
      - 3 characters (lower valid boundary)
      - 2 characters
      - 10 characters
      - Sci-Fi
      - 49 characters
      - 50 characters (upper valid boundary)
    - Invalid:
      - 1 character 
      - 2 characters 
      - 51 characters 
      - 60 characters
    - Edge Cases:
      - NULL
      - ""
      - " "
      - "#5-char"

## Episodes

| Partition Type           | Partition           | Test Case Values            |
|--------------------------|---------------------|-----------------------------|
| Equivalence Partitioning | Valid EpisodeNumber | 1, 2, 500, 998, 999         |
|                          | Invalid Episode     | -1, -5, 1000, 2000, int.max |
| Boundary Value Analysis  | Valid Lower Bound   | 1                           |
|                          | Valid Upper Bound   | 999                         |
|                          | Invalid Lower Bound | 0                           |
|                          | Invalid Upper Bound | 1000                        |
| Edges                    | Episode edges       | NULL, 0                     |
| Equivalence Partitioning | Valid SeasonNumber  | 1, 2, 55, 98, 99            |
|                          | Invalid Season      | 0, -1, -5.5, 100, 5.5, 501  |
| Boundary Value Analysis  | Valid Lower Bound   | 1                           |
|                          | Valid Upper Bound   | 99                          |
| Edges                    | Season edges        | NULL, 5.5, -5.5             |
- list of test cases:
  - **EpisodeNumber:**
    - Valid:
      - 1
      - 2
      - 500
      - 998
      - 999
    - Invalid:
      - -1
      - -5
      - 1000
      - 2000
      - int.max
    - Edge Cases:
      - Null
      - 0
  - **SeasonNumber:**
  - Valid:
    - 1
    - 2
    - 55
    - 98
    - 99
  - Invalid:
    - 0
    - -1
    - -5.5
    - 100
    - 5.5
    - 501
  - Edge Cases:
    - Null

## Aliases 
| Partition Type           | Partition            | Test Case Values                     |
|--------------------------|----------------------|--------------------------------------|
| Equivalence Partitioning | Valid Region         | 3-char, 50-char ,99-char             |
|                          | Invalid Region       | 1-char, 150-char                     |
| Boundary Value Analysis  | Valid Lower Bound    | 2-char                               |
|                          | Valid Upper Bound    | 100-char                             |
|                          | Invalid Lower Bound  | ""(empty string)                     |
|                          | Invalid Upper Bound  | 101-char                             |
| Edges                    | Region edges         | NULL, " ", any special characters    |
| Equivalence Partitioning | Valid Language       | 3-char, "en-us"                      |
|                          | Invalid Language     | 1-char, 6-char, 10-char              |
| Boundary Value Analysis  | Language Lower Bound | 2-char                               |
|                          | Language Upper Bound | 5-char                               |
| Edges                    | Language edges       | NULL, "", " "                        |
| Equivalence Partitioning | Valid Title          | 6-char, 50-char, 254-char, star-wars |
|                          | Invalid Title        | 3-char, 400-char, star#wars          |
| Boundary Value Analysis  | Title Lower Bound    | 5-char                               |
|                          | Title Upper Bound    | 255-char                             |
| Edges                    | Title edges          | NULL, "", " ", #asbas-char etc.      |




## Person

| Partition Type           | Partition         | Test Case Values                                     |
|--------------------------|-------------------|------------------------------------------------------|
| Equivalence Partitioning | Valid Name        | 2-char, 3-char, 50-char ,99-char                     |
|                          | Invalid Name      | 1-char, 100-char, 101-char, 150-char, str.max        |
| Boundary Value Analysis  | Valid Lower Bound | 2-char                                               |
|                          | Valid Upper Bound | 100-char                                             |
| Edges                    | Name edges        | NULL, " ", ""(empty string) , any special characters |
| Equivalence Partitioning | Valid BirthYear   | 1995                                                 |
|                          | Invalid BirthYear | 1, 19, 199 , 19999                                   |
| Edges                    | BirthYear edges   | NULL, 0000                                           |
| Equivalence Partitioning | Valid EndYear     | 2020                                                 |
|                          | Invalid EndYear   | 1, 19, 199, 19999, EndYear (2025) > BirthYear (1945) |
| Edges                    | EndYear edges     | NULL, 0000                                           |

- list of test cases:
- **Name:**
  - Valid:
    - 2 characters
    - 3 characters
    - 50 characters
    - 99 characters
  - Invalid:
    - 1 character 
    - 100 characters 
    - 101 characters 
    - 150 characters 
    - string.max (maximum representable length)
  - Edge Cases:
    - NULL
    - "" (empty string)
    - " " (space)
    - any special characters e.g., #5-char
- **BirthYear:**
  - Valid:
    - 1995
  - Invalid:
    - 1
    - 19
    - 199 
    - 19999
  - Edge Cases:
    - 0000
    - Null
- **EndYear:**
  - Valid:
    - 2020
  - Invalid:
    - 1
    - 19
    - 199 
    - 19999
    - EndYear (2025) > BirthYear (1945)
  - Edge Cases:
    - 0000
    - Null
  









