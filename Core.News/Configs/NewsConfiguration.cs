using Core.News.Mail;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Core.News.Configs
{
    /// <summary>
    /// Class CryptoConfig.
    /// </summary>
    public class NewsConfiguration
    {
        const string file = ".\\crypto.config.json";
        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [save stories].
        /// </summary>
        /// <value><c>true</c> if [save stories]; otherwise, <c>false</c>.</value>
        public bool SaveStories { get; set; }
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public string Database { get; set; }
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
            this.Interval = 1000 * 60 * 60 * 24;
            Database = "Crypto.News.CIK_Lite";
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>CryptoConfig.</returns>
        public static NewsConfiguration Load()
        {
            if (File.Exists(file) == false) return null;

            var json = File.ReadAllText(file);
            var config = JsonConvert.DeserializeObject<NewsConfiguration>(json);
            return config;
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            var config = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(file, config);
        }
        
    }
}
