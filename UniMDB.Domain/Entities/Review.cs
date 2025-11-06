namespace UniMDB.Domain.Entities;

public class Review
{
    public int id_review { get; set; }
    public int id_user { get; set; }
    public string id_movie_mdb { get; set; }
    public int review { get; set; }
    public string comment { get; set; }
    public DateTime created_at { get; set; }
}