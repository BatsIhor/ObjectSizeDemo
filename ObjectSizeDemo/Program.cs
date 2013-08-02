using System;

namespace ObjectSizeDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            EmptyWithMethods obj = new EmptyWithMethods();

            ObjectSizes();

            OtherSizes();

            Console.ReadLine();
        }

        private static void OtherSizes()
        {
            Console.WriteLine();
            long diff;

            long before = GC.GetTotalMemory(true);
            Mixed2 obj = new Mixed2();
            long after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("Mixed2: \t\t" + diff);

            //---------------------------------------

            before = GC.GetTotalMemory(true);
            OneInt32 obj1 = new OneInt32();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("OneInt32: \t" + diff);

            //---------------------------------------
            
            before = GC.GetTotalMemory(true);
            TwoInt32 obj2 = new TwoInt32();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("TwoInt32: \t" + diff);

            //---------------------------------------
            
            before = GC.GetTotalMemory(true);
            ThreeInt32 obj3 = new ThreeInt32();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("ThreeInt32: \t" + diff);

            //---------------------------------------
            
            before = GC.GetTotalMemory(true);
            Mixed1 obj4 = new Mixed1();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("Mixed1: \t" + diff);

            //---------------------------------------
            
            before = GC.GetTotalMemory(true);
            Mixed2 obj5 = new Mixed2();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("Mixed2: \t" + diff);

            //---------------------------------------
        }

        private static void ObjectSizes()
        {
            int size = 100;
            double diff;

            //long beforeObj = GC.GetTotalMemory(true);
            //object obj = new object();
            //long afterObj = GC.GetTotalMemory(true);
            //diff = afterObj - beforeObj;

            //Console.WriteLine("object: \t" + diff);

            ////---------------------------------------

            //long beforeObjArray0 = GC.GetTotalMemory(true);
            //object[] array0 = new object[0];
            //long afterObjArray0 = GC.GetTotalMemory(true);
            //diff = afterObjArray0 - beforeObjArray0;

            //Console.WriteLine("array0: \t" + diff);

            ////---------------------------------------

            //long beforeObjArray = GC.GetTotalMemory(true);
            //object[] arrayEmpty = new object[size];
            //long afterObjArray = GC.GetTotalMemory(true);
            //diff = afterObjArray - beforeObjArray;

            //Console.WriteLine("array: \t\t" + (diff));
            //Console.WriteLine("array: \t\t" + (diff/size - 0.16));

            ////---------------------------------------

            long before = GC.GetTotalMemory(true);
            object[] array100 = new object[size]; //416 + 100 * 12

            for (int i = 0; i < size; i++)
            {
                array100[i] = new object();
            }
            long after = GC.GetTotalMemory(true);
            diff = after - before;
            Console.WriteLine("array100: \t" + diff);
            Console.WriteLine("PerШobject: \t" + diff/size);

            // Stop the GC from messing up our measurements 
            //GC.KeepAlive(arrayEmpty);
            //GC.KeepAlive(array0);
            //GC.KeepAlive(obj);
            //GC.KeepAlive(array100);

        }

        class Empty { }

        class EmptyWithMethods
        {
            private object obj;
            private int obj1;
            private byte bt;
            public void DO(int x)
            {}

            public virtual void DOVirtual(int x)
            {}
        }

        class OneInt32 { int x; }
        class TwoInt32 { int x, y; }
        class ThreeInt32 { int x, y, z; }

        class Mixed1
        {
            int x;
            byte b1, b2, b3, b4;
            int y, z;
        }

        class Mixed2
        {
            private delegate void del();

            private del concreet;
            int x;
            byte b1;
            int y, z;
            byte b2, b3, b4;
        }
    }
}
