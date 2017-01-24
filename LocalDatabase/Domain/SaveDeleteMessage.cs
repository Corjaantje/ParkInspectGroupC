using System;

namespace LocalDatabase.Domain
{
    public class SaveDeleteMessage
    {
        public string Action { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}