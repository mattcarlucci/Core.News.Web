using Core.News.Mail;
using Core.News.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.News
{
    public class Connection
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Store { get; set; }
    }

    /// <summary>
    /// Class Configuration
    /// </summary>
    public class NewsConfiguration 
    {        
        /// <summary>
        /// The file
        /// </summary>
        public const string configFile = ".\\news.config.json";
        
        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        /// <value>The connections.</value>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public string Connection { get; set; }
        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval { get; set; }
        /// <summary>
        /// Gets or sets the interval start.
        /// </summary>
        /// <value>The interval start.</value>
        public string IntervalStart { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [save stories].
        /// </summary>
        /// <value><c>true</c> if [save stories]; otherwise, <c>false</c>.</value>
        public bool SaveStories { get; set; }
      
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public EmailConfiguration EmailConfiguration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsConfiguration"/> class.
        /// </summary>
        public NewsConfiguration()
        {
            this.Interval = 720;
         //   Database = "News.Core.SqlServer.Models.DbNewsContext";
        }

        public static NewsConfiguration Load(string json)
        {
            var config = JsonConvert.DeserializeObject<NewsConfiguration>(json);
            config.GetDefaultConnection();
            return config;
        }
        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>       
        public static NewsConfiguration Load()
        {
            // if (File.Exists(configFile) == false) return null;
            var path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(NewsConfiguration)).Location);
            var json = File.ReadAllText(path + "\\" + configFile);
            var config = JsonConvert.DeserializeObject<NewsConfiguration>(json);
            config.GetDefaultConnection();
            return config;
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            var config = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(configFile, config);
        }

        /// <summary>
        /// Gets the default connection.
        /// </summary>
        /// <returns>Connection.</returns>
        public Connection GetDefaultConnection()
        {
            try
            {
                return Connections.Single(s => s.Key == this.Connection);
            }
            catch (Exception)
            {

                throw new InvalidConfigurationException("No Default Database connection found in " + configFile);
            }
            
        }
        
    }
}
