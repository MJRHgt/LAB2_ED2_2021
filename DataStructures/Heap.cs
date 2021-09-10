using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class Heap<T>
    {
        public Nodo<T> raiz;
        public int Cant_Nodos;

        /// <summary>
        /// Metódo para agregar un nodo 
        /// </summary>
        /// <param name="valor">Valor que se quiere ingresar</param>
        /// <param name="Det_Prioridad">Delegado para determinar cual prioridad es mayor</param>
        public void Agregar(T valor, Delegate Det_Prioridad)
        {
            Nodo<T> nod_nuevo = new Nodo<T>();
            nod_nuevo.Valor = valor;
            Cant_Nodos++;
            if (raiz == null)
            {
                raiz = nod_nuevo;
                raiz.Padre = null;
            }
            else
            {
                Realizar_Recorrido(nod_nuevo);
                intercambiar_valor_padre(nod_nuevo, Det_Prioridad);
            }
        }

        /// <summary>
        /// Metodo para encontrar la posición donde se coloca el siguiente nodo (forma invariante)
        /// Segun la cantidad de nodos que posea la cola
        /// </summary>
        /// <param name="Nuevo">Valor que se agregara</param>
        private void Realizar_Recorrido(Nodo<T> Nuevo)
        {
            Nodo<T> Guia = raiz;
            string bin = Binario_Conver();
            for (int i = 1; i < bin.Length; i++)
            {
                if (Convert.ToString(bin[i]).Equals("0"))
                {
                    if (Guia.Hijoizq == null)
                    {
                        Guia.Hijoizq = Nuevo;
                        Nuevo.Padre = Guia;
                    }
                    else
                    {
                        Guia = Guia.Hijoizq;
                    }
                }
                else
                {
                    if (Guia.Hijoder == null)
                    {
                        Guia.Hijoder = Nuevo;
                        Nuevo.Padre = Guia;
                    }
                    else
                    {
                        Guia = Guia.Hijoder;
                    }
                }
            }
        }

        /// <summary>
        /// Intercambiar el valor entre el nodo padre e hijo si es necesario (orden invariante)
        /// </summary>
        /// <param name="agregado"></param>
        /// <param name="Deter_Prioridad"></param>
        private void intercambiar_valor_padre(Nodo<T> agregado, Delegate Deter_Prioridad)
        {
            if (agregado.Padre != null)
            {
                T aux_val = agregado.Padre.Valor;
                if ((bool)Deter_Prioridad.DynamicInvoke(agregado.Padre.Valor, agregado.Valor))
                {
                    agregado.Padre.Valor = agregado.Valor;
                    agregado.Valor = aux_val;
                    intercambiar_valor_padre(agregado.Padre, Deter_Prioridad);
                }
            }
        }

        /// <summary>
        /// Metodo para eliminar el valor de la raiz (nodo con mayor prioridad)
        /// </summary>
        /// <param name="Comparar">Delegado para comparar prioridades</param>
        /// <param name="Det_prioridad">Delegado para determinar cual prioridad es mayor</param>
        /// <returns>Retorna el valor que se encuentra en la raiz</returns>
        public T Eliminar(Delegate Comparar, Delegate Det_prioridad, Delegate Comparar_Val)
        {
            T aux = default(T);
            if (raiz != null)
            {
                aux = raiz.Valor;
                Nodo<T> cola = Encontrar_ultimo_nodo();
                raiz.Valor = cola.Valor;
                borrar_ult_nodo(cola, Comparar_Val);
                if (raiz != null) { intercambiar_valor_hijo(raiz, Comparar, Det_prioridad); }
                Cant_Nodos--;
            }
            return aux;
        }

        /// <summary>
        /// Metodo para borrar el ultimo nodo agregado, según forma invariante
        /// </summary>
        /// <param name="ultimo">Ultimo nodo que se agrego</param>
        /// <param name="Comparador">Delegado para comparar prioridades</param>
        private void borrar_ult_nodo(Nodo<T> ultimo, Delegate Comparador)
        {
            Nodo<T> aux_last = ultimo;
            if (ultimo.Padre != null)
            {
                if (ultimo.Padre.Hijoder != null && ultimo.Padre.Hijoizq != null)
                {
                    if ((int)Comparador.DynamicInvoke(ultimo.Valor, ultimo.Padre.Hijoder.Valor) == 0)
                    {
                        ultimo.Padre.Hijoder = null;
                    }
                    else
                    {
                        ultimo.Padre.Hijoizq = null;
                    }
                }
                else if (ultimo.Padre.Hijoizq != null)
                {
                    ultimo.Padre.Hijoizq = null;
                }
                else if (ultimo.Padre.Hijoder != null)
                {
                    ultimo.Padre.Hijoder = null;
                }
            }
            else
            {
                raiz = null;
            }

        }


        /// <summary>
        /// Metodo para encontrar cual es el ultimo nodo según forma invariante
        /// </summary>
        /// <returns>Ultimo nodo que se agrego</returns>
        private Nodo<T> Encontrar_ultimo_nodo()
        {
            Nodo<T> Guia = raiz;
            string bin = Binario_Conver();
            for (int i = 1; i < bin.Length; i++)
            {
                if (Convert.ToString(bin[i]).Equals("0"))
                {
                    Guia = Guia.Hijoizq;
                }
                else
                {
                    Guia = Guia.Hijoder;
                }
            }
            return Guia;
        }

        /// <summary>
        /// Metodo para intercambiar el nodo padre e hijo si es necesario (para la eliminación)
        /// </summary>
        /// <param name="movido">Nodo sobre el cual se realizar el movimiento de valor</param>
        /// <param name="Comparador">Delegado para comparar las prioridades</param>
        /// <param name="Deter_Prior">Delegado para determinar cual prioridad es mayor</param>
        private void intercambiar_valor_hijo(Nodo<T> movido, Delegate Comparador, Delegate Deter_Prior)
        {
            T aux_val = movido.Valor;
            if (movido.Hijoder != null && movido.Hijoizq != null)
            {
                if ((int)Comparador.DynamicInvoke(movido.Hijoizq.Valor, movido.Hijoder.Valor) > 0)
                {
                    if ((bool)Deter_Prior.DynamicInvoke(movido.Valor, movido.Hijoder.Valor))
                    {
                        movido.Valor = movido.Hijoder.Valor;
                        movido.Hijoder.Valor = aux_val;
                        intercambiar_valor_hijo(movido.Hijoder, Comparador, Deter_Prior);
                    }
                }
                else
                {
                    if ((bool)Deter_Prior.DynamicInvoke(movido.Valor, movido.Hijoizq.Valor))
                    {
                        movido.Valor = movido.Hijoizq.Valor;
                        movido.Hijoizq.Valor = aux_val;
                        intercambiar_valor_hijo(movido.Hijoizq, Comparador, Deter_Prior);
                    }
                }
            }
            else if (movido.Hijoder != null)
            {
                if ((bool)Deter_Prior.DynamicInvoke(movido.Valor, movido.Hijoder.Valor))
                {
                    movido.Valor = movido.Hijoder.Valor;
                    movido.Hijoder.Valor = aux_val;
                    intercambiar_valor_hijo(movido.Hijoder, Comparador, Deter_Prior);
                }
            }
            else if (movido.Hijoizq != null)
            {
                if ((bool)Deter_Prior.DynamicInvoke(movido.Valor, movido.Hijoizq.Valor))
                {
                    movido.Valor = movido.Hijoizq.Valor;
                    movido.Hijoizq.Valor = aux_val;
                    intercambiar_valor_hijo(movido.Hijoizq, Comparador, Deter_Prior);
                }
            }
        }

        /// <summary>
        /// Metodo para convertir la cantidad de nodos en binario
        /// </summary>
        /// <returns>Numero binario de la cantidad de nodos</returns>
        private string Binario_Conver()
        {
            int total_nodo = Cant_Nodos;
            string cadena = "";
            if (total_nodo > 0)
            {
                while (total_nodo > 0)
                {
                    if (total_nodo % 2 == 0)
                    {
                        cadena = "0" + cadena;
                    }
                    else
                    {
                        cadena = "1" + cadena;
                    }
                    total_nodo = (total_nodo / 2);
                }
            }
            return cadena;
        }
    }
}
