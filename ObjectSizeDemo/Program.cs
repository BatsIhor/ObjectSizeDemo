using System;

namespace ObjectSizeDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ObjectSizes();
            OtherSizes();

            Console.ReadLine();
        }

        private static void OtherSizes()
        {
            Console.WriteLine();
            long diff;

            long before = GC.GetTotalMemory(true);
            Empty obj = new Empty();
            long after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("Empty: \t\t" + diff);

            //---------------------------------------

            before = GC.GetTotalMemory(true);
            EmptyWithMethods obj0 = new EmptyWithMethods();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("EmptyWithMethods: " + diff);

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

            before = GC.GetTotalMemory(true);
            Mixed3 obj6 = new Mixed3();
            after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("Mixed3: \t" + diff);

            //---------------------------------------
        }

        private static void ObjectSizes()
        {
            int size = 1000;
            double diff;

            long beforeObj = GC.GetTotalMemory(true);
            object obj = new object();
            long afterObj = GC.GetTotalMemory(true);
            diff = afterObj - beforeObj;

            Console.WriteLine("obj: \t\t" + diff);

            //---------------------------------------

            long beforeObjArray0 = GC.GetTotalMemory(true);
            object[] array0 = new object[0];
            int[] arrayInt = new int[0];
            long afterObjArray0 = GC.GetTotalMemory(true);
            diff = afterObjArray0 - beforeObjArray0;

            Console.WriteLine("array0: \t" + diff);

            //---------------------------------------

            long beforeObjArray = GC.GetTotalMemory(true);
            object[] arrayEmpty = new object[size];
            long afterObjArray = GC.GetTotalMemory(true);
            diff = afterObjArray - beforeObjArray;

            Console.WriteLine("arrayEmpty: \t" + (diff));
            Console.WriteLine("Per object: \t" + ((diff - 16) / size));

            //---------------------------------------

            //PLEASE COMMENT CODE ABOVE AS GC DOESN'T ALLOW TO MEASURE NEXT array100 PROPERLY

            long before = GC.GetTotalMemory(true);
            object[] array100 = new object[100]; //416 + 100 * 12

            for (int i = 0; i < 100; i++)
            {
                array100[i] = new object();
            }
            long after = GC.GetTotalMemory(true);
            diff = after - before;

            Console.WriteLine("array100: \t" + diff);
            Console.WriteLine("Per object: \t" + (diff - 416) / 100);
        }

        #region Simple objects just for counting their size

        class Empty { }

        class EmptyWithMethods
        {
            public void DO(int x)
            { }

            public virtual void DOVirtual(int x)
            { }
        }

        class OneInt32 { int x; }
        class TwoInt32 { int x, y; }
        class ThreeInt32 { int x, y, z; }

        class Mixed1
        {
            int x;
            byte b1;
        }
        
        class Mixed2
        {
            int x;
            byte b1, b2, b3, b4;
        }

        class Mixed3
        {
            private delegate void del();

            private del concreet;
            int x;
            byte b1;
            int y, z;
            byte b2, b3, b4;
        }
        #endregion
    }
}
