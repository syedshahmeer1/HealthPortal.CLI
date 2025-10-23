using System;
using System.Collections.Generic;

namespace HealthPortalLibrary.models;

public class Patients
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }

    public string? Race { get; set; }
    public string? Gender { get; set; }

    public List<MedicalNote> Notes { get; } = new();

    private string GetDiagnosesString()
    {
        if (Notes == null || Notes.Count == 0) return "None";
        var parts = new List<string>();
        foreach (var n in Notes)
        {
            if (n != null && !string.IsNullOrWhiteSpace(n.Diagnosis))
                parts.Add(n.Diagnosis!);
        }
        return parts.Count == 0 ? "None" : string.Join(", ", parts);
    }

    private string GetPrescriptionsString()
    {
        if (Notes == null || Notes.Count == 0) return "None";
        var parts = new List<string>();
        foreach (var n in Notes)
        {
            if (n != null && !string.IsNullOrWhiteSpace(n.Prescription))
                parts.Add(n.Prescription!);
        }
        return parts.Count == 0 ? "None" : string.Join(", ", parts);
    }

    public override string ToString()
    {
        var diag = GetDiagnosesString();
        var presc = GetPrescriptionsString();
        var bd = BirthDate.HasValue ? BirthDate.Value.ToString("yyyy-MM-dd") : "Unknown";
        return $"{Id}. {Name} | {bd} | {Race} | {Gender} | {Address} | Notes: {Notes.Count}\n" +
               $"Diagnoses: {diag}\nPrescriptions: {presc}";
    }
}
