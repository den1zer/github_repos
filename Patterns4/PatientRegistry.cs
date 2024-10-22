using LR1_3;
using System.Collections.Generic;
//pattern singleton
public class PatientRegistry
{
    private static PatientRegistry _instance;
    private static readonly object _lock = new object();
    private List<Patient> registeredPatients;


    private PatientRegistry()
    {
        registeredPatients = new List<Patient>();
    }

  
    public static PatientRegistry GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new PatientRegistry();
            }
            return _instance;
        }
    }

   
    public void RegisterPatient(Patient patient)
    {
        registeredPatients.Add(patient);
    }

    
    public List<Patient> GetRegisteredPatients()
    {
        return registeredPatients;
    }
}
