using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Security.Principal;
using System.Text.RegularExpressions;


namespace VitaliiPianykh.FileWall.Shared
{
    public partial class Ruleset
    {
        #region Public Methods

        /// <summary>Gets RulesRow by path ID and process path.</summary>
        /// <param name="pathID">ID of protected path.</param>
        /// <param name="processPath">Path of process.</param>
        public RulesRow GetRulesRow(int pathID, string processPath)
        {
            var SelectedPaths = (PathsRow[]) Paths.Select("ID=" + pathID);
            if (SelectedPaths.Length == 0)
                throw new ArgumentException("Path with specified ID is not found.", "pathID");

            var ProcessRow = Processes.FindByPath(processPath);
            if(ProcessRow == null)
                throw new ArgumentException("Specified process path is not found.", "processPath");

            var SelectedRules = (RulesRow[])Rules.Select("PathID='" + SelectedPaths[0].ID + "' AND " + "ProcessID='" + ProcessRow.ID + "'");
            
            return SelectedRules.Length == 0 ? null : SelectedRules[0];
        }

        #endregion


        #region Tables

        public partial class CategoriesDataTable
        {
            /// <summary>Finds category by name.</summary>
            public CategoriesRow FindByName(string name)
            {
                var SelectedRows = Select("Name='" + name + "'");

                if (SelectedRows.Length == 0)
                    return null;

                return (CategoriesRow) SelectedRows[0];
            }
        }


        public partial class ItemsDataTable
        {
            public ItemsRow GetItemsRow(string categoryName, string itemName)
            {
                var Ruleset = (Ruleset)DataSet;

                // Try to find category.
                var CategoryRow = Ruleset.Categories.FindByName(categoryName);
                if (CategoryRow == null)
                    return null;

                var ItemRows = Select("Name='" + itemName + "' AND CategoryID=" + CategoryRow.ID);

                if (ItemRows.Length == 0)
                    return null;

                return (ItemsRow) ItemRows[0];
            }
        }


        public partial class PathsDataTable
        {
            public PathsRow FindByPath(string path)
            {
                var SelectedRows = Select("Path='" + path + "'");

                if (SelectedRows.Length == 0)
                    return null;

                return (PathsRow)SelectedRows[0];
            }
        }

        public partial class ProcessesDataTable
        {
            public ProcessesRow FindByPath(string path)
            {
                var SelectedRows = Select("Path='" + path + "'");

                if (SelectedRows.Length == 0)
                    return null;

                return (ProcessesRow)SelectedRows[0];
            }
        }

        #endregion


        #region Rows

        public partial class CategoriesRow
        {
            /// <summary>Converts ImageData to <see cref="Image"/> and returns image.</summary>
            public Image Image
            {
                get { return ConvertArrayToImage(ImageData); }
            }
        }


        public partial class ItemsRow
        {
            /// <summary>Converts ImageData to Image and returns image.</summary>
            public Image Image
            {
                get { return ConvertArrayToImage(ImageData); }
            }
        }


        public partial class RulesRow
        {
            public string ProtectedPath
            {
                get { return PathsRow.Path; }
            }


            public string ProcessPath
            {
                get { return ProcessesRow.Path; }
            }
        }

        public partial class PathsRow
        {
            public string[] ExpandedPaths
            {
                get
                {
                    var Result = new List<string>();

                    if (!AdvRegistry.IsRegistryPath(Path))
                    {
                        switch (Path.ToUpper())
                        {
                            case "%OPERAPROFILES%":
                                var operaProfilesPath = Environment.ExpandEnvironmentVariables("%APPDATA%\\Opera");

                                if(Directory.Exists(operaProfilesPath) == false)
                                    break;  // It seems that opera is not installed.
                            
                                // Every sub-dirrectory is opera profile.
                                foreach (var profilePath in Directory.GetDirectories(operaProfilesPath))
                                    Result.Add(profilePath);

                                break;
                            case "SAFARIPROFILES":
                                break;
                            case "FIREFOXPROFILES":
                                break;
                            default:
                                Result.Add(AdvEnvironment.ExpandEnvironmentVariables(Path));
                                break;
                        }
                    }
                    else
                    {
                        switch (AdvRegistry.GetRootKey(Path))
                        {
                            case "HKEY_LOCAL_MACHINE":
                            case "HKLM":
                                Result.Add(Regex.Replace(Path,
                                                         @"(?:HKLM|HKEY_LOCAL_MACHINE)\\(.*)",
                                                         @"\REGISTRY\MACHINE\$1"));
                                break;

                            case "HKEY_USERS":
                            case "HKU":
                                Result.Add(Regex.Replace(Path,
                                                         @"(?:HKU|HKEY_USERS)\\(.*)",
                                                         @"\REGISTRY\USERS\$1"));
                                break;

                            case "HKEY_CURRENT_USER":
                            case "HKCU":
                                foreach (var sid in AdvEnvironment.GetAllUserSIDs())
                                    Result.Add(Regex.Replace(Path,
                                                             @"(?:HKCU|HKEY_CURRENT_USER)(\\.*)",
                                                             @"\REGISTRY\USER\" + sid + @"$1"));
                                break;

                            default:
                                throw new InvalidOperationException("Such path is not supported " + Path + ".");
                        }

                        // Cut value paths
                        if (Regex.IsMatch(Result[0], @"(.*?)\\\\.*"))
                            Result[0] = Regex.Replace(Result[0], @"(.*)\\\\.*", "$1");
                    }

                    return Result.ToArray();
                }
            }
        }

        #endregion


        #region Private Static Methods

        private static Image ConvertArrayToImage(byte[] imageData)
        {
            Image newImage;

            if (imageData == null || imageData.Length == 0)
                return null;

            using (var ms = new MemoryStream(imageData, 0, imageData.Length))
            {
                ms.Write(imageData, 0, imageData.Length);
                newImage = Image.FromStream(ms, true);
            }
            return newImage;
        }

        #endregion
    }

}
