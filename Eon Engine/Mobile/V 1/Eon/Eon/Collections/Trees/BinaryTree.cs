/* Created 10/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.Collections.Trees
{
    /// <summary>
    /// defines a tree structure which can only
    /// have two child nodes per parent node.
    /// </summary>
    /// <typeparam name="T">The type of values to 
    /// be held by this BinaryTree</typeparam>
    public class BinaryTree<T> where T : IComparable
    {
        BinaryTreeNode<T> root;

        /// <summary>
        /// The root of all BinaryTreeNodes
        /// </summary>
        public BinaryTreeNode<T> Root
        {
            get { return root; }
        }

        /// <summary>
        /// Adds a node to the BinaryTree.
        /// </summary>
        /// <param name="value">The value of the BinaryTreeNode to be added.</param>
        public void AddNode(T value)
        {
            if (root == null)
            {
                root = new BinaryTreeNode<T>(value);
                root.TreeDepth = 1;
            }
            else
            {
                BinaryTreeNode<T> curr = root;
                BinaryTreeNode<T> next;

                int depth = root.TreeDepth;

                while (true)
                {
                    depth++;

                    if (curr.Value.CompareTo(value) <= 0)
                    {
                        next = curr.LeftNode;

                        if (next == null)
                        {
                            curr.LeftNode = new BinaryTreeNode<T>(value);
                            curr.LeftNode.TreeDepth = depth;
                            break;
                        }
                    }
                    else
                    {
                        next = curr.RightNode;

                        if (next == null)
                        {
                            curr.RightNode = new BinaryTreeNode<T>(value);
                            curr.RightNode.TreeDepth = depth;
                            break;
                        }
                    }

                    curr = next;
                }
            }
        }

        /// <summary>
        /// Finds a BinaryTreeNode based on a specified value.
        /// </summary>
        /// <param name="value">The value to be found in a BinaryTreeNode.</param>
        /// <returns>The result of the search.</returns>
        public BinaryTreeNode<T> FindNode(T value)
        {
            BinaryTreeNode<T> curr = null;

            if (root != null)
            {
                curr = root;

                while (curr.Value.CompareTo(value) != 0)
                {
                    if (curr.Value.CompareTo(value) < 0)
                        curr = curr.LeftNode;
                    else
                        curr = curr.RightNode;
                }
            }

            if (curr != null)
                return curr;
            else
                throw new ArgumentNullException("No node found containing the given value: " + value.ToString());
        }

        /// <summary>
        /// Finds a BinaryTreeNode based on a integer value.
        /// </summary>
        /// <param name="value">The value to be found in this BinaryTree.</param>
        /// <returns>The result of the search.</returns>
        public BinaryTreeNode<T> FindNode(int value)
        {
            BinaryTreeNode<T> curr = null;

            if (root != null)
            {
                curr = root;

                while (curr.Value.CompareTo(value) != 0)
                {
                    if (curr.Value.CompareTo(value) < 0)
                        curr = curr.LeftNode;
                    else
                        curr = curr.RightNode;
                }
            }

            if (curr != null)
                return curr;
            else
                throw new ArgumentNullException("No node found containing" +
                    " the given value: " + value);
        }

        /// <summary>
        /// Removes a BinaryTreeNode with the specified value. 
        /// </summary>
        /// <param name="value">The value to be searched for.</param>
        public bool RemoveNode(BinaryTreeNode<T> node)
        {
            BinaryTreeNode<T> curr, parent;
            bool isLeftChild = true;

            curr = parent = root;

            while (curr.Value.CompareTo(node.Value) != 0)
            {
                parent = curr;

                if (node.Value.CompareTo(curr.Value) < 0)
                {
                    isLeftChild = true;
                    curr = curr.LeftNode;
                }
                else
                {
                    isLeftChild = false;
                    curr = curr.RightNode;
                }

                if (curr == null)
                    return false;

                if (curr.LeftNode == null && curr.RightNode == null)
                {
                    if (curr == root)
                        root = curr.LeftNode;
                    else
                        if (isLeftChild)
                            parent.LeftNode = curr.RightNode;
                        else
                            parent.RightNode = curr.RightNode;
                }
                else
                {
                    if (curr.RightNode == null)
                    {
                        if (curr == root)
                            root = curr.RightNode;
                        else
                            if (isLeftChild)
                                parent.LeftNode = curr.LeftNode;
                            else
                                parent.RightNode = curr.LeftNode;
                    }
                    else
                    {
                        if (curr.LeftNode == null)
                        {
                            if (curr == root)
                                root = curr.LeftNode;
                            else
                                if (isLeftChild)
                                    parent.LeftNode = curr.RightNode;
                                else
                                    parent.RightNode = curr.RightNode;
                        }
                        else
                        {
                            BinaryTreeNode<T> successor = GetSuccessor(curr);

                            if (curr == root)
                                root = successor;
                            else
                                if (isLeftChild)
                                    parent.LeftNode = successor;
                                else
                                    parent.RightNode = successor;

                            successor.LeftNode = curr.LeftNode;
                        }
                    }
                }

            }

            return true;
        }

        /// <summary>
        /// finds the successor of a node.
        /// </summary>
        /// <param name="node">The node to get the successor of.</param>
        /// <returns>The successor of a node (if one exists).</returns>
        public BinaryTreeNode<T> GetSuccessor(BinaryTreeNode<T> node)
        {
            BinaryTreeNode<T> curr, parent, successor;

            successor = node;
            parent = node;
            curr = node.RightNode;

            while (curr != null)
            {
                parent = successor;
                successor = curr;
                curr = curr.LeftNode;
            }

            if (successor != node.RightNode)
            {
                parent.LeftNode = successor.RightNode;
                successor.RightNode = node.RightNode;
            }

            return successor;
        }
    }
}
