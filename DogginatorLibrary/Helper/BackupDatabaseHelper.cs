using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DogginatorLibrary.Helper
{
    public static class BackupDatabaseHelper
    {
        #region fields

        private static DateTime _dataBaseDate;
        private static DateTime _dateOnStartup = DateTime.Now;
        private static string _fileNamePrefix = DateTime.Now.ToString("yyyy-MM-dd");

        #endregion

        #region Properties
        public static DateTime output { get; set; }

        #endregion

        #region Methods

        public static void BackupDatabase()
        {
            if (!Directory.Exists(GlobalConfig.DatabaseBackupPath()))
            {
                Directory.CreateDirectory(GlobalConfig.DatabaseBackupPath());
            }
            if (!File.Exists($"{GlobalConfig.DatabaseBackupPath()}\\{_fileNamePrefix}_{GlobalConfig.DATABASEBACKUPFILENAME}"))
            {
                File.Copy(GlobalConfig.DatabaseFilename(), $"{GlobalConfig.DatabaseBackupPath()}\\{_fileNamePrefix}_{GlobalConfig.DATABASEBACKUPFILENAME}");
            }
            else
            {
                _dataBaseDate = File.GetLastWriteTime($"{GlobalConfig.DatabaseBackupPath()}\\{GlobalConfig.DATABASEBACKUPFILENAME}");
                TimeSpan sp = _dateOnStartup - _dataBaseDate;
                if (sp.Days > 3)
                {
                    if (File.Exists($"{GlobalConfig.DatabaseBackupPath()}\\{_fileNamePrefix}_{GlobalConfig.DATABASEBACKUPFILENAME}"))
                    {
                        File.Delete($"{GlobalConfig.DatabaseBackupPath()}\\{_fileNamePrefix}_{GlobalConfig.DATABASEBACKUPFILENAME}");
                    }
                    File.Copy(GlobalConfig.DatabaseFilename(), $"{GlobalConfig.DatabaseBackupPath()}\\{_fileNamePrefix}_{GlobalConfig.DATABASEBACKUPFILENAME}");
                }
            }
        }

        #endregion
    }
}
