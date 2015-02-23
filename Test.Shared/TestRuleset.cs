using System.Collections.Generic;
using VitaliiPianykh.FileWall.Shared;
using AdvTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Test.Shared
{
    [TestClass]
    public class TestRuleset
    {
        #region GetRulesRow

        [TestMethod]
        public void GetRulesRow()
        {
            var ruleset = new Ruleset();
            var Rule = AddRule(ruleset, @"c:\test.txt", @"c:\knownproc.exe");

            Assert.AreEqual(Rule, ruleset.GetRulesRow(Rule.PathID, @"c:\knownproc.exe"));
        }

        [TestMethod]
        public void GetRulesRow_UnknownPathID()
        {
            var ruleset = new Ruleset();
            AddRule(ruleset, @"c:\test.txt", @"c:\knownproc.exe");

            AdvAssert.ThrowsArgument(() => ruleset.GetRulesRow(333, @"c:\knownproc.exe"), "pathID");
        }

        [TestMethod]
        public void GetRulesRow_UnknownProcessPath()
        {
            var ruleset = new Ruleset();
            var Rule = AddRule(ruleset, @"c:\test.txt", @"c:\knownproc.exe");

            AdvAssert.ThrowsArgument(() => ruleset.GetRulesRow(Rule.PathID, @"c:\unknownn_process.exe"), "processPath");
        }

        [TestMethod]
        public void GetRulesRow_NoRule()
        {
            var ruleset = new Ruleset();
            var PathID = ruleset.Paths.AddPathsRow(@"c:\path.txt").ID;
            ruleset.Processes.AddProcessesRow(@"c:\process.exe");

            Assert.IsNull(ruleset.GetRulesRow(PathID, @"c:\process.exe"));
        }

        // TODO: Move it to ruleset.
        private static Ruleset.RulesRow AddRule(Ruleset ruleset, string path, string processPath)
        {
            return AddRule(ruleset, RuleAction.Allow, true, path, processPath);
        }
        
        // TODO: Move it to ruleset.
        private static Ruleset.RulesRow AddRule(Ruleset ruleset, RuleAction ruleAction, bool active, string path, string processPath)
        {

            var Pr = ruleset.Processes.FindByPath(processPath);
            if (Pr == null)
                Pr = ruleset.Processes.AddProcessesRow(processPath);
            var Pth = ruleset.Paths.FindByPath(path);
            if (Pth == null)
                Pth = ruleset.Paths.AddPathsRow(path);

            var C = ruleset.Categories.FindByName("Test");
            if (C == null)
                C = ruleset.Categories.AddCategoriesRow("Test", null, string.Empty);
            var I = ruleset.Items.GetItemsRow("Test", "Test");
            if (I == null)
                I = ruleset.Items.AddItemsRow("Test", null, C);
            return ruleset.Rules.AddRulesRow("Test", ruleAction, false, Pth, Pr, I);
        }

        #endregion

        [TestMethod]
        public void Categories_FindByName()
        {
            var ruleset = new Ruleset();

            var Category = ruleset.Categories.AddCategoriesRow("TestCategory", null, string.Empty);

            Assert.AreEqual(Category, ruleset.Categories.FindByName("TestCategory"));
            Assert.IsNull(ruleset.Categories.FindByName("Unexistent"));
        }

        [TestMethod]
        public void Items_GetItemsRow()
        {
            var ruleset = new Ruleset();
            var Category = ruleset.Categories.AddCategoriesRow("TestCategory", null, string.Empty);
            var Item = ruleset.Items.AddItemsRow("Item", null, Category);

            Assert.AreEqual(Item, ruleset.Items.GetItemsRow("TestCategory", "Item"));
            Assert.IsNull(ruleset.Items.GetItemsRow("UnexistentCategory", "Item"));
            Assert.IsNull(ruleset.Items.GetItemsRow("TestCategory", "UnexistentItem"));
        }

        [TestMethod]
        public void Paths_FindByPath()
        {
            var ruleset = new Ruleset();
            var PathRow = ruleset.Paths.AddPathsRow("c:\\testpath.txt");

            Assert.AreEqual(PathRow, ruleset.Paths.FindByPath("c:\\testpath.txt"));
            Assert.IsNull(ruleset.Paths.FindByPath("c:\\invalid_testpath.txt"));
        }

        [TestMethod]
        public void Paths_ExpandedPath_HKLM()
        {
            var TestRow = new Ruleset().Paths.AddPathsRow("123");
            var Val1 = @"HKLM\Sofware";
            var Val2 = @"HKEY_LOCAL_MACHINE\Sofware";
            var ExpectedValue = @"\REGISTRY\MACHINE\Sofware";

            TestRow.Path = Val1;
            Assert.AreEqual(1, TestRow.ExpandedPaths.Length);
            Assert.AreEqual(ExpectedValue, TestRow.ExpandedPaths[0]);

            TestRow.Path = Val2;
            Assert.AreEqual(1, TestRow.ExpandedPaths.Length);
            Assert.AreEqual(ExpectedValue, TestRow.ExpandedPaths[0]);
        }

        [TestMethod]
        public void Paths_ExpandedPath_HKU()
        {
            var TestRow = new Ruleset().Paths.AddPathsRow("123");
            var Val1 = @"HKU\123";
            var Val2 = @"HKEY_USERS\123";
            var ExpectedValue = @"\REGISTRY\USERS\123";

            TestRow.Path = Val1;
            Assert.AreEqual(1, TestRow.ExpandedPaths.Length);
            Assert.AreEqual(ExpectedValue, TestRow.ExpandedPaths[0]);

            TestRow.Path = Val2;
            Assert.AreEqual(1, TestRow.ExpandedPaths.Length);
            Assert.AreEqual(ExpectedValue, TestRow.ExpandedPaths[0]);
        }

        [TestMethod]
        public void Paths_ExpandedPath_HKCU()
        {
            var ExpectedList = new List<string>();
            foreach (var sid in AdvEnvironment.GetAllUserSIDs())
                ExpectedList.Add(@"\REGISTRY\USER\" + sid + @"\Software");
            var TestRow = new Ruleset().Paths.AddPathsRow("123");

            TestRow.Path = @"HKCU\Software";
            CollectionAssert.AreEquivalent(ExpectedList, TestRow.ExpandedPaths);
            TestRow.Path = @"HKEY_CURRENT_USER\Software";
            CollectionAssert.AreEquivalent(ExpectedList, TestRow.ExpandedPaths);
        }

        [TestMethod]
        public void Paths_ExpandedPath_NotSupportedRegistryPaths()
        {
            var rs = new Ruleset();
            var PathRow = rs.Paths.AddPathsRow("123");

            var TestValues = new[]
                                 {
                                     @"HKEY_CURRENT_CONFIG\Software",
                                     @"HKCC\Software",
                                     @"HKEY_CLASSES_ROOT\._sln",
                                     @"HKCR\._sln",
                                     @"HKEY_DYN_DATA\123",
                                     @"HKDD\123",
                                     @"HKEY_PERFORMANCE_DATA\123",
                                     @"HKPD\123"
                                 };

            foreach (var testValue in TestValues)
            {
                PathRow.Path = testValue;
                AdvAssert.ThrowsInvalidOperation(delegate { var p = PathRow.ExpandedPaths; });
            }            
        }

        [TestMethod]
        public void Paths_ExpandedPath_WillCutKeysWithValueAtTheEnd()
        {
            var rs = new Ruleset();
            var pathRow = rs.Paths.AddPathsRow(@"HKU\Windows\\DefaultValue");

            // Current version of driver doesn't support protecting of values
            // So ExpandedPaths property must cut values and return only key path.
            Assert.AreEqual(@"\REGISTRY\USERS\Windows", pathRow.ExpandedPaths[0]);
        }

        [TestMethod]
        public void Processes_FindByPath()
        {
            var ruleset = new Ruleset();
            var ProcessRow = ruleset.Processes.AddProcessesRow("c:\\testpath.exe");

            Assert.AreEqual(ProcessRow, ruleset.Processes.FindByPath("c:\\testpath.exe"));
            Assert.IsNull(ruleset.Processes.FindByPath("c:\\invalid_testpath.exe"));
        }

        [TestMethod]
        public void CategoriesRow_ImageData_Null()
        {
            var ruleset = new Ruleset();
            var CategoryRow = ruleset.Categories.AddCategoriesRow("Test", null, "Test");

            Assert.IsNull(CategoryRow.Image);
        }

        [TestMethod]
        public void CategoriesRow_ImageData_Empty()
        {
            var ruleset = new Ruleset();
            var CategoryRow = ruleset.Categories.AddCategoriesRow("Test", new byte[] {}, "Test");

            Assert.IsNull(CategoryRow.Image);
        }

        [TestMethod]
        public void ItemsRow_ImageData_Null()
        {
            var ruleset = new Ruleset();
            var CategoryRow = ruleset.Categories.AddCategoriesRow("Test", null, "Test");
            var ItemRow = ruleset.Items.AddItemsRow("Test", null, CategoryRow);

            Assert.IsNull(ItemRow.Image);
        }

        [TestMethod]
        public void ItemsRow_ImageData_Empty()
        {
            var ruleset = new Ruleset();
            var CategoryRow = ruleset.Categories.AddCategoriesRow("Test", null, "Test");
            var ItemRow = ruleset.Items.AddItemsRow("Test", new byte[]{}, CategoryRow);

            Assert.IsNull(ItemRow.Image);
        }

        [TestMethod]
        public void PathsRow_ExpandedPath()
        {
            var ruleset = new Ruleset();
            var PathRow = ruleset.Paths.AddPathsRow("%windir%\\testpath.txt");
            Assert.AreEqual(1, PathRow.ExpandedPaths.Length);
            Assert.AreEqual(PathRow.ExpandedPaths[0], AdvEnvironment.ExpandEnvironmentVariables("%windir%\\testpath.txt"));
        }

        [TestMethod]
        public void Merge_Workaround()
        {
            var protectedPath = @"c:\test.txt";
            var processPath = @"c:\knownproc.exe";

            // Fill the ruleset.
            var ruleset = new Ruleset();
            var c = ruleset.Categories.AddCategoriesRow("TestCategory", new byte[] { }, string.Empty);
            var i = ruleset.Items.AddItemsRow("TestItem", null, c);
            var path = ruleset.Paths.AddPathsRow(protectedPath);
            var proc = ruleset.Processes.AddProcessesRow(processPath);
            var rule = ruleset.Rules.AddRulesRow("TestRule", RuleAction.Ask, false, path, proc, i);            
            
            ruleset.AcceptChanges();
            Assert.IsNull(ruleset.GetChanges());


            // Copy ruleset.
            var ruleset2 = (Ruleset)ruleset.Copy();

            // To some changes to ruleset2.
            ruleset2.Rules[0].Log = false;

            // Get pure list of changes.
            ruleset2.Relations.Clear();         // <=== This is exactly workaround.
            ruleset2.EnforceConstraints = false;// <=== This is exactly workaround.
            var changes = ruleset2.GetChanges();
            Assert.IsNotNull(changes);

            ruleset.Merge(changes, true);

            Assert.IsFalse(ruleset2.Rules[0].Log);
        }
    }
}
