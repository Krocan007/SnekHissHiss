using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnekJadro
{
    public class NemennePoleVeci
    {
        public NemennePoleVeci(Vec[,] poleVeci)
        {
            this.poleVeci = poleVeci;
            RozmerX = poleVeci.GetLength(0);
            RozmerY = poleVeci.GetLength(1);
        }
        Vec[,] poleVeci { get; }
        public int RozmerX { get; }
        public int RozmerY { get; }


        public Vec this[int x, int y]
        {
            get
            {
                return poleVeci[x, y];
            }
        }
    }
    public enum Vec {nic, had, zradlo, krajX, krajY}
    public interface IMonitor
    {
        void PrijmiPoleVeci(NemennePoleVeci poleVeci);
        void Refresh();
    }
}
