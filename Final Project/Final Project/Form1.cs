using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        private DataTable Dt = new DataTable();
        private Vehicle vehicle = new Vehicle();
        public Form1()
        {
            InitializeComponent();
            comboBox2.Items.AddRange(vehicle.Car.ToArray());
            comboBox2.TextChanged += comboBox2_SelectedIndexChanged;
            comboBox3.Items.AddRange(vehicle.Colors.ToArray());
            comboBox3.TextChanged += comboBox3_SelectedIndexChanged;
            DateTime startTime = DateTime.Today;
            DateTime endTime = DateTime.Today.AddDays(1);
            TimeSpan interval = TimeSpan.FromMinutes(30);

            for (DateTime time = startTime; time < endTime; time += interval)
            {
                comboBox1.Items.Add(time.ToString("HH:mm"));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "เลือกเวลา";
            Dt.Columns.Add("ชื่อผู้จอง");
            Dt.Columns.Add("หมายเลขที่จอด");
            Dt.Columns.Add("วันที่จอง");
            Dt.Columns.Add("เวลาที่จอง");
            Dt.Columns.Add("รุ่นของรถ");
            Dt.Columns.Add("สีของรถ");
            Dt.Columns.Add("หลายเลขทะเบียนรถ");
            dataGridView1.DataSource = Dt;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string search = comboBox2.Text.ToLower();
            List<string> fillered = vehicle.Car.Where(brand => brand.ToLower().Contains(search)).ToList();
            comboBox2.Items.AddRange(fillered.ToArray());
            comboBox2.DroppedDown = true;
            comboBox2.SelectionStart = search.Length;
            comboBox2.SelectionLength = comboBox2.Text.Length - search.Length;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string search = comboBox3.Text.ToLower();
            List<string> fillered = vehicle.Colors.Where(brand => brand.ToLower().Contains(search)).ToList();
            comboBox3.Items.AddRange(fillered.ToArray());
            comboBox3.DroppedDown = true;
            comboBox3.SelectionStart = search.Length;
            comboBox3.SelectionLength = comboBox3.Text.Length - search.Length;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV File|*.csv";
            saveFileDialog.Title = "Save Carpark Data";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // write data to a .csv file
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in Dt.Rows)
                {
                    sb.AppendLine(string.Join(",", row.ItemArray));
                }
                File.WriteAllText(saveFileDialog.FileName, sb.ToString());

            }
            Dt.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV File|*.csv";
            openFileDialog.Title = "Open Carpark Data";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // read data from a .csv file
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');
                    Dt.Rows.Add(fields);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dt.Rows.Add(NameBox.Text, SectionBox.Text, dateTimePicker1.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text, NumBox.Text);
        }
    }
}
