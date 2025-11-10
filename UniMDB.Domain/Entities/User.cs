using System.Text.Json.Serialization;

namespace UniMDB.Domain.Entities;

//  Essa classe será o modelo que enviaremos pro Migrations para traduzir as propriedas aqui presente 
//  para os atributos da tabela 'Users'
public class User
{
    public uint Id_user { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; }  = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; }  = string.Empty;

    [JsonIgnore]
    public virtual ICollection<Review> reviews { get; set; }
}