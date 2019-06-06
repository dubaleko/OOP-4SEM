using System;

namespace Lab12
{
    class HouseBuilder : IHouseBuilder
    {
        private string result = String.Empty;

        public void BuildRoof(string material)
        {
            result += " с [" + material + "] крышей,";
        }

        public void BuildWall(string material)
        {
            result += " с [" + material + "] стеной,";
        }

        public void BuildWindow(string material)
        {
            result += " с [" + material + "] окном,";
        }

        public void BuildDoor(string material)
        {
            result += " с [" + material + "] дверью,";
        }

        public string GetResult()
        {
            return "Дом" + result;
        }
    }
}
