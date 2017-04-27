using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

// Add NuGet <package id="Microsoft.Extensions.Configuration" and
// <package id="Microsoft.Extensions.Configuration.Json"
// .NET Framework 4.x use the following path:
//.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\.."))

public class Program
{
    public static IConfigurationRoot Configuration { get; set; }
    public static void Main(string[] args = null)
    {
        //var builder = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json");

        //Configuration = builder.Build();

        //Console.WriteLine($"option1 = {Configuration["option1"]}");
        //Console.WriteLine($"option2 = {Configuration["option2"]}");
        //Console.WriteLine(
        //    $"option1 = {Configuration["subsection:suboption1"]}");
        CreateFileDirectory(new string[] { "test1", "Test2" });
    }

    private static void CreateFileDirectory(string[] paramsString)
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        Configuration = builder.Build();
        var result = new StringBuilder(200);
        var fileDirectory =  Configuration["UploadFileDirectory:UploadTemplateDirectory"];
        var uploadFilePath = Directory.GetCurrentDirectory() + $@"{fileDirectory}";
        result.Append(uploadFilePath);
        foreach (var param in paramsString)
        {
            result.AppendFormat("\\" + param);
        }
        Console.WriteLine( result.ToString().TrimEnd('\\'));
    }

    public class FileUploadConfig
    {
        public FileUploadConfig()
        {

        }
        public string UploadTemplateDirectory { get; set; }
        public string UploadDirectory { get; set; }
    }
}