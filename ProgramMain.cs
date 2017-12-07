using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Parallel_Binary_Search_Tree
{
    class ProgramMain
    {
        static void Main(string[] args)
        {
            var parallelBST = new Tree<int,char>();
            var BST = new Tree<int, char>();
            Stopwatch time = new Stopwatch();

            int min = 0;
            int max = 10000000;

            int[] keysToInsert = new int[10000000];
            int[] keysToDelete = new int[10000000];
            int[] keysToFind = new int[10000000];

            Random random = new Random();

            //заполняем
            for (int i = 0; i < keysToInsert.Length; i++)
            {
                keysToInsert[i] = random.Next(min, max);
            }

            for (int i = 0; i < keysToDelete.Length; i++)
            {
                int j = random.Next(0, keysToInsert.Length);
                keysToDelete[i] = keysToInsert[j];
            }

            for (int i = 0; i < keysToFind.Length; i++)
            {
                int j = random.Next(0, keysToInsert.Length);
                keysToFind[i] = keysToInsert[j];
            }

            //замеряем последовательное дерево
            time.Start();
            foreach (int key in keysToInsert)
            {
                BST.Insert(key, (char)random.Next(97,122));
            }
            time.Stop();
            Console.WriteLine("BST insert: {0}", time.Elapsed);

            time.Restart();
            foreach (int key in keysToDelete)
            {
                BST.Delete(key);
            }
            time.Stop();
            Console.WriteLine("BST delete: {0}", time.Elapsed);

            time.Restart();
            foreach (int key in keysToFind)
            {
                BST.Search(key);
            }
            time.Stop();
            Console.WriteLine("BST search: {0}", time.Elapsed);

            //замеряем параллельное
            time.Restart();
            Parallel.ForEach(keysToInsert, key => {
                parallelBST.Insert(key, (char)random.Next(97, 122));
            });
            time.Stop();
            Console.WriteLine("parallelBST insert: {0}", time.Elapsed);

            time.Restart();
            Parallel.ForEach(keysToDelete, key => {
                parallelBST.Delete(key);
            });
            time.Stop();
            Console.WriteLine("parallelBST delete: {0}", time.Elapsed);

            time.Restart();
            Parallel.ForEach(keysToFind, key => {
                parallelBST.Search(key);
            });
            time.Stop();
            Console.WriteLine("parallelBST search: {0}", time.Elapsed);

            //parallelBST.Print(parallelBST.root);
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }

}
