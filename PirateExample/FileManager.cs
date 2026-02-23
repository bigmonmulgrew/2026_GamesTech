public static class FileManager
{
    static string BASE_PATH = Directory.GetCurrentDirectory();
    static string LOG_FILE_NAME = "pirate_log.txt";
    static string LOG_FILE_PATH = Path.Combine(BASE_PATH, LOG_FILE_NAME);

    public static string LogFilePath { get { return LOG_FILE_PATH; } }

    public static void SaveText(string content)
    {
        File.WriteAllText(LOG_FILE_PATH, content);
    }

    public static string LoadText(string path)
    {
        if (File.Exists(path)) return File.ReadAllText(path);

        return null;
    }
}