using System.Diagnostics;

namespace ProHomeWork3;

internal static class Program
{
    private static async Task Main()
    {
        var path = Environment.CurrentDirectory;
        var directoryInfo = new DirectoryInfo(path)?.Parent?.Parent?.Parent;
        Console.WriteLine(directoryInfo);
        await CountSpacesInThreeFiles();
        Console.WriteLine("");
        await CountSpacesInAllFilesInDirectory(directoryInfo.FullName);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static async Task CountSpacesInAllFilesInDirectory(string directory)
    {
        var sw = new Stopwatch();
        sw.Start();
        var files = Directory.GetFiles(directory);
        var tasks = new Dictionary<string, Task<int>>();
        
        foreach (var fileInfo in files)
        {
            var task = Task.Run(() => CountSpacesInFile(fileInfo));
            tasks.Add(fileInfo, task);
        }
        foreach (var keyValuePair in tasks)
        {
            try
            {
                var result = await keyValuePair.Value;
                Console.WriteLine($"In file '{keyValuePair.Key}': {result} spaces");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file {keyValuePair.Key}: {e.Message}");
                throw;
            }
        }
        sw.Stop();
        Console.WriteLine($"Calculating spaces in all files in directory completed in {sw.ElapsedMilliseconds} milliseconds");

    }
    private static async Task CountSpacesInThreeFiles()
    {
        var sw = new Stopwatch();
        sw.Start();
        var files = new string[]
        { 
            "NewFile1.txt",
            "NewFile2.txt",
            "NewFile3.txt"
        };
        var tasks = new Dictionary<string, Task<int>>();
        
        foreach (var fileInfo in files)
        {
            var task = Task.Run(() => CountSpacesInFile(fileInfo));
            tasks.Add(fileInfo, task);
        }
        foreach (var keyValuePair in tasks)
        {
            try
            {
                var result = await keyValuePair.Value;
                Console.WriteLine($"In file '{keyValuePair.Key}': {result} spaces");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file {keyValuePair.Key}: {e.Message}");
                throw;
            }
        }
        sw.Stop();
        Console.WriteLine($"Calculating spaces in three files completed in {sw.ElapsedMilliseconds} milliseconds");
    }

    private static int CountSpacesInFile(string file)
    {
        var text = File.ReadAllText(file);
        return text.Count((c) => c == ' ');
    }
}



