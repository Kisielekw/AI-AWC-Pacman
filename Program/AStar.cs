using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class NodePath : IComparable<NodePath>
    {
        public NodePath LastNode { get; private set; }
        public Vector2 Position { get; private set; }
        public int StepsToNode { get; private set; }

        private float heuristic;

        public NodePath(Vector2 pPosition, int pSteps, float pHeuristic, NodePath pLastNode = null)
        {
            Position = pPosition;
            LastNode = pLastNode;
            StepsToNode = pSteps;
            heuristic = pHeuristic;
        }

        public int CompareTo(NodePath other)
        {
            float score = StepsToNode + heuristic;
            float otherScore = other.StepsToNode + other.heuristic;

            if (otherScore > score) return -1;
            else if (otherScore < score) return 1;
            else return 0;
        }
    }

    internal class AStar
    {
        public Vector2[] Path 
        {
            get;
            private set;
        }

        public void CreatePath(Vector2 pFrom, Vector2 pTo, List<Wall> pWalls)
        {
            if(!CheckCorectValues(pFrom, pTo, pWalls))
            {
                throw new Exception("Values entered are invalid");
            }

            if(pFrom == pTo)
            {
                Path = new Vector2[1] { pTo };
                return;
            }

            bool isFinished = false;
            List<NodePath> queue = new List<NodePath>();
            List<Vector2> visitedNodes = new List<Vector2>();

            float length = (pTo - pFrom).Length(); 
            queue.Add(new NodePath(pFrom, 0, length));

            while(!isFinished)
            {
                queue.Sort();
                NodePath currentNode = queue[0];
                queue.RemoveAt(0);

                if(currentNode.Position == pTo)
                {
                    CreatePath(currentNode);
                    isFinished = true;
                    continue;
                }

                List<Vector2> potentialNodes = new List<Vector2>(4)
                {
                    currentNode.Position + new Vector2(50, 0),
                    currentNode.Position + new Vector2(-50, 0),
                    currentNode.Position + new Vector2(0, 50),
                    currentNode.Position + new Vector2(0, -50)
                };

                foreach(Vector2 visited in visitedNodes)
                {
                    if (potentialNodes.Contains(visited))
                    {
                        potentialNodes.Remove(visited);
                    }
                }

                foreach(Wall wall in pWalls)
                {
                    if (potentialNodes.Contains(wall.Position))
                    {
                        potentialNodes.Remove(wall.Position);
                    }
                }

                foreach(Vector2 newPos in potentialNodes)
                {
                    length = (pTo - newPos).Length();
                    queue.Add(new NodePath(newPos, currentNode.StepsToNode + 1, length, currentNode));
                }

                visitedNodes.Add(currentNode.Position);
            }
        }

        public static bool CheckCorectValues(Vector2 pFrom, Vector2 pTo, List<Wall> pWalls)
        {
            if(pFrom.X > 925 || pFrom.X < 25 || pTo.X > 875 || pTo.X < 75) return false;
            if (pFrom.Y > 875 || pFrom.Y < 75 || pTo.Y > 875 || pTo.Y < 75) return false;

            int x = (int)pFrom.X + 25;
            int y = (int)pFrom.Y + 25;
            if(x % 50 != 0 || y % 50 != 0) return false;

            x = (int)pTo.X + 25;
            y = (int)pTo.Y + 25;
            if (x % 50 != 0 || y % 50 != 0) return false;

            foreach(Wall wall in pWalls)
            {
                if(wall.Position == pTo) return false;
            }

            return true;
        }

        private void CreatePath(NodePath pNode)
        {
            List<Vector2> path = new List<Vector2>();
            while (true)
            {
                if(pNode == null) break;
                path.Add(pNode.Position);
                pNode = pNode.LastNode;
            }

            Path = new Vector2[path.Count];
            int count = 0;
            for(int i = path.Count - 1; i >= 0; i--)
            {
                Path[count] = path[i];
                count++;
            }
            return false;
        }
    }
}
