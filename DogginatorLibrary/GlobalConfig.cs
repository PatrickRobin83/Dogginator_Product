using DogginatorLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary
{
    public static class GlobalConfig
    {
        #region Fields

        #endregion

        #region Properties
        public static IDataConnection Connection { get; private set; }
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

        public  static string HashThePassword(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string passHash = GetMd5Hash(md5Hash, password);


                return passHash;
            }
        }

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
        #endregion
    }
}
