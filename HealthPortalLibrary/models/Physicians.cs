using System;

namespace HealthPortalLibrary.models;

public class Physicians
{
     public int Id { get; set; }
    public string? Name { get; set; }
    public string? LicenseNumber { get; set; }
    public DateOnly? GraduationDate { get; set; }

     public string? Specializations { get; set; }

    public override string ToString()
        => $"{Id}. {Name} | Lic #{LicenseNumber} | Grad {GraduationDate} | [{Specializations}]";

}
