using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class Hueso3D
    {
        private Vector3 _posicion;
        private Quaternion _rotacion;
        private float _longitud;

        private Hueso3D _huesoAnterior;

        public Hueso3D(Vector3 posicion, Quaternion rotacion, float longitud)
        {
            _posicion = posicion;
            _rotacion = rotacion;
            _longitud = longitud;

            _huesoAnterior = null;
        }

        public Hueso3D(Hueso3D huesoAnterior, Quaternion rotacion, float longitud)
        {
            _huesoAnterior = huesoAnterior;
            _rotacion = rotacion;
            _longitud = longitud;
        }

        public Vector3 Posicion { get => (_huesoAnterior == null) ? _posicion : _huesoAnterior.Extremo(); }

        public Quaternion Rotacion { get => (_huesoAnterior == null) ? _rotacion : _huesoAnterior.Rotacion * _rotacion; set => _rotacion = value; }

        public Vector3 Extremo()
        {
            Quaternion rotacion = Rotacion;
            Vector3 direccion = new Vector3(rotacion.y, rotacion.z, rotacion.w);
            return Posicion + direccion * _longitud;
        }
    }
}
