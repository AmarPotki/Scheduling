using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Domain
{
    internal class Availability
    {
        public string Name { get; set; }
        public Clinician Clinician { get; set; }
        public Location Location { get; set; }
        public IReadOnlyList<Service> Services { get; set; }
    }
}
