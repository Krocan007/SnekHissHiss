using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnekJadro
{
    public class Snek
    {
        public Snek(Vec[,] mapa)
        {
            had = new int[1, 2];
            had[0, 0] = 5;
            had[0, 1] = 5;
            otoceni= Otoceni.nahoru;
            this.mapa=mapa;
        }
        private enum Otoceni { nahoru, dolu, pravo, levo }
        private int[,] had;
        private int[,] staryHad;
        private int skore;
        Otoceni otoceni;
        Vec[,] mapa;

        public int Skore { get => skore; set => skore = value; }

        public Vec[,] HniSe ()
        {
            Checkuj();
            CekejAOtocSe();
            if (CheckujZradlo())
            {
                VyrostHada();
                Skore += 1;
            }
            SmazOcas();
            UpdatujHada();
            NakresliHada();
            return mapa;

        }
        public void Checkuj()
        {
            switch (otoceni)
            {
                case Otoceni.nahoru:
                    if (mapa[had[0, 0], had[0, 1] - 1] == Vec.had)
                        throw new NotImplementedException();
                    break;
                case Otoceni.dolu:
                    if (mapa[had[0, 0], had[0, 1] + 1] == Vec.had)
                        throw new NotImplementedException();
                    break;
                case Otoceni.pravo:
                    if (mapa[had[0, 0] + 1, had[0, 1]] == Vec.had)
                        throw new NotImplementedException();
                    break;
                case Otoceni.levo:
                    if (mapa[had[0, 0] - 1, had[0, 1]] == Vec.had)
                        throw new NotImplementedException();
                    break;
                default:
                    break;
            }
        }

        public bool CheckujZradlo()
        {
            switch (otoceni)
            {
                case Otoceni.nahoru:
                    if (mapa[had[0, 0], had[0, 1] - 1] == Vec.zradlo)
                    {
                        mapa[had[0, 0], had[0, 1] - 1] = Vec.nic;
                        return true;
                    }
                    return false;
                case Otoceni.dolu:
                    if (mapa[had[0, 0], had[0, 1] + 1] == Vec.zradlo)
                    {
                        mapa[had[0, 0], had[0, 1] + 1] = Vec.nic;
                        return true;
                    }
                    return false;
                case Otoceni.pravo:
                    if (mapa[had[0, 0] + 1, had[0, 1]] == Vec.zradlo)
                    {
                        mapa[had[0, 0] + 1, had[0, 1]] = Vec.nic;
                        return true;
                    }
                    return false;
                case Otoceni.levo:
                    if (mapa[had[0, 0] - 1, had[0, 1]] == Vec.zradlo)
                    {
                        mapa[had[0, 0] - 1, had[0, 1]] = Vec.nic;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        public void CekejAOtocSe()
        {
            Thread.Sleep(250);
            if (Console.KeyAvailable)
            {
                var sipka = Console.ReadKey(true).Key;
                switch (sipka)
                {
                    case ConsoleKey.LeftArrow:
                        if (otoceni != Otoceni.pravo)
                        {
                            otoceni = Otoceni.levo;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (otoceni != Otoceni.dolu)
                        {
                            otoceni = Otoceni.nahoru;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (otoceni != Otoceni.levo)
                        {
                            otoceni = Otoceni.pravo;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (otoceni != Otoceni.nahoru)
                        {
                            otoceni = Otoceni.dolu;
                        }
                        break;
                    default:
                        break;
                }
            }
            Thread.Sleep (250);


        }
        public void NakresliHada()
        {
            for (int i = 0; i < had.GetLength(0); i++)
            {
                mapa[had[i,0], had[i,1]] = Vec.had;
            }
        }
        public void UpdatujHada()
        {
            for (int i = had.GetLength(0)-1; i > 0; i--)
            {
                had[i, 0] = had[i - 1, 0];
                had[i, 1] = had[i - 1, 1];
            }
            switch (otoceni)
            {
                case Otoceni.nahoru:
                    had[0, 1] -=1;
                    break;
                case Otoceni.dolu:
                    had[0, 1] += 1;
                    break;
                case Otoceni.pravo:
                    had[0, 0] += 1;
                    break;
                case Otoceni.levo:
                    had[0, 0] -= 1;
                    break;
                default:
                    break;
            }

        }
        public void SmazOcas()
        {
            mapa[had[had.GetLength(0) - 1, 0], had[had.GetLength(0) - 1, 1]]=Vec.nic;
        }
        public void VyrostHada()
        {
            staryHad = had;
            had = new int[staryHad.GetLength(0) + 1, 2];
            for (int i = 0; i < staryHad.GetLength(0); i++)
            {
                had[i + 1, 0] = staryHad[i, 0];
                had[i + 1, 1] = staryHad[i, 1];
            }

            switch (otoceni)
            {
                case Otoceni.nahoru:
                    had[0, 1] = had[1, 1] - 1;
                    had[0, 0] = had[1, 0];
                    break;
                case Otoceni.dolu:
                    had[0, 1] = had[1, 1] + 1;
                    had[0, 0] = had[1, 0];
                    break;
                case Otoceni.pravo:
                    had[0, 0] = had[1, 0] + 1;
                    had[0, 1] = had[1, 1];
                    break;
                case Otoceni.levo:
                    had[0, 0] = had[1, 0] - 1;
                    had[0, 1] = had[1, 1];
                    break;
                default:
                    break;
            }
        }
    }
}
