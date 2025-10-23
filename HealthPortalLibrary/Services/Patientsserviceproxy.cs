using System;
using System.Collections.Generic;
using HealthPortalLibrary.models;

namespace HealthPortalLibrary;


public class Patientsserviceproxy
{
    private List<Patients?> patients = new List<Patients?>();
    private static Patientsserviceproxy? instance;
    private static object Instancelock = new object();

    public static Patientsserviceproxy Instance
    {
        get
        {
            lock (Instancelock)
            {
                instance ??= new Patientsserviceproxy();
                return instance;
            }
        }
    }

    public List<Patients?> Patientss => patients;
    public Patients? AddOrUpdate(Patients? patient)
    {
        if (patient == null) return null;
        if (patient.Id <= 0)
        {
            var maxId = -1;
            if (patients.Any())
            {
                maxId = patients.Select(p => p?.Id ?? -1).Max();
            }
            else
                {
                    maxId = 0;
                }
                patient.Id = ++maxId;
                patients.Add(patient);
            }

            return patient;
        }
 public Patients? Delete(int id)
        {
            var patientToDelete = patients
                .Where(b => b != null)
                .FirstOrDefault(b => (b?.Id ?? -1) == id);

            if (patientToDelete != null)
                patients.Remove(patientToDelete);

            return patientToDelete;
        }


}
