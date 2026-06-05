using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;

namespace Projekt.WebAPI.Models
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
