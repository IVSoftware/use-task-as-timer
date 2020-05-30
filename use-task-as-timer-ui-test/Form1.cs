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
                LongRunningUpdateTask();

            }).GetAwaiter().OnCompleted(()=>
            {
                RestartDelay();
            });
        }
        private void LongRunningUpdateTask()
        {
            BeginInvoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText(
                    Environment.NewLine + "BeginUpdate..." + Environment.NewLine
                );
            });

            // This is a stress test to make sure we're
            // not blocking the UI thread even when we 
            // block THIS thread for 5 seconds at a time.

            Task.Delay(5000).Wait();
            BeginInvoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText(
                    Environment.NewLine + "...EndUpdate" + Environment.NewLine
                );
            });
        }
    }
}
