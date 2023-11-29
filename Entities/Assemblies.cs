using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Assemblies
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; } = string.Empty;

        public List<Operation> operations = new List<Operation>();
    }
}
