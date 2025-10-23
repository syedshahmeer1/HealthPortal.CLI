using System;
using System.Collections.Generic;
using System.Linq;
using HealthPortalLibrary.models;

namespace HealthPortalLibrary;

public class Appointmentserviceproxy
{
    private List<Appointments?> appointments = new List<Appointments?>();
    private static Appointmentserviceproxy? instance;
    private static object Instancelock = new object();

    public static Appointmentserviceproxy Instance
    {
        get
        {
            lock (Instancelock)
            {
                instance ??= new Appointmentserviceproxy();
                return instance;
            }
        }
    }

    public List<Appointments?> Appointmentss => appointments;

    // Returns null if invalid (outside hours or double-booked)
    public Appointments? AddOrUpdate(Appointments? appt)
    {
        if (appt == null) return null;

        // validate here (proxy-enforced rules)
        if (!WithinBusinessHours(appt.Start, appt.DurationMinutes)) return null;
        if (PhysicianOverlaps(appt.PhysicianId, appt.Start, appt.DurationMinutes, appt.Id)) return null;

        // same ID assignment pattern as your Patientsserviceproxy
        if (appt.Id <= 0)
        {
            var maxId = -1;
            if (appointments.Any())
            {
                maxId = appointments.Select(a => a?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            appt.Id = ++maxId;
            appointments.Add(appt);
        }
        // else: updates happen by reference (matches your style)

        return appt;
    }

    public Appointments? Delete(int id)
    {
        Appointments? toDel = null;
        foreach (var a in appointments)
        {
            if (a != null && a.Id == id) { toDel = a; break; }
        }
        if (toDel != null) appointments.Remove(toDel);
        return toDel;
    }

    // ---- validation helpers (kept inside proxy) ----
    private static bool WithinBusinessHours(DateTime start, int durationMinutes)
    {
        if (start.DayOfWeek < DayOfWeek.Monday || start.DayOfWeek > DayOfWeek.Friday) return false;
        var dayStart = start.Date.AddHours(8);   // 08:00
        var dayEnd   = start.Date.AddHours(17);  // 17:00
        var end      = start.AddMinutes(durationMinutes);
        return start >= dayStart && end <= dayEnd;
    }

    private bool PhysicianOverlaps(int physicianId, DateTime start, int durationMinutes, int currentId)
    {
        var end = start.AddMinutes(durationMinutes);
        foreach (var a in appointments)
        {
            if (a != null && a.PhysicianId == physicianId && a.Id != currentId)
            {
                var otherStart = a.Start;
                var otherEnd   = a.Start.AddMinutes(a.DurationMinutes);
                if (start < otherEnd && otherStart < end) return true; // overlap
            }
        }
        return false;
    }
}
