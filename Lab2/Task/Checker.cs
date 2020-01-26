using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch.Task
{
    public enum Type { White, Black }
    public class Checker
    {
        public string Name { set; get; }
        public Type Color { set; get; }
        public List<Move> Moves { set; get; }

        public Checker(string name, Type color)
        {
            Name = name;
            Color = color;
            Moves = new List<Move>();
        }

        public Checker Copy()
        {
            return new Checker(Name, Color) { Moves = this.Moves.ToList() };
        }
    }
}
