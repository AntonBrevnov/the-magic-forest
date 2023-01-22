using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TMFServer
{
    class Program
    {
        static int port = 8080;
        static string ipAddress = "127.0.0.1";

        static Socket socket;
        static Dictionary<string, PlayerData> playerList = new Dictionary<string, PlayerData>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Создание сервера...");

            Console.Write("Введите ip адрес: ");
            ipAddress = Console.ReadLine();
            Console.Write("Введите порт: ");
            port = Convert.ToInt32(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Запуск сервера. Ожидайте...");
            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Bind(ipPoint);
                socket.Listen(15);
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка запуска: " + exc.Message);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Сервер запущен.");
            Console.ForegroundColor = ConsoleColor.Gray;

            try
            {
                while (true)
                {
                    Socket handler = socket.Accept();
                    int bytes = 0;
                    byte[] data = new byte[1024]; 

                    do
                    {
                        // получение буферов запроса от клиентов
                        bytes = handler.Receive(data);
                        string stringBuffer = Encoding.Unicode.GetString(data, 0, bytes);

                        // извлечение данных из стркового буфера
                        string[] requestString = stringBuffer.Split(':');

                        // если команда запроса "подключение", то добавляем нового клиента
                        if (requestString[0] == "client_connect")
                        {
                            // уведомляем хоста об успешном подключении клиента
                            Console.WriteLine($"{requestString[1]} подключился к серверу");
                            // добавляем новый набор данных в таблицу
                            playerList.Add(requestString[1], new PlayerData());
                        } 
                        // если команда запроса "отключение", то удаляем клиента из списка
                        else if (requestString[0] == "client_disconnect")
                        {
                            // уведомляем хоста об отключении клиента
                            Console.WriteLine($"{requestString[1]} отключился от сервера");
                            // удаляем набор данных из таблицы
                            playerList.Remove(requestString[1]);
                        }
                        // если команда запроса "обновление", то обновляем данные о кленте
                        else if (requestString[0] == "client_update")
                        {
                            // выделяем категории данных из строкового пакета
                            string[] playerDataTypes = requestString[1].Split('/');

                            // разбиваем категории данных
                            string playerName = playerDataTypes[0];
                            string[] playerPosition = playerDataTypes[1].Split(' ');
                            string[] playerHealthable = playerDataTypes[2].Split(' ');
                            string[] playerFightable = playerDataTypes[3].Split(' ');

                            // обнавляем данные в таблице игроков
                            // позиция
                            playerList[playerName].PositionX = float.Parse(playerPosition[0]);
                            playerList[playerName].PositionY = float.Parse(playerPosition[1]);
                            // здоровье
                            playerList[playerName].HealthPoint = int.Parse(playerHealthable[0]);
                            playerList[playerName].HungerPoint = int.Parse(playerHealthable[1]);
                            playerList[playerName].DrinkPoint = int.Parse(playerHealthable[2]);
                            // сила
                            playerList[playerName].ManaPoint = int.Parse(playerFightable[0]);
                            playerList[playerName].DamagePoint = int.Parse(playerFightable[1]);
                            playerList[playerName].UltimateDamagePoint = int.Parse(playerFightable[2]);
                        }
                        // если команда запроса "получение информации", то отпрвляем в ответ на запрос данные о клиенте
                        else if (requestString[0] == "client_info")
                        {
                            byte[] buffer = new byte[1024];
                            try
                            {
                                // соствляем пакет из данных об игроке
                                string playerData = $"{playerList[requestString[1]].PositionX} {playerList[requestString[1]].PositionY}/" +
                                    $"{playerList[requestString[1]].HealthPoint} {playerList[requestString[1]].HungerPoint} {playerList[requestString[1]].DrinkPoint}/" +
                                    $"{playerList[requestString[1]].ManaPoint} {playerList[requestString[1]].DamagePoint} {playerList[requestString[1]].UltimateDamagePoint}";

                                buffer = Encoding.Unicode.GetBytes(playerData);
                                // отпрвляем пакет
                                handler.Send(buffer);
                            }
                            catch (Exception exc)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Ошибка отправки: " + exc.Message);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                        // если команда запроса "проверка на новых игроков", то отпрвляем в ответ на запрос данные об игроках
                        else if (requestString[0] == "client_check_new")
                        {
                            byte[] buffer = new byte[1024];
                            try
                            {
                                // составляем пакет со списком имён игроков
                                string playerListInfo = "";
                                foreach (var player in playerList)
                                    playerListInfo += player.Key + "/";
                                // отправляем пакет
                                buffer = Encoding.Unicode.GetBytes(playerListInfo);
                                handler.Send(buffer);
                            }
                            catch (Exception exc)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Ошибка отправки: " + exc.Message);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                    }
                    while (handler.Available > 0);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + exc.Message);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Нажмите любую клавишу для закрытия.");
            Console.ReadKey();
        }
    }
}
