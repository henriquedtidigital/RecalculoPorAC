using RecalculoPorAC.Enums;

namespace RecalculoPorAC.Dtos;

public class InsumoDto
{
    public TipoInsumo TipoInsumo { get; set; }
    public int CodigoInsumo { get; set; }
    public decimal Quantidade { get; set; }
    public decimal QuantidadeTe { get; set; }
    public decimal DensidadeReal { get; set; }
    public decimal RazaoPorGrupo { get; set; }
    public decimal? QuantidadeSacos { get; set; }
}
