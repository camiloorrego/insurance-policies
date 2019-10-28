using System;

namespace Insurance.Policies.Domain.Factories
{
    public static class RulesFactory
    {
        public static Func<decimal, bool> Build(string type, decimal val)
        {
            Func<decimal, bool> v = null;

            switch (type)
            {
                case ">":
                    v = x => x > val;
                    break;
                case "<":
                    v = x => x < val;
                    break;


            }
            return v;
        }


    }
}
