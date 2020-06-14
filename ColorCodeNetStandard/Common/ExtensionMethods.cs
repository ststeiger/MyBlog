// Copyright (c) Microsoft Corporation.  All rights reserved.


namespace ColorCode.Common
{


    public static class ExtensionMethods
    {
        public static void SortStable<T>(this System.Collections.Generic.IList<T> list,
                                         System.Comparison<T> comparison)
        {
            Guard.ArgNotNull(list, "list");

            int count = list.Count;

            for (int j = 1; j < count; j++)
            {
                T key = list[j];

                int i = j - 1;
                for (; i >= 0 && comparison(list[i], key) > 0; i--)
                {
                    list[i + 1] = list[i];
                }

                list[i + 1] = key;
            }
        }


    }


}
