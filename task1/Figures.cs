using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    public struct BoundingBox
    {
        public Vector3 MinimumPoint { get; private set; }
        public Vector3 MaximumPoint { get; private set; }

        public BoundingBox(Vector3 minimum, Vector3 maximum)
        {
            MinimumPoint = minimum;
            MaximumPoint = maximum;
        }

        public override string ToString()
        {
            return $"BoundingBox: Minimum in {MinimumPoint}, Maximum in {MaximumPoint}";
        }
    }
    public abstract class Figure
    {
        public Vector3 position { get; set; }
        private Figure[] childrens;
        public Figure parent;
        private static int licznik = 0;
        public readonly int id;

        public Figure(Vector3 pozycja, int liczbael = 2)
        {
            id = licznik++;
            this.position = pozycja;
            childrens = new Figure[liczbael];

        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public int GetMaxNumberOfChildren()
        {
            return childrens.Length;
        }
        public void AddChild(Figure child) 
        {
            if(child == null)
            {
                throw new ArgumentNullException("Child figure cannot be null.");
            }
            if(childrens.Length == GetChildCount())
            {
                Array.Resize(ref childrens, childrens.Length * 2);
            }
            childrens[GetNextAvailableChildIndex()] = child;
            child.setParent(this);
        }
        public void setParent(Figure parent)
        {
            this.parent = parent;
        }
        private int GetNextAvailableChildIndex()
        {
            for (int i = 0; i < childrens.Length; i++)
            {
                if (childrens[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
        public string GetTreeString(int depth = 0)
        {
            string result = new string(' ', depth * 2) + this.ToString() + Environment.NewLine;
            foreach (var child in childrens)
            {
                if (child != null)
                    result += child.GetTreeString(depth + 1);
            }
            return result;
        }

        public int GetChildCount()
        {
            int count = 0;
            foreach (var child in childrens)
            {
                if (child != null)
                    count++;
            }
            return count;
        }

        public void Move(Vector3 wektor)
        {
            position = Vector3.Add(position, wektor);

        }

        public Vector3 GetCartesianSpaceCoordinates()
        {
            if(parent == null)
            {
                return position;
            }
            else
            {
                return Vector3.Add(position, parent.GetCartesianSpaceCoordinates());
            }
            
        }
    }
    class Sphere: Figure
    {
        private float radius;

        public Sphere(Vector3 pozycja ,float radius): base(pozycja) 
        {
            this.radius = radius;
        }

        public override string ToString()
        {
            Vector3 cartesianCoordinates = GetCartesianSpaceCoordinates();
            return $"Sphere ID {id}: Position in {cartesianCoordinates}, Radius={radius}";

        }

    }

    class Cylinder: Figure
    {
        private float radius;
        private float height;
        public Cylinder(Vector3 pozycja, float radius, float height) : base(pozycja)
        {
            this.radius = radius;
            this.height = height;
        }

        public override string ToString()
        {
            Vector3 cartesianCoordinates = GetCartesianSpaceCoordinates();
            return $"Cylinder ID {id}: Position in {cartesianCoordinates}, Radius={radius}, Height={height}";
        }

    }
}
