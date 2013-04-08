namespace Proctology.Interview.CSharp.LikeStatistics
{
    using System;
    using System.IO;

    internal static class FileSystem
    {
        internal static string[] GetFiles(string path, string extension)
        {
            return Directory.GetFiles(path, "*." + extension);
        } // GetFiles()

        internal static string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        } // GetDirectories()
    } // class FileSystem
} // namespace Proctology.Interview.CSharp.LikeStatistics
