# Tutorial How to use Leaflet Map on .NET 8 with Blazor
Welcome

## Environment used
Here's a guide to using Leafler on blazor in .NET 8.
On this version, I use <b>Interactive Auto</b>

![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/8322dcf0-f5f5-42f8-8181-6a55153a938f)
<i></br> Doc : https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0 </i>


## Important element
### In .razor page
Add the page rendering mode below the @page tag in .razor file, otherwise, the page will remain static
</br> In our case, I added :
</br> <center> ![image](https://github.com/Oceanedqn/LeafletMap-.NET-8-Blazor/assets/33551018/36949c02-87fa-4aa0-a002-d0ac90f5dfcc) </center>

### In App.razor
Add the following lines for the css of leafler map
</br> <b> In head </b> : 
* link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"

</br> <b> In body </b> : 
* script src="./Pages/Leaflet.razor.js" (./Pages/NameFile.razor.js in client side or ./Components/Pages/NameFile.razor.js in server side)
* script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"
