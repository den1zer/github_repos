
public abstract class Patient
{
    public string Surname { get; }
    public bool Sex { get; }
    public int Days { get; }
    public int Condition { get; }
    public int Success { get; }
    public DateTime DateOfEntry { get; }

    protected Patient(string surname, bool sex, int days, int condition, int success)
    {
        Surname = surname;
        Sex = sex;
        Days = days;
        Condition = condition;
        Success = success;
        DateOfEntry = DateTime.Now;
    }

    public abstract string GetTypeDescription();
}


public class Outpatient : Patient
{
    public Outpatient(string surname, bool sex, int days, int condition, int success)
        : base(surname, sex, days, condition, success) { }

    public override string GetTypeDescription() => "Амбулаторний пацієнт";
}


public class Inpatient : Patient
{
    public Inpatient(string surname, bool sex, int days, int condition, int success)
        : base(surname, sex, days, condition, success) { }

    public override string GetTypeDescription() => "Стаціонарний пацієнт";
}


public class EmergencyPatient : Patient
{
    public EmergencyPatient(string surname, bool sex, int days, int condition, int success)
        : base(surname, sex, days, condition, success) { }

    public override string GetTypeDescription() => "Пацієнт з невідкладною допомогою";
}
