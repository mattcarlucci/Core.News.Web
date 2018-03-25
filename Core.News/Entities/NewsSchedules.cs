using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.News.Entities
{
    public class NewsSchedules
    {
        private string key;
        private IEnumerable<DateTime> enumerable;

        public NewsSchedules(string key, IEnumerable<DateTime> enumerable)
        {
            this.key = key;
            this.enumerable = enumerable;
        }

        [StringLength(64)]
        [Key]
        public string Schedule { get; set; }
        public DateTimeOffset NewsStamp { get; set; }
    }
}
