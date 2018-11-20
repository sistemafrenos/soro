using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK
{
    class cResumen
    {
        double activos = 0;

        public double Activos
        {
            get 
            {
               return bancos + caja + cuentasxcobrar + inventarios;
            }
            set { activos = value; }
        }
        double saldo = 0;

        public double Saldo
        {
            get { return Activos - cuentasxpagar; }
            set { saldo = value; }
        }
        double inventarios = 0;

        public double Inventarios
        {
            get { return inventarios; }
            set { inventarios = value; }
        }
        double cuentasxcobrar = 0;

        public double Cuentasxcobrar
        {
            get { return cuentasxcobrar; }
            set { cuentasxcobrar = value; }
        }
        double bancos = 0;

        public double Bancos
        {
            get { return bancos; }
            set { bancos = value; }
        }
        double caja = 0;

        public double Caja
        {
            get { return caja; }
            set { caja = value; }
        }
        double cuentasxpagar = 0;

        public double Cuentasxpagar
        {
            get { return cuentasxpagar; }
            set { cuentasxpagar = value; }
        }
    }
}
