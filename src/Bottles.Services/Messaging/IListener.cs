namespace Bottles.Services.Messaging
{
    public interface IListener
    {
        void Receive<T>(T message);
    }
}