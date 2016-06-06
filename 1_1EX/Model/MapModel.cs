using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_1EX.Model
{
    public class MapModel
    {
        
        double top;
        double left;
        Resurs res;

        public  MapModel() { }

        public MapModel( double t, double l, Resurs r)
        {
            res = r;
            top = t;
            left = l;
           
        }

        public Resurs Res
        {
            get { return res; }
            set { res = value; }
        }

        public double Top
        {
            get { return top; }
            set { top = value; }
        }

        public double Left
        {
            get { return left; }
            set { left = value; }
        }

        

    }
}
