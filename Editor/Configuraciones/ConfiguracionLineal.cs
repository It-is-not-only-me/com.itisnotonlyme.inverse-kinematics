namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionLineal : Configuracion<float>
    {
        private static int _cantidadDeVariables = 1;

        private float _distancia;

        public ConfiguracionLineal(float distancia, ITransformacion<float> transformacion) 
            : base(transformacion, _cantidadDeVariables)
        {
            _distancia = distancia;
        }

        protected override float this[int i] { get => _distancia; set => _distancia = value; }

        public override void ActualizarEstado(ITransformacion<float> transformacion)
        {
            transformacion.ActualizarEstado(_distancia);
        }
    }
}
