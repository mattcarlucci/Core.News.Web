using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.News.Entities
{
    public class NewsSchedules
    {
        
        [StringLength(64)]
        [Key]
        public string Schedule { get; set; }
        public DateTime NewsStamp { get; set; }
    }
}
