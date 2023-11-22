using System;
using System.Drawing;
using System.Xml.Linq;

namespace Lab06_PL
{
    class Set
    {
        private int[] tab;
        private int size;

        public Set(params int[] args)
        {
            tab = new int[args.Length];
            size = 0;
            foreach (int arg in args)
            {
                if (!Contains(arg))
                {
                    tab[size] = arg;
                    size++;
                }
            }
        }

        private bool Contains(int element)
        {
            for (int i = 0; i < size; i++)
            {
                if (tab[i] == element)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            string result = "{";
            for(int i = 0;i < size; i++)
            {
                result += tab[i].ToString();
                result += ",";
            }
            result += "}";
            return result;
        }

        public int GetElementsArrayCapacity()
        {
             return size;
        }

        public static Set operator + (Set a, Set b)
        {
            Set result = new Set(a.GetElements());
            foreach(int i in b.GetElements())
            {
                result.Add(i);
            }
            return result;   
        }
        public static Set operator -(Set a, Set b)
        {
            Set result = new Set(a.GetElements());
            foreach (int i in b.GetElements())
            {
                result.Remove(i);
            }
            return result;
        }

        public void Add(int element)
        {
            if (!Contains(element))
            {
                if (size == tab.Length)
                {
                    Array.Resize(ref tab, size * 2);
                }
                tab[size] = element;
                size++;
            }
        }

        public int[] GetElements()
        {
            int[] result = new int[size];
            Array.Copy(tab, result, size);
            return result;
        }

        public void Remove(int element)
        {
            for (int i = 0; i < size; i++)
            {
                if (tab[i] == element)
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        tab[j] = tab[j + 1];
                    }
                    size--;
                    break;
                }
            }
        }
        public bool IsEmpty()
        {
            return size == 0;
        }

        public static bool operator ==(Set A, Set B)
        {
            if (A.size != B.size) return false;
            foreach (int arg in A.GetElements())
            {
                if (!B.Contains(arg))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Set A, Set B)
        {
            return !(A == B);
        }

        public static bool operator true(Set x)
        {
            return x.GetElementsArrayCapacity() > 0;
        }
        public static bool operator false(Set x)
        {
            return x.GetElementsArrayCapacity() <= 0;
        }
        public static Set operator &(Set a, Set b)
        {
            Set result = a + b - (a - b) - (b - a);

            return result;
        }
        public static Set operator |(Set a, Set b)
        {
            return (a.GetElementsArrayCapacity() > 0 || b.GetElementsArrayCapacity() > 0) ? new Set(1) : new Set();
        }

    }
}
