namespace SignalRDemo.Models
{
    public class Participant
    {
        public long id { set; get; }
        public string UserId { get; set; }
        public long RoomId { get; set; }

        public Room Room { get; set; }

        public ApplicationUser User { get; set; }
    }
}
