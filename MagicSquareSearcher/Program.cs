using System;
using System.Collections.Generic;

namespace ConsoleApplication2
{
    class Program
    {

        const int squareSize = 3;
        const int squaresToTest = 10;

        static long[] theSquare;
        static int[] theSquareSeq;
        static long[] squareNumbers;
        static int squareLength;

        static LinkedList<int> seqList;

        static void Main(string[] args)
        {
            
            squareLength = squareSize * squareSize;
            theSquare = new long[squareLength];
            theSquareSeq = new int[squareLength];
            squareNumbers = new long[squaresToTest];
            int newSeq;
            bool done = false;

            seqList = new LinkedList<int>();

            // precalculate squares
            for (long i = 1; i <= squaresToTest; i++)
            {
                squareNumbers[i - 1] = i;// * i;
            }

            int ticks = Environment.TickCount;
            processCell(0);
            Console.WriteLine("That took " + (Environment.TickCount - ticks).ToString() + " ms");

            Console.ReadKey();
        }



        private static void processCell(int cellIndex)
        {
            bool nextSquare;
            for (int i = 0; i < squaresToTest; i++)
            {
                nextSquare = false;
                for (int c = 0; c < cellIndex; c++)
                {
                    if (theSquareSeq[c] == i)
                    {
                        nextSquare = true;
                        break;
                    }
                }
                if (nextSquare)
                {
                    continue;
                }

                theSquareSeq[cellIndex] = i;
                theSquare[cellIndex] = squareNumbers[i];

                if (cellIndex < squareLength - 1)
                {
                    processCell(cellIndex + 1);
                } else
                {
                    verifyMagicSquare();
                }
            }
        }

        private static void verifyMagicSquare()
        {
            long result = theSquare[0] + theSquare[1] + theSquare[2];
            if (result != (theSquare[3] + theSquare[4] + theSquare[5])) { return; }
            if (result != (theSquare[6] + theSquare[7] + theSquare[8])) { return; }
            if (result != (theSquare[0] + theSquare[3] + theSquare[6])) { return; }
            if (result != (theSquare[1] + theSquare[4] + theSquare[7])) { return; }
            if (result != (theSquare[2] + theSquare[5] + theSquare[8])) { return; }
            if (result != (theSquare[0] + theSquare[4] + theSquare[8])) { return; }
            if (result != (theSquare[2] + theSquare[4] + theSquare[6])) { return; }

            Console.WriteLine("Magic square found!");
            Console.WriteLine(theSquare[0].ToString() + " " + theSquare[1].ToString() + " " + theSquare[2].ToString());
            Console.WriteLine(theSquare[3].ToString() + " " + theSquare[4].ToString() + " " + theSquare[5].ToString());
            Console.WriteLine(theSquare[6].ToString() + " " + theSquare[7].ToString() + " " + theSquare[8].ToString());

        }

    }
}
