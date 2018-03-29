// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-17-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-17-2018
// ***********************************************************************
// <copyright file="EmailConfigContext.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Core.News.Mail
{
    /// <summary>
    /// Class EmailConfigurationContext
    /// </summary>
    public class EmailConfigContext : IDisposable

    {
        /// <summary>
        /// The configuration file
        /// </summary>
        public const string ConfigFile = "email.settings.json";

        /// <summary>
        /// Lock Config while running, since we update it. Using Config file for state. 
        /// TODO: Move to database with a UI Editor
        /// </summary>
        static FileStream dbLock = new FileStream(ConfigFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);


        /// <summary>
        /// The synchronize lock
        /// </summary>
        private static readonly object syncLock = new object();

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>EmailConfiguration.</returns>
        public static EmailConfiguration Load(ILoggerFactory loggerFactory)
        {
            EmailConfiguration emailConfiguration = null;
            try
            {
                lock (syncLock)
                {
                    if (File.Exists(ConfigFile) == false)
                    {
                        throw new FileNotFoundException(ConfigFile);
                    }
                    var path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(EmailConfiguration)).Location);
                    var json = ReadFile(); // File.ReadAllText(path + "\\" + ConfigFile);
                    emailConfiguration = JsonConvert.DeserializeObject<EmailConfiguration>(json);
                }
            }catch(Exception ex)
            {               
                loggerFactory.CreateLogger<EmailConfiguration>().
                    LogError(ex, "Email services will be disabled");              
            }          
            return emailConfiguration;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public static void Save(IEmailConfiguration config)
        {
            lock (syncLock)
            {
                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                WriteFile(json);
            }
        }
        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="json">The json.</param>
        private static void WriteFile(string json)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(json);
            dbLock.SetLength(0);
            dbLock.Write(info, 0, info.Length);
            dbLock.Flush();
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <returns>System.String.</returns>
        private static string ReadFile()
        {
            byte[] buffer;

            int length = (int)dbLock.Length;      // get file length
            buffer = new byte[length];            // create buffer
            int count;                            // actual number of bytes read
            int sum = 0;                          // total number of bytes read


            while ((count = dbLock.Read(buffer, sum, length - sum)) > 0)
                sum += count;  // sum is a buffer offset for next reading

            return Encoding.UTF8.GetString(buffer);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).                   
                    dbLock.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EmailConfigContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
