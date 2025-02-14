using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    // Clase derivada para humedad
    public class Humidity(string id, double humidity) : Sensor(id, "Humidity")
    {
        public override void DataProcess()
        {
            Console.WriteLine($"Procesando datos de humedad: {humidity}%");
        }
    }
}
