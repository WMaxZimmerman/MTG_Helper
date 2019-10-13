using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigDeckPlays.DAL.Repositories;
using System.IO;
using System.Text;
using System;

namespace BigDeckPlays.Tests.DAL.Repositories
{
    [TestClass]
    public class TestConsoleRepository
    {
        private StringWriter consoleMock;
        private StringBuilder mockConsole = new StringBuilder();
        private ConsoleRepository _repo;

        [TestInitialize]
        public void Init()
        {
            consoleMock = new StringWriter(mockConsole);
            System.Console.SetOut(consoleMock);

            _repo = new ConsoleRepository();
        }

        [TestMethod]
        public void WriteLine_OutputsLineToConsole()
        {
            var message = "hello world";
            var expectedMessage = message + Environment.NewLine;
            _repo.WriteLine(message);

            Assert.AreEqual(expectedMessage, mockConsole.ToString());
        }
    }
}
