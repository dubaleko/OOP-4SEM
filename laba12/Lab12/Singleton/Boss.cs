using System;

namespace Lab12
{
    [Serializable]
    public class Boss : Prototype<Boss>
    {
        private static Boss boss;

        private Boss() { }

        public static Boss GetInstance()
        {
            return boss ?? (boss = new Boss());
        }
    }
}
