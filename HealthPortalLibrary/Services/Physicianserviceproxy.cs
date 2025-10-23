using System;
using System.Collections.Generic;
using HealthPortalLibrary.models;

namespace HealthPortalLibrary;


public class Physicianserviceproxy
{
    private List<Physicians?> physicians = new List<Physicians?>();
    private static Physicianserviceproxy? instance;
    private static object Instancelock = new object();

    public static Physicianserviceproxy Instance
    {
        get
        {
            lock (Instancelock)
            {
                instance ??= new Physicianserviceproxy();
                return instance;
            }
        }
    }

    public List<Physicians?> Physicians => physicians;
    public Physicians? AddOrUpdate(Physicians? physician)
    {
        if (physician == null) return null;
        if (physician.Id <= 0)
        {
            var maxId = -1;
            if (physicians.Any())
            {
                maxId = physicians.Select(p => p?.Id ?? -1).Max();
            }
            else
                {
                    maxId = 0;
                }
                physician.Id = ++maxId;
                physicians.Add(physician);
            }

            return physician;
        }
 public Physicians? Delete(int id)
        {
            var physicianToDelete = physicians
                .Where(b => b != null)
                .FirstOrDefault(b => (b?.Id ?? -1) == id);

            if (physicianToDelete != null)
                physicians.Remove(physicianToDelete);

            return physicianToDelete;
        }


}