using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_Minado
{
    internal class Game
    {
        private bool isGameRunning = true;
        public void startGame()
        {
            Console.WriteLine("Carregando...");
            Campo objCampoMinado = new Campo();
            objCampoMinado.x = 10;
            objCampoMinado.y = 8;

            // Cria a matriz e armazena o objeto Bloco, já definindo o que é bomba e o que não é
            objCampoMinado.colocandoBlocosNoCampoMinado();

            // Verifica cada quadrado da matriz e armazena no bloco a quantidade de bombas ao redor
            objCampoMinado.adicionarQuantidadeDeBombasEmVolta();

            while (isGameRunning)
            {
                int choiceY = 0;
                int choiceX = 0;

                string errorMsg = "";
                string strCampoMinado = objCampoMinado.printOnCosoleCampoMinado(objCampoMinado.campoMinado, objCampoMinado.x);

                bool isChoiceRigth;

                Console.Clear();
                Console.WriteLine("\n" + strCampoMinado);

                // Prevenir que a escolha do jogador não seja nem maior nem menor que Y
                do
                {
                    // Prevenir que a escolha do jogador não seja nula nem uma letra
                    try
                    {
                        Console.WriteLine("Escolha a coordenada de Y:");
                        choiceY = Convert.ToInt32(Console.ReadLine()) - 1;

                        isChoiceRigth = (choiceY < 0 || choiceY > objCampoMinado.y);
                        errorMsg = isChoiceRigth ? "Coordenada de Y inválida" : "";
                        Console.WriteLine(errorMsg);
                    }
                    catch { Console.WriteLine("\n-- Erro: A coordenada não pode ser texto nem estar vazia --\n"); }

                } while (choiceY < 0 || choiceY > objCampoMinado.y);

                // Prevenir que a escolha do jogador não seja nem maior nem menor que X
                do
                {
                    // Prevenir que a escolha do jogador não seja nula nem uma letra
                    try
                    {
                        Console.WriteLine("Escolha a coordenada de X:");
                        choiceX = Convert.ToInt32(Console.ReadLine()) - 1;

                        isChoiceRigth = (choiceX < 0 || choiceX > objCampoMinado.x);
                        errorMsg = isChoiceRigth ? "Coordenada de X inválida" : "";
                        Console.WriteLine(errorMsg);

                    }
                    catch { Console.WriteLine("\n-- Erro: A coordenada não pode ser texto nem estar vazia  --\n"); }
                } while (choiceX < 0 || choiceX > objCampoMinado.x);

                // Se for uma bomba, fim do jogo
                if (objCampoMinado.campoMinado[choiceY][choiceX].isBomb == true)
                {
                    Console.WriteLine("\nFim de Jogo\nVOCÊ EXPLODIU\n\n");
                    objCampoMinado.mostrarTodasBombas();
                    break;
                }
                // Executar clique no campo minado
                objCampoMinado.campoMinado = objCampoMinado.clickInBloco(choiceY, choiceX, objCampoMinado.campoMinado);
            }
        }
    }
}
