namespace Lab12
{
    class HouseDirector
    {
        private readonly IHouseBuilder builder;

        public HouseDirector(HouseBuilder builder)
        {
            this.builder = builder;
        }

        public string CreateHouse(string roof, string wall, string window, string door)
        {
            builder.BuildRoof(roof);
            builder.BuildWall(wall);
            builder.BuildWindow(window);
            builder.BuildDoor(door);
            return builder.GetResult();
        }
    }
}
