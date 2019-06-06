// предоставлет интерфейс для создания семейств взаимосвязанных или взаимозависимых объектов, не специфицируя их конкретных классов
namespace Lab12
{
    interface IGeometryFactory
    {
        IGeometryObject CreateGeometryObject();
    }
}
