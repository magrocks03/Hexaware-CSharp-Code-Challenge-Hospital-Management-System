using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.exception
{
    public class PatientNumberNotFoundException : Exception
    {
        public PatientNumberNotFoundException()
            : base("Patient number not found in the database.") { }

        public PatientNumberNotFoundException(string message)
            : base(message) { }

        public PatientNumberNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}

