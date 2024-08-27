using RecalculoPorAC.Tracos;


var novoAc = 0.701m; // O valor do novo AC vem da tela.

/*
 * O recálculo por AC é feito somente em Traço que:
 * É Traço Manual de Concreto;
 * Possui Traço Econômico Ativo.
 */
var traco = TracosMock.CriarTracoManualConcretoMock_SemAdicaoCimenticia();

Tracos.ValidarInsumoTraco(traco);

Tracos.PreencherRazaoTodosInsumosPorGrupo(traco);

var novoConsumoTe = traco.Agua / novoAc; // Consumo é quantidade do aglomerante em KG

var novoVolumeAglomeranteTe = Tracos.CalcularVolumeAglomerante(traco, novoConsumoTe); // Volume em LT

var volumeInsumosOriginal = Tracos.CalcularVolumeInsumos(traco);

var volumeArgamassaOriginal = Tracos.CalcularVolumeArgamassa(traco, volumeInsumosOriginal);

var novoVolumeAreia = volumeArgamassaOriginal - novoVolumeAglomeranteTe; 

var novoVolumeBrita = volumeInsumosOriginal - volumeArgamassaOriginal;

var novaQuantidadeAreia = Tracos.CalcularQuantidadeAreia(traco, novoVolumeAreia);

var novaQuantidadeBrita = Tracos.CalcularQuantidadeBrita(traco, novoVolumeBrita);

Tracos.RecalcularAglomeranteTe(traco, novoConsumoTe);

Tracos.RecalcularAreiaTe(traco, novaQuantidadeAreia);

Tracos.RecalcularBritaTe(traco, novaQuantidadeBrita);

Tracos.RecalcularAditivosTe(traco, novoConsumoTe);




