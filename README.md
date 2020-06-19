<h1 align="center">
   KinderGarten
</h1>

<h4  align="center">  
  A web-based platform for a social network for kindergartens and parents.
</h4>

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
$ Add-migration Migration
```
```
$ Update-database
```
#### Final Step to run project

Setup Solution.web as a startup project.
