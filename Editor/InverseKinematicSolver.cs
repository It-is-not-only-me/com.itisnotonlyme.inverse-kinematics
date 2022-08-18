using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class InverseKinematics
    {
        private float _perturbacion, _multiplicador, _errorMinimo;
        private int _cantidadIteraciones;

        public InverseKinematics(float perturbacion, float multiplicador, int cantidadIteraciones, float errorMinimo)
        {
            _perturbacion = perturbacion;
            _multiplicador = multiplicador;
            _cantidadIteraciones = cantidadIteraciones;
            _errorMinimo = errorMinimo;
        }

        public int Aplicar(INodo nodoBase, IValor valorInicial, IValor valorObjetivo)
        {
            return InverseKinematicSolver.Aplicar(nodoBase, valorInicial, valorObjetivo, _perturbacion, _multiplicador, _cantidadIteraciones, _errorMinimo);
        }
    }

    public static class InverseKinematicSolver
    {
        public static int Aplicar(INodo nodoBase, IValor valorInicial, IValor valorObjetivo, float perturbacion, float multiplicador, int cantidadIteraciones, float errorMinimo)
        {
            CalcularErrorDeEstadoActual funcionAMinimizar = new CalcularErrorDeEstadoActual(nodoBase, valorInicial, valorObjetivo);
            int numeroDeIteraciones = 0;

            float resultadoAnterior = funcionAMinimizar.Evaluar();

            if (resultadoAnterior < errorMinimo)
                return numeroDeIteraciones;

            Iterar(funcionAMinimizar, nodoBase, resultadoAnterior, perturbacion, multiplicador);
            resultadoAnterior = funcionAMinimizar.Evaluar();
            numeroDeIteraciones++;

            for (; numeroDeIteraciones < cantidadIteraciones && resultadoAnterior > errorMinimo; numeroDeIteraciones++)
            {
                Iterar(funcionAMinimizar, nodoBase, resultadoAnterior, perturbacion, multiplicador);
                resultadoAnterior = funcionAMinimizar.Evaluar();
            }

            return numeroDeIteraciones;
        }

        private static void Iterar(IFuncionMinimizar funcionAMinimizar, INodo nodoBase, float evaluacionAnterior, float perturbacion, float mulpliciador)
        {
            CalcularGradiente(funcionAMinimizar, nodoBase, evaluacionAnterior, perturbacion);
            AplicarGradiente(nodoBase, mulpliciador);
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