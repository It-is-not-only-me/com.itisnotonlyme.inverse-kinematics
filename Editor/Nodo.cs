namespace ItIsNotOnlyMe.InverseKinematics
{
    public class Nodo : INodo
    {
        private IConfiguracion[] _configuraciones;
        private INodo _nodoSiguiente;

        public Nodo(IConfiguracion[] configuraciones, INodo nodoSiguiente = null)
        {
            _configuraciones = configuraciones;
            _nodoSiguiente = nodoSiguiente;
        }

        public INodo NodoSiguiente => _nodoSiguiente;

        public void CalcularGradiente(IFuncionMinimizar funcionAMinimizar, float evaluacionAnterior, float perturbacion)
        {
            foreach (IConfiguracion configuracion in _configuraciones)
                configuracion.GuardarGradiente(funcionAMinimizar, evaluacionAnterior, perturbacion);
        }

        public void AplicarGradiente(float multiplicador)
        {
            foreach (IConfiguracion configuracion in _configuraciones)
                configuracion.AplicarGradiente(multiplicador);
        }

        public IValor Transformar(IValor valor)
        {
            IValor valorResultado = valor;

            foreach (IConfiguracion configuracion in _configuraciones)
                valorResultado = configuracion.Transformar(valorResultado);

            return valorResultado;
        }
    }
}
