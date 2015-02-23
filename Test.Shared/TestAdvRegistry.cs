using System;
using System.IO;
using VitaliiPianykh.FileWall.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;


namespace Test.Shared
{
    [TestClass]
    public class TestAdvRegistry
    {
        // TODO: Test all methods with paths which contains only root element.

        #region Test Environment

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var Key = Registry.CurrentUser.CreateSubKey("existent");
            if(Key == null)
                throw new ApplicationException("Can't create existent key in HKCU.");

            Key.SetValue("existent", "TEST");

            if(Registry.CurrentUser.OpenSubKey("nonexistent") != null)
                throw new ApplicationException("nonexistent key exists in HKCU.");

            if(AdvRegistry.GetValueData(ExistentKey_NonexistentVal_Path) != null)
                throw new ApplicationException("nonexistent val exists in HKCU\\existent.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            Registry.CurrentUser.DeleteSubKey("existent", false);
        }

        public string ExistentKeyPath { get { return @"HKCU\existent"; } }
        public string NonexistentKeyPath { get { return @"HKCU\nonexistent"; } }
        private string ExistentValPath { get { return @"HKCU\existent\\existent"; } }
        private string ExistentKey_NonexistentVal_Path { get { return @"HKCU\existent\\nonexistent"; } }


        #endregion

        [TestMethod]
        public void IsRegistryPath()
        {
            string[] TestRegistryPaths = new[]
                                             {
                                                 @"HKEY_LOCAL_MACHINE\SOFTWARE\FGUpdate\Config",
                                                 @"HKEY_LOCAL_MACHINE\SOFTWARE\Windows 3.1 Migration Status\REG.DAT",
                                                 @"HKEY_USERS\S-1-5-21-515967899-606747145-682003330-500\Identities\{30EA52FF-4D40-469E-B12F-CE5262A1495F}\Software\Microsoft\Outlook Express\5.0\Rules\Filter\FFA\Actions\000",
                                                 @"HKEY_CURRENT_USER\Printers\\DeviceOld",
                                                 @"HKEY_LOCAL_MACHINE\SOFTWARE"
                                             };
            foreach(var RegistryPath in TestRegistryPaths)
                if(!AdvRegistry.IsRegistryPath(RegistryPath))
                    Assert.Fail("AdvRegistry.IsRegistryPath returned false on:\r\n" + RegistryPath);
        }

        [TestMethod]
        public void IsRegistryPathOnIcorrectData()
        {
            string[] TestRegistryPaths = new[]
                                             {
                                                 @"C:\Documents and Settings",
                                                 @"C:\Documents and Settings\All Users\Favorites",
                                                 @"D:\Documents\Pictures",
                                                 @"HKY_CURRENT_USER\Printers\\DeviceOld",
                                                 @"HKEY_LOCAL_MACHIN\SOFTWARE\FGUpdate\Config\\Time1"
                                             };
            foreach (var RegistryPath in TestRegistryPaths)
                if (AdvRegistry.IsRegistryPath(RegistryPath))
                    Assert.Fail("AdvRegistry.IsRegistryPath returned true on:\r\n" + RegistryPath);
        }


        [TestMethod]
        public void IsKeyExists()
        {
            Assert.AreEqual(AdvRegistry.IsKeyExists(ExistentKeyPath), true);
            Assert.AreEqual(AdvRegistry.IsKeyExists(NonexistentKeyPath), false);
        }


        [TestMethod]
        public void GetValueData()
        {
            Assert.AreEqual("TEST", AdvRegistry.GetValueData(ExistentValPath));
        }

        #region SetValue

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException)) ]
        public void SetValueData_NullKey()
        {
            AdvRegistry.SetValueData(null, "Test value data.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetValueData_EmptyKey()
        {
            AdvRegistry.SetValueData("", "Test value data.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetValueData_NullData()
        {
            AdvRegistry.SetValueData(ExistentValPath, null);
        }

        [TestMethod]
        public void SetValueData_EmptyData()
        {
            AdvRegistry.SetValueData(ExistentValPath, "");
            Assert.AreEqual(string.Empty, AdvRegistry.GetValueData(ExistentValPath));
        }
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SetValueData_NonexistentKey()
        {
            AdvRegistry.SetValueData(NonexistentKeyPath, string.Empty);
        }


        [TestMethod]
        public void SetValueData_ExistentKeyAndVal()
        {
            var TestValueData = "Test Value Data";
            AdvRegistry.SetValueData(ExistentValPath, TestValueData);

            Assert.AreEqual(TestValueData, AdvRegistry.GetValueData(ExistentValPath));
        }


        [TestMethod]
        public void SetValueData_ExistentKeyNonexitentVal()
        {
            var TestValueData = "Test Value Data";
            AdvRegistry.SetValueData(ExistentKey_NonexistentVal_Path, TestValueData);
            Assert.AreEqual(TestValueData, AdvRegistry.GetValueData(ExistentKey_NonexistentVal_Path));
        }

        [TestMethod]
        public void SetValueData_WithoutValueNameInPath()
        {
            var TestValueData = "Test Value Data";
            AdvRegistry.SetValueData(ExistentKeyPath, TestValueData);
            Assert.AreEqual(TestValueData, AdvRegistry.GetValueData(ExistentKeyPath));
        }


        #endregion

        [TestMethod]
        public void CreateKey()
        {
            if(Registry.CurrentUser.OpenSubKey("TestKey") != null)
                Assert.Fail(@"HKCU\TestKey already exists.");

            AdvRegistry.CreateKey(@"HKCU\TestKey");

            if (Registry.CurrentUser.OpenSubKey("TestKey") == null)
                Assert.Fail(@"HKCU\TestKey failed to create.");
            else
                Registry.CurrentUser.DeleteSubKey("TestKey");
        }


        [TestMethod]
        public void DeleteKey_ForExistentKey()
        {
            AdvRegistry.DeleteKey(ExistentKeyPath);
            Assert.IsFalse(AdvRegistry.IsKeyExists(ExistentKeyPath));
        }


        [TestMethod]
        public void DeleteKey_ForExistentKeyWithSubkeys()
        {
            AdvRegistry.CreateKey(ExistentKeyPath + "\\Subkey1");
            AdvRegistry.CreateKey(ExistentKeyPath + "\\Subkey2");

            AdvRegistry.DeleteKey(ExistentKeyPath);
            Assert.IsFalse(AdvRegistry.IsKeyExists(ExistentKeyPath));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteKey_ForNonexistentKey()
        {
            AdvRegistry.DeleteKey(NonexistentKeyPath);
        }

        [TestMethod]
        public void TestOpenSubKey()
        {
            // Is OpenSubKey able to open existent path?
            var key = AdvRegistry.OpenSubKey(ExistentKeyPath);
            Assert.AreEqual(key.Name, "HKEY_CURRENT_USER\\existent");

            // Is OpenSubKey able to open path which contains only root element?
            key = AdvRegistry.OpenSubKey("HKLM");
            Assert.AreEqual(Registry.LocalMachine, key);

            // When opening nonexistent path OpenSubKye must return null
            key = AdvRegistry.OpenSubKey(NonexistentKeyPath);
            Assert.IsNull(key);
        }
    }
}