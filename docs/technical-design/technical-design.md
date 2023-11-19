# Technical Design

## Table of Contents
- [Languages](#languages)
- [Frameworks](#frameworks)
- [Entity Relation Diagram](#entity-relation-diagram)
- [Class Diagrams](#class-diagrams)
- [Coding Conventions](#coding-conventions)

## Languages
Hank's Mineral Emporium will be implemented with [C#](https://learn.microsoft.com/en-us/dotnet/csharp/), [HTML](https://developer.mozilla.org/en-US/docs/Web/HTML), and [CSS](https://developer.mozilla.org/en-US/docs/Web/CSS). C# was chosen for cross-platform support and for its web framework, [Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor). In addition, the team has experience in writing Java, which is similar syntactically to C#. HTML and CSS are widely used for websites, and are also used with Blazor.

## Frameworks
Hanks Mineral Emporium will also be implemented with the following frameworks:
- [ASP.NET](https://learn.microsoft.com/en-us/aspnet/overview) with [Blazor Server](https://learn.microsoft.com/en-us/aspnet/core/blazor/hosting-models) provides a streamlined way of creating a webserver in C# and HTML. Blazor Server can have performance issues when the server is under load from many users, but for a website with the scale of Hank's Mineral Emporium this should not be an issue. 
- [MudBlazor](https://github.com/MudBlazor/MudBlazor) provides nicely formatted components to be used in the webserver HTML, allowing for less custom-written CSS. MudBlazor also tends to look nicer than custom website formatting and has great documentation. 

## Entity Relation Diagram
The following ERD shows the entities that compose Hank's Mineral Emporium.

![ERD](/docs/technical-design/entity-relation-diagram.png)

### Fields
The following are the fields for each entity. PK stands for Primary Key, FK stands for Foreign Key.

#### User
| Property | Type    | Default | Nullable | Relationship           | Notes  |
|----------|---------|---------|----------|------------------------|--------|
| userID   | int, PK |         | No       | Related to<br> Receipt |        |
| username | string  |         | No       |                        | Unique |
| password | string  |         | No       |                        | Hashed |
| isAdmin  | bool    | false   | No       |                        |        |

#### Receipt
| Property  | Type    | Default | Nullable | Relationship        | Notes |
|-----------|---------|---------|----------|---------------------|-------|
| receiptID | int, PK |         | No       | Related to<br> Sale |       |
| userID    | int, FK |         | No       | Related to<br> User |       |
| shipping  | decimal | 0.00    | No       |                     |       |
| tax       | decimal | 0.00    | No       |                     |       |
| total     | decimal | 0.00    | No       |                     |       |

#### Item
| Property    | Type    | Default | Nullable | Relationship        | Notes |
|-------------|---------|---------|----------|---------------------|-------|
| itemID      | int, PK |         | No       | Related to<br> Sale |       |
| name        | string  |         | No       |                     |       |
| price       | decimal | 0.00    | No       |                     |       |
| description | string  |         | Yes      |                     |       |
| imagePath   | string  |         | No       |                     |       |

#### Sale
| Property  | Type    | Default | Nullable | Relationship           | Notes |
|-----------|---------|---------|----------|------------------------|-------|
| saleID    | int, PK |         | No       |                        |       |
| itemID    | int, FK |         | No       | Related to<br> Item    |       |
| receiptID | int, FK |         | No       | Related to<br> Receipt |       |

### Example Data
The following is an example of the data in the database and the data types.

#### Users
| userID *PK | username | password | isAdmin |
|------------|----------|----------|---------|
| 1          | dtebor   | H#ank''s | true    |
| 2          | mingram  | Awes0me  | false   |
| 3          | zgrey    | M1nEr#l  | false   |
| 4          | wpitts   | Emp0r1um | false   |

#### Receipts
| RecieptID *PK | userID *FK | shipping | tax  | total |
|---------------|------------|----------|------|-------|
| 1             | 1          | 0.00     | 0.30 | 5.30  |
| 2             | 3          | 29.00    | 2.52 | 44.52 |

#### Items
| itemID *PK | name     | price | description | imagePath                            |
|------------|----------|-------|-------------|--------------------------------------|
| 1          | Amethyst | 5.00  | Purple      | assets/inventory_images/amethyst.png |
| 2          | Quartz   | 3.00  | Clear       | assets/inventory_images/quartz.png   |
| 3          | Ruby     | 10.00 | Red         | assets/inventory_images/ruby.png     |

#### Sales
| saleID *PK | itemID *FK | receiptID *FK |
|------------|------------|---------------|
| 1          | 1          | 1             |
| 2          | 3          | 2             |
| 3          | 2          | 2             |

### Seed Data
The following represents the seed data that will be used to populate the database.

#### Users
| userID *PK | username | password | isAdmin |
|------------|----------|----------|---------|
| 1          | admin    | password | true    |
| 2          | nonAdmin | psswrd22 | false   |
| 3          | admin2   | 123456   | true    |

#### Receipts
| RecieptID *PK | userID *FK | shipping | tax | total |
|---------------|------------|----------|-----|-------|
| -             | -          | -        | -   | -     |

#### Items
| itemID *PK | name      | price | description                                                         | imagePath                               |
|------------|-----------|-------|---------------------------------------------------------------------|-----------------------------------------|
| 1          | Amethyst  | 5.00  | - Total Weight: 150g<br> - Count: 1<br> - Pure purple color         | ![Amethyst](https://rb.gy/ia1g1l)       |
| 2          | Quartz    | 3.00  | - Total Weight: 200g<br> - Count: 1<br> - Partly cloudy             | ![Quartz](https://rb.gy/er99ix)         |
| 3          | Ruby      | 10.00 | - Total Weight: 190g<br> - Count: 1<br> - Roughly cut               | ![Ruby](https://rb.gy/ro2ire)           |
| 4          | Pyrite    | 2.00  | - Total Weight: 230g<br> - Count: 1<br> - Unique shape              | ![Pyrite](https://shorturl.at/hkEMU)    |
| 5          | Hematite  | 6.00  | - Total Weight: 250g<br> - Count: 13<br> - Homogeneous shape        | ![Hermatite](https://rb.gy/xwv0ij)      |
| 6          | Calcite   | 20.00 | - Total Weight: 750g<br> - Count: 10<br> - Varied color, shape      | ![Calcite](https://rb.gy/s1xnci)        |
| 7          | Gypsum    | 9.00  | - Total Weight: 650g<br> - Count: 6<br> - Varied color, shape, size | ![Gypsum](https://shorturl.at/goJ25)    |
| 8          | Feldspar  | 8.50  | - Total Weight: 260g<br> - Count: 1                                 | ![Feldspar](https://shorturl.at/czBIX)  |
| 9          | Magnetite | 9.25  | - Total Weight: 180g<br> - Count: 3<br> - Geometric cuts            | ![Magnetite](https://shorturl.at/dijwJ) |
| 10         | Topaz     | 15.00 | - Total Weight: 210g<br> - Count: 1<br> - Glass-like clarity        | ![Topaz](https://shorturl.at/rBDG0)     |

#### Sales
| saleID *PK | itemID *FK | receiptID *FK |
|------------|------------|---------------|
| -          | -          | -             |

## Class Diagrams
The class diagrams for HanksMineralEmporium can be found [here](/docs/technical-design/class-diagram.md).

## Data Storage
JSON will be used to store data for HanksMineralEmporium. For the size of HanksMineralEmporium, JSON will be sufficient. Newtonsoft.Json will be used to serialize and deserialize the JSON data. There will be a separate JSON file for Users, Items, Receipts, and Sales.

## Coding Conventions
Microsoft is the creator of C# and as such their naming and coding conventions will be used. The Google style guide will be used for HTML and CSS.
- [C# Naming Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)
- [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [HTML and CSS Conventions](https://google.github.io/styleguide/htmlcssguide.html)

In addition, the following conventions will be used:
- Code will only be commented if it is not self-explanatory.
- To make changes to the project, a new branch will be created from the `main` branch. The branch will be named `feature-<feature-name>` or `bugfix-<bug-name>`. When the feature or bugfix is complete, a pull request will be created and the code will be reviewed by another team member. Once the code is approved, it will be merged into `main`.
- The `main` branch will always be in a working state. If a feature or bugfix is not complete, it will be pushed to a branch other than `main`.
- Prototype code will be written in a `prototype-<prototype-name>` branch. This code will not be merged into `main`.
