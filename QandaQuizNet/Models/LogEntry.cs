using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QandaQuizNet.Models
{
    public class LogEntry
    {
        [Key]
        public int LogEntryId { get; set; }
        public DateTime LogEntryDateTime { get; set; }
        public string LogEntryDescription { get; set; }
    }
}