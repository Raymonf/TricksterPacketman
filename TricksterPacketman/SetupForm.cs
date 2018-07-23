using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TricksterPacketman
{
    public partial class SetupForm : Form
    {
        public static CaptureDeviceList Devices = null;

        public SetupForm()
        {
            InitializeComponent();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                var devices = CaptureDeviceList.Instance;

                Devices = devices;

                cmbAdapter.Invoke(() =>
                {
                    cmbAdapter.Enabled = true;

                    foreach (var dev in devices)
                    {
                        /* Description */
                        //Console.WriteLine("{0}) {1} {2}", i, dev.Name, dev.Description);

                        cmbAdapter.Items.Add($"{((LibPcapLiveDevice)dev).Interface.FriendlyName}");
                    }

                    if (devices.Count > 0)
                    {
                        cmbAdapter.SelectedIndex = 0;
                    }
                });

                StatusStrip.Invoke(() =>
                {
                    StatusLabel.Text = "Ready";
                });

                BtnSelect.Invoke(() =>
                {
                    BtnSelect.Enabled = true;
                });
            }).Start();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (cmbAdapter.SelectedIndex < 0)
            {
                MessageBox.Show("Select an adapter.");
                return;
            }

            Program.Device = Devices[cmbAdapter.SelectedIndex];

            var filter = "";
            var ports = TxtPorts.Text.Split(' ');
            for (var i = 0; i < ports.Length; i++)
            {
                filter += "tcp port ";
                filter += ports[i];

                if (i < ports.Length - 1)
                {
                    filter += " or ";
                }
            }

            Program.Filter = filter.Trim();

            new ListForm().Show();
            Hide();
        }

        private void BtnPreset_LTO_Click(object sender, EventArgs e)
        {
            TxtPorts.Text = "9980 10006 13336 22006 23006 24006 25006 26006";
        }

        private void BtnPreset_tTO_Click(object sender, EventArgs e)
        {
            TxtPorts.Text = "9980 4006 13339 22006 22007 22008 22009";
        }
    }
}
