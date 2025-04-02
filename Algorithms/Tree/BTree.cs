namespace Algorithms.Tree;

public sealed class BTree
{
    private class BTreeNode(bool isLeaf)
    {
        public List<int> Keys { get; set; } = new List<int>();
        public List<BTreeNode> Children { get; set; } = new List<BTreeNode>();
        public bool IsLeaf { get; set; } = isLeaf;
    }

    private BTreeNode root;
    private readonly int degree; // Минимальная степень дерева (t ≥ 2)

    public BTree(int degree)
    {
        if (degree < 2)
            throw new ArgumentException("Degree must be at least 2");
        
        this.degree = degree;
        root = new BTreeNode(true);
    }

    // Поиск ключа
    public bool Contains(int key)
    {
        return Search(root, key) != null;
    }

    private static BTreeNode Search(BTreeNode node, int key)
    {
        int i = 0;
        while (i < node.Keys.Count && key > node.Keys[i])
            i++;

        if (i < node.Keys.Count && key == node.Keys[i])
            return node;

        return node.IsLeaf ? null : Search(node.Children[i], key);
    }

    // Вставка ключа
    public void Insert(int key)
    {
        if (root.Keys.Count == 2 * degree - 1)
        {
            var newRoot = new BTreeNode(false);
            newRoot.Children.Add(root);
            SplitChild(newRoot, 0);
            root = newRoot;
        }
        InsertNonFull(root, key);
    }

    private void InsertNonFull(BTreeNode node, int key)
    {
        int i = node.Keys.Count - 1;

        if (node.IsLeaf)
        {
            while (i >= 0 && key < node.Keys[i])
            {
                i--;
            }
            node.Keys.Insert(i + 1, key);
        }
        else
        {
            while (i >= 0 && key < node.Keys[i])
            {
                i--;
            }
            i++;

            if (node.Children[i].Keys.Count == 2 * degree - 1)
            {
                SplitChild(node, i);
                if (key > node.Keys[i])
                    i++;
            }
            InsertNonFull(node.Children[i], key);
        }
    }

    private void SplitChild(BTreeNode parentNode, int childIndex)
    {
        var child = parentNode.Children[childIndex];
        var newChild = new BTreeNode(child.IsLeaf);

        parentNode.Keys.Insert(childIndex, child.Keys[degree - 1]);

        newChild.Keys.AddRange(child.Keys.GetRange(degree, degree - 1));
        child.Keys.RemoveRange(degree - 1, degree);

        if (!child.IsLeaf)
        {
            newChild.Children.AddRange(child.Children.GetRange(degree, degree));
            child.Children.RemoveRange(degree, degree);
        }

        parentNode.Children.Insert(childIndex + 1, newChild);
    }

    // Удаление ключа
    public void Delete(int key)
    {
        Delete(root, key);
        
        if (root.Keys.Count == 0 && !root.IsLeaf)
        {
            root = root.Children[0];
        }
    }

    private void Delete(BTreeNode node, int key)
    {
        int idx = FindKeyIndex(node, key);

        if (idx < node.Keys.Count && node.Keys[idx] == key)
        {
            if (node.IsLeaf)
            {
                node.Keys.RemoveAt(idx);
            }
            else
            {
                DeleteFromNonLeaf(node, idx);
            }
        }
        else
        {
            if (node.IsLeaf)
                return;

            bool isLastChild = idx == node.Children.Count;
            
            if (node.Children[idx].Keys.Count < degree)
                FillChild(node, idx);

            if (isLastChild && idx > node.Children.Count)
                Delete(node.Children[idx - 1], key);
            else
                Delete(node.Children[idx], key);
        }
    }

    private static int FindKeyIndex(BTreeNode node, int key)
    {
        int idx = 0;
        while (idx < node.Keys.Count && key > node.Keys[idx])
            idx++;
        return idx;
    }

    private void DeleteFromNonLeaf(BTreeNode node, int idx)
    {
        int key = node.Keys[idx];

        if (node.Children[idx].Keys.Count >= degree)
        {
            int predecessor = GetPredecessor(node, idx);
            node.Keys[idx] = predecessor;
            Delete(node.Children[idx], predecessor);
        }
        else if (node.Children[idx + 1].Keys.Count >= degree)
        {
            int successor = GetSuccessor(node, idx);
            node.Keys[idx] = successor;
            Delete(node.Children[idx + 1], successor);
        }
        else
        {
            MergeChildren(node, idx);
            Delete(node.Children[idx], key);
        }
    }

    private static int GetPredecessor(BTreeNode node, int idx)
    {
        var current = node.Children[idx];
        while (!current.IsLeaf)
            current = current.Children[current.Children.Count - 1];
        
        return current.Keys[current.Keys.Count - 1];
    }

    private static int GetSuccessor(BTreeNode node, int idx)
    {
        var current = node.Children[idx + 1];
        while (!current.IsLeaf)
            current = current.Children[0];
        
        return current.Keys[0];
    }

    private void FillChild(BTreeNode node, int idx)
    {
        if (idx != 0 && node.Children[idx - 1].Keys.Count >= degree)
            BorrowFromLeft(node, idx);
        else if (idx != node.Children.Count - 1 && node.Children[idx + 1].Keys.Count >= degree)
            BorrowFromRight(node, idx);
        else
        {
            if (idx != node.Children.Count - 1)
                MergeChildren(node, idx);
            else
                MergeChildren(node, idx - 1);
        }
    }

    private static void BorrowFromLeft(BTreeNode node, int idx)
    {
        var child = node.Children[idx];
        var leftSibling = node.Children[idx - 1];

        child.Keys.Insert(0, node.Keys[idx - 1]);
        node.Keys[idx - 1] = leftSibling.Keys[leftSibling.Keys.Count - 1];
        leftSibling.Keys.RemoveAt(leftSibling.Keys.Count - 1);

        if (!child.IsLeaf)
        {
            child.Children.Insert(0, leftSibling.Children[^1]);
            leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
        }
    }

    private static void BorrowFromRight(BTreeNode node, int idx)
    {
        var child = node.Children[idx];
        var rightSibling = node.Children[idx + 1];

        child.Keys.Add(node.Keys[idx]);
        node.Keys[idx] = rightSibling.Keys[0];
        rightSibling.Keys.RemoveAt(0);

        if (!child.IsLeaf)
        {
            child.Children.Add(rightSibling.Children[0]);
            rightSibling.Children.RemoveAt(0);
        }
    }

    private static void MergeChildren(BTreeNode node, int idx)
    {
        var child = node.Children[idx];
        var rightSibling = node.Children[idx + 1];

        child.Keys.Add(node.Keys[idx]);
        child.Keys.AddRange(rightSibling.Keys);

        if (!child.IsLeaf)
            child.Children.AddRange(rightSibling.Children);

        node.Keys.RemoveAt(idx);
        node.Children.RemoveAt(idx + 1);
    }

    // Обход дерева (In-order)
    public List<int> Traverse()
    {
        var result = new List<int>();
        Traverse(root, result);
        return result;
    }

    private static void Traverse(BTreeNode node, List<int> result)
    {
        if (node == null) return;

        for (int i = 0; i < node.Keys.Count; i++)
        {
            if (!node.IsLeaf)
                Traverse(node.Children[i], result);
            
            result.Add(node.Keys[i]);
        }

        if (!node.IsLeaf)
            Traverse(node.Children[^1], result);
    }
}
