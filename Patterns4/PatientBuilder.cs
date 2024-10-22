using LR1_3;
//pattern build
public class PatientBuilder
{
    private string _surname;
    private bool _sex;
    private int _days;
    private int _condition;
    private int _success;

    public PatientBuilder SetSurname(string surname)
    {
        _surname = surname;
        return this;
    }

    public PatientBuilder SetSex(bool sex)
    {
        _sex = sex;
        return this;
    }

    public PatientBuilder SetDays(int days)
    {
        _days = days;
        return this;
    }

    public PatientBuilder SetCondition(int condition)
    {
        _condition = condition;
        return this;
    }

    public PatientBuilder SetSuccess(int success)
    {
        _success = success;
        return this;
    }

    public Patient Build()
    {
        return new Patient(_surname, _sex, _days, _condition, _success);
    }
}
