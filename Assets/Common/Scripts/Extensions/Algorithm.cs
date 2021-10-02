using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Algorithm
{
	public static float BinarySearch(float min, float max, Func<float, bool> bigEnough) {
		var result = min;
		var step = max - min;
		while (step > float.Epsilon) {
			if (!bigEnough(result + step)) {
				result += step;
			}
			step /= 2;
		}
		return result;
	}

	public class Weighted<Vertex> 
	{
		public Vertex to;
		public float weight;
		public Weighted(Vertex to, float weight) {
			this.to = to;
			this.weight = weight;
		}
	}

	public static Map<Vertex, Weighted<Vertex>> Dijkstra<Vertex>(Vertex start, Func<Vertex, IEnumerable<Weighted<Vertex>>> edges, int maxSteps = 10000) {
		Map<Vertex, Weighted<Vertex>> result = new Map<Vertex, Weighted<Vertex>>(() => new Weighted<Vertex>(default(Vertex), float.PositiveInfinity));
		HashSet<Vertex> relaxated = new HashSet<Vertex>();
		result[start] = new Weighted<Vertex>(default(Vertex), 0);
		for (int i = 0; i < maxSteps; i++) {
			var vertex = result.Keys.Except(relaxated).MinBy(v => result[v].weight);
			if (vertex == null) {
				break;
			}
			relaxated.Add(vertex);
			edges(vertex).ForEach(e => {
				var newPath = result[vertex].weight + e.weight;
				if (newPath < result[e.to].weight) {
					result[e.to] = new Weighted<Vertex>(vertex, newPath);
				}
			});
		}
		return result;
	}


	public static IEnumerable<ReachedNode<Node>> BFS<Node>(
		Node start,
		Func<Node, IEnumerable<Node>> edges,
		Func<Node, bool> endNode = null,
		int maxDistance = int.MaxValue
	) {
		endNode = endNode ?? (n => false);
		HashSet<Node> visited = new HashSet<Node>();
		var q = new Queue<ReachedNode<Node>>();
		var reachedStart = new ReachedNode<Node>(start, 0, null);
		q.Enqueue(reachedStart);
		yield return reachedStart;
		visited.Add(start);
		while (q.Count > 0) {
			var p = q.Dequeue();
			foreach (Node n in edges(p.node)) {
				if (!visited.Contains(n)) {
					var next = new ReachedNode<Node>(n, p.distance + 1, p);
					if (next.distance > maxDistance) {
						continue;
					}
					q.Enqueue(next);
					yield return next;
					if (endNode(n)) {
						yield break;
					}
					visited.Add(n);
				}
			}
		}
	}

	public static ReachedNode<Node> Closest<Node>(
		Node start,
		Func<Node, IEnumerable<Node>> edges,
		Func<Node, bool> endNode = null,
		int maxDistance = int.MaxValue
	) {
		endNode = endNode ?? (n => false);
		return BFS(start, edges, endNode, maxDistance).LastOrDefault(p => endNode(p.node));
	}

	public static bool Reachable<Node>(
		Node start,
		Func<Node, IEnumerable<Node>> edges,
		Func<Node, bool> endNode = null,
		int maxDistance = int.MaxValue
	) {
		return BFS(start, edges, endNode, maxDistance).Any(p => endNode(p.node));
	}

	public static IEnumerable<Node> ShortestPath<Node>(
		Node start,
		Func<Node, IEnumerable<Node>> edges,
		Func<Node, bool> endNode = null,
		int maxDistance = int.MaxValue
	) {
		return ReversedShortestPath(start, edges, endNode, maxDistance)?.Reverse();
	}

	public static IEnumerable<Node> ReversedShortestPath<Node>(
		Node start,
		Func<Node, IEnumerable<Node>> edges,
		Func<Node, bool> endNode = null,
		int maxDistance = int.MaxValue
	) {
		var target = BFS(start, edges, endNode, maxDistance).LastOrDefault(p => endNode(p.node));
		if (target == null) {
			return null;
		}
		return RestoreReversedShortestPath(start, target);
	}

	public static IEnumerable<Node> RestoreReversedShortestPath<Node>(Node start, ReachedNode<Node> target) {
		yield return target.node;
		for (int i = 0; i < 1000 && !target.node.Equals(start); i++) {
			target = target.from;
			yield return target.node;
		}
	}
}