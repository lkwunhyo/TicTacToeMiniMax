namespace QUT
{
    // This stateful class is used to encapsulate the process of counting how many nodes have been explored by the most recent call to the Minimax function
    public class NodeCounter
    {
        private static int count;

        // Called at the beginning each time Minimax function is called
        public static void Reset()
        {
            count = 0;
        }

        // Called for each recursive call to Minimax function
        public static void Increment()
        {
            count++;
        }

        // Used to retrieve the final node count as a read only property
        public static int Count => count;
    }
}
