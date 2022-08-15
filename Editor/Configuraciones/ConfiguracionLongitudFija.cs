using System;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionLongitudFija : Configuracion<float>
    {
        private static int _cantidadDeVariables = 0;

        public ConfiguracionLongitudFija(float longitud, ITransformacion<float> transformacion)
            : base(longitud, transformacion, _cantidadDeVariables)
        {
        }

        protected override float this[int i] { get => throw new ArgumentOutOfRangeException(); set => throw new ArgumentOutOfRangeException(); }
    }
}
