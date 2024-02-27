using Microsoft.AspNetCore.Http;

namespace AqaratProject.Models
{
    public class ProductViewModel
    {
       public string Name { get; set; }


        public IFormFile File { get; set; }
    }
}
