/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   BackupDatabaseHelper.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.IO;

namespace de.rietrob.dogginator_product.DogginatorLibrary.Helper
{
    public static class BackupDatabaseHelper
    {
        #region fields

        private static DateTime _dataBaseDate;
        private static DateTime _dateOnStartup = DateTime.Now;
        private static string _fileNamePrefix = DateTime.Now.ToString("yyyy-MM-dd");

        #endregion

        #region Properties

        #endregion

        #region Constructor
        #endregion

        #region Methods
        /// <summary>
        /// Checks if the database has a backup file if or if its older than 3 Days. If that is true the database will be stored in a backup database file
        /// </summary>
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
