using System.Collections.Generic;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class CalcularErrorDeEstadoActual : IFuncionMinimizar
    {
        private INodo _nodoBase;
        private IValor _valorInicial, _valorObjetivo;

        public CalcularErrorDeEstadoActual(INodo nodoBase, IValor valorInicial, IValor valorObjetivo)
        {
            _nodoBase = nodoBase;
            _valorInicial = valorInicial;
            _valorObjetivo = valorObjetivo;
        }

        public float Evaluar()
        {
            IValor valorObtenido = InverseKinematicSolver.Funcion(_nodoBase, _valorInicial);
            IValor diferencia = _valorObjetivo.Sumar(valorObtenido.Multiplicar(-1f));

            return diferencia.Modulo();
        }
    }
}