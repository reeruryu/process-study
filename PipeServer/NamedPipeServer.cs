using System;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace PipeServer
{
    public class NamedPipeServer
    {
        const string MUTEX_NAME = "MyNamedPipeServerMutex";
        private static NotifyIcon notifyIcon;
        private static System.Windows.Forms.Timer iconBlinkTimer;
        private static Mutex mutex;

        [STAThread]
        static void Main()
        {
            // 1. Mutex
            bool createdNew;
            mutex = new Mutex(true, MUTEX_NAME, out createdNew);

            if (!createdNew)
            { // 다른 인스턴스가 이미 run 중
                // MessageBox.Show("Another instance of the application is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Tray Icon
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("C:\\Web\\Test2\\PipeServer2\\PipeServer\\star.ico");
            notifyIcon.Visible = true;
            notifyIcon.Text = "Named Pipe Server";

            ContextMenu contextMenu = new ContextMenu();
            MenuItem exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += (sender, e) => Application.Exit();
            contextMenu.MenuItems.Add(exitMenuItem);

            notifyIcon.ContextMenu = contextMenu;

            // 2-1. Icon Timer
            iconBlinkTimer = new System.Windows.Forms.Timer();
            iconBlinkTimer.Interval = 1000;
            iconBlinkTimer.Tick += IconBlinkTimer_Tick;
            iconBlinkTimer.Start();

            // 3. pipe server
            Task.Run(() => StartServer());

            // 1-2. application 종료 시, Mutex Release
            Application.ApplicationExit += (sender, e) =>
            {
                mutex.ReleaseMutex();
            };

            Application.Run();
        }

        static async Task StartServer()
        {
            while (true)
            {
                using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("MyPipe", PipeDirection.InOut))
                {
                    Console.WriteLine("Waiting for connection...");
                    await pipeServer.WaitForConnectionAsync();

                    Console.WriteLine("Connected!");

                    using (BinaryReader reader = new BinaryReader(pipeServer))
                    {

                        string value = reader.ReadString();
                        string text = reader.ReadString();
                        /* // Read the length of the incoming image data
                         int imageDataLength = reader.ReadInt32();
                         // Read the image data
                         byte[] imageData = reader.ReadBytes(imageDataLength);*/
                        // Read the additional value from the client

                        // Generate a random file name
                        /*string randomFileName = Path.GetRandomFileName();
                        string currentDate = DateTime.Now.ToString("yyyyMMdd");
                        // Create the directory if it doesn't exist
                        string directoryPath = Path.Combine(Environment.CurrentDirectory, currentDate);
                        Directory.CreateDirectory(directoryPath);
                        // Combine the directory path, random file name, and file extension
                        string filePath = Path.Combine(directoryPath, randomFileName + ".jpg");*/

                        // saving the image to a file
                        try
                        {
                            Test(value);
                            SendResponse(pipeServer, "ok");
                            // File.WriteAllBytes(filePath, imageData);
                            // Console.WriteLine("Image saved successfully at: " + filePath);
                        }
                        catch (AccessViolationException ex)
                        {
                            Console.WriteLine($"AccessViolationException: {ex.Message}");
                            SendResponse(pipeServer, "retry");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception: {ex.Message}");
                            SendResponse(pipeServer, "ex:" + ex.Message);
                        }
                        
                    }
                }
            }

        }
        

        static void SendResponse(NamedPipeServerStream pipeServer, string response)
        {
            using (StreamWriter writer = new StreamWriter(pipeServer))
            {
                writer.WriteLine(response);
                writer.Flush();
            }
        }

        private static void IconBlinkTimer_Tick(object sender, EventArgs e)
        {
            if (notifyIcon.Icon == SystemIcons.Application)
                notifyIcon.Icon = new Icon("C:\\Web\\Test2\\PipeServer2\\PipeServer\\star.ico");
            else
                notifyIcon.Icon = SystemIcons.Application;
        }

        static void Test(String additionalValue)
        {
            switch (additionalValue)
            {
                case "3":
                    throw new AccessViolationException();
                case "4":
                    break;
                    // hang
                    /*Console.WriteLine("Received additional value 4");
                    Thread.Sleep(100000);
                    break;*/
                default:
                    break;
            }
        }
    }
}
