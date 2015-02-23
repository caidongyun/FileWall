using VitaliiPianykh.FileWall.Shared;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Shared
{
    [TestClass]
    public class TestLogEntryData
    {
        [TestMethod]
        public void Constructor()
        {
            var data = new LogEntryData("somedate", true, AccessType.REGISTRY, @"d:\my documents\pics", @"c:\win\malware.exe");
                
            Assert.AreEqual(data.Date, "somedate");
            Assert.AreEqual(data.IsAllowed, true);
            Assert.AreEqual(data.Path, @"d:\my documents\pics");
            Assert.AreEqual(data.ProcessPath, @"c:\win\malware.exe"); 
            Assert.AreEqual(data.AccessType, AccessType.REGISTRY);
        }

        [TestMethod]
        public void Serialize_Null()
        {
            AdvAssert.ThrowsArgumentNull(() => LogEntryData.Serialize(null), "data");
        }

        [TestMethod]
        public void Deserialize_EmptyArray()
        {
            AdvAssert.ThrowsArgument(() => LogEntryData.Deserialize(new byte[]{}), "data");
        }

        [TestMethod]
        public void Deserialize_Null()
        {
            AdvAssert.ThrowsArgumentNull(() => LogEntryData.Deserialize(null), "data");
        }

        [TestMethod]
        public void Deserialize_WrongData()
        {
            var wrongData = new byte[450];
            for (var i = 0; i < 450; i++)
                wrongData[i] = (byte) i;

            AdvAssert.ThrowsArgument(() => LogEntryData.Deserialize(new byte[] {}), "data");
        }

        [TestMethod]
        public void Serialize_Desirialize()
        {
            // NOTE: Watch that all properties LogEntryData have non default values.
            var serialized = new LogEntryData("somedate", true, AccessType.REGISTRY, @"d:\my documents\pics", @"c:\win\malware.exe");
            
            var data = LogEntryData.Serialize(serialized);
            var deserialized = LogEntryData.Deserialize(data);

            Assert.IsTrue(PropertyComparer.AreEqual(serialized, deserialized));
        }
    }
}
