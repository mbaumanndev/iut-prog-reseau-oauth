À propos
========

Ce dépôt contient du matériel pédagogique à destination des étudiants de LP RGI à l'IUT Informatique d'Amiens.

Pré-requis
----------

Pour utiliser ce dépôt, vous aurez besoin de :

- .NET Core SDK 3.0 ou plus

Utilisation
-----------

Pour ce cours, nous utiliserons la ligne de commande `dotnet`. Nous aurons donc besoin de quatre terminaux.

Pour lancer le serveur d'identité :

```
dotnet run --project .\src\IutAmiens.ProgReseau.IdentityServer\
```

Pour lancer l'API de démo :

```
dotnet run --project .\src\IutAmiens.ProgReseau.ApiExemple\
```

Pour lancer l'application console de test :

```
dotnet run --project .\src\IutAmiens.ProgReseau.Client\
```

Pour lancer l'application web :

```
dotnet run --project .\src\IutAmiens.ProgReseau.WebUi\
```
