using RecalculoPorAC.Dtos;
using RecalculoPorAC.Enums;

namespace RecalculoPorAC.Tracos
{
    public static class TracosMock
    {
        public static TracoDto CriarTracoManualConcretoMock_SemAdicaoCimenticia()
        {
            return new TracoDto
            {
                Consumo = 346,
                Agua = 215,
                Insumos = new List<InsumoDto>
            {
                 new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Areia,
                    CodigoInsumo = 2,
                    Quantidade = 691,
                    QuantidadeTe = 612,
                    DensidadeReal = 2.535m
                },
                  new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Areia,
                    CodigoInsumo = 14,
                    Quantidade = 296,
                    QuantidadeTe = 408,
                    DensidadeReal = 2.688m
                },
                  new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Brita,
                    CodigoInsumo = 1,
                    Quantidade = 800,
                    QuantidadeTe = 778,
                    DensidadeReal = 2.646m
                },
                new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Cimento,
                    CodigoInsumo = 156,
                    Quantidade = 346,
                    QuantidadeTe = 335,
                    DensidadeReal = 3.15m
                },
                 new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Aditivo,
                    CodigoInsumo = 244,
                    Quantidade = 3.29m,
                    QuantidadeTe = 3,
                    DensidadeReal = 1.07m
                },
                  new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Aditivo,
                    CodigoInsumo = 410,
                    Quantidade = 0.87m,
                    QuantidadeTe = 0.842m,
                    DensidadeReal = 1.24m
                },
                   new InsumoDto
                {
                    TipoInsumo = TipoInsumo.Aditivo,
                    CodigoInsumo = 420,
                    Quantidade = 2.43m,
                    QuantidadeTe = 2,
                    DensidadeReal = 1.13m
                },
            }
            };
        }
    }
}
