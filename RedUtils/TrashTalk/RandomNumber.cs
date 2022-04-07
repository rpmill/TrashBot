using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedUtils.TrashTalk
{
    class RandomNumber
    {
        private static readonly Random _random = new Random();

        public static int GetRandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
