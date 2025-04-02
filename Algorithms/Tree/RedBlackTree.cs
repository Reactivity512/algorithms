namespace Algorithms.Tree;

public sealed class RedBlackTree
{
    private enum NodeColor { Red, Black }

    private class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public NodeColor Color { get; set; }

        public Node(int value)
        {
            Value = value;
            Color = NodeColor.Red; // Новые узлы всегда красные
        }
    }

    private Node root;
    private readonly Node nil; // Фиктивный листовой узел

    public RedBlackTree()
    {
        nil = new Node(-1)
        {
            Color = NodeColor.Black
        };
        root = nil;
    }

    public void Insert(int value)
    {
        var newNode = new Node(value)
        {
            Left = nil,
            Right = nil,
            Parent = nil
        };

        Insert(newNode);
    }

    private void Insert(Node newNode)
    {
        Node parent = nil;
        Node current = root;

        // Поиск места для вставки
        while (current != nil)
        {
            parent = current;
            if (newNode.Value < current.Value)
                current = current.Left;
            else if (newNode.Value > current.Value)
                current = current.Right;
            else
                return; // Дубликаты не допускаются
        }

        newNode.Parent = parent;

        if (parent == nil)
            root = newNode;
        else if (newNode.Value < parent.Value)
            parent.Left = newNode;
        else
            parent.Right = newNode;

        InsertFixup(newNode);
    }

    // Балансировка после вставки
    private void InsertFixup(Node node)
    {
        while (node.Parent.Color == NodeColor.Red)
        {
            if (node.Parent == node.Parent.Parent.Left)
            {
                Node uncle = node.Parent.Parent.Right;
                
                // красный
                if (uncle.Color == NodeColor.Red)
                {
                    node.Parent.Color = NodeColor.Black;
                    uncle.Color = NodeColor.Black;
                    node.Parent.Parent.Color = NodeColor.Red;
                    node = node.Parent.Parent;
                }
                else
                {
                    // черный, узел — правый потомок
                    if (node == node.Parent.Right)
                    {
                        node = node.Parent;
                        LeftRotate(node);
                    }
                    
                    // черный, узел — левый потомок
                    node.Parent.Color = NodeColor.Black;
                    node.Parent.Parent.Color = NodeColor.Red;
                    RightRotate(node.Parent.Parent);
                }
            }
            else
            {
                // Симметричный случай
                Node uncle = node.Parent.Parent.Left;
                
                if (uncle.Color == NodeColor.Red)
                {
                    node.Parent.Color = NodeColor.Black;
                    uncle.Color = NodeColor.Black;
                    node.Parent.Parent.Color = NodeColor.Red;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Left)
                    {
                        node = node.Parent;
                        RightRotate(node);
                    }
                    
                    node.Parent.Color = NodeColor.Black;
                    node.Parent.Parent.Color = NodeColor.Red;
                    LeftRotate(node.Parent.Parent);
                }
            }
        }
        
        root.Color = NodeColor.Black;
    }

    // Удаление значения
    public void Delete(int value)
    {
        var node = FindNode(value);
        if (node == nil)
            return;

        Delete(node);
    }

    private void Delete(Node node)
    {
        Node y = node;
        Node x;
        NodeColor originalColor = y.Color;

        if (node.Left == nil)
        {
            x = node.Right;
            Transplant(node, node.Right);
        }
        else if (node.Right == nil)
        {
            x = node.Left;
            Transplant(node, node.Left);
        }
        else
        {
            y = Minimum(node.Right);
            originalColor = y.Color;
            x = y.Right;

            if (y.Parent == node)
                x.Parent = y;
            else
            {
                Transplant(y, y.Right);
                y.Right = node.Right;
                y.Right.Parent = y;
            }

            Transplant(node, y);
            y.Left = node.Left;
            y.Left.Parent = y;
            y.Color = node.Color;
        }

        if (originalColor == NodeColor.Black)
            DeleteFixup(x);
    }

    // Балансировка после удаления
    private void DeleteFixup(Node node)
    {
        while (node != root && node.Color == NodeColor.Black)
        {
            if (node == node.Parent.Left)
            {
                Node sibling = node.Parent.Right;
                
                if (sibling.Color == NodeColor.Red)
                {
                    sibling.Color = NodeColor.Black;
                    node.Parent.Color = NodeColor.Red;
                    LeftRotate(node.Parent);
                    sibling = node.Parent.Right;
                }
                
                if (sibling.Left.Color == NodeColor.Black && sibling.Right.Color == NodeColor.Black)
                {
                    sibling.Color = NodeColor.Red;
                    node = node.Parent;
                }
                else
                {
                    if (sibling.Right.Color == NodeColor.Black)
                    {
                        sibling.Left.Color = NodeColor.Black;
                        sibling.Color = NodeColor.Red;
                        RightRotate(sibling);
                        sibling = node.Parent.Right;
                    }
                    
                    sibling.Color = node.Parent.Color;
                    node.Parent.Color = NodeColor.Black;
                    sibling.Right.Color = NodeColor.Black;
                    LeftRotate(node.Parent);
                    node = root;
                }
            }
            else
            {
                // Симметричный случай
                Node sibling = node.Parent.Left;
                
                if (sibling.Color == NodeColor.Red)
                {
                    sibling.Color = NodeColor.Black;
                    node.Parent.Color = NodeColor.Red;
                    RightRotate(node.Parent);
                    sibling = node.Parent.Left;
                }
                
                if (sibling.Right.Color == NodeColor.Black && sibling.Left.Color == NodeColor.Black)
                {
                    sibling.Color = NodeColor.Red;
                    node = node.Parent;
                }
                else
                {
                    if (sibling.Left.Color == NodeColor.Black)
                    {
                        sibling.Right.Color = NodeColor.Black;
                        sibling.Color = NodeColor.Red;
                        LeftRotate(sibling);
                        sibling = node.Parent.Left;
                    }
                    
                    sibling.Color = node.Parent.Color;
                    node.Parent.Color = NodeColor.Black;
                    sibling.Left.Color = NodeColor.Black;
                    RightRotate(node.Parent);
                    node = root;
                }
            }
        }
        
        node.Color = NodeColor.Black;
    }

    public bool Contains(int value)
    {
        return FindNode(value) != nil;
    }

    private Node FindNode(int value)
    {
        Node current = root;
        
        while (current != nil)
        {
            if (value < current.Value)
                current = current.Left;
            else if (value > current.Value)
                current = current.Right;
            else
                return current;
        }
        
        return nil;
    }

    private void LeftRotate(Node x)
    {
        Node y = x.Right;
        x.Right = y.Left;
        
        if (y.Left != nil)
            y.Left.Parent = x;
        
        y.Parent = x.Parent;
        
        if (x.Parent == nil)
            root = y;
        else if (x == x.Parent.Left)
            x.Parent.Left = y;
        else
            x.Parent.Right = y;
        
        y.Left = x;
        x.Parent = y;
    }

    private void RightRotate(Node y)
    {
        Node x = y.Left;
        y.Left = x.Right;
        
        if (x.Right != nil)
            x.Right.Parent = y;
        
        x.Parent = y.Parent;
        
        if (y.Parent == nil)
            root = x;
        else if (y == y.Parent.Right)
            y.Parent.Right = x;
        else
            y.Parent.Left = x;
        
        x.Right = y;
        y.Parent = x;
    }

    private void Transplant(Node u, Node v)
    {
        if (u.Parent == nil)
            root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;
        
        v.Parent = u.Parent;
    }

    private Node Minimum(Node node)
    {
        while (node.Left != nil)
            node = node.Left;
        return node;
    }

    // Обходы дерева
    public List<int> InOrder()
    {
        var result = new List<int>();
        InOrder(root, result);
        return result;
    }

    private void InOrder(Node node, List<int> result)
    {
        if (node != nil)
        {
            InOrder(node.Left, result);
            result.Add(node.Value);
            InOrder(node.Right, result);
        }
    }

    public List<int> PreOrder()
    {
        var result = new List<int>();
        PreOrder(root, result);
        return result;
    }

    private void PreOrder(Node node, List<int> result)
    {
        if (node != nil)
        {
            result.Add(node.Value);
            PreOrder(node.Left, result);
            PreOrder(node.Right, result);
        }
    }

    public List<int> PostOrder()
    {
        var result = new List<int>();
        PostOrder(root, result);
        return result;
    }

    private void PostOrder(Node node, List<int> result)
    {
        if (node != nil)
        {
            PostOrder(node.Left, result);
            PostOrder(node.Right, result);
            result.Add(node.Value);
        }
    }
}
