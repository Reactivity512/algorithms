namespace Algorithms.Tree;

public sealed class BinarySearchTree
{
    private class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
        }
    }

    private Node root;

    // Вставка элемента
    public void Insert(int value)
    {
        root = Insert(root, value);
    }

    private static Node Insert(Node node, int value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        if (value < node.Value)
        {
            node.Left = Insert(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = Insert(node.Right, value);
        }

        return node;
    }

    // Поиск элемента
    public bool Contains(int value)
    {
        return Contains(root, value);
    }

    private static bool Contains(Node node, int value)
    {
        if (node == null)
        {
            return false;
        }

        if (value == node.Value)
        {
            return true;
        }

        return value < node.Value 
            ? Contains(node.Left, value) 
            : Contains(node.Right, value);
    }

    // Удаление элемента
    public void Delete(int value)
    {
        root = Delete(root, value);
    }

    private static Node Delete(Node node, int value)
    {
        if (node == null)
        {
            return null;
        }

        if (value < node.Value)
        {
            node.Left = Delete(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = Delete(node.Right, value);
        }
        else
        {
            // Узел с одним потомком или без потомков
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            // Узел с двумя потомками: получаем наименьший элемент в правом поддереве
            node.Value = MinValue(node.Right);

            // Удаляем этот наименьший элемент
            node.Right = Delete(node.Right, node.Value);
        }

        return node;
    }

    private static int MinValue(Node node)
    {
        int min = node.Value;
        while (node.Left != null)
        {
            min = node.Left.Value;
            node = node.Left;
        }
        return min;
    }

    // Обходы дерева
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
