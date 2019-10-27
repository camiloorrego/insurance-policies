using System;

namespace Insurance.Policies.Infraestructure.Factories
{
    public static class ExpresionFactory
    {
        public static Func<int, bool> Build(string type, int val)
        {
            Func<int, bool> v = null;

            switch (type)
            {
                case ">":
                    v = x => x > val;
                    break;


            }
            return v;
        }


    }
}
