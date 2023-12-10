namespace FileScanner;

class Program
{
    static void Main(string[] args)
    {
        // get the path to scan from the command line arguments
        var path = args[0];

        // call the scanner function
        Scan(path);
    }

    static void Scan(string path)
    {
        // get all files in the directory
        var files = Directory.EnumerateFiles(path);

        // for each file in the directory
        foreach (var file in files)
        {
            // print out information about the file: name, size, and full path
            var fileInfo = new FileInfo(file);
            Console.WriteLine($"File name: {fileInfo.Name}, Size: {fileInfo.Length}, Path: {fileInfo.FullName}");
        }

        // get all subdirectories in the directory
        var directories = Directory.EnumerateDirectories(path);

        // for each subdirectory in the directory
        foreach (var directory in directories)
        {
            // print out information about the subdirectory: name and full path
            var directoryInfo = new DirectoryInfo(directory);
            Console.WriteLine($"Directory name: {directoryInfo.Name}, Path: {directoryInfo.FullName}");

            // call the scanner function for the subdirectory (this is the recursion)
            Scan(directory);
        }
    }
}
