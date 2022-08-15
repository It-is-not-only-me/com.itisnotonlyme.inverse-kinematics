using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public static class InverseKinematicSolver
    {
        public static void Aplicar(INodo nodoBase, IValor valorInicial, IValor valorObjetivo, float perturbacion, float multiplicador, int cantidadIteraciones, float errorMinimo)
        {
            float resultadoAnterior = ValorMinimizar(nodoBase, valorInicial, valorObjetivo);

            Iterar(nodoBase, valorInicial, valorObjetivo, perturbacion, multiplicador);

            float resultadoActual = ValorMinimizar(nodoBase, valorInicial, valorObjetivo);

            for (int iteracion = 1; iteracion < cantidadIteraciones && CalculoDeError(resultadoAnterior, resultadoActual) > errorMinimo; iteracion++)
            {
                resultadoAnterior = resultadoActual;

                Iterar(nodoBase, valorInicial, valorObjetivo, perturbacion, multiplicador);

                resultadoActual = ValorMinimizar(nodoBase, valorInicial, valorObjetivo);
            }
        }

        private static void Iterar(INodo nodoBase, IValor valorInicial, IValor valorObjetivo, float perturbacion, float mulpliciador)
        {
            CalcularGradiente(nodoBase, valorInicial, valorObjetivo, perturbacion);
            AplicarGradiente(nodoBase, mulpliciador);
        }

        private static float CalculoDeError(float resultadoAnterior, float resultadoActual)
        {
            return Mathf.Abs(resultadoActual - resultadoAnterior);
        }

        private static void CalcularGradiente(INodo nodoBase, IValor valorIncial, IValor valorObjetivo, float perturbacion)
        {
            foreach (INodo nodoActual in CaminoDeNodos(nodoBase))
                nodoActual.CalcularGradiente(ValorMinimizar, nodoBase, valorIncial, valorObjetivo, perturbacion);
        }

        private static void AplicarGradiente(INodo nodoBase, float multiplicador)
        {
            foreach (INodo nodoActual in CaminoDeNodos(nodoBase))
                nodoActual.AplicarGradiente(multiplicador);
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

            foreach (INodo nodoActual in CaminoDeNodos(nodoBase))
                valorResultado = nodoActual.Transladar(valorResultado);

            return valorResultado;
        }

        private static IEnumerable<INodo> CaminoDeNodos(INodo nodoBase)
        {
            for (INodo nodoActual = nodoBase; nodoActual != null; nodoActual = nodoActual.NodoSiguiente)
                yield return nodoActual;
        }
    }
}