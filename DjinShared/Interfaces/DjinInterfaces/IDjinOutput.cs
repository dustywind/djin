namespace Djin.Shared.Interfaces
{
    public interface IDjinOutput
    {
        IDjinOutput Init(string connectionString);

        void Write(byte[] message);

        void Write(byte[] message, int offset, int length);

        void Write(string message);

        void WriteLine(string message);

        void Flush();

        void Close();
    }
}
