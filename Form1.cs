using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.ServiceProcess;

namespace test_serial_com
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] param = new byte[8];
            int size = 0;

            //パラメータのセット
            param[size++] = 0x02;
            param[size++] = (byte)('C');
            param[size++] = (byte)('U');
            param[size++] = (byte)('R');
            param[size++] = 0x03;

            //サーボの書き込み
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write(param, 0, size);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                serialPort1.Open();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int i = 0;
            int b;
            byte[] buf = new byte[1024];
            
            while(true)
            {
                b = serialPort1.ReadByte();
                buf[i++] = (byte)(b);
                if(b == 0x03)
                {
                    break;
                }
            }

            //int len = serialPort1.Read(buf, 0, 1024);
            //string s = Encoding.GetEncoding("Shift_JIS").GetString(buf, 0, len);
            //System.Diagnostics.Debug.WriteLine(s);
            string s = System.Text.Encoding.ASCII.GetString(buf);
            System.Diagnostics.Debug.Write(s);
            System.Diagnostics.Debug.Write("\n");
            //System.Diagnostics.Debug.WriteLine((char)(b));
            //MessageBox.Show((char)(b), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            /*
            byte[] buf = new byte[1024];
            int len = port.Read(buf, 0, 1024);
            string s = Encoding.GetEncoding("Shift_JIS").GetString(buf, 0, len);
            port.WriteLine(s);
            Console.Write("received: " + s);
            */
        }
    }
}
