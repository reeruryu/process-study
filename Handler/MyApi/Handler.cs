using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Web;

namespace MyApi
{
    public class Handler : IHttpHandler
    {
        static string logFilePath;

        public Handler()
        {
            string logDirectory = @"C:\inetpub\logs\LogFiles\tmp";
            logFilePath = Path.Combine(logDirectory, $"Log_{DateTime.Now.ToString("yyyy-MM-dd")}.log");
        }


        public void ProcessRequest(HttpContext context)
        {
            string value = context.Request.QueryString["value"];
            string text = context.Request.QueryString["text"];

            if (value == "1")
            {
                Boolean flag = KillProcess();
                if (flag)
                {
                    context.Response.StatusCode = 200;
                    context.Response.Write("server stop");
                    AppendLog("server stop");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("server stop fail");
                    AppendLog("server stop fail");
                }
            }
            else if (value == "2")
            {
                Boolean flag = StartProcess();
                if (flag)
                {
                    context.Response.StatusCode = 200;
                    context.Response.Write("server start");
                    AppendLog("server start");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("server start fail");
                    AppendLog("server start fail");
                }
            }
            else if (value == "3")
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                ".", "MyPipe", PipeDirection.InOut))
                {
                    pipeClient.Connect();
                    using (BinaryWriter writer = new BinaryWriter(pipeClient))
                    {
                        writer.Write(value);
                        writer.Write(text);
                        writer.Flush();

                        using (StreamReader reader = new StreamReader(pipeClient))
                        {
                            string response = reader.ReadLine();
                            context.Response.StatusCode = 200;
                            context.Response.Write(response);
                            AppendLog(response);

                        }
                    }
                }
            }
            else if (value == "4")
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                ".", "MyPipe", PipeDirection.InOut))
                {
                    pipeClient.Connect();
                    using (BinaryWriter writer = new BinaryWriter(pipeClient))
                    {
                        writer.Write(value);
                        writer.Write(text);
                        writer.Flush();

                        using (StreamReader reader = new StreamReader(pipeClient))
                        {
                            string response = reader.ReadLine();
                            context.Response.StatusCode = 200;
                            context.Response.Write(response);
                            AppendLog(response);
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }

        static Boolean StartProcess()
        {
            try
            {
                Process process = Process.Start("C:\\Web\\Test2\\PipeServer2\\PipeServer\\bin\\Release\\PipeServer.exe");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
                return false;
            }
        }

        static Boolean KillProcess()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("PipeServer");
                foreach (Process process in processes)
                {
                    process.Kill();
                    Console.WriteLine("kill " + process.Id);
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error killing process by name: {ex.Message}");
                return false;
            }
        }

        public void AppendLog(string msg)
        {
            string logMessage = "Log entry: " + DateTime.Now.ToString() + " - " + msg;

            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine(logMessage);
            }
        }
    }

}
