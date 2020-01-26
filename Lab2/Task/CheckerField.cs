using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch.Task
{
    public class CheckerField
    {
        #region Init Checkers
        Checker w1 = new Checker("w1", Type.White);
        Checker w2 = new Checker("w2", Type.White);
        Checker w3 = new Checker("w3", Type.White);

        Checker b1 = new Checker("b1", Type.Black);
        Checker b2 = new Checker("b2", Type.Black);
        Checker b3 = new Checker("b3", Type.Black);

        List<Checker> checkers; 
        #endregion

        #region Cells
        public Cell A1 { set; get; }
        public Cell A2 { set; get; }

        public Cell B1 { set; get; }
        public Cell B2 { set; get; }
        public Cell B3 { set; get; }
        
        public Cell C2 { set; get; }
        public Cell C3 { set; get; }

        public List<Cell> Cells { set; get; }
        #endregion

        public List<Move> Moves { set; get; }

        public string GetMoves
        {
            get => $"[{string.Join(", ", Moves)}]";
        }

        public List<Move> Path { set; get; }

        public CheckerField()
        {
            checkers = new List<Checker> { w1, w2, w3, b1, b2, b3 };
            #region Init Cells
            A1 = new Cell("A1");
            A2 = new Cell("A2");

            B1 = new Cell("B1");
            B2 = new Cell("B2");
            B3 = new Cell("B3");
            
            C2 = new Cell("C2");
            C3 = new Cell("C3");

            Cells = new List<Cell>() { A1, A2, B1, B2, B3, C2, C3};
            #endregion

            #region Init Moves
            Moves = new List<Move>();
            Moves.Add(new Move(A1, A2));
            Moves.Add(new Move(A1, B1));

            Moves.Add(new Move(A2, B2));
            Moves.Add(new Move(A2, C2));

            Moves.Add(new Move(B1, B2));
            Moves.Add(new Move(B1, B3));

            Moves.Add(new Move(B2, C2));
            Moves.Add(new Move(B3, C3));

            Moves.Add(new Move(C2, C3));
            #endregion

        }

        public void InitStartFieldCells()
        {
            A1.Content = b1;
            A2.Content = b2;

            B1.Content = b3;
            B2.Content = null;

            B3.Content = w1;

            C2.Content = w2;
            C3.Content = w3;

            foreach (var checker in checkers)
            {
                checker.Moves.Clear();
            }
        }

        public void InitEndFieldCells()
        {
            A1.Content = w1;
            A2.Content = w2;

            B1.Content = w3;
            B2.Content = null;

            B3.Content = b1;

            C2.Content = b2;
            C3.Content = b3;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is CheckerField)
            {
                var field = obj as CheckerField;
                for (int i = 0; i < Cells.Count; i++)
                {
                    if (Cells[i].Content == null && field.Cells[i].Content == null) continue;
                    if (Cells[i].Content == null || field.Cells[i].Content == null) return false;
                    if (Cells[i].Content.Color != field.Cells[i].Content.Color) return false;
                }
            }
            return true;
        }


        public void InitPath()
        {
            Path = new List<Move>();
        }
        
    }
}
