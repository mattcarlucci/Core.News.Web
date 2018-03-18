using System.Collections.Generic;

namespace Crypto.Compare.Configs
{
    /// <summary>
    /// Class EmailConfig.
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// Class MailUser.
        /// </summary>
        public class MailUser
        {
            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            /// <value>The email.</value>
            public string Email { get; set; }
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="MailUser"/> is active.
            /// </summary>
            /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
            public bool Active { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="MailUser"/> class.
            /// </summary>

            public MailUser() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="MailUser"/> class.
            /// </summary>
            /// <param name="email">The email.</param>
            public MailUser(string email)
            {
                this.Email = email;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable email].
        /// </summary>
        /// <value><c>true</c> if [enable email]; otherwise, <c>false</c>.</value>
        public bool EnableEmail { get; set; }

        /// <summary>
        /// Gets or sets the SMTP host.
        /// </summary>
        /// <value>The SMTP host.</value>
        public string SmtpHost { get; set; }
        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        /// <value>The SMTP port.</value>
        public int SmtpPort { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value><c>true</c> if [use SSL]; otherwise, <c>false</c>.</value>
        public bool UseSsl { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public MailUser From { get; set; }
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public List<MailUser> To { get; set; }
        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>The cc.</value>
        public List<MailUser> Cc { get; set; }
        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>The BCC.</value>
        public List<MailUser> Bcc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfig"/> class.
        /// </summary>
        public EmailConfig()
        {
            To = new List<MailUser>();
            From = new MailUser();
            Cc = new List<MailUser>();
            Bcc = new List<MailUser>();
        }
    }
}
