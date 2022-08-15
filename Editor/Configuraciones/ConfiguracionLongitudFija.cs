using System;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionLongitudFija : Configuracion<float>
    {
        private static int _cantidadDeVariables = 0;

        private float _longitud;

        public ConfiguracionLongitudFija(float longitud, ITransformacion<float> transformacion)
            : base(transformacion, _cantidadDeVariables)
        {
            _longitud = longitud;
        }

        protected override float this[int i] { get => throw new ArgumentOutOfRangeException(); set => throw new ArgumentOutOfRangeException(); }

        public override void ActualizarEstado(ITransformacion<float> transformacion)
        {
            transformacion.ActualizarEstado(_longitud);
        }
    }
}
