using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch.Task
{
    public class Move
    {
        public Cell First { set; get; }
        public Cell Second { set; get; }

        public Checker Checker
        {
            set
            {
                if (First.Content != null) First.Content = value;
                else Second.Content = value;
            }
            get
            {
                if (First.Content != null) return First.Content;
                return Second.Content;
            }
        }

        public Move(Cell first, Cell second)
        {
            First = first;
            Second = second;
        }

        public Cell NextVertex(Cell cell)
        {
            if (cell.Name == First.Name) return Second;
            return First;
        }

        public bool HasCell(Cell cell)
        {
            return cell.Name == First.Name || cell.Name == Second.Name;
        }

        public void Transfer()
        {
            Checker ch = Checker;
            ch.Moves.Add(this);
            Checker checker = First.Content;
            First.Content = Second.Content;
            Second.Content = checker;
        }

        public void RollBack()
        {
            Checker checker = First.Content;
            First.Content = Second.Content;
            Second.Content = checker;
            Checker.Moves.RemoveAt(Checker.Moves.Count - 1);
        }

        public override string ToString()
        {
            Cell second = First.Content == null ? First : Second;
            Cell first = First.Content == null ? Second : First;
            return Checker.Name + ": " + first.Name + "->" + second.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Move)
            {
                var move = obj as Move;
                return move.HasCell(First) && move.HasCell(Second);
            }
            return false;
        }
    }
}
