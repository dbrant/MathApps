using System;

namespace MagicSquareFinder
{
    class Program
    {

        const int squareSize = 3;
        const int squaresToTest = 100;

        static long[] theSquare;
        static bool[] squareDirty;
        static long[] squareNumbers;
        static int squareLength;

        static long currentResult, tempResult, tempResult2;

        static int ticks;

        static void Main(string[] args)
        {
            
            squareLength = squareSize * squareSize;
            theSquare = new long[squareLength];
            squareNumbers = new long[squaresToTest];
            squareDirty = new bool[squaresToTest];

            // precalculate squares
            for (int i = 1; i <= squaresToTest; i++)
            {
                squareNumbers[i - 1] = (long)i;// * (long)i;
            }
            
            processCell(0);
            Console.ReadKey();
        }



        private static void processCell(int cellIndex)
        {
            if (Environment.TickCount - ticks > 5000)
            {
                ticks = Environment.TickCount;
                printMagicSquare();
            }
            
            for (int i = 0; i < squaresToTest; i++)
            {
                if (squareDirty[i]) { continue; }

                theSquare[cellIndex] = squareNumbers[i];
                if (cellIndex < squareLength - 1) {

                    if (cellIndex == 2) {
                        currentResult = theSquare[0] + theSquare[1] + theSquare[2];
                    }

                    if (cellIndex == 3)
                    {
                        if (theSquare[0] + theSquare[3] >= currentResult)
                        {
                            break;
                        }
                        squareDirty[i] = true;
                        processCell(cellIndex + 1);
                        squareDirty[i] = false;
                    }
                    else if (cellIndex == 4)
                    {
                        if (theSquare[1] + theSquare[4] >= currentResult
                            || theSquare[3] + theSquare[4] >= currentResult
                            || theSquare[0] + theSquare[4] >= currentResult
                            || theSquare[2] + theSquare[4] >= currentResult)
                        {
                            break;
                        }
                        squareDirty[i] = true;
                        processCell(cellIndex + 1);
                        squareDirty[i] = false;
                    }
                    else if (cellIndex == 5)
                    {
                        tempResult = theSquare[3] + theSquare[4] + theSquare[5];
                        if (tempResult > currentResult)
                        {
                            break;
                        }
                        else if (tempResult == currentResult)
                        {
                            squareDirty[i] = true;
                            processCell(cellIndex + 1);
                            squareDirty[i] = false;
                        }
                    }
                    else if (cellIndex == 6)
                    {
                        tempResult = theSquare[0] + theSquare[3] + theSquare[6];
                        tempResult2 = theSquare[2] + theSquare[4] + theSquare[6];
                        if (tempResult > currentResult)
                        {
                            break;
                        }
                        else if (tempResult2 > currentResult)
                        {
                            break;
                        }
                        else if (tempResult == currentResult && tempResult2 == currentResult)
                        {
                            squareDirty[i] = true;
                            processCell(cellIndex + 1);
                            squareDirty[i] = false;
                        }
                    }
                    else if (cellIndex == 7)
                    {
                        tempResult = theSquare[1] + theSquare[4] + theSquare[7];
                        if (tempResult > currentResult)
                        {
                            break;
                        }
                        else if (tempResult == currentResult)
                        {
                            squareDirty[i] = true;
                            processCell(cellIndex + 1);
                            squareDirty[i] = false;
                        }
                    }
                    else
                    {
                        squareDirty[i] = true;
                        processCell(cellIndex + 1);
                        squareDirty[i] = false;
                    }


                }
                else {
                    verifyMagicSquare();
                }
            }
        }




        private static void verifyMagicSquare()
        {
            if (currentResult != (theSquare[6] + theSquare[7] + theSquare[8])) { return; }
            if (currentResult != (theSquare[2] + theSquare[5] + theSquare[8])) { return; }
            if (currentResult != (theSquare[0] + theSquare[4] + theSquare[8])) { return; }

            Console.WriteLine("----- Magic square found! -----");
            printMagicSquare();
            Console.WriteLine("----------");
        }

        private static void printMagicSquare() {
            Console.WriteLine(theSquare[0].ToString() + " " + theSquare[1].ToString() + " " + theSquare[2].ToString());
            Console.WriteLine(theSquare[3].ToString() + " " + theSquare[4].ToString() + " " + theSquare[5].ToString());
            Console.WriteLine(theSquare[6].ToString() + " " + theSquare[7].ToString() + " " + theSquare[8].ToString());
        }


    }
}
