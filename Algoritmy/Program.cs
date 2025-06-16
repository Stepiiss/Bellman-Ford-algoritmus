using System;
using System.Collections.Generic;

class Edge
{
    public int Source, Destination, Weight;
    public Edge(int source, int destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}

class Graph
{
    public int Vertices;
    public List<Edge> Edges;

    public Graph(int vertices)
    {
        Vertices = vertices;
        Edges = new List<Edge>();
    }

    public void AddEdge(int source, int destination, int weight)
    {
        Edges.Add(new Edge(source, destination, weight));
    }

    public void BellmanFord(int source)
    {
        int[] distance = new int[Vertices];
        for (int i = 0; i < Vertices; i++)
            distance[i] = int.MaxValue;

        distance[source] = 0;

        for (int i = 0; i < Vertices - 1; i++)
        {
            foreach (Edge edge in Edges)
            {
                if (distance[edge.Source] != int.MaxValue &&
                    distance[edge.Source] + edge.Weight < distance[edge.Destination])
                {
                    distance[edge.Destination] = distance[edge.Source] + edge.Weight;
                }
            }
        }

        //Kontrola záporných cyklů
        foreach (Edge edge in Edges)
        {
            if (distance[edge.Source] != int.MaxValue &&
                distance[edge.Source] + edge.Weight < distance[edge.Destination])
            {
                Console.WriteLine("Graf obsahuje záporný cyklus.");
                return;
            }
        }

        //Výpis výsledků
        Console.WriteLine("Vzdálenosti z vrcholu " + source + ":");
        for (int i = 0; i < Vertices; i++)
        {
            if (distance[i] == int.MaxValue)
                Console.WriteLine($"Do vrcholu {i}: ∞");
            else
                Console.WriteLine($"Do vrcholu {i}: {distance[i]}");
        }
    }
}

class Program
{
    static void Main()
    {
        Graph graph = new Graph(6);
        graph.AddEdge(0, 1, 10);
        graph.AddEdge(0, 2, 3);
        graph.AddEdge(1, 3, 2);
        graph.AddEdge(2, 1, 1);
        graph.AddEdge(2, 3, 8);
        graph.AddEdge(2, 4, 2);
        graph.AddEdge(3, 4, 4);
        graph.AddEdge(4, 5, 1);
        graph.AddEdge(5, 3, 4);

        graph.BellmanFord(0); // Spuštění algoritmu z vrcholu 0
    }
}
