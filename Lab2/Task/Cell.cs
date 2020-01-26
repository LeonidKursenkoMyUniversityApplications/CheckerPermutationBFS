using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch.Task
{
    public class Cell
    {
        public string Name { set; get; }
        public Checker Content { set; get; }

        public Cell(string name, Checker content)
        {
            Name = name;
            Content = content;
        }

        public Cell(string name)
        {
            Name = name;
        }
    }
}
