namespace ChannelInFullNet
{
    public abstract class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OutMessage : Message
    {
    }

    public class InMessage : Message
    {
     
    }
    public class PubMessage:OutMessage
    {
        public string Topic { get; set; }
    }
}