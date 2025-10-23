using System;

namespace HealthPortalLibrary.models;

public class MedicalNote
{
        
    


    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }

    public override string ToString()
        => $" Dx: {Diagnosis} | Rx: {Prescription}";
}