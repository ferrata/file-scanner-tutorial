using Npgsql;

namespace FileScanner;

class Program
{
    static void Main(string[] args)
    {
        // get the path to scan from the command line arguments
        var path = args[0];

        // create a connection to the database
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=file-scanner";
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        // call the scanner function
        Scan(connection, path);
    }

    static void Scan(NpgsqlConnection connection, string path, int? parentId = null)
    {
        // get all files in the directory
        var files = Directory.EnumerateFiles(path);

        // for each file in the directory
        foreach (var file in files)
        {
            // save information about the file to the database
            var fileInfo = new FileInfo(file);

            var command = new NpgsqlCommand("INSERT INTO file_system_entry (name, path, size, is_folder, parent_id) VALUES (@name, @path, @size, @is_folder, @parent_id)", connection);
            command.Parameters.AddWithValue("name", fileInfo.Name);
            command.Parameters.AddWithValue("path", fileInfo.FullName);
            command.Parameters.AddWithValue("size", fileInfo.Length);
            command.Parameters.AddWithValue("is_folder", false);
            command.Parameters.AddWithValue("parent_id", NpgsqlTypes.NpgsqlDbType.Integer, (object)parentId ?? DBNull.Value);
            command.ExecuteNonQuery();
        }

        // get all subdirectories in the directory
        var directories = Directory.EnumerateDirectories(path);

        // for each subdirectory in the directory
        foreach (var directory in directories)
        {
            // save information about the subdirectory to the database
            var directoryInfo = new DirectoryInfo(directory);

            var command = new NpgsqlCommand("INSERT INTO file_system_entry (name, path, size, is_folder, parent_id) VALUES (@name, @path, @size, @is_folder, @parent_id) RETURNING id", connection);
            command.Parameters.AddWithValue("name", directoryInfo.Name);
            command.Parameters.AddWithValue("path", directoryInfo.FullName);
            command.Parameters.AddWithValue("size", 0);
            command.Parameters.AddWithValue("is_folder", true);
            command.Parameters.AddWithValue("parent_id", NpgsqlTypes.NpgsqlDbType.Integer, (object)parentId ?? DBNull.Value);

            // execute the command and get the generated ID for this subdirectory
            var id = (int)command.ExecuteScalar();

            // call the scanner function for the subdirectory (this is the recursion)
            Scan(connection, directory, id);
        }
    }
}