namespace Delegate
{
    public class DelegateClient
    {
        private readonly DelegateServer _server ;

        public DelegateClient(DelegateServer server)
        {
            _server = server;
            _server.AddNumber+= ServerOnAddNumber;
        }

        private void ServerOnAddNumber(object? sender, NumberEventArgs e)
        {
            e.NumberValue++;
        }
    }
}