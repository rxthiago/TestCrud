using System.Reflection.Metadata.Ecma335;

namespace _1_Domain.Entities;

public class Produto
{
    public int ID { get; set; }
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public int Quantidade { get; set; }

}
