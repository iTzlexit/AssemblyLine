using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// One to many relationship 1 Assembly to many Operations 
    /// </summary>
    public class Assembly 
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; } = string.Empty;
        
        List<Operation> operations = new List<Operation>();
    }
}
