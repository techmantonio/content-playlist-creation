# Content Playlist Creation

[Problem Statement Document](https://github.com/techmantonio/content-playlist-creation/blob/master/documents/Antonio_McMichael_%20SAP_Take_Home_Coding_Exercise_%20Content_Playlist_Creation_(Prerolls).docx)

## Assumptions
- All "name" properties are being used as unique identifiers.
- Currently only one content video can be associated with a playlist.
- A content video can be a part of a playlist without any pre-rolls.
- A pre-roll must be associated with content and cannot have its own playlist.

## Execution Instructions
1. Extract the folder [ContentPlaylistCreation.zip](https://github.com/techmantonio/content-playlist-creation/blob/master/ContentPlaylistCreation.zip)
2. Navigate to the directory containing the executable ContentPlaylistCreation.exe.
3. Invoke the executable ContentPlaylistCreation.exe and pass in the parameters defined in the following table:

Argument Name | Position | Required | Example
--- | --- | --- | ---
Input File Path | 0 | yes | test-content-data-1.json
Content Id | 1 | yes | MI3
Country Code | 2 | yes | UK

## Example Execution
```
ContentPlaylistCreation.exe "source\ContentPlaylistCreation.UnitTests\TestInputFiles\test-content-data-1.json" MI3 UK
```

## Test Results
Unit test results.

![Unit Test Pass](https://github.com/techmantonio/content-playlist-creation/blob/master/documents/unit-test-pass.png "Unit Test Pass")