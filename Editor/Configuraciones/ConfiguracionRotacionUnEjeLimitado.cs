using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacionUnEjeLimitado : Configuracion<float>
    {
        private static int _cantidadDeVariables = 1;

        private float _rotacion;
        private float _rotacionMinimo, _rotacionMaximo;

        public ConfiguracionRotacionUnEjeLimitado(float rotacion, float minimo, float maximo, ITransformacion<float> transformacion)
            : base(transformacion, _cantidadDeVariables)
        {
            _rotacion = rotacion;
            _rotacionMinimo = minimo;
            _rotacionMaximo = maximo;
        }

        protected override float this[int i]
        {
            get => _rotacion;
            set
            {
                float limitadoPorMaximo = Mathf.Min(value, _rotacionMaximo);
                _rotacion = Mathf.Max(limitadoPorMaximo, _rotacionMinimo);
            }
        }

        public override void ActualizarEstado(ITransformacion<float> transformacion)
        {
            transformacion.ActualizarEstado(_rotacion);
        }
    }
}
