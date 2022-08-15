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

            if (resultadoAnterior < errorMinimo)
                return;

            Iterar(funcionAMinimizar, nodoBase, resultadoAnterior, perturbacion, multiplicador);

            float resultadoActual = funcionAMinimizar.Evaluar();
            float errorActual = CalculoDeError(resultadoAnterior, resultadoActual);
            Debug.Log(errorActual);

            int iteracion;
            for (iteracion = 1; iteracion < cantidadIteraciones &&  errorActual > errorMinimo; iteracion++)
            {
                resultadoAnterior = resultadoActual;

                Iterar(funcionAMinimizar, nodoBase, resultadoAnterior, perturbacion, multiplicador);

                resultadoActual = funcionAMinimizar.Evaluar();

                errorActual = CalculoDeError(resultadoAnterior, resultadoActual);
            }

            Debug.Log(iteracion);
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

        public static IValor Funcion(INodo nodoBase, IValor valorInicial)
        {
            IValor valorResultado = valorInicial;

            foreach (INodo nodoActual in CaminoDeNodos(nodoBase))
                valorResultado = nodoActual.Transformar(valorResultado);

            return valorResultado;
        }

        private static IEnumerable<INodo> CaminoDeNodos(INodo nodoBase)
        {
            for (INodo nodoActual = nodoBase; nodoActual != null; nodoActual = nodoActual.NodoSiguiente)
                yield return nodoActual;
        }
    }
}