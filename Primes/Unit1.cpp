/*
    Prime Spiral Generator
    Copyright (C) 2007-2013 Dmitry Brant
    http://dmitrybrant.com

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/
//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include <math.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
: TForm(Owner),
  bmp(NULL),
  working(false)
{
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormCloseQuery(TObject *Sender, bool &CanClose){
    CanClose = !working;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormDestroy(TObject *Sender){
    if(bmp) delete bmp;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::cmdGoClick(TObject *Sender)
{
    //generate sieve
	int numIntegers = txtNumIntegers->Text.ToIntDef(0);
    bool* sieve = new bool[numIntegers];

    int powersof2m1[32] = {0};

    Screen->Cursor = crHourGlass;
    cmdGo->Enabled = false;
    cmdSave->Enabled = false;
    working = true;
    try{

        //clear arrays
        memset(sieve, 0, sizeof(bool)*numIntegers);
        // 0 and 1 are not primes
        sieve[0] = true;
        sieve[1] = true;

        for(int i=0; i<31; i++) powersof2m1[i] = (1<<i)-1;

        if(bmp) delete bmp;
        bmp = new Graphics::TBitmap();
        bmp->PixelFormat = pf4bit;
        bmp->Width = (sqrt(numIntegers)+2);
        bmp->Height = bmp->Width;

        //resize the image
        PaintBox1->Width = (sqrt(numIntegers)+2);
        PaintBox1->Height = PaintBox1->Width;

        TCanvas* canvas = bmp->Canvas;

        //set up colors
        TColor backColor=shpBackground->Brush->Color;
        TColor primeColor=shpPrimes->Brush->Color;
        TColor twinColor=shpTwinPrimes->Brush->Color;
        TColor mersenneColor=shpMersennePrimes->Brush->Color;

        int totalPrimes=0;
        int numOne=0;
        int numThree=0;
        int numSeven=0;
        int numNine=0;

        int x=PaintBox1->Width/2, y=PaintBox1->Height/2;
        int direction=1, moves=1, move=0, moveswitch=0;
        canvas->MoveTo(x, y);
        canvas->Brush->Color = backColor;
        canvas->Pen->Color = clWhite;
        canvas->FillRect(::Rect(0, 0, bmp->Width, bmp->Height));
        canvas->Brush->Style = bsClear;

        int mult, prevPrime=1;
        bool found;
        DWORD col;

        //generate the sieve
        for(int i=2; i<numIntegers; i++){
            found = false;
            if(!sieve[i]){  //found a new prime
                totalPrimes++;

                mult = i;
                while(1){
                    mult += i;
                    if(mult >= numIntegers) break;
                    sieve[mult] = true;
                }
            }

            if((i % 1000000) == 0){
                ProgressBar1->Position = __int64(i)*__int64(100)/__int64(numIntegers);
                Application->ProcessMessages();
            }
        }

        int n, numMoves, primeIndex=0;
		int polX2=txtPolX2->Text.ToIntDef(0);
		int polX=txtPolX->Text.ToIntDef(0);
		int polC=txtPolC->Text.ToIntDef(0);

        for(int i=0; i<2000000000; i++){

            found=false;

            n = polX2*i*i + polX*i + polC;

            if(n < 0) continue;
            if(n > numIntegers) break;

            if(!sieve[n]){     //it's a prime!
                found = true;
                primeIndex++;

                if((n - prevPrime) == 2)  //twin prime!
                    SetPixel(canvas->Handle, x, y, twinColor);
                else
                    SetPixel(canvas->Handle, x, y, primeColor);

                for(int k=0; k<31; k++)
                if(n == powersof2m1[k]){
                    SetPixel(canvas->Handle, x, y, mersenneColor);
                    //canvas->Ellipse(x-3,y-3,x+3,y+3);
                }

            }



            //for(int k=0; k<numMoves; k++){
                switch(direction){
                case 1:
                    x++;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    x--;
                    break;
                case 4:
                    y++;
                    break;
                }
                move++;
                if(move == moves){
                    move = 0;
                    moveswitch++;
                    if(moveswitch == 2){
                        moveswitch=0;
                        moves++;
                    }
                    direction++;
                    if(direction > 4) direction = 1;
                }
            //}


            if(found) prevPrime = n;
            if((i % 100000) == 0){
                ProgressBar1->Position = __int64(i)*__int64(100)/__int64(numIntegers);
                Application->ProcessMessages();
            }
        }


        lblNumPrimes->Caption = UnicodeString(totalPrimes);

        //repaint it
        PaintBox1Paint(NULL);

    }__finally{
        delete [] sieve;
        ProgressBar1->Position = 0;
        Screen->Cursor = crDefault;
        cmdGo->Enabled = true;
        cmdSave->Enabled = true;
        working = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::cmdAnalyzeClick(TObject *Sender)
{
    //scan for vertical and horizontal lines where primes don't exist
    /*
    //scan vertical down
    DWORD colour;
    cmdAnalyze->Caption = "1";
    for(int i=1; i<Image1->Width; i++){
        for(int j=0; j<Image1->Height; j++){
            colour = GetPixel(Image1->Canvas->Handle, i, j);
            if(colour == 0x0)
                SetPixel(Image1->Canvas->Handle, i, j, 0xFF0000);
            else
                break;
        }
    }


    //scan vertical up
    cmdAnalyze->Caption = "2";
    for(int i=Image1->Width-5; i>0; i--){
        for(int j=Image1->Height-5; j>0; j--){
            colour = GetPixel(Image1->Canvas->Handle, i, j);
            if(colour != 0x0)
                SetPixel(Image1->Canvas->Handle, i, j, 0xFF00FF);
            else
                break;
        }
    }

    /*
    //scan horizontal right
    cmdAnalyze->Caption = "3";
    for(int i=0; i<Image1->Height; i++){
        for(int j=0; j<Image1->Width; j++){
            colour = GetPixel(Image1->Canvas->Handle, j, i);
            if(colour == 0x0)
                SetPixel(Image1->Canvas->Handle, j, i, 0xFF00);
            else
                break;
        }
    }

    //scan horizontal left
    cmdAnalyze->Caption = "4";
    for(int i=Image1->Height-5; i>0; i--){
        for(int j=Image1->Width-5; j>0; j--){
            colour = GetPixel(Image1->Canvas->Handle, j, i);
            if(colour != 0xBBBBBB)
                SetPixel(Image1->Canvas->Handle, j, i, 0xFF);
            else
                break;
        }
    }

    //try diagonally
    cmdAnalyze->Caption = "5";
    int bound = Image1->Width-1, dummy;
    for(int i=0; i<Image1->Width; i++){
        for(int j=0; j<Image1->Height; j++){
            dummy=i+j;
            if(dummy>bound) break;
            colour = GetPixel(Image1->Canvas->Handle, dummy, j);
            if(colour != 0xFFFFFF)
                SetPixel(Image1->Canvas->Handle, dummy, j, 0xA0FF);
            else
                break;
        }
    }

    cmdAnalyze->Caption = "6";
    for(int i=0; i<Image1->Height; i++){
        for(int j=0; j<Image1->Width; j++){
            //if(i+j>=Image1->Width) continue;
            colour = GetPixel(Image1->Canvas->Handle, i, i+j);
            if(colour != 0xFFFFFF)
                SetPixel(Image1->Canvas->Handle, i, i+j, 0xA0FF);
            else
                break;
        }
    }

    cmdAnalyze->Caption = "&Analyze";

    */
}
//---------------------------------------------------------------------------

