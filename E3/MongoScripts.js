// Modelo de documento (SensorEvent)
{
    "DispositivoID": "sensor_001",
    "FechaHora": "2023-10-05T14:30:00Z",
    "Valor": 25.5
}

// Consulta de agregación para promedio por dispositivo
db.SensorEventos.aggregate([
    {
        $group: {
            _id: "$DispositivoID",
            PromedioValor: { $avg: "$Valor" }
        }
    }
]);