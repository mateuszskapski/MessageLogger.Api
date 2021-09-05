using MessageLogger.Api.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MessageLogger.Api.Models
{
    public class LogMessage
    {
        [NotDefault]
        public Guid MessageId { get; set; }

        [NotDefault]
        public DateTime Date { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Message { get; set; }
    }
}