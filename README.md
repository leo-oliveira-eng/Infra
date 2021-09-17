# Infra ![.NET Core](https://github.com/leo-oliveira-eng/Infra/workflows/.NET%20Core/badge.svg?branch=master) [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) [![NuGet](https://img.shields.io/nuget/vpre/Infra.BaseRepository)](https://www.nuget.org/packages/Infra.BaseRepository)

Package that encapsulate the implementation of the repository pattern using EF core with Unit of Work.

## Installation

Infra.BaseRepository is available on [NuGet](https://www.nuget.org/packages/Infra.BaseRepository/). You can find the raw NuGet file [here](https://www.nuget.org/api/v2/package/Infra.BaseRepository/1.0.0-pre-2) or install it by the commands below depending on your platform:

 - Package Manager
```
pm> Install-Package Infra.BaseRepository -Version 1.0.0-pre-2
```

 - via the .NET Core CLI:
```
> dotnet add package Infra.BaseRepository --version 1.0.0-pre-2
```

 - PackageReference
```
<PackageReference Include="Infra.BaseRepository" Version="1.0.0-pre-2" />
```

 - PaketCLI
```
> paket add Infra.BaseRepository --version 1.0.0-pre-2
```

## Setup

To perform the configuration and ensure proper dependency injection for Unit of Work, just include the following code on the startup of your project or in the class you are using for this purpose:

```csharp

...

services.AddScoped<IUnitOfWork, UnitOfWork<YourContext>>();

...

```

## Hout to Use

### Repository

The IRepository interface already has the basic methods of a CRUD that are implemented by Repository. IRepository contract is shown below.

```csharp
public interface IRepository<T> where T : Entity
{
    Task AddAsync(T entity);
    Task<Maybe<T>> FindAsync(Guid code);
    Task<List<T>> GetAllAsync();
    Task RemoveAsync(T entity);
    Task UpdateAsync(T entity);
}

```

Class implementation is performed as follows:

```csharp

public class YourClassRepository : Repository<YourClass, YourContext>, IYourClassRepository
{
    public YourClassRepository(YourContext context) : base(context) { }

    // Methods
}

```

### Unit of Work

After performing any operation in the database, use the Unit of Work to commit these changes.

```csharp

...

  await YourClassRepository.UpdateAsync(entity);

  if (!await Uow.CommitAsync())
      return response.WithCriticalError("Failed to try to update the entity");

...

```

## License [![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fleo-oliveira-eng%2FInfra.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Fleo-oliveira-eng%2FInfra?ref=badge_shield)
The project is under [MIT License](LICENSE.md), so it grants you permission to use, copy, and modify a piece of this software free of charge, as is, without restriction or warranty.

[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fleo-oliveira-eng%2FInfra.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Fleo-oliveira-eng%2FInfra?ref=badge_large)
