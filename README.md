# RTL

RTL assignment where you need to scrape TVMaze, persist the data in a storage and make it public using a REST API.
<br>For more information about the assignment check: `assignment.pdf`

## Installation 
1. Build the NuGet packages
2. Make sure there is a Sql Server database available. I used a Docker container https://hub.docker.com/_/microsoft-mssql-server
3. Replace the connection string with your own credentials in the two json setting files `WebApi/appsettings.json` and `Scraper/appsettings.json`
5. Run the `Scraper` console project once for scraping TVMaze (I only scrape the top 250 shows). The `Scraper` project will let the `Domain` run the migrations for you.
6. Run the `WebApi` project
<br><br><br>

## Approach
I started off with building the `Scraper` and `Domain` project. I went for an IoC approach, because I like to work in an environment where there is a loosely coupled design (Thanks to Angular ;) ). I created seperate models for the `Scraper` and `Domain` project and used Automapper for object mapping, because if a property is changed in the TVMaze API I only need to change the mapper.
The `Domain` project uses the entity framework as ORM and makes the data available through the repository pattern. Because it is easier to add extra functionality to the program and to test, and it encapsulate the logic to access data.
Next, the `WebAPI` project use also IoC and configure the `Services` project. The `Services` project than configure the `Domain` project. The API uses a different model than the `Domain` models, so it is decoupled. Automapper is used again for object mapping. The ordering of the birthday is done in the `Services` project because it is a memory based operation. In my opinion the repositories need to be occupied with data access and not with memory operations.
The `Common` project is for the paging options. I created a separate project for this, because it belongs in multiple projects (Domain, Services and WebApi).

Time spent: 5 hours.

![Diagram](diagram.png)
