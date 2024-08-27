namespace RecalculoPorAC.Dtos
{
    public class TracoDto
    {
        public int Agua { get; set; }
        public decimal Consumo { get; set; }
        public List<InsumoDto> Insumos { get; set; }
    }
}
