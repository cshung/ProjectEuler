namespace Common
{
    public class Pair<T, U>
    {
        public static Pair<T, U> Create(T item1, U item2)
        {
            return new Pair<T, U>(item1, item2);
        }

        private Pair(T item1, U item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public T Item1 { get; set; }

        public U Item2 { get; set; }

    }
}
