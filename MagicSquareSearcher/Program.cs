using System;

namespace MagicSquareFinder
{
    class Program
    {

        const int squareSize = 3;
        const int numberCount = 1000;

        static long[] theSquare;
        static bool[] squareDirty;
        static long[] numberPool;
        static int squareLength;

        static long currentResult;
        static long val4, val5, val6, val7, val8;
        static int ind4, ind5, ind6, ind7, ind8;

        static int ticks;

        static void Main(string[] args)
        {
            
            squareLength = squareSize * squareSize;
            theSquare = new long[squareLength];
            numberPool = new long[numberCount];
            squareDirty = new bool[numberCount];

            // precalculate squares
            for (int i = 1; i <= numberCount; i++)
            {
                numberPool[i - 1] = (long)i * (long)i;
            }
            
            processCell(0);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }



        private static void processCell(int cellIndex)
        {
            if (Environment.TickCount - ticks > 5000)
            {
                ticks = Environment.TickCount;
                printMagicSquare();
            }

            if (cellIndex < 4)
            {
                for (int i = 0; i < numberCount; i++)
                {
                    if (squareDirty[i]) { continue; }
                    theSquare[cellIndex] = numberPool[i];

                    if (cellIndex == 2)
                    {
                        currentResult = theSquare[0] + theSquare[1] + theSquare[2];
                    }

                    squareDirty[i] = true;
                    processCell(cellIndex + 1);
                    squareDirty[i] = false;
                }
            }
            else
            {
                // we're at the 5th cell, so we can construct the rest of the square!

                val6 = currentResult - theSquare[0] - theSquare[3];
                ind6 = indexOf(val6);
                if (ind6 == -1 || squareDirty[ind6]) { return; }
                theSquare[6] = val6;
                squareDirty[ind6] = true;

                val4 = currentResult - theSquare[6] - theSquare[2];
                ind4 = indexOf(val4);
                if (ind4 == -1 || squareDirty[ind4])
                {
                    squareDirty[ind6] = false;
                    return;
                }
                theSquare[4] = val4;
                squareDirty[ind4] = true;

                val5 = currentResult - theSquare[4] - theSquare[3];
                ind5 = indexOf(val5);
                if (ind5 == -1 || squareDirty[ind5])
                {
                    squareDirty[ind4] = false;
                    squareDirty[ind6] = false;
                    return;
                }
                theSquare[5] = val5;
                squareDirty[ind5] = true;

                val7 = currentResult - theSquare[1] - theSquare[4];
                ind7 = indexOf(val7);
                if (ind7 == -1 || squareDirty[ind7])
                {
                    squareDirty[ind4] = false;
                    squareDirty[ind5] = false;
                    squareDirty[ind6] = false;
                    return;
                }
                theSquare[7] = val7;
                squareDirty[ind7] = true;

                val8 = currentResult - theSquare[0] - theSquare[4];
                ind8 = indexOf(val8);
                if (ind8 == -1 || squareDirty[ind8])
                {
                    squareDirty[ind4] = false;
                    squareDirty[ind5] = false;
                    squareDirty[ind6] = false;
                    squareDirty[ind7] = false;
                    return;
                }
                theSquare[8] = val8;

                verifyMagicSquare();

                squareDirty[ind4] = false;
                squareDirty[ind5] = false;
                squareDirty[ind6] = false;
                squareDirty[ind7] = false;
            }
            
        }

        private static int indexOf(long val)
        {
            for (int i = 0; i < numberCount; i++)
            {
                if (numberPool[i] == val)
                {
                    return i;
                }
                else if (numberPool[i] > val)
                {
                    break;
                }
            }
            return -1;
        }


        private static void verifyMagicSquare()
        {
            if (currentResult != (theSquare[6] + theSquare[7] + theSquare[8])) { return; }
            if (currentResult != (theSquare[2] + theSquare[5] + theSquare[8])) { return; }

            Console.WriteLine("----- Magic square found! -----");
            printMagicSquare();
            Console.WriteLine("----- Press any key to continue -----");
            Console.ReadKey();
        }

        private static void printMagicSquare() {
            Console.WriteLine(theSquare[0].ToString() + " " + theSquare[1].ToString() + " " + theSquare[2].ToString());
            Console.WriteLine(theSquare[3].ToString() + " " + theSquare[4].ToString() + " " + theSquare[5].ToString());
            Console.WriteLine(theSquare[6].ToString() + " " + theSquare[7].ToString() + " " + theSquare[8].ToString());
        }


    }
}
