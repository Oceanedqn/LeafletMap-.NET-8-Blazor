# Tutorial How to use Leaflet Map on .NET 8 with Blazor

## Steps (english)
#### Creating a blazor project in .NET 8. In my case, I'm using interactive mode.
I choose "blazor web app", then move on to the next step.
<br> ![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/19f2e776-634e-49bf-9d2b-ad9c1f629705)

#### File creation
In the client part of the project, I create three new files in the /Pages folder.
* Leaflet.razor
* Leaflet.razor.cs
* Leaflet.razor.js

##### Leaflet.razor
I add the line below. It will be enough to display the map
<br>`<div id="map" style="width: 75vw; height: 70vh;"></div>`
<br><br> You must also add the following tags at the very top of the file:
<br> `@page "/map"`
<br> `@rendermode InteractiveAuto`

##### Leaflet.razor.cs
It's not necessary to use a .razor.cs file, as it's possible to write the code in the @code{} tag on the .razor file. However, the best practice is to create a .razor.cs file to separate the display part from the code part.
<br><br> In this file, I use IJSRunetime to invoke functions in javascript.

##### Leaflet.razor.js
I use a function to force the position request window in the browser ("requestLocationPermission").
<br> I assign coordinates to the map and display it ("loadMap").

## Etapes (français)
#### Création du projet blazor en .NET 8. Dans mon cas j'utilise l'interactive mode.
Je choisis "blazor web app", puis je passe à l'étape suivante.
<br> ![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/19f2e776-634e-49bf-9d2b-ad9c1f629705)

#### Création du fichier
Dans la partie client du projet, je crée trois nouveaux fichiers dans le dossier /Pages. 
* Leaflet.razor
* Leaflet.razor.cs
* Leaflet.razor.js

##### Leaflet.razor
J'ajoute la ligne ci-dessous. Elle suffira pour afficher la map
<br>`<div id="map" style="width: 75vw; height: 70vh;"></div>`
<br><br> Il faut également ajouter les balises suivantes tout en haut du fichier :
<br> `@page "/map"`
<br> `@rendermode InteractiveAuto`

##### Leaflet.razor.cs
Il n'est pas obligé d'utiliser un fichier .razor.cs parce qu'il est possible d'écrire le code dans la balise @code{} sur le fichier .razor. Cependant, la bonne pratique consiste à créer un fichier .razor.cs afin de séparer correctement la partie affichage de la partie code. 
<br><br> Dans ce fichier, j'utilise IJSRunetime pour invoquer les fonctions en javascript.

##### Leaflet.razor.js
J'utilise une fonction pour forcer la fenêtre de demande de position dans le navigateur ("requestLocationPermission").
<br> J'assigne des coordonnées à la map et je l'affiche ("loadMap").

## Environment used
Here's a guide how to implement Leaflet on blazor in .NET 8.
On this version, I use <b>Interactive Auto</b>

![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/8322dcf0-f5f5-42f8-8181-6a55153a938f)


## Important element
### In .razor page
Add the page rendering mode below the @page tag in .razor file, otherwise, the page will remain static
</br> In our case, I added :
</br> <center> ![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/36949c02-87fa-4aa0-a002-d0ac90f5dfcc) </center>

## Problems encountered :
* At the moment "edge" doesn't always display the correct location, whereas "firefox" does. I think the problem comes from the vpn or the browser settings.

## Site overview
![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/4cd4f577-2598-4103-89e5-d5f3cc346a17)


## Documentation used
* Leaflet : https://leafletjs.com/
* Render mode : https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0
