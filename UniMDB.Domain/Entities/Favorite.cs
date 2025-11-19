using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMDB.Domain.Entities;

public class Favorite
{
    public uint id_favorite { get; set; }
    public uint id_user { get; set; }
    [StringLength(maximumLength:12)]
    public string id_movie_mdb { get; set; }
    
    public virtual User user { get; set; }
}