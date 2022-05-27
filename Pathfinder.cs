using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Map mainMap = new Map();
            Console.WriteLine(mainMap.findCapacity());

            mainMap.createNode("Creation Station");
            mainMap.createNode("Unfunny Station");
            mainMap.createNode("Broken Station");
            mainMap.createNode("Station Station");
            mainMap.createNode("Home Station");
            mainMap.createNode("Away Station");
            mainMap.createNode("Lonely Station");
            mainMap.createNode("Autonomous Transport Station"); // God I'm so funny.
            mainMap.createNode("Degeneracy");

            Console.WriteLine(mainMap.findCapacity());
            // ### Creating connections between nodes ## \\
            // Note: When a connection is made it connections both sides, so some stations have up to 5 connections but in their section they may only show 1.
            // Note extended: Look at the diagram to understand what I mean.

            // Creation Station ->
            mainMap.connectNodes("Creation Station", "Station Station", 3);
            mainMap.connectNodes("Creation Station", "Home Station", 3);

            // Unfunny Station ->
            mainMap.connectNodes("Unfunny Station", "Autonomous Transport Station", 1);
            mainMap.connectNodes("Unfunny Station", "Away Station", 1);

            // Broken Station ->
            mainMap.connectNodes("Broken Station", "Station Station", 3);
            mainMap.connectNodes("Broken Station", "Lonely Station", 5);

            // Station Station ->
            mainMap.connectNodes("Station Station", "Away Station", 2);

            // Home Station ->
            mainMap.connectNodes("Home Station", "Away Station", 1);
            mainMap.connectNodes("Home Station", "Autonomous Transport Station", 2);

            // Away Station ->
            mainMap.connectNodes("Away Station", "Autonomous Transport Station", 2);

            // Lonely Station ->
            mainMap.connectNodes("Lonely Station", "Autonomous Transport Station", 5);

            // Autonomous Transport Station ->
            mainMap.connectNodes("Autonomous Transport Station", "Degeneracy", 5);

            // Degeneracy ->

            // Write full map in text format
            Console.WriteLine(mainMap.ToString());

            Console.WriteLine(mainMap.pathTo("Creation Station", "Creation Station"));

            Console.WriteLine(mainMap.pathTo("Creation Station", "Station Station"));

            Console.WriteLine(mainMap.pathTo("Creation Station", "Lonely Station"));

            Console.WriteLine(mainMap.pathTo("Creation Station", "Broken Station"));

            Console.WriteLine(mainMap.pathTo("Creation Station", "Degeneracy"));

            Console.ReadLine(); // This is so VS doesn't automatically close the terminal window
        }
    }
    class Map
    {
        Node[] nodeList = new Node[20]; // Maps can have a maximum of 20 nodes

        public void createNode(string Name)
        {
            int counter = 0;
            while (nodeList[counter] != null)
            {
                counter += 1;
            }

            Console.WriteLine($"Space found, Creating Node: {Name}");
            nodeList[counter] = new Node(Name);
        }
        public void connectNodes(string station1Name, string station2Name, int pathWeight)
        {
            int node1 = getIndex(station1Name);
            int node2 = getIndex(station2Name);

            if (node1 == -1 || node2 == -1)
            {
                Console.WriteLine("Connection Failed");
            }
            else
            {
                Console.WriteLine($"Creating a {pathWeight} connection between '{station1Name}' and '{station2Name}'");
                nodeList[node1].addNewConnection(nodeList[node2], pathWeight);
                nodeList[node2].addNewConnection(nodeList[node1], pathWeight);
            }
        }
        public string findCapacity()
        {
            int counter = 0;
            for (int i = 0; i < nodeList.Length; i++)
            {
                if (nodeList[i] == null)
                {
                    counter += 1;
                }
            }

            return $"Map Capacity: {counter} Nodes Remaining";
        }
        private int getIndex(string stationName)
        {
            for (int i = 0; i < nodeList.Length; i++)
            {
                if (nodeList[i].Name == stationName)
                {
                    return i;
                }
            }

            return -1;
        }
        public string pathTo(string startingStation, string endStation)
        {
            if (startingStation == endStation)
            {
                return "You're already there, smarty-pants.";
            }
            string path = startingStation; // Format: StationName -(pathWeight)> Ex. Unfunny Station -(3)> Central Station
            int start = getIndex(startingStation);
            int finish = getIndex(endStation);

            Node workingNode = nodeList[start];
            Node lowestNode = new Node("No");
            int lowestPath = 100;
            List<string> visitedNames = new List<string>();

            while (true)
            {
                for (int i = 0; i < workingNode.connections.Length; i++)
                {
                    if (workingNode.connections[i] != null)
                    {
                        if (workingNode.connections[i].Name == endStation)
                        {
                            path += $" -({workingNode.pathWeights[i]})> {endStation}";
                            return path;
                        }
                        else if (workingNode.pathWeights[i] <= lowestPath && !visitedNames.Contains(workingNode.connections[i].Name))
                        {
                            lowestNode = workingNode.connections[i];
                            lowestPath = workingNode.pathWeights[i];
                        }
                    }
                }

                visitedNames.Add(workingNode.Name);
                if (workingNode == lowestNode)
                {
                    string tempSave = visitedNames.ElementAt(1);
                    visitedNames.Clear();
                    visitedNames.Add(tempSave);

                    workingNode = nodeList[start];
                    lowestPath = 100;

                    path = startingStation;
                    
                    /*string thatToo = "";
                    foreach (string s in visitedNames)
                    {
                        thatToo += ", " + s;
                    }
                    Console.WriteLine(thatToo);
                    return "get looped idiot"; */
                }
                else
                {
                    path += $" -({lowestPath})> {lowestNode.Name}";
                    workingNode = lowestNode;
                    lowestPath = 100;
                }
            }
        }
        public override string ToString()
        {
            string fullList = "";

            foreach (Node n in nodeList)
            {
                if (n != null)
                {
                    fullList += "--- " + n.Name + " ---" + n.ToString();
                }
            }

            return fullList;
        }
    }
    class Node
    {
        // Two arrays.  One of connections & one of path weights
        // The connection has the same index at the path weight.
        // Fuck you, I'll do it how I want to.

        public string Name;

        // These variables should not be public.  This is an unsafe way to do this.  It's midnight right now and I do not care.  This is for fun, not production code.
        public Node[] connections = new Node[10]; // Nodes can have a maximum of 20 connections
        public int[] pathWeights = new int[10]; // Nodes can have a maximum of 20 connections

        public Node(string Name)
        {
            this.Name = Name;
        }

        public void addNewConnection(Node newNode, int newWeight)
        {
            int counter = 0;
            while (connections[counter] != null)
            {
                counter += 1;
            }

            connections[counter] = newNode;
            pathWeights[counter] = newWeight;
        }
        public override string ToString()
        {
            string fullList = "\n";

            for (int i = 0; i < connections.Length; i++)
            {
                if (connections[i] != null)
                {
                    fullList += $"\t{i}: {connections[i].Name}, {pathWeights[i]}\n";
                }
            }

            return fullList;
        }
    }
}
