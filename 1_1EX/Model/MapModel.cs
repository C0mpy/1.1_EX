using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_1EX.Model
{
    public class MapModel
    {
        string resource_id;
        double top;
        double left;
        string img_path;



        public  MapModel() { }

        public MapModel(string id, double t, double l, string path)
        {
            resource_id = id;
            top = t;
            left = l;
            img_path = path;
        }

        public string Resource_id
        {
            get { return resource_id; }
            set { resource_id = value; }
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

        public string Img_path
        {
            get { return img_path; }
            set { img_path = value; }
        }

    }
}
