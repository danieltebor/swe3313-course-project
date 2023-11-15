# Technical Design

## Languages
Hank's Mineral Emporium will be implemented with C#, HTML, and CSS. C# was chosen for cross-platform support and for its web framework, Blazor. In addition, the team has experience in writing Java, which is similar syntactically to C#. HTML and CSS are widely used for websites, and are also used with Blazor.

## Frameworks
The frameworks to be used are [ASP.NET](), [Blazor Server](), and [MudBlazor]().
- ASP.NET with Blazor server provides a streamlined way of creating a webserver in C# and HTML. Blazor Server can have performance issues when the server is under load from many users, but for a website with the scale of Hank's Mineral Emporium this should not be an issue. 
- MudBlazor provides nicely formatted components to be used in the webserver HTML, allowing for less custom-written CSS. MudBlazor also tends to look nicer than custom website formatting and has great documentation. 

## Entity Relation Diagram
The following ERD shows the entities that compose Hank's Mineral Emporium.

![ERD](/docs/technical-design/entity-relation-diagram.png)



## Coding Conventions
Microsoft is the creator of C# and as such their naming and coding conventions will be used. The Google style guide will be used for HTML and CSS.
- [C# Naming Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)
- [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [HTML and CSS Conventions](https://google.github.io/styleguide/htmlcssguide.html)

