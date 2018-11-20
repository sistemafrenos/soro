using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class Resultados
    {
        decimal billete = 0;

        public decimal Billete
        {
            get { return billete; }
            set { billete = value; }
        }
        decimal cantidad = 0;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        decimal monto = 0;

        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }
    }
}
