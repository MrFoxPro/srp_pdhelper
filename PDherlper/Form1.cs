using API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using SAMP_API;
using System.Drawing;

namespace PDherlper
{
    public partial class Form1 : Form
    {
        class Day
        {
            public List<AmbulanceCall> Calls = new List<AmbulanceCall>();
            public DateTime date;
        }
        class Settings
        {
            public string Rank = "Сержант";
            public int diffMoscowTime = 0;
            public bool enableAmmo = true;
            public bool enableFlint = true;
            public bool enableIdlewood = true;
            public bool enableMulholland = true;
            public bool enableVictim = true;
            public bool enableSMS = true;
            public bool enableWanted = true;
            public bool enablePaintball = true;
            public string RobCodeWord = "rob fletcher";
            public bool showPass = false;
            public string Tag = "";
        }
        struct Person
        {
            public string Name;
            public int Id;
        }
        struct AmbulanceCall
        {
            public Person Caller;
            public double Distance;
            public bool Accepted;
        }

        static Chat Chat;
        static Game Game;
        static Player Player;
        static World World;
        static Vehicle Vehicle;
        static RemotePlayer remotePlayer;
        static InputSimulator sim;
        static ZoneManager zoneManager;
        // static System.Threading.Timer TimerReport;
        int diffWithMoscowHours = 0;
        static List<AmbulanceCall> AmbulanceCalls;
        // static int HealedCount = 0;
        static string MyName;

