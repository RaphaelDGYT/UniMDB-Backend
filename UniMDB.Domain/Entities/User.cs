using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniMDB.Domain.Entities;

public class User
{
    public uint id_user { get; set; }

    public string name { get; set; }

    public string username { get; set; }

    public string email { get; set; }

    public string password { get; set; }


    [JsonIgnore]
    public virtual ICollection<Review> reviews { get; set; }
    [JsonIgnore]
    public virtual ICollection<Favorite> favorites { get; set; }
}