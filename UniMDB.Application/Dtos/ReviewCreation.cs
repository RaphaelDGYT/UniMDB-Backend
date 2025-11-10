namespace UniMDB.Application.Dtos;

//  DTO usado pela API para obtermos apenas as informações uteis para a criação de uma Review

public class ReviewCreation
{
    public byte score { get; set; }
    public string comment { get; set; }
}