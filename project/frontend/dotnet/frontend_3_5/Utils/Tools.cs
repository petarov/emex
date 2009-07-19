using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace frontend_3_5.Utils
{
    class Tools
    {
        private static Random rnd = null;

        public static List<Hashtable> shuffle(List<Hashtable> list)
        {
            if (list.Count < 1)
                return list;

            List<Hashtable> newList = new List<Hashtable>(list.Count + 1);

            if ( rnd == null )
                rnd = new Random((int)DateTime.Now.Ticks);

            int index = 0;
            while (list.Count > 0)
            {
                index = rnd.Next(0, list.Count);
                newList.Add(list[index]);
                list.RemoveAt(index);
            }

            list.Clear();
            list = null;

            return newList;
        }
    }
}
