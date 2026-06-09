using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti.Model
{
    public class UserModelDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Article>? Articles { get; set; } = new List<Article>();

    }
}
