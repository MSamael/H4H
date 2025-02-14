using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    // Clase derivada para temperatura
    public class Temperature(string id, double temperature) : Sensor(id, "Temperature")
    {
        public override void DataProcess()
        {
            Console.WriteLine($"Procesando datos de temperatura {temperature}°C");
        }
    }
}
