// See https://aka.ms/new-console-template for more information

using ConsoleApp1;

var swagger = new swaggerClient("https://localhost:7238/", new HttpClient());

var all = await swagger.GetCoursesAsync();

foreach (var item in all)
{
    Console.WriteLine(item.Title);
}
