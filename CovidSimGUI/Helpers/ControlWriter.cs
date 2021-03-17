using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CovidSimGUI.Helpers
{
    public class ControlWriter : TextWriter
    {
        private TextBox textbox;
        public ControlWriter(TextBox textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            textbox.Text += value;
        }

        public override void Write(string value)
        {
            var test = textbox.Dispatcher;
            // Dispatcher.Invoke(()=> {
            //     // Code causing the exception or requires UI thread access
            //     textbox.Text += value;
            // });
        }

        public override Encoding Encoding => Encoding.ASCII;
    }
}
