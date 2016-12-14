using System;

namespace LocalDatabase.Domain
{
    public class UpdateMessage
    {
        public string LocalDatabaseName { get; set; }
        public string CentralDatabaseName { get; set; }
        public int LocalId { get; set; }
        public int LocalId_Extra { get; set; }
        public int CentralId { get; set; }
        public int CentralId_Extra { get; set; }
        public DateTime LocalId_DateTime { get; set; }
        public DateTime CentralId_DateTime { get; set; }
        public DateTime LocalDateTime { get; set; }
        public DateTime CentralDateTime { get; set; }
        public string Message { get; set; }
    }
}