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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LR1_3
{
    public partial class Form1 : Form
    {
        List<Patient> patients = new List<Patient>();
        public Form1()
        {
            InitializeComponent();
        }

        public void Updatee()
        {
            string c = "c", sc = "sc", s = "s";
            dataGridView1.Rows.Clear();
            foreach (Patient A in patients)
            {
                if (A.Condition == 1) c = "Легкий";
                else if (A.Condition == 2) c = "Середній";
                else if (A.Condition == 3) c = "Важкий";
                else if (A.Condition == 4) c = "Невиліковний";
                else c = " ";
                if (A.Success == 1) sc = "Одужує";
                else if (A.Success == 2) sc = "Стабільний";
                else if (A.Success == 3) sc = "Важкий";
                else if (A.Success == 4) sc = "Невиліковний";
                else sc = " ";
                if (A.Sex == true) s = "Чоловік";
                else s = "Жінка";
                dataGridView1.Rows.Add(A.Surname, s, A.DateOfEntry.ToString(), c, sc);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            bool a = true;
            if (a == true)
            {
                DateTime year = dateTimePicker1.Value;
                TimeSpan difference = DateTime.Now - year;
                int days = (int)difference.TotalDays; // Дні

                int s = 0;
                int sc = 0;
                if (comboBox1.Text == "Легкий") s = 1;
                else if (comboBox1.Text == "Середній") s = 2;
                else if (comboBox1.Text == "Важкий") s = 3;
                else if (comboBox1.Text == "Невиліковний") s = 4;
                else s = 0;

                if (comboBox2.Text == "Одужує") sc = 1;
                else if (comboBox2.Text == "Стабільний") sc = 2;
                else if (comboBox2.Text == "Важкий") sc = 3;
                else if (comboBox2.Text == "Невиліковний") sc = 4;
                else sc = 0;

                patients.Add(new Patient(
                    textBox3.Text == null ? " " : textBox3.Text,
                    radioButton1.Checked,
                    days,
                    s,
                    sc)
                );
            }
            else
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    if (index >= 0 && index < patients.Count)
                    {
                        patients.Add(new Patient(patients[index]));
                    }
                    else
                    {
                        MessageBox.Show("Помилка", "0");
                    }
                }
                else
                {
                    MessageBox.Show("Помилка", "0");
                }
            }

            Updatee();
        }



        private void Create_Click_1(object sender, EventArgs e)
        {
            patients.Add(new Patient());
            Updatee();
        }

        private void заумовчуваннямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                foreach (Patient A in patients)
                {
                    DateTime year = dateTimePicker1.Value;
                    TimeSpan difference = DateTime.Now - year;
                    int days = (int)difference.TotalDays; // Дні
                    int s = 0;
                    int sc = 0;
                    if (comboBox1.Text == "Легкий") s = 1;
                    else if (comboBox1.Text == "Середній") s = 2;
                    else if (comboBox1.Text == "Важкий") s = 3;
                    else if (comboBox1.Text == "Невиліковний") s = 4;
                    else s = 0;

                    if (comboBox2.Text == "Одужує") sc = 1;
                    else if (comboBox2.Text == "Стабільний") sc = 2;
                    else if (comboBox2.Text == "Важкий") sc = 3;
                    else if (comboBox2.Text == "Невиліковний") sc = 4;
                    else sc = 0;
                    dataGridView1.Rows.Add(
                    textBox3.Text == null ? " " : textBox3.Text +
                    radioButton1.Checked +
                    days +
                    s +
                    sc);
                }
            }
        }
        //Abstract Factory
        private void button2_Click(object sender, EventArgs e)
        {

            IPatientFactory factory = new DefaultPatientFactory();
            DateTime year = dateTimePicker1.Value;
            TimeSpan difference = DateTime.Now - year;
            int days = (int)difference.TotalDays;

            patients.Add(factory.CreatePatient(
                textBox3.Text == null ? " " : textBox3.Text,
                radioButton1.Checked,
                days,
                comboBox1.SelectedIndex + 1, 
                comboBox2.SelectedIndex + 1  
            ));
            Updatee();


        }
        //Builder
        private void button3_Click(object sender, EventArgs e)
        {

            DateTime year = dateTimePicker1.Value;
            TimeSpan difference = DateTime.Now - year;
            int days = (int)difference.TotalDays;

            var builder = new PatientBuilder()
                .SetSurname(textBox3.Text == null ? " " : textBox3.Text)
                .SetSex(radioButton1.Checked)
                .SetDays(days)
                .SetCondition(comboBox1.SelectedIndex + 1)
                .SetSuccess(comboBox2.SelectedIndex + 1);

            patients.Add(builder.Build());
            Updatee();


        }
        //Singleton
        private void button4_Click(object sender, EventArgs e)
        {
           
            string surname = textBox3.Text;
            bool sex = radioButton1.Checked; 
            int days = (DateTime.Now - dateTimePicker1.Value).Days;
            int condition = comboBox1.SelectedIndex + 1; 
            int success = comboBox2.SelectedIndex + 1; 

           
            Patient newPatient = new Patient(surname, sex, days, condition, success);

            
            PatientRegistry registry = PatientRegistry.GetInstance();
            registry.RegisterPatient(newPatient);

        
            UpdateRegisteredPatients();
        }


        private void UpdateRegisteredPatients()
        {
            var registry = PatientRegistry.GetInstance();
            var patients = registry.GetRegisteredPatients();

            

            foreach (var patient in patients)
            {
                string sexStr = patient.Sex ? "Чоловік" : "Жінка";
                string conditionStr = GetConditionString(patient.Condition);
                string successStr = GetSuccessString(patient.Success);

                dataGridView1.Rows.Add(patient.Surname, sexStr, patient.DateOfEntry, conditionStr, successStr);
            }
        }

        private string GetConditionString(int condition)
        {
            switch (condition)
            {
                case 1: return "Легкий";
                case 2: return "Середній";
                case 3: return "Важкий";
                case 4: return "Невиліковний";
                default: return "Невизначено";
            }
        }

        private string GetSuccessString(int success)
        {
            switch (success)
            {
                case 1: return "Одужує";
                case 2: return "Стабільний";
                case 3: return "Важкий";
                case 4: return "Невиліковний";
                default: return "Невизначено";
            }
        }
    }
}
    

