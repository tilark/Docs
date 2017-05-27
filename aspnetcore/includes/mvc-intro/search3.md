<!--
[!code-html[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Views/Shared/_Layout.cshtml?highlight=7,31)]


[!code-csharp[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Controllers/MoviesController.cs?name=snippet_1stSearch)]

[!code-csharp[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Controllers/MoviesController.cs?name=snippet_SearchNull)]

![Index view](../../tutorials/first-mvc-app/search/_static/ghost.png)


[!code-csharp[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Startup.cs?highlight=5&name=snippet_1)]

--> 

Now when you submit a search, the URL contains the search query string. Searching will also go to the `HttpGet Index` action method, even if you have a `HttpPost Index` method.

![Browser window showing the searchString=ghost in the Url and the movies returned, Ghostbusters and Ghostbusters 2, contain the word ghost](../../tutorials/first-mvc-app/search/_static/search_get.png)

The following markup shows the change to the `form` tag:

```html
<form asp-controller="Movies" asp-action="Index" method="get">
   ```

## Adding Search by Genre

Add the following `MovieGenreViewModel` class to the *Models* folder:

[!code-csharp[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Models/MovieGenreViewModel.cs)]

The movie-genre view model will contain:

   * A list of movies.
   * A `SelectList` containing the list of genres. This will allow the user to select a genre from the list.
   * `movieGenre`, which contains the selected genre.

Replace the `Index` method in `MoviesController.cs` with the following code:

[!code-csharp[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Controllers/MoviesController.cs?name=snippet_SearchGenre)]

The following code is a `LINQ` query that retrieves all the genres from the database.

[!code-csharp[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Controllers/MoviesController.cs?name=snippet_LINQ)]

The `SelectList` of genres is created by projecting the distinct genres (we don't want our select list to have duplicate genres).

```csharp
movieGenreVM.genres = new SelectList(await genreQuery.Distinct().ToListAsync())
   ```

## Adding search by genre to the Index view

Update `Index.cshtml` as follows:

[!code-HTML[Main](../../tutorials/first-mvc-app/start-mvc/sample/MvcMovie/Views/Movies/IndexFormGenreNoRating.cshtml?highlight=1,15,16,17,28,31,34,37,43)]

Test the app by searching by genre, by movie title, and by both.