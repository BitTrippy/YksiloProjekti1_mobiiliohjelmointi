using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YksiloProjekti1.Services
{
    public class JsonDatabase
    {
        
        public List<JsonResponse> Responses { get; set; }

        public JsonDatabase()
        {
            Responses = new List<JsonResponse>();
        }
    }

    public class JsonResponse
    {
        public int Id { get; set; }
        public string Response { get; set; }
    }

    
}
