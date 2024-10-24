﻿## Kapaix Mini Project: Energy Data Scraper

### Overview

This project is a simple web scraping tool built using C# and Microsoft Playwright. It extracts hourly energy capacity data from the BritNed website for electricity transmission between Great Britain (GB) and the Netherlands (NL) and saves the data in CSV format.

### Features

Automated Web Scraping using Playwright.
Time Zone Conversion from CET/CEST to UTC.
Data Export to CSV format, saved automatically to the user's Documents folder.

### How to run

.NET SDK: Download and install from Microsoft .NET.
Playwright for .NET

### Clone the repository

```bash
git clone https://github.com/SamuelOdigie/Kapaix-mini-project.git
cd Kapaix-mini-project
code .
```

### Then add playwright

```bash
dotnet add package Microsoft.Playwright
playwright install
```

### Build then run

```bash
dotnet build
dotnet run

```

### Setting the Date

You can set the date for which you want to scrape data by modifying the `desiredDate` variable in the `Program.cs` file. For example:

```csharp
DateTime desiredDate = new DateTime(2024, 10, 07); // Change the date as needed
```

### Example output on console and Csv(optional):

```bash
      UTC        GB-NL        NL-GB
22:00 - 23:00    1055         1055
23:00 - 00:00    1049         1049
00:00 - 01:00    1052         1052

```
![image](https://github.com/user-attachments/assets/67b934e1-2b14-4b5b-a724-a907211aa610)
