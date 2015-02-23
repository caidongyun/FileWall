using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;


namespace VitaliiPianykh.FileWall.Shared
{
    public sealed class AdvRegistry
    {
        #region "Dynamic" Part

        private readonly string rootKeyName;
        private readonly string subKeyPath;
        private readonly string valueName;

        /// <summary>Construct new <see cref="AdvRegistry"/> class instance.</summary>
        /// <param name="registryPath">Must match to <see cref="RegistryPathPattern"/> regex pattern.</param>
        private AdvRegistry(string registryPath)
        {
            // Check correctness of registryPath.
            var Match = Regex.Match(registryPath, RegistryPathPattern);
            if(!Match.Success)
                throw new ArgumentException("Incorrect registry path. It must match following regex pattern:\r\n" + RegistryPathPattern);

            rootKeyName = Match.Groups["ROOT"].Value;
            subKeyPath = Match.Groups["PATH"].Value;
            valueName = Match.Groups["VAL"].Value;
        }


        /// <summary>Determines whether the specified key exists.</summary>
        private bool KeyExists
        {
            get
            {
                var SubKey = RootKeys[rootKeyName].OpenSubKey(subKeyPath);
                if (SubKey != null)
                    return true;

                return false;
            }
        }


        /// <summary>Returns value from registry.</summary>
        private object GetValueData()
        {
            if(!KeyExists)
                throw new FileNotFoundException("Specified key cannot be found.");

            var SubKey = RootKeys[rootKeyName].OpenSubKey(subKeyPath);

// ReSharper disable PossibleNullReferenceException
            return SubKey.GetValue(valueName);
// ReSharper restore PossibleNullReferenceException
        }

        #endregion


        #region Static Part

        private static readonly Dictionary<string, RegistryKey> RootKeys = new Dictionary<string, RegistryKey>();

        static AdvRegistry()
        {
            // List of available keys and they keys in registry.
            RootKeys.Add("HKCR", Registry.ClassesRoot);
            RootKeys.Add("HKEY_CLASSES_ROOT", Registry.ClassesRoot);
            RootKeys.Add("HKCU", Registry.CurrentUser);
            RootKeys.Add("HKEY_CURRENT_USER", Registry.CurrentUser);
            RootKeys.Add("HKLM", Registry.LocalMachine);
            RootKeys.Add("HKEY_LOCAL_MACHINE", Registry.LocalMachine);
            RootKeys.Add("HKU", Registry.Users);
            RootKeys.Add("HKEY_USERS", Registry.Users);
            RootKeys.Add("HKCC", Registry.CurrentConfig);
            RootKeys.Add("HKEY_CURRENT_CONFIG", Registry.CurrentConfig);
            RootKeys.Add("HKDD", Registry.DynData);
            RootKeys.Add("HKEY_DYN_DATA", Registry.DynData);
            RootKeys.Add("HKPD", Registry.PerformanceData);
            RootKeys.Add("HKEY_PERFORMANCE_DATA", Registry.PerformanceData);
        }


        /// <summary>Regex pattern for all registry paths.</summary>
        private static string RegistryPathPattern
        {
            get
            {
                // Create pattern based on list of available root key names (see RootKeys).
                var Pattern = "(?:(?<ROOT>";
                foreach (var KeyName in RootKeys.Keys)
                    Pattern += KeyName + "|";
                Pattern = Pattern.Remove(Pattern.Length - 1);// Delete last | delimiter.
                Pattern += ")\\\\(?:(?:(?<PATH>.*)\\\\\\\\(?<VAL>.*))|(?<PATH>.*?\\\\.*)|(?<PATH>.*)))|(?<ROOT>";
                foreach (var KeyName in RootKeys.Keys)
                    Pattern += KeyName + "|";
                Pattern = Pattern.Remove(Pattern.Length - 1);// Delete last | delimiter.
                Pattern += ")";

                return Pattern;
            }
        }


        // TODO: TEST it.
        public static string GetRootKey(string path)
        {
            return new AdvRegistry(path).rootKeyName;
        }

        public static void CreateKey(string registryPath)
        {
            var AdvReg = new AdvRegistry(registryPath);

            if(AdvReg.KeyExists)
                throw new ArgumentException("Key already exists.");

            var CreatedKey = RootKeys[AdvReg.rootKeyName].CreateSubKey(AdvReg.subKeyPath);
            if(CreatedKey == null)
                throw new ApplicationException("Operation failed.");
        }

        public static RegistryKey OpenSubKey(string registryPath)
        {
            var AdvReg = new AdvRegistry(registryPath);

            if(AdvReg.KeyExists == false)
                return null;

            if (AdvReg.subKeyPath == string.Empty)
                return RootKeys[AdvReg.rootKeyName];
            else
                return RootKeys[AdvReg.rootKeyName].OpenSubKey(AdvReg.subKeyPath);
        }


        public static void DeleteKey(string registryPath)
        {
            var AdvReg = new AdvRegistry(registryPath);

            RootKeys[AdvReg.rootKeyName].DeleteSubKeyTree(AdvReg.subKeyPath);
        }


        /// <summary>Determines correctness of registry path.</summary>
        public static bool IsRegistryPath(string registryPath)
        {
            return Regex.IsMatch(registryPath, RegistryPathPattern);
        }


        /// <summary>Determines whether the specified key exists.</summary>
        public static bool IsKeyExists(string registryPath)
        {
            var AdvReg = new AdvRegistry(registryPath);
            return AdvReg.KeyExists;
        }


        /// <summary>Returns value from registry.</summary>
        public static object GetValueData(string registryPath)
        {
            var AdvReg = new AdvRegistry(registryPath);
            return AdvReg.GetValueData();
        }


        /// <summary>Returns value from registry. If value doen't exists returns <see cref="defaultValue"/>.</summary>
        public static object GetValueData(string registryPath, object defaultValue)
        {
            var AdvReg = new AdvRegistry(registryPath);
            var Value = AdvReg.GetValueData();
            return Value ?? defaultValue;
        }

        /// <summary>Set value data in registry.</summary>
        public static void SetValueData(string valuePath, string valueData)
        {
            if (valuePath == null)
                throw new ArgumentNullException("valuePath");
            if (valuePath == string.Empty)
                throw new ArgumentOutOfRangeException("valuePath");
            if (valueData == null)
                throw new ArgumentNullException("valueData");

            var AdvReg = new AdvRegistry(valuePath);

            if (!AdvReg.KeyExists)
                throw new FileNotFoundException("Can't locate registry key.", AdvReg.rootKeyName + @"\" + AdvReg.subKeyPath);

            // ReSharper disable PossibleNullReferenceException
            RootKeys[AdvReg.rootKeyName].OpenSubKey(AdvReg.subKeyPath, true).SetValue(AdvReg.valueName, valueData, RegistryValueKind.String);
            // ReSharper restore PossibleNullReferenceException
        }

        #endregion



    }
}