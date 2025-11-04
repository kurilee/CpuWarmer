using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarmUp
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<CancellationTokenSource> token_sources = new List<CancellationTokenSource>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            var task = Task.Run(() =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                }
            }, cts.Token);
            tasks.Add(task);
            token_sources.Add(cts);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (tasks.Count == 0 || token_sources.Count == 0) return;

            var task = tasks[0];
            var cts = token_sources[0];
            tasks.RemoveAt(0);
            token_sources.RemoveAt(0);
            cts.Cancel();
            task.Wait();
            task.Dispose();
        }
    }
}
