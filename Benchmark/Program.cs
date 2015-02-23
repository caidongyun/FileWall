using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Shared;


namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var accessesWithout = 0;
                var accessesWith = 0;

                // Load ruleset from file.
                var ruleset = new Ruleset();
                ruleset.ReadXml("ruleset.xml");

                Console.WriteLine("********************************************************************************");
                Console.WriteLine("*               FileWall BENCHMARK UTILITY                                *");
                Console.WriteLine("*          NOTE: ADMINISTRATIVE PRIVILEGES REQUIRED                            *");
                Console.WriteLine("********************************************************************************");

                Console.WriteLine("Enter benchmark time in minutes:");
                var benchmarkTime = Convert.ToInt16(Console.ReadLine());
                if(benchmarkTime < 1)
                    throw new InvalidOperationException("Wrong benchmark time.");
                
                Console.WriteLine("Shutdown FileWall and press <Enter>");
                Console.ReadLine();

                // Do benchmark without FileWall.

                accessesWithout = Benchmark(ruleset, benchmarkTime);

                // Allow bencnhmark application to access anywhere.
                SetupRules(ruleset);

                Console.WriteLine("Start FileWall and press <Enter>.");
                Console.ReadLine();

                accessesWith = Benchmark(ruleset, benchmarkTime);

                Console.WriteLine("********************************************************************************");
                Console.WriteLine("*                  BENCHMARK RESULTS                                           *");
                Console.WriteLine("********************************************************************************");
                Console.WriteLine("* Access count WITHOUT FileWall \t\t " + accessesWithout);
                Console.WriteLine("* Access count WITH FileWall \t\t " + accessesWith);
                Console.WriteLine("********************************************************************************");
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                var dlg = new ThreadExceptionDialog(ex);
                dlg.ShowDialog();
            }
        }

        private static void SetupRules(Ruleset ruleset)
        {
            var benchmarkProcess = ruleset.Processes.FindByPath(Application.ExecutablePath);
            if (benchmarkProcess == null)
                benchmarkProcess = ruleset.Processes.AddProcessesRow(Application.ExecutablePath);
                
            var category = ruleset.Categories.FindByName("Benchmark");
            if (category == null)
                category = ruleset.Categories.AddCategoriesRow("Benchmark", null, string.Empty);

            var item = ruleset.Items.GetItemsRow("Benchmark", "Benchmark");
            if (item == null)
                item = ruleset.Items.AddItemsRow("Benchmark", null, category);

            var deleteList = new List<Ruleset.RulesRow>();
            foreach (Ruleset.RulesRow rule in ruleset.Rules)
                if(rule.Name.StartsWith("Benchmark rule #"))
                    deleteList.Add(rule);
            foreach (var rule in deleteList)
                rule.Delete();

            for (int i = 0; i < ruleset.Paths.Count; i++)
            {
                var path = ruleset.Paths[i];
                ruleset.Rules.AddRulesRow("Benchmark rule #" + i, RuleAction.Allow, false, path, benchmarkProcess, item);
            }

            ruleset.AcceptChanges();
            ruleset.WriteXml("ruleset.xml");
        }

        private static int Benchmark(Ruleset ruleset, int benchmarkTime)
        {
            int accessCounter = 0;

            var time = DateTime.Now.AddMinutes(benchmarkTime);
            while (DateTime.Now < time)
            {
                foreach (Ruleset.PathsRow path in ruleset.Paths)
                {
                    // Try to open every file in list.
                    foreach (var fsPath in ExpandFSPaths(path))
                    {
                        FileStream file = null;
                        try
                        {
                            if (File.Exists(fsPath))
                                file = File.Open(fsPath, FileMode.Open, FileAccess.Read);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            if (file != null)
                                file.Close();
                        }
                    }

                    if (AdvRegistry.IsRegistryPath(path.Path) && AdvRegistry.IsKeyExists(path.Path))
                        AdvRegistry.GetValueData(path.Path);
                }
                accessCounter++;
                Console.Write(".");
            }
            Console.WriteLine();

            return accessCounter;
        }

        private static string[] ExpandFSPaths(Ruleset.PathsRow path)
        {
            var Result = new List<string>();

            if (!AdvRegistry.IsRegistryPath(path.Path))
                Result.Add(AdvEnvironment.ExpandEnvironmentVariables(path.Path));

            return Result.ToArray();
        }
    }
}
