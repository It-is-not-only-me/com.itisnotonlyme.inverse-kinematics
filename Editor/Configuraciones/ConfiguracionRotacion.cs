using System;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacion : Configuracion<Vector3>
    {
        private static int _cantidadDeVariables = 3;

        private Vector3 _direccion;

        public ConfiguracionRotacion(Vector3 direccionInicial, ITransformacion<Vector3> transformacion) 
            : base(transformacion, _cantidadDeVariables)
        {
            _direccion = direccionInicial;
        }

        protected override float this[int i] { get => _direccion[i]; set => _direccion[i] = value; }

        public override void ActualizarEstado(ITransformacion<Vector3> transformacion)
        {
            transformacion.ActualizarEstado(_direccion);
        }
    }
}
