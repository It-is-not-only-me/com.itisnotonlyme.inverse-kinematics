using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class Hueso2D
    {
        private Vector2 _posicion;
        private float _rotacion;
        private float _longitud;

        Hueso2D _huesoAnterior;

        public Hueso2D(Vector2 posicion, float rotacion, float longitud)
        {
            _posicion = posicion;
            _rotacion = rotacion;
            _longitud = longitud;

            _huesoAnterior = null;
        }

        public Hueso2D(Hueso2D huesoAnterior, float rotacion, float longitud)
        {
            _huesoAnterior = huesoAnterior;
            _rotacion = rotacion;
            _longitud = longitud;
        }

        public Vector2 Posicion { get => (_huesoAnterior == null) ? _posicion : _huesoAnterior.Extremo(); }

        public float Rotacion { get => (_huesoAnterior == null) ? _rotacion : _huesoAnterior.Rotacion + _rotacion; set => _rotacion = value; }

        public Vector2 Extremo()
        {
            float rotacion = Rotacion;
            Vector2 direccion = new Vector2(Mathf.Cos(rotacion), Mathf.Sin(rotacion));
            return Posicion + direccion * _longitud;
        }
    }
}
