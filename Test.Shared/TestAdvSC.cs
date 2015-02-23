using VitaliiPianykh.FileWall.Shared;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Shared
{
    [TestClass]
    public class TestAdvSC
    {
        [TestMethod]
        public void Constructor_Null()
        {
            AdvAssert.ThrowsArgumentNull(() => new AdvSC(null), "serviceName");
        }
    }
}
