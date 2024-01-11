using System;
using System.Windows.Forms;
using System.Net.Http;

namespace PipeClient
{
    public partial class Form1 : Form
    {
        static string apiUrl = "http://localhost:8081/pipe";
        static int cnt = 0;
        static int maxCnt = 2;
        // static System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();
            // timer = new System.Timers.Timer(2000);
            // timer.Elapsed += OnTimedEvent;
        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string queryString = $"value=1&text=1";
                string fullUrl = $"{apiUrl}?{queryString}";

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"서버 응답: {responseBody}");
                    }
                    else
                    {
                        MessageBox.Show($"에러 응답 코드: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류 발생: {ex.Message}");
                }
            }
            
        }

        
        private async void button2_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string queryString = $"value=2&text=2";
                string fullUrl = $"{apiUrl}?{queryString}";

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"서버 응답: {responseBody}");
                    }
                    else
                    {
                        MessageBox.Show($"에러 응답 코드: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류 발생: {ex.Message}");
                }
            }
            
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string queryString = $"value=3&text=3";
                string fullUrl = $"{apiUrl}?{queryString}";

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = "fail";
                        while (cnt < maxCnt)
                        {
                            responseBody = await response.Content.ReadAsStringAsync();
                            if (responseBody == "retry")
                            {
                                // kill
                                queryString = $"value=1&text=1";
                                fullUrl = $"{apiUrl}?{queryString}";
                                response = await httpClient.GetAsync(fullUrl);

                                // create
                                queryString = $"value=2&text=2";
                                fullUrl = $"{apiUrl}?{queryString}";
                                response = await httpClient.GetAsync(fullUrl);

                                queryString = $"value=3&text=3";
                                fullUrl = $"{apiUrl}?{queryString}";
                                response = await httpClient.GetAsync(fullUrl);
                                cnt++;
                            }
                        }
                        cnt = 0;
                        MessageBox.Show($"서버 응답: {responseBody}");
                    }
                    else
                    {
                        MessageBox.Show($"에러 응답 코드: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류 발생: {ex.Message}");
                }
            }

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string queryString = $"value=4&text=4";
                string fullUrl = $"{apiUrl}?{queryString}";

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"서버 응답: {responseBody}");
                    }
                    else
                    {
                        MessageBox.Show($"에러 응답 코드: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류 발생: {ex.Message}");
                }
            }
            
        }

        // 1. ok
        /*Boolean flag = false;
        Process[] processes = Process.GetProcessesByName("PipeServer");
        foreach (Process process in processes)
        {
            flag = true;
        }

        if (!flag)
        {
            StartProcess();
        }

        using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(
            ".", "MyPipe", PipeDirection.InOut))
        {
            Console.WriteLine("Attempting to connect to the server...");
            pipeClient.Connect();
            Console.WriteLine("Connected to the server!");

            using (BinaryWriter writer = new BinaryWriter(pipeClient))
            {
                // Use OpenFileDialog to let the user select a file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select Image File";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string imagePath = openFileDialog.FileName;
                    Console.WriteLine(imagePath);
                    // Read the selected file as a byte array
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    // Send the length of the image data to the server
                    writer.Write(imageData.Length);
                    // Send the image data to the server
                    writer.Write(imageData);
                    // Send additional value "1" to the server
                    writer.Write("1");
                    writer.Flush();

                    // Receive the response from the server
                    using (StreamReader reader = new StreamReader(pipeClient))
                    {
                        string response = reader.ReadLine();
                        WriteResponse(response, pipeClient);
                    }

                    cnt = 0;

                }
                else
                {
                    Console.WriteLine("File selection canceled.");
                }
            }

        }*/

        // 2. access violation
        /*using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(
            ".", "MyPipe", PipeDirection.InOut))
        {
            Console.WriteLine("Attempting to connect to the server...");
            pipeClient.Connect();
            Console.WriteLine("Connected to the server!");

            using (BinaryWriter writer = new BinaryWriter(pipeClient))
            {
                // Use OpenFileDialog to let the user select a file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select Image File";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string imagePath = openFileDialog.FileName;
                    // Read the selected file as a byte array
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    // Send the length of the image data to the server
                    writer.Write(imageData.Length);
                    // Send the image data to the server
                    writer.Write(imageData);
                    // Send additional value "2" to the server
                    writer.Write("2");
                    writer.Flush();

                    // Receive the response from the server
                    using (StreamReader reader = new StreamReader(pipeClient))
                    {
                        string response = reader.ReadLine();
                        WriteResponse(response, pipeClient);
                    }

                }
                else
                {
                    Console.WriteLine("File selection canceled.");
                }
            }

        }*/

        // 3. 일반 exception
        /*using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(
            ".", "MyPipe", PipeDirection.InOut))
        {
            Console.WriteLine("Attempting to connect to the server...");
            pipeClient.Connect();
            Console.WriteLine("Connected to the server!");

            using (BinaryWriter writer = new BinaryWriter(pipeClient))
            {
                // Use OpenFileDialog to let the user select a file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select Image File";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string imagePath = openFileDialog.FileName;
                    // Read the selected file as a byte array
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    // Send the length of the image data to the server
                    writer.Write(imageData.Length);
                    // Send the image data to the server
                    writer.Write(imageData);
                    // Send additional value "3" to the server
                    writer.Write("3");
                    writer.Flush();

                    // Receive the response from the server
                    using (StreamReader reader = new StreamReader(pipeClient))
                    {
                        string response = reader.ReadLine();
                        WriteResponse(response, pipeClient);
                    }

                }
                else
                {
                    Console.WriteLine("File selection canceled.");
                }
            }

        }*/

        // 4. hang
        /*using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(
            ".", "MyPipe", PipeDirection.InOut))
        {
            Console.WriteLine("Attempting to connect to the server...");
            pipeClient.Connect();
            Console.WriteLine("Connected to the server!");

            using (BinaryWriter writer = new BinaryWriter(pipeClient))
            {
                // Use OpenFileDialog to let the user select a file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select Image File";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string imagePath = openFileDialog.FileName;
                    // Read the selected file as a byte array
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    // Send the length of the image data to the server
                    writer.Write(imageData.Length);
                    // Send the image data to the server
                    writer.Write(imageData);
                    // Send additional value "4" to the server
                    writer.Write("4");
                    writer.Flush();

                    //-------------------------------------------------------
                    // Set up a timer to handle timeout
                    timer.Start();
                    //-------------------------------------------------------
                    // Receive the response from the server
                    using (StreamReader reader = new StreamReader(pipeClient))
                    {
                        string response = reader.ReadLine();
                        WriteResponse(response, pipeClient);
                    }

                }
                else
                {
                    Console.WriteLine("File selection canceled.");
                }
            }

        }*/

        /*private void WriteResponse(String response, NamedPipeClientStream pipeClient)
        {
            if (response == "ok")
            {
                MessageBox.Show(response);
                cnt = 0;
            }
            else if (response == "retry")
            {
                MessageBox.Show(response);
                KillProcess();
                if (cnt < maxCnt)
                {
                    StartProcess();
                    cnt++;
                    MessageBox.Show(cnt.ToString());
                }
                else
                {
                    MessageBox.Show("sever down");
                }
                
            }
            else if (response != null && response.StartsWith("ex:"))
            {
                string ex = response.Substring(3);
                MessageBox.Show(ex);
            }
        }*/

        /*static void StartProcess()
        {
            try
            {
                Process process = Process.Start("C:\\Web\\Test2\\PipeServer2\\PipeServer\\bin\\Release\\PipeServer.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
            }
        }

        static void KillProcess()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("PipeServer");
                foreach (Process process in processes)
                {
                    process.Kill();
                    
                    Console.WriteLine("kill " + process.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error killing process by name: {ex.Message}");
            }
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            KillProcess();
            if (cnt < maxCnt)
            {
                MessageBox.Show("hang");
                StartProcess();
                cnt++;
                MessageBox.Show(cnt.ToString());
            }
            else
            {
                MessageBox.Show("sever down");
            }
        }*/
    }
}