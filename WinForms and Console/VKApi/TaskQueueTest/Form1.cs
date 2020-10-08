using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskQueueTest
{
    public partial class Form1 : Form
    {
        readonly TaskQueue taskQueue;
        int i;

        public Form1()
        {
            InitializeComponent();
            taskQueue = new TaskQueue();
            i = 0;
        }

        private async void Button1_ClickAsync(object sender, EventArgs e)
        {
            i++;
            await taskQueue.Enqueue(Download, i);
        }

        private async Task Download(int index)
        {
            try
            {
                await new WebClient().DownloadFileTaskAsync("https://cs632405.vkuservideo.net/7/e87OzQ6Mjc0MjQ6/videos/870edf02f0.720.mp4?extra=itUGSKH6hiyG7cXmx31QsisoJAjeNCLanwGxnzt2KOU8sp13W29YWiVjLNepwkcXPqSrCM5Y5912leVCGg53CdB8dEmXkZvluO9EsF1_jLpktmMfBccuBOcNbSK9EIVzxqhckdNkrdHEef0aYOBE-XdPuw", "E:\\" + index + ".mp4");
            }
            catch (Exception)
            {

            }
        }

        private async void Button2_ClickAsync(object sender, EventArgs e)
        {
            await taskQueue.Dequeue();
        }
    }
}
