/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   GlobalConfig.cs
 *   Date			:   2019-01-26 01:40
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using de.rietrob.dogginator_product.DogginatorLibrary.DataAccess;
using de.rietrob.dogginator_product.DogginatorLibrary.Enums;
using System;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace de.rietrob.dogginator_product.DogginatorLibrary
{
    public static class GlobalConfig
    {
        #region Fields

        #endregion

        #region Properties
        public static IDataConnection Connection { get; private set; }
        public const string DATABASEBACKUPFILENAME = @"Dogginator_Backup.db"; 
        public const string CANCEL = "Cancel";
        public const string USERCREATED = "User Created";
        public const string USERDELETED = "User Deleted";
        public const string USEREDIT = "User Edited";

        #endregion

        #region Contstructor

        #endregion

        #region Methods
        /// <summary>
        /// Select which Datastorage to use 
        /// </summary>
        /// <param name="dbType"></param>
        public static void InitalizeConnections(DataBaseType dbType)
        {
            if (dbType == DataBaseType.SQLite)
            {
                SQLiteConnector sql = new SQLiteConnector();
                Connection = sql;
            }
        }

        /// <summary>
        /// get the Connection String from the Configuration Manager
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        /// <summary>
        ///  Database Filename
        /// </summary>
        public static string DatabaseFilename()
        {
            return ConfigurationManager.AppSettings[0].ToString();
        }
        /// <summary>
        /// Backup Path for the Database
        /// </summary>
        public static string DatabaseBackupPath()
        {
            return $"{ConfigurationManager.AppSettings[1].ToString()}";
        }
        /// <summary>
        /// Function to encrypt the password into a Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns>password string in decrypted</returns>
        public  static string HashThePassword(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string passHash = GetMd5Hash(md5Hash, password);


                return passHash;
            }
        }
        /// <summary>
        /// Converts the given string into a md5 hash
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns>hexadecimal string</returns>
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        /// <summary>
        /// Determine the current Week of the Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The Week in a number between 1 and 52</returns>
        public static int getWeekOfYear(System.DateTime date)
        {
            CultureInfo CUI = CultureInfo.CurrentCulture;
            return CUI.Calendar.GetWeekOfYear(date, CUI.DateTimeFormat.CalendarWeekRule, CUI.DateTimeFormat.FirstDayOfWeek);
        }
        /// <summary>
        /// Determine the first day of the week 
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns>The Date of the Monday in the Week</returns>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;

            DayOfWeek firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
        #endregion
    }
}