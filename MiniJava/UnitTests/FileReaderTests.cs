using Helper;
using NUnit.Framework;

namespace UnitTests
{
    /// @author Jani Viherväs
    /// @version 27.2.2014
    ///
    /// <summary>
    /// Unit tests for the FileReader class
    /// </summary>
    [TestFixture]
    public class FileReaderTests
    {
        private string _fileName;
        private string _fileExtension;
        private string _filePath;
        private FileReader _fileReader;
        private string _file;

        [SetUp]
        protected void SetUp()
        {
            _fileName = "test";
            _fileExtension = ".txt";
            _file = _fileName + _fileExtension;
            _filePath = System.IO.Directory.GetCurrentDirectory() + "\\";
            _fileReader = new FileReader(_fileExtension);
            const string lines = "multiple lines1\n" +
                                 "multiple lines2\n" +
                                 "multiple lines3";

            System.IO.File.WriteAllText(_filePath + _file, lines);
        }

        [TearDown]
        protected void TearDown()
        {
            System.IO.File.Delete(_filePath + _file);
        }


        [Test]
        public void TestCanReadFileFromPath()
        {
            var list = _fileReader.ReadFile(_filePath + _file);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("multiple lines1", list[0]);
            Assert.AreEqual("multiple lines2", list[1]);
            Assert.AreEqual("multiple lines3", list[2]);
        }


        [Test]
        public void TestCanReadFile()
        {
            var list = _fileReader.ReadFile(_file);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("multiple lines1", list[0]);
            Assert.AreEqual("multiple lines2", list[1]);
            Assert.AreEqual("multiple lines3", list[2]);
        }


        [Test]
        public void TestEndOfFileMarkGetsAdded()
        {
            const string endMark = "$$";
            var list = _fileReader.ReadFile(_file, endMark);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("multiple lines1", list[0]);
            Assert.AreEqual("multiple lines2", list[1]);
            Assert.AreEqual("multiple lines3" + endMark, list[2]);
        }


        [Test]
        public void TestThrowsExceptionWithWrongFileExtension()
        {
            Assert.Throws<Exceptions.FileException>(() => _fileReader.ReadFile(_fileName + ".mpl"));
        }


        [Test]
        public void TestThrowsExceptionIfFileIsNotFound()
        {
            Assert.Throws<Exceptions.FileException>(() => _fileReader.ReadFile(_fileName + "test" + _fileExtension));
        }

    }
}
