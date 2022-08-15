using System;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface INodo
    {
        public INodo NodoSiguiente { get; }

        public IValor Transladar(IValor valor);

        public void CalcularGradiente(Func<INodo, IValor, IValor, float> funcion, INodo nodoBase, IValor valorInicial, IValor valorObjetivo, float perturbacion);

        public void AplicarGradiente(float multiplicador);
    }
}