        Settings settings;
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            settings = LoadSettings();
            ApplySettings();
            sim = new InputSimulator();
            zoneManager = new ZoneManager();
            bool initResult = API.API.TryInit();
            int newApiInited = SAMP_API.API.Init();
            Log("Ожидаем запуск sampiqa");
            var loadingTask = Task.Run(() =>
            {
                while (!initResult && newApiInited < 1)
                {
                    initResult = API.API.TryInit();
                    newApiInited = SAMP_API.API.Init();
                    Thread.Sleep(10);
                }
                if (this.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {

                        Log("SAMP обнаружен");
                    }));
                }
            });
            await loadingTask;
            LoadAll();


        }
        private void Log(string msg)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => logTextBox.AppendText(msg + Environment.NewLine)));
        }
        private void LoadAll()
        {
            Thread.Sleep(3000);
            Chat = Chat.GetInstance();
            Game = Game.GetInstance();
            Player = Player.GetInstance();
            World = World.GetInstance();
            Vehicle = Vehicle.GetInstance();
            remotePlayer = RemotePlayer.GetInstance();

            AmbulanceCalls = new List<AmbulanceCall>();
            Chat.OnChatMessage += OnChatMessageReceived;
            Chat.OnCommand += OnCommandReceived;
            MyName = Player.GetName();
            LoadRobberies();


            Task.Run(() =>
            {
                List<string> robsToRemove = new List<string>();
                while (true)
                {

                    foreach (var r in Robberies)
                    {
                        TimeSpan delta = r.Value.AddMinutes(28) - DateTime.Now;
                        if (delta.Minutes > 0 && delta.Minutes < 4)
                        {
                            Chat.Send($"/fb [Warning] Через {delta.Minutes} минуты(у) планируется ограбление {r.Key}");
                            SAMP_API.API.ShowGameText($"Prepare for robbery. {r.Key} will be in {delta.Minutes} minutes", 2, 1);
                            Task.Delay(1200);
                        }
                        else if (delta.Hours < -2)
                        {
                            robsToRemove.Add(r.Key);
                        }
                    }
                    foreach (string rob in robsToRemove)
                    {
                        Robberies.Remove(rob);
                    }
                    DrawUIRobTimings();
                    Task.Delay(1000 * 60).Wait();
                }
            });


            SAMP_API.API.DestroyAllVisual();
            Thread.Sleep(500);
            SAMP_API.API.ShowGameText("PDHelper by FoxPro loaded", 20, 1);

            Chat.RegisterCommand("/rob", (c, v) =>
            {
                SendRobInfo();
            });
            Chat.RegisterCommand("/sfollow", (c, v) =>
            {
                Thread.Sleep(1100);
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id).Replace("_", " ");
                Chat.Send($"/me взял преступника за локоть и повел его");
                Thread.Sleep(1170);
                Chat.Send($"/follow {id}");
            });
            Chat.RegisterCommand("/clearrob", (c, v) =>
            {
                ClearRob();
            });
            Chat.RegisterCommand("/scput", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                Thread.Sleep(1100);

                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);

                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id).Replace("_", " ");
                Thread.Sleep(1170);
                Chat.Send("/me открыл дверь автомобиля");
                Thread.Sleep(1170);
                Chat.Send($"/me ухватил {name} за руки и шею");
                Thread.Sleep(1170);
                Chat.Send($"/cput {id}");
                Thread.Sleep(1170);
                Chat.Send("/me закрыл дверь");
            });
            Chat.RegisterCommand("/scuff", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                Thread.Sleep(1100);
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id).Replace("_", " ");
                Chat.Send($"/me снял наручники с пояса и зацепил на руки {name}");
                Thread.Sleep(1150);
                Chat.Send($"/cuff {id}");
            });
            Chat.RegisterCommand("/smdc", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                Thread.Sleep(1100);
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                // string name = remotePlayer.GetPlayerNameById(id, true);
                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id);
                Thread.Sleep(1170);
                Chat.Send($"/seeme включил мобильный компьютер и пробил информацию о {name}".Replace("_", " "));
                Thread.Sleep(1170);
                Chat.Send($"/mdc {id}");
            });
            Chat.RegisterCommand("/sfrisk", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                Thread.Sleep(1100);
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id).Replace("_", " ");
                Thread.Sleep(1170);
                Chat.Send("/me достал из кармана и надел перчатки");
                Thread.Sleep(1170);
                Chat.Send($"/me похлопал {name} по телу, проверил карманы, обыскав его");
                Thread.Sleep(1170);
                Chat.Send($"/frisk {id}");
            });
            Chat.RegisterCommand("/sticket", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id).Replace("_", " ");
                Thread.Sleep(1170);
                Chat.Send($"/me взял документы {name}");
                Thread.Sleep(1170);
                Chat.Send($"/me достал бланк и ручку, вписал данные паспорта {name} и поставил роспись");
            });
            Chat.RegisterCommand("/shi", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                Thread.Sleep(1100);
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                string MyName = Player.GetName().Replace("_", " ");
                Chat.Send($"{nameTextBox.Text} LSPD {MyName}.");
                if (shouldShowPassCheckBox.Checked)
                {
                    Thread.Sleep(1100);
                    Chat.Send($"/showpass {id}");
                }

            });
            Chat.RegisterCommand("/sceject", (c, v) =>
            {
                if (v.Length < 1)
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                Thread.Sleep(1170);
                if (string.IsNullOrEmpty(v[0]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                uint id = uint.Parse(v[0]);
                string name = SAMP_API.API.GetPlayerNameByIDEx((int)id).Replace("_", " ");
                //Thread.Sleep(1170);
                Chat.Send("/me открыл дверь");
                Thread.Sleep(1170);
                Chat.Send($"/me взял {name} за шею");
                Thread.Sleep(1170);
                Chat.Send($"/ceject {id}");
            });
            Chat.RegisterCommand("/aac", OnServiceAccepted);
            Chat.RegisterCommand("/aacc", OnCustomServiceAccepted);
            Chat.RegisterCommand("/ph", (c, v) =>
            {
                Chat.AddMessage("PoliceHelper is working!", "f56342");
                Thread.Sleep(1450);
                Chat.Send("/f Ребят. Я складываю полномочия и передаю их новому шерифу LSPD - Диме Галкину.");
            });
            Chat.RegisterCommand("/gs", (c, v) =>
            {
                Chat.AddMessage($"Маркер находится в {GetCurrentCity()}, {GetCurrentZone()}");
            });
            Chat.RegisterCommand("/delo", (c, v) =>
            {
                if (v.Length < 3)
                {
                    Chat.AddMessage("Введите id, количество звезд и параметр [off/dead/ok]");
                    return;
                }
                Thread.Sleep(1100);
                if (string.IsNullOrEmpty(v[0]) || string.IsNullOrEmpty(v[1]) || string.IsNullOrEmpty(v[2]))
                {
                    Chat.AddMessage("Введите id человека");
                    return;
                }
                string deloid = v[0];
                int srok = int.Parse(v[1]);
                string type = v[2];
                string srok_s = "года";
                if (srok == 1) srok_s = "год";
                else if (srok == 5 || srok == 6) srok_s = "лет";
                if (type == "off")
                    Chat.Send($"/f {settings.Tag} Дело № XX закрыто. Преступнику грозит {srok} {srok_s} ТЗ.");
                else if (type == "dead")
                    Chat.Send($"/f {settings.Tag} Дело № {deloid} закрыто. Преступник доправлен в больницу, ему грозит {srok} {srok_s} ТЗ.");
                else Chat.Send($"/f {settings.Tag} Дело № {deloid} закрыто. Преступнику грозит {srok} {srok_s} ТЗ.");
            });
            Chat.RegisterCommand("/mhstats", (c, v) =>
            {
                var ds = GetAllDays();
                if (ds != null)
                {
                    int totalCallAccepted = 0;
                    foreach (var day in ds)
                    {
                        totalCallAccepted += day.Calls.Count;
                    }
                    string report = $"Всего принято вызовов: {totalCallAccepted}";
                    Chat.AddMessage(report);
                    Log(report);
                }
            });
            Chat.AddMessage("PoliceHelper by FoxPro [Dima_Galkin] loaded sucessfully!", "f56342");
            Chat.AddMessage("/ph - статус программы, /gs - текущая зона и город, /aac - принять последний вызов, /aacc [id] - принять вызов от id", "f56342");
            Chat.AddMessage("/clearrob, /scput, /sceject, /shi, /scuff, /sfrisk, /smdc", "f56342");
            Chat.AddMessage("/gs - текущее местоположение маркера на карте", "f56342");
            Thread.Sleep(50);

            var days = GetAllDays();
            if (days != null)
            {
                int totalCallAccepted = 0;
                foreach (var day in days)
                {
                    if (day.date.Date == DateTime.Now.Date)
                    {
                        AmbulanceCalls = day.Calls;

                    }
                    totalCallAccepted += day.Calls.Count;
                }
                string report = $"Всего принято вызовов: {totalCallAccepted}";
                Chat.AddMessage(report);
                Log(report);
            }
        }

        List<int> timingLabelIds = new List<int>();
        private void DrawUIRobTimings()
        {
            foreach (int i in timingLabelIds)
            {
                SAMP_API.API.TextDestroy(i);
            }
            timingLabelIds = new List<int>();
            int offset = 0;
            int id = SAMP_API.API.TextCreate("Segoe UI", 15, false, true, 10, 260, -0xfa7f28, "Timings:", true, true);
            timingLabelIds.Add(id);
            foreach (var item in Robberies)
            {
                offset++;
                id = SAMP_API.API.TextCreate("Segoe UI", 9, false, false, 10, 260 + 15 * offset, -0x284efa, $"{item.Key}: {item.Value.AddHours(diffWithMoscowHours).ToShortTimeString()}. Will be in {item.Value.AddHours(diffWithMoscowHours).AddMinutes(28).ToShortTimeString()}", true, true);
                timingLabelIds.Add(id);
            }
        }


        private void SaveSettings()
        {

            settings.diffMoscowTime = (int)moscowTimeDiffDropdown.Value;

            settings.enableAmmo = handleAmmoLSCheckBox.Checked;
            settings.enableFlint = handleFlintCheckBox.Checked;
            settings.enableMulholland = handleMulholandCheckBox.Checked;
            settings.enableVictim = handleVictimLSCheckBox.Checked;
            settings.enableIdlewood = handleIdleewodCheckBox.Checked;
            settings.enableSMS = enableSMSInfoCheckBox.Checked;
            settings.enablePaintball = enablePaintballNotificationCheckBox.Checked;
            settings.enableWanted = showWantedCheckBox.Checked;
            settings.showPass = shouldShowPassCheckBox.Checked;

            settings.Rank = nameTextBox.Text;

            settings.Tag = tagTextBox.Text;
            settings.RobCodeWord = RobCodeWord.Text;
            File.WriteAllText("settings.txt", JsonConvert.SerializeObject(settings));
        }
        private Settings LoadSettings()
        {
            if (!File.Exists("settings.txt")) return new Settings();
            string fileText = File.ReadAllText("settings.txt", System.Text.Encoding.UTF8);
            return JsonConvert.DeserializeObject<Settings>(fileText);
        }
        private void ApplySettings()
        {

            handleAmmoLSCheckBox.Checked = settings.enableAmmo;
            handleFlintCheckBox.Checked = settings.enableFlint;
            handleMulholandCheckBox.Checked = settings.enableMulholland;
            handleVictimLSCheckBox.Checked = settings.enableVictim;
            handleIdleewodCheckBox.Checked = settings.enableIdlewood;

            enableSMSInfoCheckBox.Checked = settings.enableSMS;
            showWantedCheckBox.Checked = settings.enableWanted;
            enablePaintballNotificationCheckBox.Checked = settings.enablePaintball;
            tagTextBox.Text = settings.Tag;

            diffWithMoscowHours = settings.diffMoscowTime;
            moscowTimeDiffDropdown.Value = settings.diffMoscowTime;

            shouldShowPassCheckBox.Checked = settings.showPass;
            nameTextBox.Text = settings.Rank;

            RobCodeWord.Text = settings.RobCodeWord;
        }
        private static void LoadRobberies()
        {
            if (File.Exists("rob.txt"))
                Robberies = JsonConvert.DeserializeObject<Dictionary<string, DateTime>>(File.ReadAllText("rob.txt")) ?? new Dictionary<string, DateTime>();
            else Robberies = new Dictionary<string, DateTime>();
        }

        /*private void TimeReport(string command, string[] args)
        {
            if (args.Length == 0)
            {
                Chat.AddMessage("Введите параметры [on/off] [время в минутах]");
                return;
            }
            else if (args.Length == 1)
            {
                if (args[0] == "off")
                {
                    if (TimerReport != null)
                        TimerReport.Dispose();
                    Chat.AddMessage("Таймер отчетов выключен");
                    return;
                }
                else
                {
                    Chat.AddMessage("Введите параметры [on/off] [время в минутах]");
                    return;
                }
            }

            string onoff = args[0];
            int period = int.Parse(args[1]);
            if (onoff == "on")
            {
                TimerReport = new System.Threading.Timer((state) => MakeReport(), null, 5000, period * 1000 * 60);
                Chat.AddMessage($"Установлен таймер на {period} миунт");
            }
        }
        */
        /*private void MakeReport(bool advanced = false)
        {
            Thread.Sleep(1100);
            Chat.Send($"/f {DateTime.Now.AddHours(diffWithMoscowHours).ToString("HH:mm")} | Патруль | Вызовов: {AmbulanceCalls.Count} | Вылечено: {HealedCount}");
            Thread.Sleep(1100);
            Chat.Send($"/f Местоположение: {GetCurrentCity()}, {GetCurrentZone()}");
            if (advanced)
            {
                Thread.Sleep(1250);
                if (AmbulanceCalls.Count == 0) return;
                Chat.Send($"/f Приняты вызовы от:");
                Thread.Sleep(1250);
                string threeNames = "";
                for (int i = 0; i < AmbulanceCalls.Count; i++)
                {
                    if (!AmbulanceCalls[i].Accepted) continue;
                    if (i == AmbulanceCalls.Count - 1)
                        threeNames += AmbulanceCalls[i].Caller.Name.Replace("_", " ");
                    else
                        threeNames += AmbulanceCalls[i].Caller.Name.Replace("_", " ") + ", ";
                    if (i % 3 == 0 || i == AmbulanceCalls.Count - 1)
                    {
                        Chat.Send($"/f {threeNames}");
                        threeNames = "";
                        Thread.Sleep(1250);
                    }
                }
            }
        }
        */
        static List<Day> GetAllDays()
        {
            if (!File.Exists("stats.txt")) return null;
            return JsonConvert.DeserializeObject<List<Day>>(File.ReadAllText("stats.txt"));
        }
        static void SaveAmbulanceCall(AmbulanceCall call)
        {
            if (!File.Exists("stats.txt"))
            {
                FileStream fs = File.Create("stats.txt");
                fs.Close();
            }
            List<Day> days = JsonConvert.DeserializeObject<List<Day>>(File.ReadAllText("stats.txt"));
            if (days == null) days = new List<Day>();
            int ind = days.FindIndex(x => x.date.Date == DateTime.Now.Date);
            if (ind > -1)
            {
                //if (HealedCount > days[ind].HealedCount)
                ///days[ind].HealedCount = HealedCount;
                days[ind].Calls.Add(call);
            }
            else
            {
                Day day = new Day
                {
                    Calls = AmbulanceCalls,
                    date = DateTime.Now,
                    //HealedCount = HealedCount
                };
                days.Add(day);
            }
            File.WriteAllText("stats.txt", JsonConvert.SerializeObject(days));
        }
        /*private static void SaveHealedCount()
        {
            if (!File.Exists("stats.txt"))
            {
                FileStream fs = File.Create("stats.txt");
                fs.Close();
            }
            List<Day> days = JsonConvert.DeserializeObject<List<Day>>(File.ReadAllText("stats.txt"));
            if (days == null) days = new List<Day>();
            int ind = days.FindIndex(x => x.date.Date == DateTime.Now.Date);
            if (ind > -1)
            {

            }
            else
            {
                Day day = new Day
                {
                    Calls = AmbulanceCalls,
                    date = DateTime.Now,
                };
                days.Add(day);
            }
            File.WriteAllText("stats.txt", JsonConvert.SerializeObject(days));
        }*/
        static string GetCurrentCity(Position pos = null)
        {
            if (pos == null) pos = new Position(Player.GetX(), Player.GetY(), Player.GetZ());
            return zoneManager.City(pos.X, pos.Y, pos.Z);
        }
        static string GetCurrentZone(Position pos = null)
        {
            if (pos == null) pos = new Position(Player.GetX(), Player.GetY(), Player.GetZ());
            return zoneManager.Zone(pos.X, pos.Y, pos.Z);
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private void OnCustomServiceAccepted(string command, string[] args)
        {
            Thread.Sleep(1100);
            if (args.Length == 0)
            {
                Chat.AddMessage("Введите id игрока, чтобы принять вызов");
                return;
            }
            int id = int.Parse(args[0]);
            AmbulanceCall call = new AmbulanceCall
            {
                Caller = new Person()
            };
            call.Caller.Id = id;
            Chat.Send($"/service ac cop {call.Caller.Id}");
            Thread.Sleep(1100);
            for (int i = Chat.ChatHistory.Count - 1; Chat.ChatHistory.Count > 12 ? i > Chat.ChatHistory.Count - 12 : i > 0; i--)
            {
                if (Chat.ChatHistory[i].Contains("Место помечено на карте красной меткой"))
                {
                    string[] words = Chat.ChatHistory[i].Split(' ');
                    call.Distance = double.Parse(words[words.Length - 2].Replace(".", ","));
                    call.Accepted = true;
                }
                else if (Chat.ChatHistory[i].Contains($"Диспетчер: {MyName} принял вызов от"))
                {
                    string[] words = Chat.ChatHistory[i].Split(' ');
                    string name = words[words.Length - 1].Split('[')[0];
                    call.Caller.Name = name;
                }
            }
            ProcessCall(call);
        }
        private void OnServiceAccepted(string command, string[] args)
        {
            Thread.Sleep(1100);
            string sname = "";

            for (int i = Chat.ChatHistory.Count - 1; Chat.ChatHistory.Count > 30 ? i > Chat.ChatHistory.Count - 30 : i > 0; i--)
            {
                if (Chat.ChatHistory[i].Contains("Диспетчер: вызов от "))
                {
                    AmbulanceCall call = new AmbulanceCall
                    {
                        Caller = new Person()
                    };
                    string dispCallLine = Chat.ChatHistory[i];
                    string[] dispCallArgs = dispCallLine.Split(' ');

                    int startBracket = dispCallLine.IndexOf('[');
                    string sid = "";
                    for (int j = startBracket + 1; j < dispCallLine.Length; j++)
                    {
                        if (dispCallLine[j] == ']') break;
                        sid += dispCallLine[j];
                    }
                    call.Caller.Id = int.Parse(sid);

                    int k = startBracket - 1;
                    while (dispCallLine[k] != 'т')
                    {
                        sname += dispCallLine[k];
                        k--;
                    }

                    call.Caller.Name = Reverse(sname.Substring(0, sname.Length - 1));
                    bool x = false;
                    string sdist = dispCallArgs[dispCallArgs.Length - 2].Replace('.', ',');
                    if (sdist.Contains(",")) x = true;
                    double distance = double.Parse(sdist);

                    if (x) distance *= 1000;
                    call.Distance = distance;

                    Chat.Send($"/service ac cop {call.Caller.Id}");
                    call.Accepted = true;
                    ProcessCall(call);
                    return;
                }
            }
            Chat.AddMessage("Не найдено вызвов", "f56342");
        }
        static double CalculateTime(double distance)
        {
            return Math.Round(distance * 2 / 2700, 0);
        }
        private void ProcessCall(AmbulanceCall call)
        {
            AmbulanceCalls.Add(call);
            SaveAmbulanceCall(call);
            Thread.Sleep(1170);
            var pos = World.GetCheckpointPos();
            string city = zoneManager.City(pos.X, pos.Y, pos.Z);
            string zone = zoneManager.Zone(pos.X, pos.Y, pos.Z);

            Chat.Send($"/f {settings.Tag} Докладываю: Принял вызов из {zone} | Нахожусь в {Player.GetZone()} | {call.Distance} м.");

            if (!Vehicle.IsSirenEnabled()) Vehicle.ToggleSirenState();
            Chat.AddMessage($"Вы приняли вызов от {call.Caller.Name} [{call.Caller.Id}]. Расстояние до него - {call.Distance}", "f56342");
            Chat.AddMessage($"Местоположение: {city}, {zone}", "f56342");
            Thread.Sleep(1210);
            Chat.Send($"/t {call.Caller.Id} Полиция в пути! Находимся в {Player.GetZone()}");
            Thread.Sleep(1210);
            double timeToPoint = CalculateTime(call.Distance);
            if (timeToPoint > 1)
                Chat.Send($"/t {call.Caller.Id} До {city}, {zone} доберемся за {timeToPoint} минуты!");
            else
            {
                if (timeToPoint == 0) Chat.Send($"/t {call.Caller.Id} До {city}, {zone} скоро доберемся!");
                else Chat.Send($"/t {call.Caller.Id} До {city}, {zone} доберемся за {timeToPoint} минуту!");
            }
            Thread.Sleep(1205);
            Chat.Send($"/t {call.Caller.Id} По возможности опишите проблему, чтобы мы были готовы!");
        }

        private static void OnCommandReceived(string command, params string[] args)
        {
            //Log("[Команда]: " + message);
        }

        /* public enum Shops {
             Flint = ""
         }*/
        static Dictionary<string, DateTime> Robberies;
        static bool isMdcRequested = false;
        struct MDCInfo
        {
            public int ID;
            public string Name;
            public int SULevel;
            public string Faction;
            public string Reason;

        }
        static MDCInfo Info;
        static DateTime LastWantedMessageReceived = DateTime.Now;
        // static System.Threading.Timer RobReportTimer;
        /*private static void SendRobberyInfo(object state)
        {
            foreach (var robbery in Robberies)
            {
                var d = (DateTime.Now - robbery.Value.AddMinutes(29));
                if (d.Minutes < 2 && d.Minutes > 0)
                {
                    //Chat.Send($"/fb Меньше чем через 2 минуты планируется ограбление {robbery.Key}");
                    Thread.Sleep(1200);
                }
            }
        }*/
        static List<string> ShopNames = new List<string>() {
            "Flint",
            "Idlewood",
            "AmmoLS",
            "Mulholland",
            "VictimLS"
        };
        void OnChatMessageReceived(DateTime time, string message)
        {
            if (message.Contains("[Ограбление "))
            {
                try
                {
                    List<string> ar = message.Replace("[", "").Replace("]", "").Trim().Split(' ').ToList();
                    int j = ar.IndexOf("Ограбление") + 1;
                    string shopName = "";
                    for (int i = j; i < ar.Count; i++)
                        shopName += ar[i];

                    bool isInLS = false;
                    foreach (var item in ShopNames)
                    {
                        if (shopName.Contains(item))
                        {
                            isInLS = true;
                            break;
                        }
                    }
                    if (isInLS)
                    {
                        if (Robberies.ContainsKey(shopName))
                            Robberies[shopName] = DateTime.Now;
                        else Robberies.Add(shopName, DateTime.Now);
                        try
                        {
                            DrawUIRobTimings();
                        }
                        catch (Exception e) {
                            Log(e.ToString());
                        }
                    }

                    File.WriteAllText("rob.txt", JsonConvert.SerializeObject(Robberies));
                }
                catch (Exception e)
                {
                    Chat.AddMessage($"Ошибка в индексации ограбления {e}");
                }
            }

            else if (message.Contains("(( m"))
            {
                string m = message.Replace("((", "").Replace("))", "").Trim();
                string[] args = m.Split(' ');
                if (args.Length < 2) return;
                int id = -1;
                bool result = int.TryParse(args.Last(), out id);
                if (result && id >= 0)
                {
                    Thread.Sleep(1300);
                    Chat.Send($"/mdc {id}");
                    Info = new MDCInfo
                    {
                        ID = id
                    };
                    isMdcRequested = true;
                }
            }

            if (isMdcRequested)
            {
                if (message.Contains("=[ MOBILE"))
                {
                    if (!isMdcRequested) return;
                    Chat.AddMessage("Сейчас сообщим в /fb инфу по МДК");
                }
                else if (message.Contains("Имя:"))
                {
                    string name = message.Split(' ').Last();
                    Info.Name = name;
                }
                else if (message.Contains("Уровень розыска:"))
                {
                    int lvl = int.Parse(message.Split(' ').Last());
                    Info.SULevel = lvl;
                }
                else if (message.Contains("Организация:"))
                {
                    string[] f = message.Split(' ');
                    int i = f.Length - 1;
                    string fName = "";
                    while (!f[i].Contains("Организация:"))
                    {
                        fName += f[i] + " ";
                        i--;
                    }
                    fName.Reverse();
                    Info.Faction = fName;
                }
                else if (message.Contains("Причина: "))
                {
                    string r = message.Split(' ').Last();
                    Info.Reason = r;
                }
                else if (message.Contains("=============================================="))
                {
                    Thread.Sleep(1050);
                    Chat.Send($"/fb {Info.Name}[{Info.ID}] SU: {Info.SULevel} Причина: {Info.Reason} {Info.Faction}");
                    isMdcRequested = false;
                }
                else if (message.Contains("Вы не в служебной машине / своем участке"))
                {
                    isMdcRequested = false;
                    Thread.Sleep(1200);
                    Chat.Send("/fb Я не в служебной машине / своем участке");
                }
            }
            else if (message.ToLower().Contains($"(( {RobCodeWord.Text.ToLower()} ))"))
            {
                SendRobInfo();
            }
            else if (message.Contains("SMS:") && message.Contains("Отправитель"))
            {
                // if(check)
                //if ()
                if (enableSMSInfoCheckBox.Checked)
                {
                    Thread.Sleep(500);
                    Chat.Send("/seedo >> Входящее СМС <<");
                }
            }
            else if (message.Contains("Вы выгнали"))
            {
                string who = message.Split(' ')[4];
                File.AppendAllText("повышения.txt", $"{who}  Исключен  {DateTime.Now.ToShortDateString()}" + Environment.NewLine);
            }
            else if (message.Contains("Вы повысили/понизили"))
            {
                string who = message.Split(' ')[4];
                string to = message.Split(' ')[6];
                File.AppendAllText("повышения.txt", $"{who}  Повышен/Понижен до {to}  {DateTime.Now.ToShortDateString()}" + Environment.NewLine);
            }
            else if (message.Contains("Вы пригласили"))
            {
                string who = message.Split(' ')[4];
                File.AppendAllText("повышения.txt", $"{who}  Принят  {DateTime.Now.ToShortDateString()}" + Environment.NewLine);
            }
            else if (message.Contains("Wanted "))
            {
                if (showWantedCheckBox.Checked)
                {
                    string reason = "";
                    int j = message.LastIndexOf('[') + 1;

                    while (message[j] != ']' && j < message.Length)
                    {
                        reason += message[j];
                        j++;
                    }
                    if (Math.Abs(DateTime.Now.Second - LastWantedMessageReceived.Second) > 3)
                    {
                        Thread.Sleep(500);
                        string m = $"/seedo >> Получил сводку: {reason} <<";
                        //Chat.AddMessage(m);
                        Chat.Send(m);
                        LastWantedMessageReceived = DateTime.Now;
                    }
                }
            }
            else if (message.Contains("Начало пейнтбола через"))
            {
                if (enablePaintballNotificationCheckBox.Checked)
                {
                    Thread.Sleep(500);
                    Chat.Send("/seedo >> Скоро пейнтбол! <<");
                }
            }
            /*else if (message.Contains("(( di ))")) {
                Chat.ShowDialog(DialogStyle.DIALOG_STYLE_LIST, "Test", "Test");
            }*/
        }
        private void SendRobInfo()
        {
            Thread.Sleep(1150);
            if (Robberies.Count == 0)
            {
                Chat.Send("/fb Не зафиксировано недавних ограблений");
                return;
            }
            foreach (var item in Robberies)
            {
                if (Math.Abs(item.Value.Hour - DateTime.Now.Hour) < 2)
                {
                    // Holy Crap
                    if (item.Key.Contains("Flint") && handleFlintCheckBox.Checked ||
                        item.Key.Contains("Mulholland") && handleMulholandCheckBox.Checked ||
                        item.Key.Contains("Ammo") && handleAmmoLSCheckBox.Checked ||
                        item.Key.Contains("Victim") && handleVictimLSCheckBox.Checked ||
                        item.Key.Contains("Idlewood") && handleIdleewodCheckBox.Checked)
                    {
                        string s = $"/fb {item.Key}: {item.Value.AddHours(diffWithMoscowHours).ToShortTimeString()}. Ожидается в {item.Value.AddHours(diffWithMoscowHours).AddMinutes(28).ToShortTimeString()}";
                        Chat.Send(s);
                        Log(s);
                        Thread.Sleep(1200);
                    }
                }
            }
        }
        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://vk.com/mrfoxpro";
            var si = new ProcessStartInfo(url);
            Process.Start(si);
            linkLabel1.LinkVisited = true;
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            diffWithMoscowHours = (int)moscowTimeDiffDropdown.Value;
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            API.API.UnInit();
            SaveSettings();
        }
        private void ClearRob()
        {
            Robberies = new Dictionary<string, DateTime>();
            File.WriteAllText("rob.txt", JsonConvert.SerializeObject(Robberies));
            Chat.AddMessage("Лог ограблений очищен");
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            ClearRob();
        }

        private void RobCodeWord_TextChanged(object sender, EventArgs e)
        {
            //settings.RobCodeWord = 
        }

        private void TagTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.Tag = tagTextBox.Text;
        }
    }
}
