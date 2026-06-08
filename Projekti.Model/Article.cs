using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekti.Model
{

    public class Article
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long Id { get; set; }

        public double CurrentPrice { get; set; }

        public long UserId { get; set; }

        [JsonIgnore]
        public UserModel? User { get; set; } = null;
    }

}



