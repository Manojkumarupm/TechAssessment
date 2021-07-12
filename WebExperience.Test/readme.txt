Solution contains two idependent solution. Kindly take below action before running the clientApp & solution

1. Please build the application once WebExperience.Test and make sure all nuget package dll's get's updated. If any error related to ClietApp can be ignored.
Please build the application once again so that errors will go way.

2. Navigate under ClientApp folder (Open that in explorer) or Open the solution in Visual Studio code tool
Folder reference sample C:\Users\~\TechAssessment\WebExperience.Test\ClientApp

Navigate under New Terminal option and do run below command so that it will install necessary folders, files under node_modules
> npm install
Once after the successfull installation kindly run the below command to run the client application
> ng serve
or you can run the same using

> ng build --watch in command prompt (cmd)

Other instructions for running Client App
# ClientApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 9.1.3.

Client Application should be cloned to local. Once after the cloning kindly use npm install to install all the dependency files



Angular UI -> request for Asset List (passing page No, Page Size and sortOrder ) and data will be fetched based on the given input
All the data send to Angular UI but only needed data presented to end users.


Kindly use chrome with below options to experience full functional testing. Thank you!

chrome.exe --disable-web-security --user-data-dir="c:/ChromeDevSession"

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