void __fastcall TForm1::cmdSaveClick(TObject *Sender){
    TSaveDialog* SaveDialog0 = new TSaveDialog(this);
	WideString fileName;
    try{
        SaveDialog0->Title = "Save Image...";
        SaveDialog0->Filter = "Bitmap Files (*.bmp)|*.bmp|All Files (*.*)|*.*";
        SaveDialog0->DefaultExt = ".bmp";
        SaveDialog0->FilterIndex = 1;
        SaveDialog0->FileName = "Untitled.bmp";
        SaveDialog0->Options << ofOverwritePrompt << ofEnableSizing;
        if(!SaveDialog0->Execute()) return;
        fileName = SaveDialog0->FileName;
    }__finally{
        delete SaveDialog0;
    }
    if(FileExists(fileName))
        DeleteFile(fileName);

    bmp->SaveToFile(fileName);
}
//---------------------------------------------------------------------------


void __fastcall TForm1::shpBackgroundMouseDown(TObject *Sender, TMouseButton Button, TShiftState Shift, int X, int Y){
    ColorDialog1->Color = ((TShape*)Sender)->Brush->Color;
    if(!ColorDialog1->Execute()) return;
    ((TShape*)Sender)->Brush->Color = ColorDialog1->Color;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::PaintBox1Paint(TObject *Sender){
    if(!bmp) return;

    TPoint p1 = PaintBox1->ClientToScreen(TPoint(0, 0));
    TPoint p2 = ScrollBox1->ClientToScreen(TPoint(0, 0));
    int dx = p2.x-p1.x;
    int dy = p2.y-p1.y;

    BitBlt(PaintBox1->Canvas->Handle, dx, dy, ScrollBox1->Width, ScrollBox1->Height, bmp->Canvas->Handle, dx, dy, SRCCOPY);
}
//---------------------------------------------------------------------------



