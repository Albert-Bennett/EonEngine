/* Created 10/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.Collections.Trees
{
    /// <summary>
    /// Defines a BinaryTreeNode in a BinaryTree.
    /// </summary>
    /// <typeparam name="T">The type of value this BinaryTreeNode will hold.</typeparam>
    [Serializable]
    public class BinaryTreeNode<T>
    {
        T val;

        int treeDepth;

        BinaryTreeNode<T> leftNode;
        BinaryTreeNode<T> rightNode;

        /// <summary>
        /// The value in this BinaryTreeNode.
        /// </summary>
        public T Value
        {
            get { return val; }
        }

        /// <summary>
        /// The depth at which this BinaryTreeNode 
        /// appears in a BinaryTree.
        /// </summary>
        public int TreeDepth
        {
            get { return treeDepth; }
           internal set { treeDepth = value; }
        }

        /// <summary>
        /// The BinaryTreeNode left of this node.
        /// </summary>
        public BinaryTreeNode<T> LeftNode
        {
            get { return leftNode; }
            internal set { leftNode = value; }
        }

        /// <summary>
        /// The BinaryTreeNode right of this node.
        /// </summary>
        public BinaryTreeNode<T> RightNode
        {
            get { return rightNode; }
            internal set { rightNode = value; }
        }

        /// <summary>
        /// Creates a new BinaryTreeNode.
        /// </summary>
        /// <param name="value"></param>
        public BinaryTreeNode(T value)
        {
            this.val = value;
        }
    }
}
