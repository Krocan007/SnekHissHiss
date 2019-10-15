using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SnekJadro;

namespace SnekKonzole
{
    class Program
    {
        static void Main(string[] args)
        {
            Vec[,] Karel = new Vec[14,14];
            Karel=VlozZradlo(Karel);
            MonitorKonzole Hovno = new MonitorKonzole();
            Karel=Hovno.VlozOhraniceni(Karel);
            Snek snek = new SnekJadro.Snek(Karel);
            snek.NakresliHada();
            NemennePoleVeci Pepa = new NemennePoleVeci(Karel);
            Hovno.PrijmiPoleVeci(Pepa);
            while (true)
            {
                Hovno.Refresh();
                Karel=snek.HniSe();
                if (ZradloNeexistuje(Karel))
                {
                    Hovno.PredelejSkore(snek.Skore);
                    VlozZradlo(Karel);
                }
            }


            
        }

        public static Vec[,] VlozZradlo(Vec[,] Mapa)
        {
            Random rnd = new Random();
            int nahodnyX = rnd.Next(1, Mapa.GetLength(0) - 2);
            int nahodnyY = rnd.Next(1, Mapa.GetLength(1) - 2);
            Mapa[nahodnyX, nahodnyY] = Vec.zradlo;
            return Mapa;
        }
        public static bool ZradloNeexistuje(Vec[,] Mapa)
        {
            for (int i = 1; i < Mapa.GetLength(0)-1; i++)
            {
                for (int j = 1; j < Mapa.GetLength(1)-1; j++)
                {
                    if (Mapa[i,j]==Vec.zradlo)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }

    class MonitorKonzole : IMonitor
    {
        NemennePoleVeci novePoleVeci;
        Vec[,] starePoleVeci;
        public void PrijmiPoleVeci(NemennePoleVeci poleVeci)
        {
            this.novePoleVeci=poleVeci;
            starePoleVeci = new Vec[poleVeci.RozmerX,poleVeci.RozmerY];
            UlozPoleDoStaryho();
            RefreshSClearem();
        }

        private void RefreshSClearem()
        {
            Console.Clear();
            for (int i = 0; i < novePoleVeci.RozmerY; i++)
            {
                for (int j = 0; j < novePoleVeci.RozmerX; j++)
                {
                    PrepisZnak(j,i);
                }
            }
        }
        void UlozPoleDoStaryho()
        {
            for (int x = 0; x < novePoleVeci.RozmerX; x++)
            {
                for (int y = 0; y < novePoleVeci.RozmerY; y++)
                {
                    starePoleVeci[x, y] = novePoleVeci[x, y];
                }
            }
        }
        public void Refresh()
        {
            for (int x = 0; x < novePoleVeci.RozmerX; x++)
            {
                for (int y = 0; y < novePoleVeci.RozmerY; y++)
                {
                    if (novePoleVeci[x,y]!=starePoleVeci[x,y])
                    {
                        PrepisZnak(x,y);
                    }
                }
            }
            UlozPoleDoStaryho();
        }

        public void PredelejSkore(int Skore)
        {
            Console.SetCursorPosition(novePoleVeci.RozmerX + 2, novePoleVeci.RozmerY + 2);
            Console.Write(Skore.ToString());
        }

        private void PrepisZnak(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            switch (novePoleVeci[x, y])
            {
                case Vec.nic:
                    Console.Write(" ");
                    break;
                case Vec.had:
                    Console.Write("#");
                    break;
                case Vec.zradlo:
                    Console.Write("*");
                    break;
                case Vec.krajX:
                    Console.Write("-");
                    break;
                case Vec.krajY:
                    Console.Write("|");
                    break;
                default:
                    break;
            }
        }
        public Vec[,] VlozOhraniceni(Vec[,] NovePole)
        {
            for (int i = 0; i < NovePole.GetLength(1); i++)
            {
                NovePole[0, i] = Vec.krajY;
                NovePole[NovePole.GetLength(0) - 1, i] = Vec.krajY;
            }
            for (int i = 0; i < NovePole.GetLength(0); i++)
            {
                NovePole[i, 0] = Vec.krajX;
                NovePole[i, NovePole.GetLength(1) - 1] = Vec.krajX;
            }
            return NovePole;
        }
    }
}
