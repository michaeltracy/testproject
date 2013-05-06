Test Project
============

This project is my own research into various tools and utilities for testing within the .NET Framework. I created two sample projects to provide something to test:

- TestProject: a small (and quite simple) calculator application, used as a base for testing UI Automation, and BDD frameworks.
- Dexter: an address-book application, used to test applications which interact with a dabase. I intentionally avoided using LINQ or Entity Framework, so I could mimic testing legacy applications that did not use ORM.

For this study, I was examining the following frameworks:

- [NSpec] (http://nspec.org/)
- [SpecFlow] (http://www.specflow.org/specflownew/)
- [White] (https://github.com/TestStack/White)
- Microsoft UI Automation
- [FactoryGirl.NET] (https://github.com/JamesKovacs/FactoryGirl.NET)
- [Plant] (https://github.com/jbrechtel/plant)
- [Massive] (https://github.com/robconery/massive)

Recommendation
--------------

Testing just about everything in the .NET framework is not only possible, but relatively easy. Most of the tools required are already included with .NET and Visual Studio, and can easily integrate with the existing workflow, testrunner, tools, etc.

### UI Testing

For User Acceptance Testing or verifying logic tied to a UI, I recommend using the Microsoft UI Automation Framework--not to be confused with the Coded UI Test Framework. 

The Coded UI Test Framework does not produce maintainable code, and the UI Automation Framework requires less effort to extend. 

White is a very good of what a UI Test Framework should look like, but I was able to get the same functionality out of the UI Automation Framework by spending no more than an hour creating extensions and a helper Screen class. 

Provided you can spend the couple of hours building the extensions for the UI Automation Framework, the Microsoft utility provides the best mix of maintainability, functionality, and cost.

### Touching the Database

When data becomes a required component for a test, three things have to happen:

1. Create sample data
2. Verify the data added as part of the test
3. Undo/rollback any changes made after the test completes

While this can be accomplished by using existing data-access components (ORM, in-house DAO, or even inline SQL), this can be very time consuming and result in using the very class under test for verification! For the purposes of this study, I was looking for tools that would accomplish the above three goals in as little time as possible.

_Plant_ is a good example of the Object Mother pattern in the .NET framework. Usage is simple, it allows for both constructor and property object initialization, and includes ability to persist objects via a callback to another utility. Plant is slightly lacking in documentation, but the framework is small enough that I could fill in the gaps by reading the source code.

_Massive_ is database access made incredibly small. By making use of .NET 4.0's dynamic and ExpandoObject, Massive allowed me to quickly insert, retrieve, and verify database interaction--all nearly inline with test code. Massive is so small that I didn't even use a DLL reference; I included the source code directly in my project. If one was forced to, a developer could reproduce Massive.cs in an afternoon.

_Transactions_ seem to be the .NET way of undoing database changes on demand. Unfortunately, PostgreSQL and Npgsql have limited support for transactions, and I had to manually create and dispose transactions in this study. When working with SQL Server 2008 and running MSDTC, creating a TransactionScope in my test setup and disposing the scope in my test tear down ensured that I could Insert, Update, and Query the test database while returning the database to its original state at the end of the test.

### Behavor-Driven Development

While not necessary for testing, I see BDD being very valuable as testing moves from Unit Testing to Integration and Acceptence testing. I see SpecFlow as the best tool to help in that transition.

SpecFlow did aid in code resuse while I was working with it. After defining one scenario (a.k.a. a test case), I was able to define additional cases in minutes.

The major benefit of SpecFlow is the readability of the Feature files. They are designed in a way that the non-technical members of your team can understand them. In other words, tests that your testers, your managers, and maybe even your customer can read.

## Other Frameworks

### NSpec

NSpec is heavily based on the Ruby framework RSpec, and showed how much I could do with as little code as possible. NSpec does this by "nesting" tests, which allows to to build up a very complicated test case out of many smaller, simpler tests. There is almost no duplication, and when something big fails, you will know exactly why.

On the downside, NSpec tries very, very hard to be Ruby, and heavily makes use of lambdas to to this. NSpec also uses its own test runner (which also tries to act like RSpec) which does not integrate with Visual Studio as easily as SpecFlow does.

If you are a Ruby on Rails developer, you will feel right at home with NSpec. If you're a veteran .NET developer, NSpec's syntax and testrunner could be difficult to learn and integrate with your workflow.

### White

White is easy to work with, and is almost exactly what I am looking for in a UI testing/automation library. The only real downside is, since White is built off of the Microsoft UI Automation Framework, there really is nothing that White does that MSAA doesn't do.

If you are just starting with UI testing in .NET, White is a good framework to practice with. If you're not afraid of writing some extensions, UI Automation will get the job done just was well, and without external dependancies.

### FactoryGirl.NET

Pulling on my experience with Ruby on Rails, FactoryGirl.NET was the logical choice to start working with an Object Mother utility to create sample data. Unfortunately, FactoryGirl in C# just doesn't really work.

First, the project is not feature complete. There is no way to persist objects to the database, and constructor arguments are not very well supported. Second, FactoryGirl.NET tries to organize the factories just like Ruby, which is a poor fit in C#'s classes. In other words, I did not find a good place to put the factories.

Find a better Object Mother, or just make your own. The pattern is not too difficult.

Conclusion
----------

Use UI Automation, Plant, and Massive (or some other dynamic data library); you should be able to test just about anything.

This study has been completed as of May 1, 2013. I will leave this up for as long as github gives me the space.
