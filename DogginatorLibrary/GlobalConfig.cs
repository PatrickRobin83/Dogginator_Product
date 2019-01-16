﻿using DogginatorLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
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

        #endregion
    }
}
