# BackBox Analysis


## Black-box design techniques (manual analysis)

- Based on requirements, we will test the following cases:

### Title Test Cases

| Partition Type           | Partition                         | Test Case Values                |
|--------------------------|-----------------------------------|---------------------------------|
| Equivalence Partitioning | Valid TitleType                   | 6-char, 7-char, 24-char         |
| Equivalence Partitioning | Invalid TitleType                 | movi, 50-char                   |
| Boundary Value Analysis  | TitleType Valid Lower Bound       | 5-char                          |
|                          | TitleType Valid Upper Bound       | 25-char                         |
|                          | TitleType InValid Lower Bound     | 1-char, 4-char                  |
|                          | TitleType Invalid Upper Bound     | 26-char                         |
| Edges                    | TitleType edges cases             | NULL, "", " ", #asbas-char etc. |
| Equivalence Partitioning | Valid PrimaryTitle                | 6-char, 50-char, 254-char       |
| Equivalence Partitioning | Invalid PrimaryTitle              | 4-char, 400-char                |
| Boundary Value Analysis  | PrimaryTitle Valid Lower Bound    | 5-char                          |
|                          | PrimaryTitle Valid Upper Bound    | 255-char                        |
|                          | PrimaryTitle Invalid Lower Bound  | 1-char , 4-char                 |
|                          | PrimaryTitle Invalid Upper Bound  | 256-char , char.max             |
| Edges                    | PrimaryTitle edges cases          | NULL, "", " ", #asbas-char etc. |
| Equivalence Partitioning | Valid OriginalTitle               | 6-char, 50-char. 254-char       |
| Equivalence Partitioning | Invalid OriginalTitle             | 4-char, 400-char                |
| Boundary Value Analysis  | OriginalTitle Valid Lower Bound   | 5-char                          |
|                          | OriginalTitle Valid Upper Bound   | 255-char                        |
|                          | OriginalTitle Invalid Lower Bound | 1-char , 4-char                 |
|                          | OriginalTitle Invalid Upper Bound | 256-char , char.max             |
| Edges                    | OriginalTitle edges cases         | NULL, "", " ", #asbas-char etc. |
| Equivalence Partitioning | Valid StartYear                   | 1999, 2024                      |
| Equivalence Partitioning | Invalid StartYear                 | 999, -2020                      |
| Boundary Value Analysis  | StartYear Lower Bound             | 1888                            |
|                          | StartYear Upper Bound             | newest year                     |
| Edges                    | StartYear edges cases             | 0000                            |
| Equivalence Partitioning | Valid EndYear                     | 2005, 2023, 2026 > 2025, NULL   |
| Equivalence Partitioning | Invalid EndYear                   | 99, -2010                       |
| Boundary Value Analysis  | EndYear Lower Bound               | endYear 2025 = 2025 startYear   |
|                          | EndYear Upper Bound               | 9999                            |
| Edges                    | EndYear edges cases               | 0000                            |
| Equivalence Partitioning | Valid RuntimeMinutes              | 61, 1000, 1439, NULL            |
| Equivalence Partitioning | Invalid RuntimeMinutes            | 50                              |
|                          | Valid Lower Bound                 | 60                              |
|                          | Valid Upper Bound                 | 1440                            |
|                          | InValid Lower Bound               | 0                               |
|                          | InValid Upper Bound               | 1441                            |
| Edges                    | RuntimeMinutes edges cases        | -1, int.max                     |



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

| Partition Type           | Partition     | Test Case Values       |
|--------------------------|---------------|------------------------|
| Equivalence Partitioning | Valid Genre   | 10-char                |
|                          | Invalid Genre | 4-char, 60-char        |
| Edges                    | Genre edges   | NULL, "", " ", #5-char |

## Episodes

| Partition Type           | Partition           | Test Case Values |
|--------------------------|---------------------|------------------|
| Equivalence Partitioning | Valid EpisodeNumber | 2, 500, 998      |
|                          | Invalid Episode     | -1, 2000         |
| Boundary Value Analysis  | Valid Lower Bound   | 1                |
|                          | Valid Upper Bound   | 999              |
|                          | Invalid Lower Bound | 0                |
|                          | Invalid Upper Bound | 1000             |
| Edges                    | Episode edges       | NULL             |
| Equivalence Partitioning | Valid SeasonNumber  | 1, 2, 500        |
|                          | Invalid Season      | 0, -1, 501       |

## Aliases 











