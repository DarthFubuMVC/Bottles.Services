namespace Bottles.Services.Messaging
{
    public interface IListener<T>
    {
        void Receive(T message);
    }
}