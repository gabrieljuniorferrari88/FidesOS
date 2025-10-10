
import { RespostaDetalheProducaoJson } from "./RespostaDetalheProducaoJson";

export interface RespostaAlocacaoTrabalhadorJson
{
	id: string;
	trabalhadorIdentificacao: string;
	valorTotal: number;
	detalhes: RespostaDetalheProducaoJson[];
}
