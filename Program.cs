using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                IPAddress ipAddress = IPAddress.Parse("192.168.100.3");
                int port = 8080;

                server = new TcpListener(ipAddress, port);

                server.Start();

                Console.WriteLine("Сервер запущено...");

                while (true)
                {
                    Console.WriteLine("Очікування клієнта...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Клієнт підключений!");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Отримано від клієнта: " + message);

                    string response = "Повідомлення отримано!";
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);

                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            finally
            {
                server?.Stop();
            }
        }
    }
}
