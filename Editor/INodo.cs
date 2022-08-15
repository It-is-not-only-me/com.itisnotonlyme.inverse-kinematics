using System;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface INodo
    {
        public INodo NodoSiguiente { get; }

        public IValor Transformar(IValor valor);

        public void CalcularGradiente(IFuncionMinimizar funcionAMinimizar, float evaluacionAnterior, float perturbacion);

        public void AplicarGradiente(float multiplicador);
    }
}
