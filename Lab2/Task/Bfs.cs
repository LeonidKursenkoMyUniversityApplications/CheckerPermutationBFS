using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BreadthFirstSearch.Task
{
    public class Bfs
    {
        public CheckerField StartField { set; get; }
        public CheckerField FinalField { set; get; }
        public List<string> PathHistory { set; get; }
        public List<Move> Path { set; get; }
        public List<List<Move>> AllPathes { set; get; }

        public string GetPath
        {
            get { return string.Join(",\n", PathHistory); }
        }

        public void Run()
        {
            StartField = new CheckerField();
            StartField.InitStartFieldCells();

            FinalField = new CheckerField();
            FinalField.InitEndFieldCells();

            Path = new List<Move>();
            PathHistory = new List<string>();
            AllPathes = new List<List<Move>>();
        }

        private int counter = 0;
        public void Step()
        {
            //if (StartField.Equals(FinalField)) return;
            Cell freeCell = StartField.Cells.First(x => x.Content == null);
            List<Move> availableMoves = StartField.Moves.Where(x => x.HasCell(freeCell)).ToList();
            AllPathes = availableMoves.Select(x => new List<Move> {x}).ToList();

            while (AllPathes.Count > 0)
            {
                Path = AllPathes[0];
                AllPathes.RemoveAt(0);
                InitPath();
                if (StartField.Equals(FinalField)) return;
                freeCell = StartField.Cells.First(x => x.Content == null);
                availableMoves = StartField.Moves.Where(x => x.HasCell(freeCell)).ToList();
                var moves = new List<Move>();
                foreach (var move in availableMoves)
                {
                    if (move.Checker.Moves.Exists(x => x.Equals(move))) continue;
                    moves.Add(move);
                }
                availableMoves = moves;
                AllPathes.AddRange(availableMoves.Select(x =>
                {
                    var path = Path.ToList();
                    path.Add(x);
                    return path;
                }).ToList());
            }

            Path.Clear();
            AllPathes.Clear();
        }


        public void Step0()
        {
            if (StartField.Equals(FinalField)) return;
            Cell freeCell = StartField.Cells.First(x => x.Content == null);
            List<Move> availableMoves = StartField.Moves.Where(x => x.HasCell(freeCell)).ToList();
            foreach (var move in availableMoves)
            {
                if(move.Checker.Moves.Exists(x => x.Equals(move))) continue;
                Path.Add(move);
                move.Transfer();

                if (StartField.Equals(FinalField)) return;
                #region Rollback
                if (Path.Count > 15)
                {
                    Path.RemoveAt(Path.Count - 1);
                    move.RollBack();
                    return;
                }
                #endregion
                Step0();
                if (StartField.Equals(FinalField)) return;
                Path.RemoveAt(Path.Count - 1);
                move.RollBack();
            }
        }

        public void InitPath()
        {
            //StartField = new CheckerField();
            StartField.InitStartFieldCells();
            //StartField.InitPath();
            //Path = StartField.Path;
            foreach (var move in Path)
            {
                move.Transfer();
            }
        }
    }
}
