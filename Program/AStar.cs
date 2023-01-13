using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal class AStar
    {
        internal class NodeInfo : IComparable<NodeInfo>
        {
            public float Heuristic { get; private set; }
            public float LowestCostToNode { get; set; }
            public int ID { get; private set; }

            public NodeInfo(int pID, float pHeuristic, float pCostToNode)
            {
                ID = pID;
                Heuristic = pHeuristic;
                LowestCostToNode = pCostToNode;
            }

            public int CompareTo(NodeInfo other)
            {
                if (this.Heuristic + this.LowestCostToNode > other.Heuristic + other.LowestCostToNode) return -1;
                else if (this.Heuristic + this.LowestCostToNode < other.Heuristic + other.LowestCostToNode) return 1;
                return 0;
            }
        }

        private List<NodeInfo> visitedNodes;
        private List<NodeInfo> nodeQueue;
        private List<Edge> shortestPathTree;

        private Graph graph;
        
        public int From { get; private set; }
        public int To { get; private set; }
        public bool isFinished { get; private set; }
        public List<Edge> ShortestPath { get { return shortestPathTree; } }

        public AStar(Graph pGraph, int pFrom, int pTo)
        {
            graph = pGraph;
            From = pFrom;
            To = pTo;

            visitedNodes = new List<NodeInfo>(graph.NodeCount);
            nodeQueue = new List<NodeInfo>(graph.NodeCount);
            float distanceToFinish = (graph.GetNode(pFrom).Position - graph.GetNode(pTo).Position).Length();
            nodeQueue.Add(new NodeInfo(From, 0, distanceToFinish));
            shortestPathTree = new List<Edge>();

            isFinished = false;

            FindPath();
        }

        private void FindPath()
        {
            while (!isFinished)
            {
                nodeQueue.Sort();
                NodeInfo currentNode = nodeQueue[nodeQueue.Count - 1];
                nodeQueue.RemoveAt(nodeQueue.Count - 1);

                if(currentNode.ID == To)
                {
                    visitedNodes.Add(currentNode);
                    isFinished = true;
                }

                SearchEdges(currentNode);
            }
        }

        private void SearchEdges(NodeInfo pCurrentNode)
        {
            foreach(Edge edge in graph.Edges)
            {
                int candidateID = -1;

                if(edge.FromID == pCurrentNode.ID) candidateID = edge.ToID;
                else if(edge.ToID == pCurrentNode.ID) candidateID = edge.FromID;

                if(candidateID > 0)
                {
                    bool visited = isVisited(candidateID);

                    bool queued = false;
                    for(int i = 0; i < nodeQueue.Count; i++)
                    {
                        queued = true;
                        float newCost = pCurrentNode.LowestCostToNode + graph.GetEdgeCost(candidateID);
                        if (nodeQueue[i].LowestCostToNode > newCost)
                        {
                            nodeQueue[i].LowestCostToNode = newCost;

                            for(int j = 0; j < shortestPathTree.Count; j++)
                            {
                                if (shortestPathTree[j].ToID == candidateID)
                                {
                                    shortestPathTree.RemoveAt(j);
                                    shortestPathTree.Add(new Edge(pCurrentNode.ID, candidateID));
                                }
                            }
                        }
                    }

                    if(!visited && !queued) 
                    {
                        float distanceToGoal = (graph.GetNode(candidateID).Position - graph.GetNode(To).Position).Length();
                        nodeQueue.Add(new NodeInfo(candidateID, graph.GetEdgeCost(edge.ID), distanceToGoal));
                        shortestPathTree.Add(new Edge(pCurrentNode.ID, candidateID));
                    }
                }
            }

            visitedNodes.Add(pCurrentNode);
        }

        private bool isVisited(int pID)
        {
            foreach (NodeInfo Node in visitedNodes)
            {
                if (Node.ID == pID) return true;
            }
            return false;
        }
    }
}
