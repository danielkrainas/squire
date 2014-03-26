namespace Squire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public static class LinkedListExtensions
    {
        public static IEnumerable<LinkedListNode<T>> TakeAllAfter<T>(this LinkedList<T> list, LinkedListNode<T> node)
        {
            list.VerifyParam("list").IsNotNull();
            node.VerifyParam("node").IsNotNull();
            if (list != node.List)
            {
                throw new InvalidOperationException("node does not belong to specified list");
            }

            while ((node = node.Next) != null)
            {
                yield return node;
            }
        }

        public static IEnumerable<T> TakeAllValuesAfter<T>(this LinkedList<T> list, LinkedListNode<T> node)
        {
            list.VerifyParam("list").IsNotNull();
            node.VerifyParam("node").IsNotNull();
            if (list != node.List)
            {
                throw new InvalidOperationException("node does not belong to specified list");
            }

            while ((node = node.Next) != null)
            {
                yield return node.Value;
            }
        }
    }
}
