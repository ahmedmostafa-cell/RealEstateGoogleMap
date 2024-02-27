using Microsoft.AspNetCore.Hosting;

namespace AqaratProject.Models
{
   
    public class IndexModel
    {
        private IHostingEnvironment env;
        public string result;
        public IndexModel(IHostingEnvironment env)
        {
            this.env = env;
        }
    }
}
