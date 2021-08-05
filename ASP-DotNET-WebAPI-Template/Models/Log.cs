using System;
using System.ComponentModel.DataAnnotations;

namespace ASP_DotNET_WebAPI_Template.Models
{
    public class Log
    {
        [Key]
        public int ID { get; set; }

        public string Message { get; set; }
        
        public string MessageTemplate { get; set; }
        
        public string Level { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public string Exception { get; set; }
        
        public string XmlProperties { get; set; }
        
        public string LogEvent { get; set; }
    }
}