using IndigoSoftware.Forms;
using IndigoSoftware.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using regeditChange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace regedit
{
    enum RecycleFlags : uint
    {
        SHERB_NOCONFIRMATION = 0x00000001,
        SHERB_NOPROGRESSUI = 0x00000002,
        SHERB_NOSOUND = 0x00000004
    }


    public partial class regeditPO : MaterialForm
    {

        //
        bool x,s,w,z,theme; // переменные для кнопок расскрыть
        public string namepro = " IndigoSoftware "; // название ПО
        public string userName = Environment.UserName; // имя пользователя в переменную
        private readonly MaterialSkinManager materialSkinManager; // объявление переменной скина
        //

        //
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        //
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        public regeditPO()                                                                                   
        {
            // загрузка
            InitializeComponent();
            //
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo600, Primary.Indigo600, Primary.Grey50, Accent.Indigo400, TextShade.WHITE);
            //
        }

        public void regeditPO_Load(object sender, EventArgs e) // происходит при загрузке формы              
        {
            // выводим версию
            lblNowVer.Text = "v." + Application.ProductVersion.ToString();
            // считываем значения
            string mode = Settings.Default["mode"].ToString(); //
            string color = Settings.Default["color"].ToString();
            //
            // настройки программы
            if (mode == "LIGHT")
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            }
            else if (mode == "DARK")
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            }
            //
            if (color == "DEFAULT")
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            }
            else if (color == "RED")
            {
                redThemeButt_Click(sender, e);
            }
            else if (color == "GREEN")
            {
                greenThemeButt_Click(sender, e);
            }
        }

        void regeditPO_FormClosed(object sender, FormClosedEventArgs e) // Происходит при закрытии ПО        
        {
            Application.Exit(); // закрывает форму, при нажатии на крестик.
        }

        async void provTTL_Click(object sender, EventArgs e) // Проверка TTL                                 
        {
            
            provTTL.Enabled = false;
            for (int prov = 0; prov < 100; prov++)
            {
                await Task.Delay(35);
                provBarr.Value++;
            }

            string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters";
            string keyName2 = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters";
            string valueName = "DefaultTTL";
            if (Registry.GetValue(keyName, valueName, null) == null || Registry.GetValue(keyName2, valueName, null) == null)
            {
                //что будет если нет таких записей
                MaterialMessageBox.Show("Внимание!\nУ вас отсутсвует DefaultTTL в одной из дирректорий.\nНажмите Установить TTL", namepro + " - Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                //что будет если записи есть
                try
                {
                    RegistryKey readKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip6\\Parameters");
                    string loadString = readKey.GetValue("DefaultTTL").ToString();
                    readKey.Close();// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip
                                    //
                    RegistryKey readKey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters");
                    string loadString2 = readKey2.GetValue("DefaultTTL").ToString();
                    readKey2.Close();


                    if (Convert.ToInt32(loadString) == 65 && Convert.ToInt32(loadString2) == 65)
                    {
                        text.Text = "Обнаружены значения TTL:\nДиректория Tcpip6 = " + loadString +
                                                            "\nДиректория Tcpip = " + loadString2 +
                                                            "\n У вас с TTL всё хорошо, шанс снятия денег минимальный!";
                    }
                    else if (Convert.ToInt32(loadString) == 64 || Convert.ToInt32(loadString2) == 64)
                    {
                        text.Text = "Обнаружены значения TTL:\nДиректория Tcpip6 = " + loadString +
                                                            "\nДиректория Tcpip = " + loadString2 +
                                                            "\n Данные необходимо заменить, ибо возможно снятие баланса.";
                    }
                    else
                    {
                        MaterialMessageBox.Show("Произошла неизвестная ошибка, сорри кодер даун.", namepro + " - Ошибка", MessageBoxButtons.OK);
                    }

                }
                catch (Exception ex)
                {
                    MaterialMessageBox.Show(ex.ToString(), namepro + "- Ошибка", MessageBoxButtons.OK);
                    return;
                }
                provBarr.Value = 0;
            }
            provBarr.Value = 0;
            provTTL.Enabled = true;

        }

        async void clearText_Click(object sender, EventArgs e) // Очистка лога TTL                           
        {
            clearText.Enabled = false;
            for (int cle = 0; cle < 100; cle++)
            {
                await Task.Delay(5);
                clearBarr.Value++;
            }
            text.Text = "Нажмите на кнопку 'проверить TTL'\n Чтобы узнать, может ли МТС снять у вас деньги за раздачу.";
            clearBarr.Value = 0;
            provBarr.Value = 0;
            changeBarr.Value = 0;

            provTTL.Enabled = true;
            changeTTL.Enabled = true;
            
            
            clearText.Enabled = true;
        }

        async void changeTTL_Click(object sender, EventArgs e) // Установить TTL                             
        {
            changeTTL.Enabled = false;
            string defttl = "DefaultTTL";
            string defttlnum = "65";
            for (int cha = 0; cha < 100; cha++)
            {
                await Task.Delay(50);
                changeBarr.Value++;
            }
            try
            {
                RegistryKey Tcpip6 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip6");
                Tcpip6.SetValue(defttl, defttlnum);
                Tcpip6.Close();
                //
                RegistryKey Tcpip = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip");
                Tcpip.SetValue(defttl, defttlnum);
                Tcpip.Close();
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(ex.ToString(), namepro + " - Ошибка", MessageBoxButtons.OK);
                return;
            }
            //
            // Заносим данные в label1 о успехе :)
            text.Text = "Обновленны следующие значения: \n" +
                "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip ;\n Установлено DefaultTTL = 65 \n" +
                "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip6 ;\n Установлено DefaultTTL = 65" +
                "\n\nВсё прошло успешно. \n\nВНИМАНИЕ! Вам необходимо перезагрузить ноутбук для того чтобы изменения пришли в силу.";
            changeBarr.Value = 0;
            changeTTL.Enabled = true;
        }

        async void materialFlatButton1_Click(object sender, EventArgs e) // Очистка кэша                     
        {
            clearCache.Enabled = false;
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(15);
                browserPro.Value++;
            }
            try
            {
                string googleCache = $@"C:\Documents and Settings\{userName}\Local Settings\Application Data\Google\Chrome\User Data\Default\Cache\";
                string yandexCache = $@"C:\Users\{userName}\AppData\Local\Yandex\YandexBrowser\User Data\Default\Cache";
                string edgeCache = $@"C:\Users\{userName}\AppData\Local\Packages\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\LocalState";
                string edgeCache2 = $@"C:\Users\{userName}\AppData\Local\Packages\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\LocalCache";

                string processYandex = "browser";
                string processGoogle = "chrome";
                string processEdge = "MicrosoftEdge";

                var YandexExists = Process.GetProcesses().Any(p => p.ProcessName == processYandex);
                var GoogleExists = Process.GetProcesses().Any(p => p.ProcessName == processGoogle);
                var EdgeExists = Process.GetProcesses().Any(p => p.ProcessName == processEdge);

                if (Directory.Exists(yandexCache) && yandex.Checked == true)
                {
                    if (YandexExists)
                    {
                        if (MaterialMessageBox.Show("Внимание! Вам необходимо закрыть браузер!\nЗакрыть?", namepro + "- Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            foreach (Process proc in Process.GetProcessesByName(processYandex))
                            {
                                proc.Kill();
                                browserPro.Value = 0;
                            }
                        }
                        else
                        {
                            yandex.Checked = false;
                            browserPro.Value = 0;
                        }
                    }
                    else
                    {
                        var yandexCachee = new DirectoryInfo(yandexCache);
                        foreach (var file in yandexCachee.GetFiles())
                            file.Delete();


                        await Task.Delay(300);
                        yandex.Checked = false;
                        browserPro.Value = 0;
                        MaterialMessageBox.Show("Успешно очищено.", namepro, MessageBoxButtons.OK);
                    }
                }
                else if (Directory.Exists(googleCache) && google.Checked == true)
                {
                    if (GoogleExists)
                    {
                        if (MaterialMessageBox.Show("Внимание! Вам необходимо закрыть браузер!\nЗакрыть?", namepro + "- Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            foreach (Process proc in Process.GetProcessesByName(processGoogle))
                            {
                                proc.Kill();
                                browserPro.Value = 0;
                            }
                        }
                        else
                        {
                            google.Checked = false;
                            browserPro.Value = 0;
                        }
                    }
                    else
                    {
                        var googleCachee = new DirectoryInfo(googleCache);
                        foreach (var file in googleCachee.GetFiles())
                            file.Delete();

                        google.Checked = false;
                        await Task.Delay(300);
                        browserPro.Value = 0;
                        MaterialMessageBox.Show("Успешно очищено.", namepro, MessageBoxButtons.OK);
                    }
                }
                else if (Directory.Exists(edgeCache) && edge.Checked == true)
                {
                    if (EdgeExists)
                    {
                        if (MaterialMessageBox.Show("Внимание! Вам необходимо закрыть браузер!\nЗакрыть?", namepro + "- Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            foreach (Process proc in Process.GetProcessesByName(processEdge))
                            {
                                proc.Kill();
                                browserPro.Value = 0;
                            }
                        }
                        else
                        {
                            edge.Checked = false;
                            browserPro.Value = 0;
                        }
                    }
                    else
                    {
                        var edgeCachee = new DirectoryInfo(edgeCache);
                        foreach (var file in edgeCachee.GetFiles())
                            file.Delete();

                        var edgeCacheee = new DirectoryInfo(edgeCache2);
                        foreach (var file in edgeCacheee.GetFiles())
                            file.Delete();


                        await Task.Delay(300);
                        edge.Checked = false;
                        browserPro.Value = 0;
                        MaterialMessageBox.Show("Успешно очищено.", namepro, MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MaterialMessageBox.Show("Вам необходимо выбрать какой-либо браузер!\n" +
                        "Или у вас отсутствует данный браузер.", namepro + "- Ошибка", MessageBoxButtons.OK);
                    browserPro.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(ex.ToString(), namepro + "- Ошибка", MessageBoxButtons.OK);
                return;
            }
            browserPro.Value = 0;
            clearCache.Enabled = true;
        }

        void materialRaisedButton1_Click(object sender, EventArgs e) // Раскрыть кэш браузера                
        {
            if (x)
            {
                browserLbl.Visible = false;
                yandex.Visible = false;
                google.Visible = false;
                edge.Visible = false;
                clearCache.Visible = false;
                browserPro.Visible = false;
            }
            else
            {
                browserLbl.Visible = true;
                yandex.Visible = true;
                google.Visible = true;
                edge.Visible = true;
                clearCache.Visible = true;
                browserPro.Visible = true;
            }
            x = !x;
        }

        async void clearWinButton_Click(object sender, EventArgs e) // Очистка win10                         
        {
            clearWinButton.Enabled = false;
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(45);
                winPro.Value++;
            }
            try
            {
                string tempDirectory = $@"C:\Windows\Temp"; // "C:\Windows\Temp" | Path.GetTempPath()
                string dnsDirectory = $@"";
                string RepeatSysDirectory = $@"";
                string dowloandsDirectory = $@"C:\Users\{userName}\Downloads"; // "C:\Users\eas1ly\Downloads"


                //очистка temp
                if (tempFiles.Checked == true)
                {

                    var sssss = new DirectoryInfo(tempDirectory);

                    // await Task.Delay(300);

                    foreach (var files in sssss.GetFiles())

                        files.Delete();


                    MaterialMessageBox.Show("Успешно очищено.", namepro, MessageBoxButtons.OK);
                    downloandsFiles.Checked = false;
                    winPro.Value = 0;

                }
                // очистка корзины
                else if (corzinaFiles.Checked == true)
                {
                    SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOSOUND | RecycleFlags.SHERB_NOCONFIRMATION);

                    await Task.Delay(300);
                    corzinaFiles.Checked = false;
                    winPro.Value = 0;
                    MaterialMessageBox.Show("Успешно очищено.", namepro, MessageBoxButtons.OK);
                }
                // очистка загрузок
                else if (Directory.Exists(dowloandsDirectory) && downloandsFiles.Checked == true)
                {
                    await Task.Delay(300);
                    var dow = new DirectoryInfo(dowloandsDirectory);
                    foreach (var file in dow.GetFiles())
                        file.Delete();
                    MaterialMessageBox.Show("Успешно очищено.", namepro, MessageBoxButtons.OK);
                    downloandsFiles.Checked = false;
                }
                else
                {
                    MaterialMessageBox.Show("Вам необходимо выбрать что-либо", namepro + "- Ошибка", MessageBoxButtons.OK);
                }
                //
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(ex.ToString(), namepro + "- Ошибка", MessageBoxButtons.OK);
                return;
            }
            winPro.Value = 0;
            clearWinButton.Enabled = true;
        }

        void clearWin10_Click(object sender, EventArgs e) // Раскрыть очистку win10                          
        {
            if (s)
            {
                clearLbl.Visible = false;
                tempFiles.Visible = false;
                corzinaFiles.Visible = false;
                clearWinButton.Visible = false;
                winPro.Visible = false;
                downloandsFiles.Visible = false;
            }
            else
            {
                clearLbl.Visible = true;
                tempFiles.Visible = true;
                corzinaFiles.Visible = true;
                clearWinButton.Visible = true;
                winPro.Visible = true;
                downloandsFiles.Visible = true;
            }
            s = !s;
        }

        void helpButt_Click(object sender, EventArgs e) // Раскрыть поддержка                                
        {
            if (w)
            {
                goVkButt.Visible = false;
                goSiteButt.Visible = false;
            }
            else
            {
                goVkButt.Visible = true;
                goSiteButt.Visible = true;
            }
            w = !w;
        }

        void teamChangeButt_Click(object sender, EventArgs e) // Раскрыть настройки темы                     
        {
            if (z)
            {
                darkTemeButt.Visible = false;
                redThemeButt.Visible = false;
                greenThemeButt.Visible = false;
                defThemeButt.Visible = false;
            }
            else
            {
                darkTemeButt.Visible = true;
                redThemeButt.Visible = true;
                greenThemeButt.Visible = true;
                defThemeButt.Visible = true;
            }
            z = !z;
        }

        public void darkTemeButt_Click(object sender, EventArgs e) // Кнопка темы с/ч                        
        {
            if (theme)
            {
                try
                {
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                    darkTemeButt.Text = "Установить тёмную тему";
                    Settings.Default["mode"] = "LIGHT";
                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MaterialMessageBox.Show(ex.Message, namepro + "- Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                try
                {
                    materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                    darkTemeButt.Text = "Установить светлую тему";
                    Settings.Default["mode"] = "DARK";
                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MaterialMessageBox.Show(ex.Message, namepro + "- Ошибка", MessageBoxButtons.OK);
                }
            }
            theme = !theme;
        }

        public void redThemeButt_Click(object sender, EventArgs e) // Красная                                
        {
            try
            {
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red600, Primary.Grey50, Accent.Red400, TextShade.WHITE);
                Settings.Default["color"] = "RED";
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(ex.Message, namepro + "- Ошибка", MessageBoxButtons.OK);
            }
        }

        public void greenThemeButt_Click(object sender, EventArgs e) // Зелёная                              
        {
            try
            {
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green600, Primary.Grey50, Accent.Green200, TextShade.WHITE);
                Settings.Default["color"] = "GREEN";
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(ex.Message, namepro + "- Ошибка", MessageBoxButtons.OK);
            }

        }

        public void defThemeButt_Click(object sender, EventArgs e) // Вернуть стандартные                    
        {
            try
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo600, Primary.Indigo600, Primary.Grey50, Accent.Indigo400, TextShade.WHITE);
                Settings.Default["mode"] = "LIGHT";
                Settings.Default["color"] = "DEFAULT";
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(ex.Message, namepro + "- Ошибка", MessageBoxButtons.OK);
            }

        }

        void getinfoButt_Click(object sender, EventArgs e) // ВК Кнопка                                      
        {
            if (MaterialMessageBox.Show("Вы уверены что хотите открыть браузер и перейти в нашу группу Вконтакте?", namepro, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start("https://vk.com/indigo.laptop");
            }
        }

        void goSiteButt_Click(object sender, EventArgs e) // Сайт кнопка                                     
        {
            if (MaterialMessageBox.Show("Вы уверены что хотите открыть браузер и перейти на наш сайт?", namepro, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start("https://indigo-software.at.ua/");
            }
        }

        void regeditPO_LocationChanged(object sender, EventArgs e) // Смена положения за границами экрана    
        {
            Size size = SystemInformation.PrimaryMonitorSize;
            if (this.Location.X < 0)
            {
                this.Location = new Point(0, this.Location.Y);
            }
            if (this.Location.Y < 0)
            {
                this.Location = new Point(this.Location.X, 0);
            }
            if (this.Location.X + this.Size.Width > size.Width)
            {
                this.Location = new Point(size.Width - this.Size.Width, this.Location.Y);
            }
            if (this.Location.Y + this.Size.Height > size.Height)
            {
                this.Location = new Point(this.Location.X, size.Height - this.Size.Height);
            }
        }

        void materialRaisedButton2_Click(object sender, EventArgs e) // Проверить обновление                 
        {
            IPStatus status = IPStatus.Unknown;
            try
            {
                status = new Ping().Send("indigo-software.at.ua").Status;
            }
            catch { }
            if (status == IPStatus.Success)
            {
                var client = new WebClient();
                var stream = client.OpenRead("https://indigo-software.at.ua/version.txt"); //Откроем поток для чтения, чтобы считать версию в инете
                client.DownloadFile("https://indigo-software.at.ua/version.txt", @"temp_version.ini"); //скачиваем актуальную версию лаунчера
                var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                string actualVers = content; //Актуальная версия лаунчера
                string lblver = Application.ProductVersion; //Локальная версия лаунчера, та которая на данный момент
                if (File.Exists(@"temp_version.ini")) //Проверим есть ли только что скаченый файл актуальной версии? для того чтобы понять ,скачался ли он
                {

                    Thread.Sleep(500);


                    if (Application.ProductVersion.ToString() == actualVers) // Сверяем версии. Тут нужно учесть, что идет проверка string не int значений, поэтому если будет отличие в точке, версия будет другой
                    {
                        string ravno = "Версии программ одинаковые. Обновление не требуется.";


                        MaterialMessageBox.Show("Ваша версиия программы: " + Application.ProductVersion + ".\n\rАктуальная версия на сервере: " + actualVers + "\n\r\n\r" + ravno, namepro + "- Обновление", MessageBoxButtons.OK);
                        File.Delete(@"temp_version.ini");
                    }

                    else
                    {
                        string neravno = "Требуется обновление.\rОбновить программу до актуальной версии?";
                        DialogResult result = MaterialMessageBox.Show("Ваша версиия программы: " + Application.ProductVersion + ".\n\rАктуальная версия программы: " + actualVers + "\n\r\n\r" + neravno, namepro + "- Обновление", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                string aprod = File.ReadAllText(@"temp_version.ini");  // Повышеная версия программы для удаления
                                MaterialMessageBox.Show("Внимание! \nСейчас начнётся обновление программы: IndigoSoftware\nВ случае если у вас запросят права администратора, разрешите нажав 'Да'.", namepro + "- Обновление", MessageBoxButtons.OK); // Качаем программу
                                WebClient webClient = new WebClient();


                                webClient.DownloadFile("https://indigo-software.at.ua/IndigoSoftware", Application.ProductName + "(" + actualVers + ")" + @".exe");
                                Process pc = new Process();
                                //pc.StartInfo.Verb = "runas"; //Если снять коментарий, CMD запустится от имени Админа, и в этом случае нужно будет указать полные пути. Типа Enveroment.CurrentDirectory
                                pc.StartInfo.FileName = "cmd";
                                pc.StartInfo.CreateNoWindow = true;
                                pc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                pc.StartInfo.Arguments = "/c @ECHO OFF && ping -n 3 127.0.0.1 && start " + Application.ProductName + "(" + actualVers + ")" + @".exe && del " + Application.ProductName + "(" + Application.ProductVersion + ")" + @".exe  / f / q ";
                                pc.Start();

                                File.Delete(@"temp_version.ini");
                                Application.Exit();
                            }
                            catch (Exception ex)
                            {
                                MaterialMessageBox.Show(ex.Message, namepro + "- Ошибка", MessageBoxButtons.OK);
                            }
                        }
                        if (result == DialogResult.No)
                        {
                            return; // Возвращаемся
                        }
                    }
                }
            }
            else
            {
                MaterialMessageBox.Show("Подключение к серверу отсутствует." +
                    "\nВозможные причины:" +
                    "\n1. У вас отсутствует подключение к интернету." +
                    "\n2. На сервере ведутся технические работы." +
                    "\n3. Брандмауэр блокирует соединение ПО с интернетом.", namepro + "- Ошибка", MessageBoxButtons.OK);
            }
        }

        void вКонтактеToolStripMenuItem_Click(object sender, EventArgs e) // ПКМ ВК                          
        {
            getinfoButt_Click(sender, e);
        }

        void сайтToolStripMenuItem_Click(object sender, EventArgs e) // ПКМ Сайт                             
        {
            goSiteButt_Click(sender, e);
        }

        void очисткаToolStripMenuItem_Click(object sender, EventArgs e) // Пкм в Очистку                     
        {
            materialTabControl1.SelectedIndex = 0;
        }

        void мТСToolStripMenuItem_Click(object sender, EventArgs e) // Пкм в МТС                             
        {
            materialTabControl1.SelectedIndex = 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void информацияToolStripMenuItem_Click(object sender, EventArgs e) // Пкм в Информацию               
        {
            materialTabControl1.SelectedIndex = 2;
        }

        void настройкиToolStripMenuItem_Click(object sender, EventArgs e) // Пкм в Настройки                 
        {
            materialTabControl1.SelectedIndex = 3;
        }

        void выходToolStripMenuItem_Click(object sender, EventArgs e) // Пкм Выход                           
        {
            if (MaterialMessageBox.Show("Вы уверены что хотите выйти?", namepro, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { }
        }

        void regeditPO_FormClosing(object sender, FormClosingEventArgs e) // Сохранение КФГ                  
        {
            Settings.Default.Save();
        }
    }
}