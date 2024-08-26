using RecalculoPorAC.Tracos;


var novoAc = 0.701m; // O valor do novo AC vem da tela.

/*
 * O recálculo por AC é feito somente em Traço que:
 * É Traço Manual de Concreto;
 * Possui Traço Econômico Ativo.
 */
var traco = Tracos.CriarTracoManualConcretoMock();

Tracos.ValidarInsumoTraco(traco);

Tracos.PreencherRazaoTodosInsumosPorGrupo(traco);

var novoConsumoTe = traco.Agua / novoAc; // Consumo é quantidade do aglomerante em KG

var volumeAglomerante = Tracos.CalcularVolumeAglomerante(traco, novoConsumoTe); // Volume em LT

var volumeInsumos = Tracos.CalcularVolumeInsumos(traco); //Vou precisar do volume total? o novo aglomerante não deveria entrar aqui?

var volumeArgamassa = Tracos.CalcularVolumeArgamassa(traco, volumeInsumos);

var volumeAreia = volumeArgamassa - volumeAglomerante;

var volumeBrita = volumeInsumos - volumeArgamassa;

var quantidadeAreia = Tracos.CalcularQuantidadeAreia(traco, volumeAreia);

var quantidadeBrita = Tracos.CalcularQuantidadeBrita(traco, volumeBrita);

Tracos.RecalcularAglomeranteTe(traco, novoConsumoTe);

Tracos.RecalcularAreiaTe(traco, quantidadeAreia);

Tracos.RecalcularBritaTe(traco, quantidadeBrita);




