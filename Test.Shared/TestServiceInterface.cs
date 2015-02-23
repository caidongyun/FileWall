using VitaliiPianykh.FileWall.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Shared
{
    [TestClass]
    public class TestServiceInterface
    {
        readonly ServiceInterface si = new ServiceInterface(new Ruleset());

        [TestMethod]
        public void FilesysBlocks_Default()
        {
            Assert.AreEqual(0u, si.FilesysBlocks);
        }

        [TestMethod]
        public void FilesysBlocks_Set()
        {
            si.FilesysBlocks = 333;
            Assert.AreEqual(333u, si.FilesysBlocks);
        }


        [TestMethod]
        public void FilesysPermits_Default()
        {
            Assert.AreEqual(0u, si.FilesysPermits);
        }

        [TestMethod]
        public void FilesysPermits_Set()
        {
            si.FilesysPermits = 222;
            Assert.AreEqual(222u, si.FilesysPermits);
        }


        [TestMethod]
        public void RegistryBlocks_Default()
        {
            Assert.AreEqual(0u, si.RegistryBlocks);
        }

        [TestMethod]
        public void RegistryBlocks_Set()
        {
            si.RegistryBlocks = 333;
            Assert.AreEqual(333u, si.RegistryBlocks);
        }


        [TestMethod]
        public void RegistryPermits_Default()
        {
            Assert.AreEqual(0u, si.RegistryPermits);
        }

        [TestMethod]
        public void RegistryPermits_Set()
        {
            si.RegistryPermits = 222;
            Assert.AreEqual(222u, si.RegistryPermits);
        }
    }
}
