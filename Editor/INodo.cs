namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface INodo
    {
        public INodo NodoSiguiente { get; }

        public IConfiguracion[] Configuraciones { get; }
        public IValor Transladar(IValor valor);
    }
}
