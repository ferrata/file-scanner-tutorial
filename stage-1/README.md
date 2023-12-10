# Stage 1

Let's write a simple file scanner that will scan a given directory, print out information about files.

## Idea

From the high level, we need to do the following:

- Get the path to the directory to scan
- Get all files in the directory
  - For each file in the directory
    - Print out information about the file: name, size, and full path

## Implementation

To accomplish that we'll use the following API provided by .NET:

- [Directory.EnumerateFiles](https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-5.0)
- [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=net-5.0)

So let's create a console application, and add the following code to the `Program.cs` file:

```csharp
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
```

See the [full code](./stage-1/FileScanner/Program.cs).

## Testing

To test it, let's create a folder with some files in it, and run the application. 

From the repository root, run the following commands:

```powershell
mkdir test-data
cd test-data
echo "Hello, World!" > hello.txt
echo "Goodbye, World!" > goodbye.txt
cd ..
dotnet run --project stage-1/FileScanner test-data
```

You should see the following output:

```text
Name: goodbye.txt, Size: 36, Path: ...\test-data\goodbye.txt
Name: hello.txt, Size: 32, Path: ...\test-data\hello.txt
```

Of course, you can use any folder you want, and you can add more files to it.

Stage 1 is complete. Let's move on to [stage 2](../stage-2/README.md).
