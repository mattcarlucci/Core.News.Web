/// <summary>
/// The Mail namespace.
/// </summary>
namespace Core.News.Mail
{
    /// <summary>
    /// Class RestSmtpConfiguration.
    /// </summary>
    public class RestSmtpConfiguration : IRestSmtp
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>The API key.</value>
        public string ApiKey { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain { get; set; }
        /// <summary>
        /// Gets or sets the host URL.
        /// </summary>
        /// <value>The host URL.</value>
        public string HostUrl { get; set; }
    }
}