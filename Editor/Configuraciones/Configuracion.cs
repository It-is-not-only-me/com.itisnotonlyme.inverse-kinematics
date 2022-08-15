using System;
using System.Collections.Generic;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public abstract class Configuracion<TTipo> : IConfiguracion
    {
        private int _cantidadDeVariables;
        private float[] _gradiente;

        private ITransformacion<TTipo> _transformacion;

        public Configuracion(ITransformacion<TTipo> transformacion, int cantidadDeVariables)
        {
            _transformacion = transformacion;

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
                float valorVariableActual = this[numeroVariable];
                this[numeroVariable] += perturbacion;
                float evaluacionActual = funcionAMinimizar.Evaluar();
                this[numeroVariable] = valorVariableActual;

                _gradiente[numeroVariable] = (evaluacionActual - evaluacionAnterior) / perturbacion;
            }
        }

        public void AplicarGradiente(float multiplicador)
        {
            foreach (int numeroVariable in _numeroVariables)
                this[numeroVariable] -= multiplicador * _gradiente[numeroVariable];
        }

        public IValor Transformar(IValor valor)
        {
            ActualizarEstado(_transformacion);
            return _transformacion.Transformar(valor);
        }

        public abstract void ActualizarEstado(ITransformacion<TTipo> transformacion);
    }
}
