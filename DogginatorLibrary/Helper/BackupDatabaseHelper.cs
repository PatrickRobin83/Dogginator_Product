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
            if (!File.Exists($"{GlobalConfig.DatabaseBackupPath()}\\{GlobalConfig.DATABASEBACKUPFILENAME}"))
            {
                File.Copy(GlobalConfig.DatabaseFilename(), $"{GlobalConfig.DatabaseBackupPath()}\\{GlobalConfig.DATABASEBACKUPFILENAME}");
            }
            else
            {
                _dataBaseDate = File.GetLastWriteTime($"{GlobalConfig.DatabaseBackupPath()}\\{GlobalConfig.DATABASEBACKUPFILENAME}");
                TimeSpan sp = _dateOnStartup - _dataBaseDate;
                if (sp.Days > 3)
                {
                    File.Copy(GlobalConfig.DatabaseFilename(), $"{GlobalConfig.DatabaseBackupPath()}\\{GlobalConfig.DATABASEBACKUPFILENAME}");
                }
            }
        }

        #endregion
    }
}
