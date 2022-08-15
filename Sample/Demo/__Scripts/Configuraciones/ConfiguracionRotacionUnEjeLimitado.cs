using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacionUnEjeLimitado : Configuracion
    {
        private static int _cantidadDeVariables = 1;

        private float _rotacion;
        private float _rotacionMinimo, _rotacionMaximo;

        private Vector3 _ejeDeRotacion, _inicioDelAngulo;

        public ConfiguracionRotacionUnEjeLimitado(float rotacion, float minimo, float maximo, Vector3 ejeDeRotacion, Vector3 inicioDelAngulo)
            : base(_cantidadDeVariables)
        {
            _rotacion = rotacion;
            _rotacionMinimo = minimo;
            _rotacionMaximo = maximo;

            _ejeDeRotacion = ejeDeRotacion;
            _inicioDelAngulo = inicioDelAngulo;
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

        public override IValor Transformar(IValor valor)
        {
            Vector3 direccion = Quaternion.AngleAxis(_rotacion, _ejeDeRotacion) * _inicioDelAngulo;
            return (valor as IValorVector).Rotar(direccion);
        }
    }
}
