# BackBox Analysis

## Black-box design techniques (manual analysis)

- Based on requirements, we are building Equivalence partitioning analysis and Boundary Value Analysis with 3 Boundary Values.

### Title Test Cases

| Field          | Valid Range          | Type    | Notes                   |
|----------------|----------------------|---------|-------------------------|
| TitleType      | 5-25 chars           | string  |                         |
| PrimaryTitle   | 5-255 chars          | string  |                         |
| OriginalTitle  | 5-255 chars          | string  |                         |
| StartYear      | 1888-2025            | integer | Current year = 2025     |
| EndYear        | NULL or >= StartYear | integer | Dependency on StartYear |
| RuntimeMinutes | 60-1440 or NULL      | integer |                         |

| Partition Type           | Partition                  | Valid Range                      | Test Case Values                             |
|--------------------------|----------------------------|----------------------------------|----------------------------------------------|
| Equivalence Partitioning | Valid ``TitleType``        | 5-25 chars & != any special char | 5-char, 6-char, 15-char, 24-char, 25-char    |
| Equivalence Partitioning | Invalid ``TitleType``      | <5 or >25 chars                  | movi, 50-char                                |
| Boundary Value Analysis  | Valid Lower Bound          |                                  | 5-char                                       |
|                          | Valid Upper Bound          |                                  | 25-char                                      |
|                          | InValid Lower Bound        |                                  | 1-char, 4-char                               |
|                          | Invalid Upper Bound        |                                  | 26-char, str.max                             |
| Edges                    | edges cases                |                                  | NULL, "", " ", #asbas-char etc.              |
| Equivalence Partitioning | Valid ``PrimaryTitle``     | 5-255 chars and dash allowed     | 6-char, 50-char, 254-char, star-wars         |
| Equivalence Partitioning | Invalid ``PrimaryTitle``   | <5 or >255 chars                 | 4-char, 400-char                             |
| Boundary Value Analysis  | Valid Lower Bound          |                                  | 5-char                                       |
|                          | Valid Upper Bound          |                                  | 255-char                                     |
|                          | Invalid Lower Bound        |                                  | 1-char , 4-char                              |
|                          | Invalid Upper Bound        |                                  | 256-char , char.max                          |
| Edges                    | edges cases                |                                  | NULL, "", " ", #asbas-char etc.              |
| Equivalence Partitioning | Valid ``OriginalTitle``    | 5-255 chars and dash allowed     | Same as PrimaryTitle (identical constraints) |
| Equivalence Partitioning | Valid ``StartYear``        | 1888-2025                        | 1888, 1889, 1999, 2024, 2025                 |
| Equivalence Partitioning | Invalid ``StartYear``      | <1888 or >2025                   | 999, -2020, 2026                             |
| Boundary Value Analysis  | Valid Lower Bound          |                                  | 1888                                         |
|                          | Valid Upper Bound          |                                  | 2025                                         |
|                          | InValid Lower Bound        | 0 - 1887                         | 999, 1887                                    |
|                          | InValid Upper Bound        | 2026 - feature                   | 2026 - feature                               |
| Edges                    | edges cases                |                                  | 0000 ,-2020                                  |
| Equivalence Partitioning | Valid ``EndYear``          | NULL or >=StartYear              | 2026 > 2025, NULL                            |
| Equivalence Partitioning | Invalid ``EndYear``        | <StartYear or invalid year       | 9, 99, 999, 2010(with startYear=2025)        |
| Boundary Value Analysis  | Valid Lower Bound          | EndYear>=StartYear               | Start=2025/End=2025                          |
|                          | Valid Upper Bound          |                                  | Start=2025/End=2026                          |
|                          | InValid Lower Bound        |                                  | Start=2025/End=2024                          |
| Edges                    | edges cases                |                                  | 0000,9999, 2026                              |
| Equivalence Partitioning | Valid ``RuntimeMinutes``   | 60-1440 or NULL                  | 60, 61, 1000, 1439, NULL                     |
| Equivalence Partitioning | Invalid ``RuntimeMinutes`` |                                  | 50                                           |
|                          | Valid Lower Bound          | 60-1440                          | 60                                           |
|                          | Valid Upper Bound          |                                  | 1440                                         |
|                          | InValid Bound              | 0-59                             | 0, 59                                        |
|                          | InValid Bound              | 1441 - max                       | 1441, max                                    |
| Edges                    | edges cases                |                                  | -1, int.max                                  |

