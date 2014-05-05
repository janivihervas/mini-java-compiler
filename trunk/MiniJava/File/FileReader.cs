using System.Collections.Generic;
using System.IO;
using Exceptions;

namespace File
{
    /// @author Jani Viherväs
    /// @version 27.2.2014
    ///
    /// <summary>
    /// Class to handle file reading
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// Gets the required file extension
        /// </summary>
        public string FileExtension { get; private set; }


        /// <summary>
        /// Creates a new file reader object.
        /// </summary>
        /// <param name="fileExtension">The required file extension. If the file to read doesn't have this extension, exception will be thrown</param>
        public FileReader(string fileExtension)
        {
            FileExtension = fileExtension;
        }


        /// <summary>
        /// Reads the file
        /// </summary>
        /// <param name="fileName">File name or path/fileName. If path is not given, current directory will be used</param>
        /// <param name="endOfFileMark">What mark to add to the end of file.</param>
        /// <returns>List containing all the lines</returns>
        public List<string> ReadFile(string fileName, string endOfFileMark = "")
        {
            if ( !fileName.Contains(FileExtension) )
            {
                throw new FileException(
                    "File is not of the correct extension. Required file extension is " + FileExtension);
            }
            var path = fileName;

            if ( !fileName.Contains("\\") )
            {
                path = Directory.GetCurrentDirectory() + "\\" + fileName;
            }
            if ( !System.IO.File.Exists(path) )
            {
                throw new FileException("File \"" + path + "\" does not exist.");
            }
            var file = new StreamReader(path);
            var result = new List<string>();
            string line;

            while ( (line = file.ReadLine()) != null )
            {
                result.Add(line);
            }

            file.Close();
            result[result.Count - 1] += endOfFileMark;
            return result;
        }
    }
}
