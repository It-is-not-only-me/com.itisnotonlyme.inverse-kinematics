using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public static class InverseKinematicSolver
    {
        public static void Aplicar(INodo nodoBase, IValor valorInicial, IValor valorObjetivo, float perturbacion, float multiplicador, int cantidadIteraciones, float errorMinimo)
        {
            CalcularErrorDeEstadoActual funcionAMinimizar = new CalcularErrorDeEstadoActual(nodoBase, valorInicial, valorObjetivo);

            float resultadoAnterior = funcionAMinimizar.Evaluar();

            Iterar(funcionAMinimizar, nodoBase, resultadoAnterior, perturbacion, multiplicador);

            float resultadoActual = funcionAMinimizar.Evaluar();

            for (int iteracion = 1; iteracion < cantidadIteraciones && CalculoDeError(resultadoAnterior, resultadoActual) > errorMinimo; iteracion++)
            {
                resultadoAnterior = resultadoActual;

                Iterar(funcionAMinimizar, nodoBase, resultadoAnterior, perturbacion, multiplicador);

                resultadoActual = funcionAMinimizar.Evaluar();
            }
        }

        private static void Iterar(IFuncionMinimizar funcionAMinimizar, INodo nodoBase, float evaluacionAnterior, float perturbacion, float mulpliciador)
        {
            CalcularGradiente(funcionAMinimizar, nodoBase, evaluacionAnterior, perturbacion);
            AplicarGradiente(nodoBase, mulpliciador);
        }

        private static float CalculoDeError(float resultadoAnterior, float resultadoActual)
        {
            return Mathf.Abs(resultadoActual - resultadoAnterior);
        }

        private static void CalcularGradiente(IFuncionMinimizar funcionAMinimizar, INodo nodoBase, float evaluacionAnterior, float perturbacion)
        {
            foreach (INodo nodoActual in CaminoDeNodos(nodoBase))
                nodoActual.CalcularGradiente(funcionAMinimizar, evaluacionAnterior, perturbacion);
        }

        private static void AplicarGradiente(INodo nodoBase, float multiplicador)
        {
            foreach (INodo nodoActual in CaminoDeNodos(nodoBase))
                nodoActual.AplicarGradiente(multiplicador);
        }

        private static IEnumerable<INodo> CaminoDeNodos(INodo nodoBase)
        {
            for (INodo nodoActual = nodoBase; nodoActual != null; nodoActual = nodoActual.NodoSiguiente)
                yield return nodoActual;
        }
    }
}