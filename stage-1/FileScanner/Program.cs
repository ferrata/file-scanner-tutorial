namespace FileScanner;

class Program
{
    static void Main(string[] args)
    {
        // get the path to scan from the command line arguments
        var path = args[0];

        // get all files in the directory
        var files = Directory.EnumerateFiles(path);

        // for each file in the directory
        foreach (var file in files)
        {
            // print out information about the file: name, size, and full path
            var fileInfo = new FileInfo(file);
            Console.WriteLine($"Name: {fileInfo.Name}, Size: {fileInfo.Length}, Path: {fileInfo.FullName}");
        }
    }
}