- List of test cases:
  - **``TitleType``:**
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
  - **``PrimaryTitle`` & ``OriginalTitle``:**
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
  - **``StartYear``:**
    - Valid:
      - 1888
      - 1889
      - 1999
      - 2024
      - 2025
    - Invalid:
      - 999
      - 1887
      - -2020
      - 2026
    - Edge Cases:
      - 0000
  - **``EndYear``:**
    - Valid:
      - 2026 endYear > 2025 startYear
      - NULL
    - Invalid:
      - 9
      - 99
      - 999
      - 2010 (with startYear=2025)
    - Edge Cases:
      - 0000
      - 9999
      - 2026 (when startYear=2025)
  - **``RuntimeMinutes``:**
    - Valid:
      - 60
      - 61
      - 1000
      - 1439
      - 1440
      - NULL
    - Invalid:
      - 0
      - 50
      - 1441
    - Edge Cases:
      - -1
      - int.max

## Genres

| Partition Type           | Partition         | Valid Range      | Test Case Values                                 |
|--------------------------|-------------------|------------------|--------------------------------------------------|
| Equivalence Partitioning | Valid ``Genre``   | 3-50 char        | 3-char, 2.char, 10-char, 49-char, 50-char Sci-Fi |
|                          | Invalid ``Genre`` | < 3 or >50 chars | 1-char, 2-char, 51-char 60-char                  |
| Boundary Value Analysis  | Valid Lower Bound |                  | 3-char                                           |
|                          | Valid Upper Bound |                  | 50-char                                          |
|                          | Invalid  Bound    | 1 - 2            | 1-char, 2-char                                   |
|                          | Invalid  Bound    | 51 - string.max  | 51-char, string.max                              |
| Edges                    | Genre edges       |                  | NULL, "", " ", #5-char                            |

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

| Partition Type           | Partition                 | Valid Range   | Test Case Values            |
|--------------------------|---------------------------|---------------|-----------------------------|
| Equivalence Partitioning | Valid ``EpisodeNumber``   | 1-999 & != 0  | 1, 2, 500, 998, 999         |
|                          | Invalid ``EpisodeNumber`` | < 1 or > 999  | -1, -5, 1000, 2000, int.max |
| Boundary Value Analysis  | Valid Lower Bound         |               | 1                           |
|                          | Valid Upper Bound         |               | 999                         |
|                          | Invalid Lower Bound       |               | 0                           |
|                          | Invalid Upper Bound       |               | 1000                        |
| Edges                    | Episode edges             |               | NULL, 0                     |
| Equivalence Partitioning | Valid ``SeasonNumber``    | 1 - 99 & != 0 | 1, 2, 55, 98, 99            |
|                          | Invalid Season            | < 1 or > 99   | 0, -1, -5.5, 100, 5.5, 501  |
| Boundary Value Analysis  | Valid Lower Bound         |               | 1                           |
|                          | Valid Upper Bound         |               | 99                          |
| Edges                    | Season edges              |               | NULL, 5.5, -5.5             |

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

