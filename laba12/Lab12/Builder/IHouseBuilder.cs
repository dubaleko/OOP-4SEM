// определяет процесс поэтапного конструирования сложного объекта, в рез-те к-го могут получаться разные представления этого объекта
namespace Lab12
{
    interface IHouseBuilder
    {
        void BuildRoof(string material);
        void BuildWall(string material);
        void BuildWindow(string material);
        void BuildDoor(string material);
        string GetResult();
    }
}
