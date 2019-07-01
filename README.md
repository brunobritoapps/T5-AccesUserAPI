# 5-AccessUserAPI

This project was generated with [.NET CORE] version 2.1.1 [EntityFramework] version 2.1.4 [Microsoft SQLSERVER] version 17

## Solution
<p>This solution contains 4 projects<p>
  @ API
    <p>This api captures the user's browsing behavior (ip, page, params, browse-version) with each new reload.page<p>
  @ APP
    <p>The APP project consumes the API through static pages HTML and JavaScript requests<p>
  @ WEB
    <p>The WEB project consumes the API through a Web Application .NET Core Project (cshtml-pages) and JavaScript requests<p>
  @ JOB
    <p>The JOB project executes an export (csv) of the user behavior data stored in the SQL SERVER base scheduled according to time-date<p>

## Install dependencies

Run in `NUGET Package Manager` Project Dependencies to install project libraries

## Create Update-Database

Create Upgrade Database from Project API/Data/Migrations/_InitialCreate.cs'