| Partition Type           | Partition            | Valid Range                | Test Case Values                               |
|--------------------------|----------------------|----------------------------|------------------------------------------------|
| Equivalence Partitioning | Valid ``Region``     | 2-char - 5-char            | 2-char, 3-char, 4-char ,5-char                 |
|                          | Invalid Region       | < 2 or > 5                 | 1-char, 6-char, 150-char                       |
| Boundary Value Analysis  | Valid Lower Bound    |                            | 2-char                                         |
|                          | Valid Upper Bound    |                            | 5-char                                         |
|                          | Invalid Lower Bound  |                            | ""(empty string)                               |
|                          | Invalid Upper Bound  |                            | 6-char                                         |
| Edges                    | Region edges         |                            | NULL, " ", any special characters, unknown     |
| Equivalence Partitioning | Valid ``Language``   | 2-char - 5-char            | 2-char, 3-char, "en-us"                        |
|                          | Invalid ``Language`` | < 2 or > 5                 | 1-char, 6-char, 10-char                        |
| Boundary Value Analysis  | Lower Bound          |                            | 2-char                                         |
|                          | Upper Bound          |                            | 5-char                                         |
| Edges                    | edges                |                            | NULL, "", " "                                  |
| Equivalence Partitioning | Valid ``Title``      | 5-char - 255 & `-` allowed | 6-char, 50-char, 254-char, 250-char, star-wars |
|                          | Invalid ``Title``    | < 5 or > 255               | 3-char, 4-char, 256-char, 400-char, star#wars  |
| Boundary Value Analysis  | Lower Bound          |                            | 5-char                                         |
|                          | Upper Bound          |                            | 255-char                                       |
| Edges                    | edges                |                            | NULL, "", " ", #asbas-char etc.                |

- list of test cases:
  - **Region:**
    - Valid:
      - 2 characters 
      - 3 characters 
      - 4 characters 
      - 5 characters 
    -Invalid:
      - 1 character 
      - 6 characters 
      - 150 characters 
      - ""(empty string)
    - Edge Cases:
      - NULL
      - " "
      - any special characters e.g., #5-char
      - unknown
  - **Language:**
    - Valid:
      - 2-char
      - 3-char
      - "en-us"
    - Invalid:
      - 1-char
      - 6-char
      - 10-char
    - Edge Cases:
      - NULL
      - ""
  - **Title:**
    - Valid:
      - 6-char
      - 50-char
      - 254-char
      - 255-char
      - star-wars
    - Invalid:
      - 3-char
      - 4-char
      - 256-char
      - 400-char
      - star#wars
    - Edge Cases:
      - NULL
      - ""
      - " "
      - #asbas-char


## Person

| Partition Type           | Partition             | Valid Range                                 | Test Case Values                                         |
|--------------------------|-----------------------|---------------------------------------------|----------------------------------------------------------|
| Equivalence Partitioning | Valid ``Name``        | 2 - 100                                     | 2-char, 3-char, 50-char ,99-char                         |
|                          | Invalid ``Nam``e      | < 1 or > 100                                | 1-char, 100-char, 101-char, 150-char, str.max            |
| Boundary Value Analysis  | Valid Lower Bound     |                                             | 2-char                                                   |
|                          | Valid Upper Bound     |                                             | 100-char                                                 |
| Edges                    | Name edges            |                                             | NULL, " ", ""(empty string) , any special characters     |
| Equivalence Partitioning | Valid ``BirthYear``   | 1888 - newest year                          | 1995                                                     |
|                          | Invalid ``BirthYear`` | < 1888 or > year.now                        | 1, 19, 199 , 19999                                       |
| Edges                    | BirthYear edges       |                                             | NULL, 0000 , -2024                                       |
| Equivalence Partitioning | Valid ``EndYear``     | 4 numeric digit & null allow &  > birthyear | 2020                                                     |
|                          | Invalid ``EndYear``   |                                             | 1, 19, 199, 19999, ``EndYear`` (2025) > BirthYear (1945) |
| Edges                    | ``EndYear`` edges     |                                             | NULL, 0000                                               |

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
- **``EndYear``:**
  - Valid:
    - 2020
  - Invalid:
    - 1
    - 19
    - 199
    - 19999
    - ``EndYear`` (2025) > BirthYear (1945)
  - Edge Cases:
    - 0000
    - Null
  