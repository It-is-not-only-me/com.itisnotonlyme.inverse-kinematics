using System;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacion : Configuracion<Vector3>
    {
        private static int _cantidadDeVariables = 3;

        public ConfiguracionRotacion(Vector3 direccionInicial, ITransformacion<Vector3> transformacion) 
            : base(direccionInicial, transformacion, _cantidadDeVariables)
        {
        }

        protected override float this[int i] { get => _valor[i]; set => _valor[i] = value; }
    }
}
