using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public static class InverseKinematicSolver
    {
        public static List<IConfiguracion> Aplicar(INodo nodoBase, IValor valorIncial, IValor valorObjetivo)
        {
            List<IConfiguracion> configuracionFinal = new List<IConfiguracion>();




            return configuracionFinal;
        }

        private static float ValorMinimizar(INodo nodoBase, IValor valorInicial, IValor valorObjetivo)
        {
            IValor valorObtenido = Funcion(nodoBase, valorInicial);
            IValor diferencia = valorObjetivo.Sumar(valorObtenido.Multiplicar(-1f));

            return diferencia.Modulo();
        }

        private static IValor Funcion(INodo nodoBase, IValor valorInicial)
        {
            IValor valorResultado = valorInicial;

            for (INodo nodoActual = nodoBase; nodoActual != null; nodoActual = nodoActual.NodoSiguiente)
                valorResultado = nodoActual.Transladar(valorResultado);

            return valorResultado;
        }
    }
}