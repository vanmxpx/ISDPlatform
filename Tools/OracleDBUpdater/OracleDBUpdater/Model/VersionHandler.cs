using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OracleDBUpdater
{
    /// <summary> Class for convenient work with database versions. </summary>
    public static class VersionHandler
    {
        public static string db_table, db_field;
        public struct Version
        {
            public uint major_version {
                get
                {
                    return version_parts[0];
                }
            }
            public uint minor_version {
                get
                {
                    return version_parts[1];
                }
            }

            public uint patch_version {
                get
                {
                    return version_parts[2];
                }
            }

            private uint[] version_parts;

            public Version(uint major_version, uint minor_version, uint patch_version)
            {
                version_parts = new uint[]
                {
                    major_version,
                    minor_version,
                    patch_version
                };
            }

            public bool Equals(Version version)
            {
                return version.major_version == this.major_version
                    && version.minor_version == this.minor_version
                    && version.patch_version == this.patch_version;
            }

            public override string ToString()
            {
                return $"{this.major_version}.{this.minor_version}.{this.patch_version}";
            }

            public uint this[int key]
            {
                get
                {
                    return version_parts[key];
                }
                set
                {
                    version_parts[key] = value;
                }
            }

            public static bool operator >(Version version1, Version version2)
            {
                bool result = false;
                for (int i = 0; i < 3; i++)
                {
                    if (version1[i] > version2[i]) { result = true; break; }
                    else if (version1[i] > version2[i]) { break; }
                }
                return result;
            }

            public static bool operator <(Version version1, Version version2)
            {
                bool result = false;
                for (int i = 0; i < 3; i++)
                {
                    if (version1[i] < version2[i]) { result = true; break; }
                    else if (version1[i] > version2[i]) { break; }
                }
                return result;
            }

            public static bool operator <=(Version version1, Version version2)
            {
                return version1.major_version <= version2.major_version
                    && version1.minor_version <= version2.minor_version
                    && version1.patch_version <= version2.patch_version;
            }

            public static bool operator >=(Version version1, Version version2)
            {
                return version1.major_version >= version2.major_version
                    && version1.minor_version >= version2.minor_version
                    && version1.patch_version >= version2.patch_version;
            }
        }
        /// <summary> Return version from file name. </summary>
        public static bool TryGetVersionFromPath(string path, out Version version)
        {
            path = Path.GetFileNameWithoutExtension(path);
            path = path.Substring(path.LastIndexOf("_v") + 2);
            return TryParseVersion(path, out version);
        }

        public static bool TryParseVersion(string str, out Version version)
        {
            bool result = true;
            //Check version
            string[] elements_of_version = str.Split('.');
            version = new Version(0, 0, 0);

            for (int i = 0; i < 3 && i < elements_of_version.Length; i++)
            {
                if (UInt32.TryParse(elements_of_version[i], out uint parse_element))
                {
                    version[i] = parse_element;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary> Return current version of database. </summary>
        public static bool TryGetCurrentDatabaseVersion(out Version version)
        {
            bool result = false;
            version = new Version();
            try
            {
                result = TryParseVersion(MyDataBase.GetDB().ExecuteQueryWithAnswer($"SELECT {db_field} FROM {db_table}"), out version);
            }
            catch { }

            return result;
        }

        /// <summary> Set database version. </summary>
        public static void TrySetCurrentDatabaseVersion(Version version)
        {
            MyDataBase.GetDB().ExecuteQueryWithoutAnswer($"UPDATE {db_table} SET {db_field} = '{version.ToString()}'");
        }

        /// <summary> Checks if this version is exist. </summary>
        public static bool IsThereVersion(string requiredVersion)
        {
            bool result = false;
            if (TryParseVersion(requiredVersion, out Version version))
            {
                result = GetDatabaseVersions().Any(v => v.Equals(version));
            }

            return result;
        }

        public static bool IsThereVersion(Version version)
        {
            return GetDatabaseVersions().Any(v => v.Equals(version));
        }

        /// <summary> Return all versions of database. </summary>
        public static IEnumerable<Version> GetDatabaseVersions()
        {
            string[] updateScriptNames = GetFilesByPattern();

            foreach (string updateScriptName in updateScriptNames)
            {
                if (TryGetVersionFromPath(updateScriptName, out Version version))
                {
                    yield return version;
                }
            }
        }

        public static string[] GetFilesByPattern()
        {
            return Directory.GetFiles(Configuration.GetVariable("UpdateFolder"), "*_v*");
        }
    }
}