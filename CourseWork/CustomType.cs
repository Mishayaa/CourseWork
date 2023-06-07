    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace CourseWork
    {
       public class CustomType
        {
            private List<long> data;

            public CustomType()
            {
                data = new List<long>();
            }

            public CustomType(List<long> list)
            {
                data = new List<long>(list);
            }

            public void Add(long item)
            {
                data.Add(item);
            }

            public void SortDescending()
            {
                data.Sort((a, b) => b.CompareTo(a));
            }

            public override string ToString()
            {
                string result = "";
                foreach (long item in data)
                {
                    result += item.ToString() + " ";
                }
                return result.TrimEnd();
            }
        }
    }
