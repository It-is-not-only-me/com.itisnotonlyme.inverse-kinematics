using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public abstract class Configuracion : IConfiguracion
    {
        private int _cantidadDeVariables;
        private float[] _gradiente;

        public Configuracion(int cantidadDeVariables)
        {
            _cantidadDeVariables = cantidadDeVariables;
            _gradiente = new float[cantidadDeVariables];
        }

        protected abstract float this[int i] { get; set; }

        private IEnumerable<int> _numeroVariables
        {
            get
            {
                for (int numeroVariable = 0; numeroVariable < _cantidadDeVariables; numeroVariable++)
                    yield return numeroVariable;
            }
        }

        public void GuardarGradiente(IFuncionMinimizar funcionAMinimizar, float evaluacionAnterior, float perturbacion)
        {
            foreach (int numeroVariable in _numeroVariables)
            {
                float derivadaParcial = DerivadaParcial(funcionAMinimizar, evaluacionAnterior, perturbacion, numeroVariable);
                Debug.Log("La derivada parcial es: " + derivadaParcial);
                _gradiente[numeroVariable] = derivadaParcial;
            }
        }

        private float DerivadaParcial(IFuncionMinimizar funcionAMinimizar, float evaluacionAnterior, float perturbacion, int numeroVariable)
        {
            float valorVariableActual = this[numeroVariable];
            this[numeroVariable] += perturbacion;
            float evaluacionActual = funcionAMinimizar.Evaluar();
            this[numeroVariable] = valorVariableActual;

            return (evaluacionActual - evaluacionAnterior) / perturbacion;
        }

        public void AplicarGradiente(float multiplicador)
        {
            foreach (int numeroVariable in _numeroVariables)
                this[numeroVariable] -= multiplicador * _gradiente[numeroVariable];
        }

        public abstract IValor Transformar(IValor valor);
    }
}
