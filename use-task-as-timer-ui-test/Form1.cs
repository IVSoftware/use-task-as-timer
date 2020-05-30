using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace use_task_as_timer_ui_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RestartDelay();
        }
        void RestartDelay()
        {
            Task.Delay(1000).GetAwaiter().OnCompleted(() => { PerformUpdate(); });
        }


        Stopwatch _stopwatch = new Stopwatch();
        private void PerformUpdate()
        {
            Task.Run(() =>
            {
                Debug.WriteLine(_stopwatch.Elapsed.ToString() + " - Update Controls");
                BeginInvoke((MethodInvoker)delegate
                {
                    richTextBox1.AppendText(
                        Environment.NewLine + "...updating..." + Environment.NewLine
                    );
                });
                Task.Delay(5000).Wait();
            }).GetAwaiter().OnCompleted(()=>
            {
                RestartDelay();
            });
        }
    }
}
