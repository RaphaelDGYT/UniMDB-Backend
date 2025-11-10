using System.Text.Json.Serialization;

namespace UniMDB.Domain.Entities;

//  Essa classe será o modelo que enviaremos pro Migrations para traduzir as propriedas aqui presente 
//  para os atributos da tabela 'Users'
public class User
{
    public uint id_user { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    [JsonIgnore]
    public virtual ICollection<Review> reviews { get; set; }
}