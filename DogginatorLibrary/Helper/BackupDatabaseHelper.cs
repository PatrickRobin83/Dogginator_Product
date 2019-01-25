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
        public static string CheckDatabaseDate()
        {
            string output = "";
            if (File.Exists(GlobalConfig.DatabaseFilename()))
            {
                output = File.GetCreationTime(GlobalConfig.DatabaseFilename()).ToShortDateString(); 
            }
            return output;
        }
    }
}
