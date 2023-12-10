# Stage 2

## Idea

So far we've created a file scanner that can scan a given directory and print out information about files.

Now we'll expand it to also scan subdirectories, still just printing out information about files. And if we find a subdirectory, we'll scan it recursively as well.

So from the high level, we need to do the following:

- Get the path to the directory to scan
- Call the scanner function

The scanner function will do the following:

- Get all files in the directory
  - For each file in the directory
    - Print out information about the file: name, size, and full path
- Get all subdirectories in the directory
  - For each subdirectory in the directory
    - Print out information about the subdirectory: name and full path
    - Call the scanner function for the subdirectory (this is the recursion)

## Implementation

To accomplish that we'll use the following API provided by .NET:

- [Directory.EnumerateFiles](https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-5.0)
- [Directory.EnumerateDirectories](https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratedirectories?view=net-5.0)
- [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=net-5.0)
- [DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo?view=net-5.0)

So let's update the console application from stage 1 (I made a copy of the stage-1/FileScanner folder and named it stage-2/FileScanner), and add the following code to the `Program.cs` file:

```csharp
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
```

## Testing

Now let's test it. From the repository root, run the following commands.

> Note: Since it's a lot of commands, I've put them in a script called [generate-stage-2-data.ps1](../generate-stage-2-data.ps1), so you can just run that script.

If you're still determined to run the commands manually, here they are 😁:

```powershell
cd test-data
mkdir subfolder1
echo "Hello, World!" > hello.txt
echo "Goodbye, World!" > goodbye.txt
echo "Hello, Subfolder1!" > subfolder1/hello.txt
echo "Goodbye, Subfolder1!" > subfolder1/goodbye.txt
mkdir subfolder1/subfolder1a
echo "Hello, Subfolder1a!" > subfolder1/subfolder1a/hello.txt
echo "Goodbye, Subfolder1a!" > subfolder1/subfolder1a/goodbye.txt
mkdir subfolder2
echo "Hello, Subfolder2!" > subfolder2/hello.txt
echo "Goodbye, Subfolder2!" > subfolder2/goodbye.txt
mkdir subfolder3
cd ..
```

Now run the application:

```powershell
dotnet run --project stage-2/FileScanner test-data
```

You should see the following output:

```text
File name: goodbye.txt, Size: 36, Path: ...\test-data\goodbye.txt
File name: hello.txt, Size: 32, Path: ...\test-data\hello.txt
Directory name: subfolder1, Path: ...\test-data\subfolder1
File name: goodbye.txt, Size: 46, Path: ...\test-data\subfolder1\goodbye.txt
File name: hello.txt, Size: 42, Path: ...\test-data\subfolder1\hello.txt
Directory name: subfolder1a, Path: ...\test-data\subfolder1\subfolder1a
File name: goodbye.txt, Size: 48, Path: ...\test-data\subfolder1\subfolder1a\goodbye.txt
File name: hello.txt, Size: 44, Path: ...\test-data\subfolder1\subfolder1a\hello.txt
Directory name: subfolder2, Path: ...\test-data\subfolder2
File name: goodbye.txt, Size: 46, Path: ...\test-data\subfolder2\goodbye.txt
File name: hello.txt, Size: 42, Path: ...\test-data\subfolder2\hello.txt
Directory name: subfolder3, Path: ...\test-data\subfolder3
```

See the [full code](./stage-2/FileScanner/Program.cs).

So far so good. But we're not done yet. We're still just printing out information about files. We need to save it to the database. We'll do that in [stage-3](../stage-3/README.md).
