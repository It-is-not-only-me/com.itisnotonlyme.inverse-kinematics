namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionLineal : Configuracion
    {
        private static int _cantidadDeVariables = 1;

        private float _distancia;

        public ConfiguracionLineal(float distancia) 
            : base(_cantidadDeVariables)
        {
            _distancia = distancia;
        }

        protected override float this[int i] { get => _distancia; set => _distancia = value; }

        public override IValor Transformar(IValor valor)
        {
            return (valor as IValorVector).Extender(_distancia);
        }
    }
}
