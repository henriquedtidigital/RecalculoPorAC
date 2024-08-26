﻿using RecalculoPorAC.Dtos;
using RecalculoPorAC.Enums;

namespace RecalculoPorAC.Tracos;

public class Tracos
{
    public static TracoDto CriarTracoManualConcretoMock()
    {
        return new TracoDto
        {
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

    public static void ValidarInsumoTraco(TracoDto traco)
    {
        /* Os Insumos do traço devem ter:
         * Custo;
         * Densidade Real;
         * Disponibilidade.
         */
    }

    public static void PreencherRazaoTodosInsumosPorGrupo(TracoDto traco)
    {
        var insumosAglomerante = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Cimento || i.TipoInsumo == TipoInsumo.AdicaoCimenticia)
            .Where(i => !i.QuantidadeSacos.HasValue); // Remover Adicao Cimenticia em Saco dessa conta

        var insumosBrita = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Brita);

        var insumosAreia = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Areia);

        PreencherPercentualInsumoPorGrupo(insumosAglomerante);
        PreencherPercentualInsumoPorGrupo(insumosBrita);
        PreencherPercentualInsumoPorGrupo(insumosAreia);
    }

    private static void PreencherPercentualInsumoPorGrupo(IEnumerable<InsumoDto> insumos)
    {
        //Olhar sempre quantidades do traço original e não TE
        var quantidadeAglomerante = insumos.Sum(i => i.Quantidade);

        foreach (var insumo in insumos)
            insumo.RazaoPorGrupo = insumo.Quantidade / quantidadeAglomerante;
    }

    public static decimal CalcularVolumeAglomerante(TracoDto traco, decimal consumo)
    {
        var insumosAglomerante = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Cimento || i.TipoInsumo == TipoInsumo.AdicaoCimenticia);
        return insumosAglomerante.Sum(insumo => consumo * insumo.RazaoPorGrupo / insumo.DensidadeReal);
    }

    public static decimal CalcularVolumeArgamassa(TracoDto traco, decimal volumeInsumos)
    {
        //Olhar sempre quantidades do traço original e não TE
        var quantidadeArgamassa = traco.Insumos.Where(
            i => i.TipoInsumo == TipoInsumo.Cimento ||
            i.TipoInsumo == TipoInsumo.AdicaoCimenticia ||
            i.TipoInsumo == TipoInsumo.Areia).Sum(i => i.Quantidade);
        //Duvida Adição em saco é considerada??

        var quantidadeBrita = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Brita).Sum(i => i.Quantidade);

        var teorArgamassa = quantidadeArgamassa / (quantidadeArgamassa + quantidadeBrita);

        return volumeInsumos * teorArgamassa;
    }

    public static decimal CalcularVolumeInsumos(TracoDto traco)
    {
        //Aditivo e Adição descontam da água e por isso não entram nessa conta
        var insumosTraco = traco.Insumos.Where(i => i.TipoInsumo != TipoInsumo.Adicoes && i.TipoInsumo != TipoInsumo.Aditivo);

        return insumosTraco.Sum(insumo => insumo.Quantidade / insumo.DensidadeReal);
    }

    public static decimal CalcularQuantidadeAreia(TracoDto traco, decimal volumeAreia)
    {
        var densidadePonderadaAreia = CalcularDensidadePonderadaInsumo(traco, TipoInsumo.Areia);
        return volumeAreia * densidadePonderadaAreia;
    }

    public static decimal CalcularQuantidadeBrita(TracoDto traco, decimal volumeBrita)
    {
        var densidadePonderadaBrita = CalcularDensidadePonderadaInsumo(traco, TipoInsumo.Brita);
        return volumeBrita * densidadePonderadaBrita;
    }

    private static decimal CalcularDensidadePonderadaInsumo(TracoDto traco, TipoInsumo tipoInsumo)
    {
        var insumos = traco.Insumos.Where(i => i.TipoInsumo == tipoInsumo);

        return insumos.Sum(insumo => insumo.RazaoPorGrupo * insumo.DensidadeReal);
    }

    public static void RecalcularAglomeranteTe(TracoDto traco, decimal consumo)
    {
        var quantidadeAdicaoSaco = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.AdicaoCimenticia && i.QuantidadeSacos.HasValue).Sum(i => i.QuantidadeSacos * i.Quantidade);
        var novoConsumo = consumo - (quantidadeAdicaoSaco ?? 0);
        var insumosAglomerante = traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Cimento || i.TipoInsumo == TipoInsumo.AdicaoCimenticia && !i.QuantidadeSacos.HasValue);
        foreach (var insumo in insumosAglomerante)
        {
            insumo.QuantidadeTe = novoConsumo * insumo.RazaoPorGrupo;
        }
    }

    public static void RecalcularAreiaTe(TracoDto traco, decimal quantidadeAreia)
    {
        foreach (var insumo in traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Areia))
        {
            insumo.QuantidadeTe = quantidadeAreia * insumo.RazaoPorGrupo;
        }
    }

    public static void RecalcularBritaTe(TracoDto traco, decimal quantidadeBrita)
    {
        foreach (var insumo in traco.Insumos.Where(i => i.TipoInsumo == TipoInsumo.Brita))
        {
            insumo.QuantidadeTe = quantidadeBrita * insumo.RazaoPorGrupo;
        }
    }
}