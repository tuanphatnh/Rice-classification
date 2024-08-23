using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public ApplicationUser FromUser { get; set; }
        public long ToRoomId { get; set; }
        public Room ToRoom { get; set; }
    }
}
