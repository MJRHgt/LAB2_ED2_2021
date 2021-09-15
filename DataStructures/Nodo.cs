using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class Nodo<T>
    {
        public Nodo<T> Padre;
        public Nodo<T> Hijoizq;
        public Nodo<T> Hijoder;
        public T Valor;
    }
}
