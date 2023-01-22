using build_alpha_0._2.ECS;
using build_alpha_0._2.NPC;
using build_alpha_0._2.UI;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace build_alpha_0._2.Core.Network
{
    public class TMFClient
    {
        Socket clientSocket;
        IPEndPoint ipPoint;
        string clientName;

        public TMFClient(string name)
        {
            clientSocket = null;
            ipPoint = null;
            clientName = name;
        }

        public bool Connect(string address, int port)
        {
            byte[] buffer;
            try
            {
                buffer = null;
                
                // инициализация конечной точки
                ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                // создание сокета
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключение к конечной точке
                clientSocket.Connect(ipPoint);
                // создание пакета данных на отправку
                buffer = Encoding.Unicode.GetBytes($"client_connect:{clientName}");
                // отправка пакета
                clientSocket.Send(buffer);

                // если подключение успешно, возвращаем true
                return true;
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка подключения: " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                // если подключение не удалось, возвращаем false
                return false;
            }
        }

        public void UpdatePlayerData(Player player)
        {
            byte[] buffer;
            try
            {
                buffer = null;
                // составляем пакет данных, который будет хранить информацию об игроке (имя, позиция, здоровье, сила)
                string stringBuffer = $"client_update:{clientName}/{player.Shape.Position.X} {player.Shape.Position.Y}/" +
                    $"{player.Healthable.HP} {player.Healthable.HG} {player.Healthable.DP}/" +
                    $"{player.Fightable.MN} {player.Fightable.DM} {player.Fightable.UDM}";
                buffer = Encoding.Unicode.GetBytes(stringBuffer);
                // инициализируем сокет
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // связываемся с конечной точкой
                clientSocket.Connect(ipPoint);
                // отправляем пакет
                clientSocket.Send(buffer);
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка обновления данных игрока: " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        public void GetPlayerData(string otherPlayerName, ref Vector2f position)
        {
            byte[] buffer;
            try
            {
                int bytes = 0;
                buffer = new byte[1024];

                // составляем пакет для отправки запроса о получении данных указанного игрока
                string playerDataString = $"client_info:{otherPlayerName}";
                buffer = Encoding.Unicode.GetBytes(playerDataString);

                // инициализируем сокет
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // связываемся с конечной точкой
                clientSocket.Connect(ipPoint);
                // отправляем пакет
                clientSocket.Send(buffer);

                // получаем ответ
                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
                string stringBuffer = Encoding.Unicode.GetString(buffer, 0, bytes);

                // разбиение данных на категории
                string[] playerDataTypes = stringBuffer.Split('/');

                // обновляем данные указанного игрока
                // позиция
                string[] playerPosition = playerDataTypes[0].Split(' ');
                position = new Vector2f(float.Parse(playerPosition[0]), float.Parse(playerPosition[1]));
                // здоровье
                string[] playerHealthable = playerDataTypes[1].Split(' ');
                // сила
                string[] playerFightable = playerDataTypes[2].Split(' ');
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка получения данных об игроке: " + otherPlayerName + ". " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        public void CheckNewPlayers(ref List<Player> playerList, ref List<UIText> playerNamesList, ref SystemManager manager)
        {
            byte[] buffer;
            try
            {
                int bytes = 0;
                buffer = new byte[1024];

                // инициализируем сокет
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // связывемся с конечной точкой
                clientSocket.Connect(ipPoint);
                // отправляем запрос о получении инфор-ции о новых игроках
                buffer = Encoding.Unicode.GetBytes("client_check_new");
                clientSocket.Send(buffer);

                // получаем ответ
                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
                string stringBuffer = Encoding.Unicode.GetString(buffer, 0, bytes);

                // извлечение данных из строкового буфера
                string[] playersNicknames = stringBuffer.Split('/');
                var names = new Dictionary<string, int>();

                // составляем список уже существеющих имен для проверки
                for (int i = 0; i < playerList.Count; i++) 
                {
                    names.Add(playerList[i].Name, i);
                }
                // проверяем список клиента
                for (int i = 0; i < playersNicknames.Length; i++)
                {
                    if (playersNicknames[i] != "")
                        // если список не имеет некоторого имени
                        if (!names.ContainsKey(playersNicknames[i]))
                        {
                            /// добавляем игрока с таким именем
                            playerList.Add(new Player(ref manager, playersNicknames[i]));
                            // создаем текстовое поле, для идентификации игрока (представляет того, кто играет за него играет)
                            playerNamesList.Add(new UIText(playersNicknames[i], 7, ResourcesManager.GetFont("ui_font"), Color.Yellow));
                        }
                }
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка получения данных о новых игроках. " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void Exit()
        {
            byte[] data;
            try
            {
                // инициалдизируем сокет
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // связываемся с конечной точкой
                clientSocket.Connect(ipPoint);
                // создаем пакет данных об отключении
                data = Encoding.Unicode.GetBytes($"client_disconnect:{clientName}");
                // отправляем пакет
                clientSocket.Send(data);

                // закрываем сокет
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ошибка отклчения: " + exc.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
