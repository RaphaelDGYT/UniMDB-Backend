using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMDB.Domain.Entities;

public class Review
{
    public uint id_review { get; set; }
    public uint id_review_user { get; set; }
    [StringLength(maximumLength:9, MinimumLength = 9)]
    public string id_movie_mdb { get; set; }
    [Range(0, 10)]
    public byte review { get; set; }
    public string comment { get; set; }
    public DateTime created_at { get; set; }
    
    public virtual User user { get; set; }
}