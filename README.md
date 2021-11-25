![Nuget Version](https://img.shields.io/nuget/v/DougMurphy.TargetFrameworks.EndOfLife?label=nuget.org)
![Nuget Downloads](https://img.shields.io/nuget/dt/DougMurphy.TargetFrameworks.EndOfLife)

# Introduction

Knowing if your Target Framework Moniker (TFM) is End of Life (EOL) or not can be very important in certain circumstances.
Maybe your company requires using supported target frameworks for compliance reasons, maybe you're publishing a NuGet package and want to make sure you're only supporting frameworks that Microsoft supports, or maybe you just want to make sure you're staying up-to-date.
No matter the reason, this NuGet package can help you out with that.

# Methods

## GetAllEndOfLifeTargetFrameworkMonikers
By default, this method will get all TFMs that are currently EOL. Two overloads are provided where you can either pass in an explicit `DateTime` to get all TFMs that will be EOL by that date, or a `TimeframeUnit` with an amount to forecast out to a date relative to current date.

## CheckTargetFrameworkForEndOfLife
This method takes in a singular TFM or semicolon-delimited pluralized TFM and returns back which one(s), if any, of them are currently EOL.

# Example Usage

## Checking if a specific TFM is EOL
```c#
using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using System;

public void CheckIfSingleTfmIsEol() {
    var tfm = "net5.0";
    TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfm);
    
    if (endOfLifeResults.EndOfLifeTargetFrameworks.Count == 0) {
        //the specified TFM is not EOL
    }
    else {
        //the specified TFM is EOL
        foreach (string endOfLifeTargetFramework in endOfLifeResults.EndOfLifeTargetFrameworks) {
            Console.WriteLine(endOfLifeTargetFramework);
        }
    }
}

public void CheckIfMultipleTfmIsEol() {
    var tfm = "net5.0;net45";
    TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfm);
    
    if (endOfLifeResults.EndOfLifeTargetFrameworks.Count == 0) {
        //none of the specified TFMs are EOL
    }
    else {
        //at least one of the specified TFMs are EOL
        foreach (string endOfLifeTargetFramework in endOfLifeResults.EndOfLifeTargetFrameworks) {
            Console.WriteLine(endOfLifeTargetFramework);
        }
    }
}
```

## Getting all TFMs that are currently EOL
```c#
using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using System;

public void GetAllTfmsThatAreEol() {
    TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers();
    
    foreach (string endOfLifeTargetFramework in endOfLifeResults.EndOfLifeTargetFrameworks) {
        Console.WriteLine(endOfLifeTargetFramework);
    }
}
```

## Getting all TFMs that are EOL within a specified forecasted date
```c#
using DougMurphy.TargetFrameworks.EndOfLife.Enums;
using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using System;

public void GetAllTfmsThatAreEolWithinOneYear() {
    //Get all EOL TFMs that are currently EOL, or will be EOL within 1 year of current date.
    TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Year, 1);
        
    foreach (string endOfLifeTargetFramework in endOfLifeResults.EndOfLifeTargetFrameworks) {
        Console.WriteLine(endOfLifeTargetFramework);
    }
}
```

# Web API Host
If you don't have something set up to automatically update your NuGet packages, and you don't want to worry about updating this one as new TFMs and EOL dates are added, check out [my API on GitHub](https://github.com/Doug-Murphy/EndOfLifeApi) for hassle-free updates!