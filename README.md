<h1 align="center">
   KinderGarten
</h1>

<h4  align="center">  
  A web-based platform for a social network for kindergartens and parents.
</h4>

### :speech_balloon: Introduction
We suggest a set up of a social network for kindergartens and parents. The application must offer parents to register for a kindergarten, communication kindergarten / parents and parents / parents.

With other services :
- **KinderGarten Space** : Profile Management, Communicate with Parents, Statistics and dashboard..
- **Parent Space** : Profile Management, Event Participation, Carpooling, FeedBacks, Search, Rating..
- **News feed, chat, and messaging** : Posts, Comments, Messages, and Notifications..
- **Admin Space** : KinderGarten, Claim Management, Reputation, Statistics..

## Getting started !

### :gear: Configuration 

#### Set up your environment

You must download Visual Studio 2019, During instalation you must tick DotNet Web, DotNet Desktop, Sql Server.

#### Database Configuration
1. Delete Migration Folder in Solution.Data .
2. Run these commands in Package Console Manager .
```
$ Enable-Migration
```
```
$ Add-migration migration_name
```
```
$ Update-database
```
#### Setup Solution.web as a startup project

Right click on the Solution Web and choose "Setup as a startup project"

#### Rebuild Solution

Right Click on Solution'JardinD'Enfant' and choose Rebuild Solution.

#### Run IIS Express

The Login page runs in your default Browser.

### :open_book: Result
<p align="center">
<img src="https://user-images.githubusercontent.com/47121168/85171006-7fe3be80-b26e-11ea-9a93-58ccdadccbb9.PNG" width="750"/> 
</p>
Hint: Using RestFul Api we did a JEE Application <a href="https://github.com/ihebsd/KinderGarten-JEEPart">`KinderGarten_JEEPART`</a> ! :sparkles:

