using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_Minado
{
    internal class Bloco
    {
        public int location;
        public int bombsArround;
        public bool isBomb;
        public bool isVisible = false;

        public int countBombsArround()
        {
            return 0;
        }

        public List<int[]> returnBoombsCoordinates(int numDeBombas, int x, int y)
        {
            List<int[]> coordenadasBombas = new List<int[]>();
            for (int j = 0; j <= numDeBombas; j++)
            {
                var rand = new Random();
                int randomNumberForX = rand.Next(0, x);
                int randomNumberForY = rand.Next(0, y);

                int[] CordXY = { randomNumberForX, randomNumberForY };

                // Se o número random não estiver na lista de coordenadas, adicionar à lista de coordenadas
                if (!(coordenadasBombas.Contains(CordXY)))
                {
                    coordenadasBombas.Add(CordXY);
                }
                else { j--; }
            }
            return coordenadasBombas;
        }


    }
}