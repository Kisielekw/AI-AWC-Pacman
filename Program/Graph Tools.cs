using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal class Node
    {
        private static int nextID = 0;

        public Vector2 Position { get; private set; }
        public int ID { get; private set; }

        public Node(Vector2 pPosition)
        {
            Position = pPosition;
            ID = nextID++;
        }
    }

    internal class Edge
    {
        private static int nextID = 0;

        public int FromID { get; private set; }
        public int ToID { get; private set;}
        public int ID { get; private set; }

        public Edge(int pFromID, int pToID)
        {
            FromID = pFromID;
            ToID = pToID;
            ID = nextID++;
        }
    }

    internal class Graph
    {
        private List<Node> nodes;
        private List<Edge> edges;

        public IEnumerable<Node> Nodes { get { return nodes; } }
        public IEnumerable<Edge> Edges { get { return edges; } }
        public int NodeCount { get { return nodes.Count;} }

        public Graph()
        {
            nodes = new List<Node>();
            edges = new List<Edge>();
        }

        public Node GetNode(int pID)
        {
            foreach(Node node in nodes)
            {
                if(node.ID == pID) return node;
            }

            return null;
        }

        public void AddNode(Vector2 pPosition)
        {
            nodes.Add(new Node(pPosition));
        }

        public void RemoveNode(int pID)
        {
            for(int i = 0; i < NodeCount; i++)
            {
                if (nodes[i].ID == pID)
                {
                    for(int j = NodeCount - 1; j >= 0; j--)
                    {
                        if (edges[j].ToID == pID) edges.RemoveAt(j);
                        if (edges[j].FromID == pID) edges.RemoveAt(j);
                    }

                    nodes.RemoveAt(i);
                    return;
                }
            }
        }

        public Edge getEdge(int pID)
        {
            foreach(Edge edge in edges)
            {
                if (edge.ID == pID) return edge;
            }
            return null;
        }

        public void AddEdge(int pFromID, int pToID)
        {
            bool fromExists = false;
            bool toExists = false;

            foreach(Node node in nodes)
            {
                if(node.ID == pFromID) fromExists = true;
                if(node.ID == pToID) toExists = true;
            }

            if(toExists && fromExists) edges.Add(new Edge(pFromID, pToID));
        }

        public void RemoveEdge(int pID)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].ID == pID) 
                {
                    edges.RemoveAt(i);
                    return;
                }
            }
        }

        public float GetEdgeCost(int pID)
        {
            Edge edge = getEdge(pID);

            if (edge == null) return -1;

            Vector2 fromPos = GetNode(edge.FromID).Position;
            Vector2 toPos = GetNode(edge.ToID).Position;

            return (fromPos - toPos).Length();
        }
    }
}
