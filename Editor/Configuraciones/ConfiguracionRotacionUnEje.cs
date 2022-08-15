namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacionUnEje : Configuracion<float>
    {
        private static int _cantidadDeVariables = 1;

        private float _rotacion;

        public ConfiguracionRotacionUnEje(float rotacion, ITransformacion<float> transformacion) 
            : base(transformacion, _cantidadDeVariables)
        {
            _rotacion = rotacion;
        }

        protected override float this[int i] { get => _rotacion; set => _rotacion = value; }

        public override void ActualizarEstado(ITransformacion<float> transformacion)
        {
            transformacion.ActualizarEstado(_rotacion);
        }
    }
}
