using System;
using System.Windows.Forms;
//stok class
namespace LR1_3
{
    
    public class Patient
    {
        private Patient patient;

        public string Surname { get; set; }
        public bool Sex { get; set; }
        public int DateOfEntry { get; set; }
        public int Condition { get; set; }
        public int Success { get; set; }

        
        public Patient()
        {
            Surname = "Шараварський";
            Sex = true;
            DateOfEntry = 5; 
            Condition = 0;
            Success = 0;
        }

        
        public Patient(string surname, bool sex, int days, int condition, int success)
        {
            Surname = surname;
            Sex = sex;
            DateOfEntry = days; 
            Condition = condition;
            Success = success;
        }

        public Patient(Patient patient)
        {
            this.patient = patient;
        }
    }
}
