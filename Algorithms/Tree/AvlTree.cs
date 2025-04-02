namespace Algorithms.Tree;

public sealed class AvlTree
{
    private class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Height { get; set; }

        public Node(int value)
        {
            Value = value;
            Height = 1;
        }
    }

    private Node root;

    public void Insert(int value)
    {
        root = Insert(root, value);
    }

    private Node Insert(Node node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);
        else
            return node; // Дубликаты не допускаются

        // Обновление высоты узла
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        // Проверка баланса и балансировка
        int balance = GetBalance(node);

        // Левый левый случай
        if (balance > 1 && value < node.Left.Value)
            return RightRotate(node);

        // Правый правый случай
        if (balance < -1 && value > node.Right.Value)
            return LeftRotate(node);

        // Левый правый случай
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // Правый левый случай
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public void Delete(int value)
    {
        root = Delete(root, value);
    }

    private Node Delete(Node node, int value)
    {
        if (node == null)
            return node;

        if (value < node.Value)
            node.Left = Delete(node.Left, value);
        else if (value > node.Value)
            node.Right = Delete(node.Right, value);
        else
        {
            // Узел с одним или без потомков
            if (node.Left == null || node.Right == null)
            {
                node = node.Left ?? node.Right;
            }
            else
            {
                // Узел с двумя потомками: получаем наименьший в правом поддереве
                Node temp = MinValueNode(node.Right);
                node.Value = temp.Value;
                node.Right = Delete(node.Right, temp.Value);
            }
        }

        // Если дерево имело только один узел
        if (node == null)
            return node;

        // Обновление высоты
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        // Проверка баланса
        int balance = GetBalance(node);

        // Балансировка
        if (balance > 1 && GetBalance(node.Left) >= 0)
            return RightRotate(node);

        if (balance > 1 && GetBalance(node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balance < -1 && GetBalance(node.Right) <= 0)
            return LeftRotate(node);

        if (balance < -1 && GetBalance(node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public bool Contains(int value)
    {
        return Contains(root, value);
    }

    private static bool Contains(Node node, int value)
    {
        if (node == null)
            return false;

        if (value < node.Value)
            return Contains(node.Left, value);
        if (value > node.Value)
            return Contains(node.Right, value);
        
        return true;
    }

    private static int GetHeight(Node node)
    {
        return node?.Height ?? 0;
    }

    private static int GetBalance(Node node)
    {
        if (node == null)
            return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    private static Node MinValueNode(Node node)
    {
        Node current = node;
        while (current.Left != null)
            current = current.Left;
        return current;
    }

    private static Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    private static Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }

    public List<int> InOrder()
    {
        var result = new List<int>();
        InOrder(root, result);
        return result;
    }

    private static void InOrder(Node node, List<int> result)
    {
        if (node != null)
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

    private static void PreOrder(Node node, List<int> result)
    {
        if (node != null)
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

    private static void PostOrder(Node node, List<int> result)
    {
        if (node != null)
        {
            PostOrder(node.Left, result);
            PostOrder(node.Right, result);
            result.Add(node.Value);
        }
    }
}
