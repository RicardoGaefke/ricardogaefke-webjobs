# ricardogaefke-webjobs
Repo for Linux WebApp WebJobs

|Build status|Release status|
|---|---|
|[![Build Status](https://dev.azure.com/ricardogaefke/ricardogaefke-webjobs/_apis/build/status/ricardogaefke-webjobs?branchName=master)](https://dev.azure.com/ricardogaefke/ricardogaefke-webjobs/_build/latest?definitionId=28&branchName=master)|[![Release Status](https://vsrm.dev.azure.com/ricardogaefke/_apis/public/Release/badge/a6358287-c573-4beb-a1ea-21b82a762938/1/1)](https://vsrm.dev.azure.com/ricardogaefke/_apis/public/Release/badge/a6358287-c573-4beb-a1ea-21b82a762938/1/1)|

## How it works

This is a simple example of async integration using Azure WebJobs on an Azure Linux Docker Compose WebApp.
It has a simple form to send an XML file with your name and email. Inserted data is sent to server and you'll always receive an answer. If everything runs fine, you'll receive an email with your XML converted to a JSON file. Otherwise, you'll receive an error message.

[Azure WebJobs](https://docs.microsoft.com/pt-br/azure/app-service/webjobs-create) are background services that are perfect for services integration and/or long time tasks (like PDF generation and external API calls).

WebJobs run connected to Azure Storage Queues and one of the best resources (in my opinion) is the ability to use a poison queue. When a run fails after X times (default is five), the message is sent to a -poison queue. So if for any reason your message falls in an error, you can have a second WebJob connected to the -poison queue to handle the situation.

## GitHub Integration

The repo of this example can be found at [GitHub/RicardoGaefke/ricardogaefke-webjobs](https://github.com/RicardoGaefke/ricardogaefke-webjobs).

## Azure Pipelines

This application is automatically built, tested and deployed by Azure Pipelines. Each git push to master or to a PR is built and tested by Azure Pipelines. In case of master if the build is ok the Release takes place and updates the app using Docker integration.

This is the public [Pipeline](https://dev.azure.com/ricardogaefke/ricardogaefke-webjobs).

## Docker Registry

I am using Docker registry to create and publish the containers of this app using the Release Pipeline. There are two Docker repos being used here:

* 1 - [ricardogaefke-webjobs](https://hub.docker.com/r/ricardogaefke/ricardogaefke-webjobs)
This is used to save the containers used by app (webjob_site, webjob_nginx, webjob_xml, webjob_xml_poison).
* 2 - [ricardogaefke-webjobs-deploy](https://hub.docker.com/r/ricardogaefke/ricardogaefke-webjobs-deploy)
This repo is used just to trigger the Azure WebHook and update the WebApp after publishing all containers to previous Docker repo.

## Tech summary:

* [ASP.NET Core](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.1) - High-performance, open-source , multi-format framework for building modern Internet-connected and cloud-based applications.
* [Azure](https://docs.microsoft.com/pt-br/azure/) - A cloud plataform.
* [Azure Pipelines](https://docs.microsoft.com/pt-br/azure/devops/pipelines/?view=azure-devops) - Implement continuous integration and continuous delivery (CI/CD) for the app and platform of your choice.
* [Azure SQL](https://azure.microsoft.com/pt-br/services/sql-database/campaign/#documentation) - Azure SQL Server.
* [Azure Storage](https://docs.microsoft.com/pt-br/azure/storage/blobs/) - Azure Storage.
* [Azure WebApp](https://docs.microsoft.com/pt-br/azure/app-service/overview) - A modern web app service that offers streamlined full-stack development from source code to global high availability.
* [Azure Webjobs](https://docs.microsoft.com/pt-br/azure/app-service/webjobs-create) - Azure Webjobs.
* [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/) - Language C# to .NET Platform.
* [Docker](https://docs.docker.com/) - Platform for developers and sysadmins to build, run, and share.
* [JQuery](https://api.jquery.com/) - A fast, small, and feature-rich JavaScript library.
* [Linux](https://linux.die.net/) - Linux documentation.
* [NGINX](https://nginx.org/en/docs/) - Free, open-source, high-performance HTTP server, reverse proxy, and IMAP/POP3 proxy server.
* [Semantic UI](https://semantic-ui.com/introduction/getting-started.html) - Interface integration and performance.
* [Typescript](https://www.typescriptlang.org/) - Typed superset of JavaScript that compiles to plain JavaScript.

## Contributing

Please read [CONTRIBUTING.md](https://github.com/RicardoGaefke/ricardogaefke-webjobs/blob/master/CONTRIBUITING) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Ricardo Gaefke** - *Initial work* - [RicardoGaefke](https://github.com/RicardoGaefke)

## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/RicardoGaefke/ricardogaefke-webjobs/blob/master/LICENSE) file for details.

