using System;

namespace HealthPortalLibrary.models
{
    public class Appointments
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public DateTime Start { get; set; }            // local time
        public int DurationMinutes { get; set; } = 60; // default 60

        public override string ToString()
        {
            return $"#{Id}  {Start:yyyy-MM-dd HH:mm} ({DurationMinutes}m)  Patient:{PatientId}  Physician:{PhysicianId}";
        }
    }
}