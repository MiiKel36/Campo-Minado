using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_Minado
{
    internal class Campo
    {

        public int x, y;
        public List<List<Bloco>> campoMinado = new List<List<Bloco>>();


        public void mostrarTodasBombas()
        {
            // Mostrar o campo minado (todos visíveis)
            for (int a = 0; a < campoMinado.Count; a++)
            {
                for (int b = 0; b < campoMinado[a].Count; b++)
                {
                    string campos = "";
                    if (campoMinado[a][b].isVisible)
                    {
                        campos = ((b + 1) % x == 0) ? $"[{campoMinado[a][b].bombsArround}]\n" : $"[{campoMinado[a][b].bombsArround}]";
                    }
                    else
                    {
                        campos = ((b + 1) % x == 0) ? $"[#]\n" : $"[#]";
                    }

                    if (campoMinado[a][b].isBomb)
                    {
                        campos = ((b + 1) % x == 0) ? $"[💣]\n" : $"[💣]";
                    }
                    Console.Write(campos);
                }
            }
        }
        public void colocandoBlocosNoCampoMinado()
        {
            Bloco bloco = new Bloco();
            double numDeBombas = 0.15 * (x * y); // 15% de bombas
            List<int[]> coordenadasBombas = bloco.returnBoombsCoordinates(Convert.ToInt32(numDeBombas), x, y);

            // Colocando blocos no campo minado
            for (int i = 0; i < y; i++)
            {
                campoMinado.Add(new List<Bloco>());
                for (int j = 0; j < x; j++)
                {
                    Bloco blocoParaCampoMinado = new Bloco();
                    int[] coordenada = { i, j };
                    blocoParaCampoMinado.isBomb = coordenadasBombas.Any(array => array.SequenceEqual(coordenada)) ? true : false; // Verifica se i é uma coordenada de bomba

                    campoMinado[i].Add(blocoParaCampoMinado);
                }
            }

        }
        public void adicionarQuantidadeDeBombasEmVolta()
        {
            // Variáveis de ângulo e quantidade de bombas
            int bombCount = 0;
            int[] XYangulos = { -1, 0, 1 };

            // Adicionar quantidade de bombas em volta ao bloco
            for (int yIndex = 0; yIndex < campoMinado.Count; yIndex++) // Passar por todas as linhas
            {
                for (int xIndex = 0; xIndex < campoMinado[yIndex].Count; xIndex++) // Passar por todas as colunas
                {
                    foreach (int YDirection in XYangulos) // Verificar 3 linhas
                    {
                        foreach (int xDirection in XYangulos) // Verificar 3 colunas
                        {
                            try
                            {
                                // Se tiver bomba em volta, adicionar quantidade de bombas no bloco
                                if (campoMinado[yIndex + YDirection][xIndex + xDirection].isBomb)
                                {
                                    bombCount++;
                                }
                            }
                            catch { }
                        }
                    }
                    campoMinado[yIndex][xIndex].bombsArround = bombCount;
                    bombCount = 0;
                }
            }

        }


        public string printOnCosoleCampoMinado(List<List<Bloco>> campoMinado, int x)
        {
            string campos = "";
            string strCampoMinado = "";

            // Mostrar o campo minado 
            for (int a = 0; a < campoMinado.Count; a++)
            {
                for (int b = 0; b < campoMinado[a].Count; b++)
                {
                    // Se o bloco já for visível
                    if (campoMinado[a][b].isVisible)
                    {
                        // Se existem bombas em volta
                        if (campoMinado[a][b].bombsArround > 0)
                        {
                            campos = ((b + 1) % x == 0) ? $"[{campoMinado[a][b].bombsArround}]\n" : $"[{campoMinado[a][b].bombsArround}]";
                            strCampoMinado = strCampoMinado + campos;
                        }
                        // Se não existem bombas em volta
                        else
                        {
                            campos = ((b + 1) % x == 0) ? "[ ]\n" : "[ ]";
                            strCampoMinado = strCampoMinado + campos;
                        }
                    }
                    else // Se o bloco não for visível
                    {

                        campos = ((b + 1) % x == 0) ? "[#]\n" : "[#]";
                        strCampoMinado = strCampoMinado + campos;
                    }
                }
            }
            return strCampoMinado;
        }

        public List<List<Bloco>> clickInBloco(int y, int x, List<List<Bloco>> campoMinado)
        {
            // Variáveis para não repetir ação em bloco
            List<int[]> blocosAbertos = new List<int[]>();
            List<int[]> blocosParaAbrir = new List<int[]>();

            // Direções para analisar blocos
            List<int[]> direcoesParaAnalizar = new List<int[]>();

            direcoesParaAnalizar.Add(new int[] { -1, 0 });
            direcoesParaAnalizar.Add(new int[] { 0, -1 });
            direcoesParaAnalizar.Add(new int[] { 0, 1 });
            direcoesParaAnalizar.Add(new int[] { 1, 0 });

            int[] blocoSelecionado = { y, x };
            blocosParaAbrir.Add(blocoSelecionado);

            int i = blocosParaAbrir.Count - 1; ; // Para sempre olhar os blocos novos que foram adicionados
            int[] coordeandaDeBlocosParaAbrir = new int[2]; // Variável para armazenar coordenada atual a se abrir

            while (i < blocosParaAbrir.Count)
            {
                // Selecionando coordenada com base na variável I
                coordeandaDeBlocosParaAbrir = blocosParaAbrir[i];

                // Erro caso o bloco em volta ultrapasse os limites da List campoMinado
                try
                {
                    // Se o bloco atual já for visível, não irá fazer nada
                    if (!(campoMinado[coordeandaDeBlocosParaAbrir[0]][coordeandaDeBlocosParaAbrir[1]].isVisible))
                    {
                        // Se o número de bombas for maior que 0, apenas deixa visível e mais nada
                        if (campoMinado[coordeandaDeBlocosParaAbrir[0]][coordeandaDeBlocosParaAbrir[1]].bombsArround > 0)
                        {
                            blocosAbertos.Add(coordeandaDeBlocosParaAbrir);
                            campoMinado[coordeandaDeBlocosParaAbrir[0]][coordeandaDeBlocosParaAbrir[1]].isVisible = true;
                        }
                        else
                        {
                            // Adiciona bloco a já abertos e deixa visível
                            blocosAbertos.Add(coordeandaDeBlocosParaAbrir);
                            campoMinado[coordeandaDeBlocosParaAbrir[0]][coordeandaDeBlocosParaAbrir[1]].isVisible = true;

                            // Passa por todos os blocos em volta
                            foreach (int[] j in direcoesParaAnalizar)
                            {
                                // Seleciona bloco em volta
                                int[] blocosAoLado = { blocosParaAbrir[i][0] + j[0], blocosParaAbrir[i][1] + j[1] };
                                // Verifica se o bloco ao lado já está na lista
                                bool isNotCoordeandaInBlocosAberto = !(blocosParaAbrir.Any(array => array.SequenceEqual(blocosAoLado)) || blocosAbertos.Any(array => array.SequenceEqual(blocosAoLado)));

                                // Se não, adiciona aos blocos a se verificar
                                if (isNotCoordeandaInBlocosAberto)
                                {
                                    blocosParaAbrir.Add(blocosAoLado);
                                }
                            }
                        }
                        i++;
                    }
                }
                catch { i++; }
            }
            return campoMinado;
        }
    }
}
