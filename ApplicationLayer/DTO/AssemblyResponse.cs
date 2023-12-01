using AssemblyLine.ApplicationLayer.DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.DTO
{
    public class AssemblyResponse
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; } = string.Empty;

        public int? DefaultOrderNumber { get; set; } 
        public List<OperationResponse> Operations { get; set; } = new List<OperationResponse>();
    }

    public static class AssemblyExtensions
    {

        public static IEnumerable<AssemblyResponse> ToAssemblyResponse(this IEnumerable<Assemblies> assemblies)
        {

            var assemblyResponses = new List<AssemblyResponse>();
            foreach (Assemblies assembly in assemblies)
            {
                var response = new AssemblyResponse
                {
                    Id = assembly.Id,
                    AssemblyName = assembly.AssemblyName,

                };

                assemblyResponses.Add(response);
            }

            return assemblyResponses.ToList();
        }

       


    }
}
