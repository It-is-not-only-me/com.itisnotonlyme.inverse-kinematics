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
            IValor valorObtenido = Funcion(_nodoBase, _valorInicial);
            IValor diferencia = _valorObjetivo.Sumar(valorObtenido.Multiplicar(-1f));

            return diferencia.Modulo();
        }

        private IValor Funcion(INodo nodoBase, IValor valorInicial)
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