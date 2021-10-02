using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReachedNode<Node>
{
    public Node node;
    public int distance;
    public ReachedNode<Node> from;

    public ReachedNode(Node node, int distance, ReachedNode<Node> from) {
        this.node = node;
        this.distance = distance;
        this.from = from;
    }
}