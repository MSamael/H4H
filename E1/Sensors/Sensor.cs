using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    // Clase base abstracta
    public abstract class Sensor
    {
        // Propiedades encapsuladas
        public string Id { get; private set; }
        public string Type { get; private set; }

        // Constructor protegido para encapsulación
        protected Sensor(string id, string type) 
        {
            Id = id;
            Type = type;
        }

        // Método abstracto (obliga a las clases derivadas a implementarlo)
        public abstract void DataProcess();
    }
}
