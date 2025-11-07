namespace UniMDB.Application.Dtos;

//  DTO usado pela API para obter apenas as partes importantes da Review

public class ReviewResponse
{
    public string user { get; set; }
    public byte score { get; set; }
    public string comment { get; set; }
    public DateTime created_at { get; set; }
}