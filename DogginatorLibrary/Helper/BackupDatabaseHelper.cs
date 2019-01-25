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
            _dataBaseDate = CheckDatabaseDate();
            TimeSpan sp = _dateOnStartup - _dataBaseDate;
            if (sp.Days > 0)
            {
                //TODO - How to copy the Database to the BackupFolder

                Console.WriteLine($"Die Tage an denen die Datnebank nicht gebackuped wurde ist größer als {sp.Days}");
            }
        }

        private static DateTime CheckDatabaseDate()
        {

            if (File.Exists(GlobalConfig.DatabaseFilename()))
            {
                output = File.GetLastWriteTime(GlobalConfig.DatabaseFilename());
            }
            return output;
        }

        #endregion
    }
}
