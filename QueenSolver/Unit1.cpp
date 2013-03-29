/*
    n-queen puzzle solver
    Copyright (C) 2002-2013 Dmitry Brant
    http://dmitrybrant.com

    Borrows some code from
    http://www.apl.jhu.edu/~hall/java/NQueens.java

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
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------






class TheThread : public TThread
{
private:

    int **array;
    int* tempAA;
    int numSolutions, numDistinctSolutions, N, numToGet;
    bool locked;

    void __fastcall Traverse(int row, int& n);
    void __fastcall Rotate(int* a, int& n);
    void __fastcall Reflect(int* a, int& n);
    void __fastcall AddSolution();
    void __fastcall TheEnd();

    void __fastcall Even_Queens_1(int N);
    void __fastcall Even_Queens_2(int N);
    void __fastcall Odd_Queens(int N);
    void __fastcall Queen_Positions(int N);
    int __fastcall Queen_Mod(int R, int N);

protected:
    void __fastcall Execute();
    void __fastcall UpdateProgress();
public:
    __fastcall TheThread(int n);
    __fastcall virtual ~TheThread();
};
//---------------------------------------------------------------------------

__fastcall TheThread::TheThread(int n)
: TThread(true),
  N(n),
  locked(false)
{
    Priority = tpLowest;
    FreeOnTerminate = false;

    array = new int*[n];
    for(int i=0; i<n; i++) array[i] = new int[n];

    tempAA = new int[n];

    numSolutions = 0;
    numDistinctSolutions = 0;

    //initialize
    for(int i=0; i<n; i++) for(int j=0; j<n; j++) array[j][i] = 0;

}
//---------------------------------------------------------------------------

__fastcall TheThread::~TheThread(){
    for(int i=0; i<N; i++) delete [] array[i];
    delete [] array;
    delete [] tempAA;
}
//---------------------------------------------------------------------------

void __fastcall TheThread::Execute(){
    if(N > 4){
        //find at least one solution
        Queen_Positions(N);
        Synchronize(&AddSolution);
        //reinitialize
        numSolutions=0;
        numDistinctSolutions=1;
        for(int i=0; i<N; i++) for(int j=0; j<N; j++) array[j][i] = 0;
    }
    Traverse(0, N);
    Synchronize(&TheEnd);
}
//---------------------------------------------------------------------------

void __fastcall TheThread::TheEnd(){
    Form1->cmdGo->Caption = "&Find Solutions";
    Form1->started = false;
    Form1->ProgressBar1->Position = Form1->ProgressBar1->Max;
    Form1->StatusBar1->SimpleText = ("Distinct solutions: " + String(numDistinctSolutions) + ", total checked: " + String(numSolutions));

    if(locked){
        Form1->lstSolutions->Items->EndUpdate();
    }

    if(Form1->lstSolutions->Items->Count > 0){
        Form1->lstSolutions->Items->Item[0]->Selected = true;
        Form1->lstSolutions->SetFocus();
    }
}
//---------------------------------------------------------------------------









//-------------------------------------------------------------------
// Works for any even N except when N is of form 6K+2 (eg N=8,14,20)
// Since this is simpler than Even_Queens_2, it will be used in
// the cases when both apply (N even but neither of form 6K+2 nor 6K).
// Note that the positions are 1-based, but stored in a 0-based array.
void __fastcall TheThread::Even_Queens_1(int N) {
    // Row 1 to N/2:   Queen on 2*Row
    for(int Row=1; Row<=N/2; Row++) {
        array[Row-1][2*Row-1]=1000;
    }
    // Row N/2+1 to N: Queen on 2*Row-N-1
    for(int Row=N/2+1; Row<=N; Row++) {
        array[Row-1][2*Row-N-1-1]=1000;
    }
}

//-------------------------------------------------------------------
// Works for any even N except when N a multiple of 6. Since
// this is more complicated than Even_Queens_1, it will not be used in
// the cases when both apply (N even but neither of form 6K+2 nor 6K).
// Note that the positions are 1-based, but stored in a 0-based array.
void __fastcall TheThread::Even_Queens_2(int N) {
    // Row 1 to N/2:   Queen on Queen_Mod(Row,N) +1
    for(int Row=1; Row<=N/2; Row++) {
        array[Row-1][Queen_Mod(Row,N)]=1000;
    }
    // Row N/2+1 to N: Queen on N - Queen_Mod(N-Row+1,N)
    for(int Row=N/2+1; Row<=N; Row++) {
        array[Row-1][N-Queen_Mod(N-Row+1,N)-1]=1000;
    }
}

//-------------------------------------------------------------------
// Neither of the even N cases place a queen on position (1,1).
// So this just places the first N-1 queens as on an N-1 sized board,
// then places the last queen on the bottom right position (N,N).
void __fastcall TheThread::Odd_Queens(int N) {
    Queen_Positions(N-1);
    array[N-1][N-1]=1000;
}

//-------------------------------------------------------------------
// Given N, returns an array of the positions for each queen.

void __fastcall TheThread::Queen_Positions(int N) {
    if(((N%2) == 0) && ((N % 6) != 2))
        Even_Queens_1(N);
    else if((N%2) == 0)
        Even_Queens_2(N);
    else
        Odd_Queens(N);
}

//-------------------------------------------------------------------
// Given R, returns (2R + N/2 - 3)mod N
int __fastcall TheThread::Queen_Mod(int R, int N) {
    return((2*R + N/2 -3) % N);
}
//-------------------------------------------------------------------











void __fastcall TheThread::Rotate(int* a, int& n){
    register int i;
    for(i=0; i<n; i++) tempAA[n-1-a[i]] = i;
    memcpy(a, tempAA, n*sizeof(int));
}
//---------------------------------------------------------------------------

void __fastcall TheThread::Reflect(int* a, int& n){
    register int i;
    for(i=0; i<n; i++) a[i] = n-1-a[i];
}
//---------------------------------------------------------------------------

void __fastcall TheThread::AddSolution(){

    numSolutions++;
    register int i;
    register int j;
    register int l;
    int n = N;

    if(Form1->mnuJustCheck->Checked){
        numDistinctSolutions++;
        if((numDistinctSolutions % 64)==0){
            Form1->StatusBar1->SimpleText = String(numDistinctSolutions);
        }
        return;
    }

    int* solution = new int[n];
    for(i=0; i<n; i++){
        for(j=0; j<n; j++){
            if(array[i][j] == 1000){
                solution[i] = j;
                break;
            }
        }
    }

    bool exists=false, reject;

    if(Form1->mnuShowDistinct->Checked){

        if(Form1->lstSolutions->Items->Count > 0){
          while(1){


            for(l=0; l<3; l++){
                //rotate once
                Rotate(solution, n);
                //compare with other solutions
                for(i=Form1->lstSolutions->Items->Count-1; i>=0; i--){
                    register int* a = (int*)Form1->lstSolutions->Items->Item[i]->Data;
                    reject = false;
                    for(j=0; j<n; j++){
                        if(a[j] != solution[j]){ reject = true; break; }
                    }
                    if(reject) continue;
                    exists = true;
                    break;
                }
                if(exists) break;
            }
            if(exists) break;

            Reflect(solution, n);
            //compare with other solutions
            for(i=Form1->lstSolutions->Items->Count-1; i>=0; i--){
                register int* a = (int*)Form1->lstSolutions->Items->Item[i]->Data;
                reject = false;
                for(j=0; j<n; j++){
                    if(a[j] != solution[j]){ reject = true; break; }
                }
                if(reject) continue;
                exists = true;
                break;
            }
            if(exists) break;

            for(l=0; l<3; l++){
                //rotate once
                Rotate(solution, n);
                //compare with other solutions
                for(i=Form1->lstSolutions->Items->Count-1; i>=0; i--){
                    register int* a = (int*)Form1->lstSolutions->Items->Item[i]->Data;
                    reject = false;
                    for(j=0; j<n; j++){
                        if(a[j] != solution[j]){ reject = true; break; }
                    }
                    if(reject) continue;
                    exists = true;
                    break;
                }
                if(exists) break;
            }

            if(!exists){
                //reflect once more
                Reflect(solution, n);
                //compare with other solutions
                for(i=Form1->lstSolutions->Items->Count-1; i>=0; i--){
                    register int* a = (int*)Form1->lstSolutions->Items->Item[i]->Data;
                    reject = false;
                    for(j=0; j<n; j++){
                        if(a[j] != solution[j]){ reject = true; break; }
                    }
                    if(reject) continue;
                    exists = true;
                    break;
                }
            }

            break;
          }
        }
    }

    
    if(!exists){
        numDistinctSolutions++;

        if(!locked){
            Form1->lstSolutions->Items->BeginUpdate();
            locked = true;
        }

        if((numDistinctSolutions % 64)==0){
            Form1->lstSolutions->Items->EndUpdate();
            Form1->lstSolutions->Repaint();
            Form1->lstSolutions->Items->BeginUpdate();
        }

        //add this solution
        TListItem* item = Form1->lstSolutions->Items->Add();
        item->Caption = ("Solution " + String(numDistinctSolutions));
        item->Data = (void*)solution;
        item->ImageIndex = 0;
    }else{
        delete [] solution;
    }
}
//---------------------------------------------------------------------------

void __fastcall TheThread::UpdateProgress(){
    Form1->ProgressBar1->Position++;
}
//---------------------------------------------------------------------------

void __fastcall TheThread::Traverse(int row, int& n){
    register int k;
    register int m;
    register int i;
    register int g;
    //if(row == 0) g = (n+1)/2; else g = n;
    g=n;
    for(i=0; i<g; i++){

        if(row == 1){ Synchronize(&UpdateProgress); }

        //is it a free space?
        if(array[row][i]) continue;

        if(Terminated) return;

        //successful!!
        //now fill them in
        //fill down
        k = row+1;
        while(k<n){ array[k][i]++; k++; }
        //fill down-left
        k = row+1;
        m = i-1;
        while(k<n && m>=0){ array[k][m]++; k++; m--; }
        //fill down-right
        k = row+1;
        m = i+1;
        while(k<n && m<n){ array[k][m]++; k++; m++; }
        //fill left-right
        m = 0;
        while(m<n){ array[row][m]++; m++; }
        array[row][i]=1000;


        //and traverse the next row!
        //oh wait... are we the last row?
        if(row == (n-1)){
            //we're a solution!
            Synchronize(&AddSolution);
        }else{
            Traverse(row+1, n);
        }


        //unfill down
        k = row+1;
        while(k<n){ array[k++][i]--; }
        //unfill down-left
        k = row+1;
        m = i-1;
        while(k<n && m>=0){ array[k][m]--; k++; m--; }
        //unfill down-right
        k = row+1;
        m = i+1;
        while(k<n && m<n){ array[k][m]--; k++; m++; }
        //unfill left-right
        m = 0;
        while(m<n){ array[row][m]--; m++; }
        array[row][i] = 0;

    }
}
//---------------------------------------------------------------------------

















__fastcall TForm1::TForm1(TComponent* Owner)
: TForm(Owner),
  started(false),
  thread(NULL)
{
    bmp = new Graphics::TBitmap;
    bmp->PixelFormat = pf16bit;
    bmp->Width = 0;
    bmp->Height = 0;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::cmdGoClick(TObject *Sender){
    if(!bmp) return;

    if(!started){
        int n = atoi(txtN->Text.t_str());
        if(n<1) return;
        N = n;

        for(int i=0; i<lstSolutions->Items->Count; i++)
            delete [] (int*)(lstSolutions->Items->Item[i]->Data);

        lstSolutions->Items->BeginUpdate();
        lstSolutions->Items->Clear();
        lstSolutions->Items->EndUpdate();

        ProgressBar1->Position = 0;
        ProgressBar1->Max = n*n;

        bmp->Width = 0;
        bmp->Height = 0;
        //clear background
        this->Canvas->Brush->Color = clAppWorkSpace;
        this->Canvas->FillRect(::Rect(lstSolutions->Width+Splitter1->Width, 0, this->ClientWidth, this->ClientHeight));

        if(thread){
            thread->Terminate();
            thread->WaitFor();
            delete thread;
            thread = NULL;
        }

        //start the thread
        thread = new TheThread(n);
        thread->Resume();
        started = true;

        cmdGo->Caption = "Stop";
    }else{
        started = false;
        thread->Terminate();
        thread->WaitFor();
        delete thread;
        thread = NULL;
        cmdGo->Caption = "&Find Solutions";
    }
}
//---------------------------------------------------------------------------


void __fastcall TForm1::FormClose(TObject *Sender, TCloseAction &Action){
    if(thread){
        thread->Terminate();
        thread->WaitFor();
        delete thread;
        thread = NULL;
    }
    for(int i=0; i<lstSolutions->Items->Count; i++)
        delete [] (int*)(lstSolutions->Items->Item[i]->Data);
    lstSolutions->Items->BeginUpdate();
    lstSolutions->Items->Clear();
    lstSolutions->Items->EndUpdate();
    if(bmp){ delete bmp; bmp = NULL; }
}
//---------------------------------------------------------------------------





void __fastcall TForm1::lstSolutionsChange(TObject *Sender, TListItem *Item, TItemChange Change){
    if(!bmp) return;
    if(Change != ctState) return;
    TListItem* item = lstSolutions->Selected;
    String str;
    if(!item) return;
    int* a = (int*)item->Data;

    int blockSize = atoi(txtSize->Text.t_str());
    int myWidth = blockSize*N+1;
    int myHeight = blockSize*N+1;

    TCanvas* canvas = bmp->Canvas;
    bmp->Width = myWidth;
    bmp->Height = myHeight;

    //clear bitmap background
    canvas->Brush->Color = clWhite;
    canvas->FillRect(::Rect(0, 0, myWidth-1, myHeight-1));
    //draw vertical lines
    canvas->Pen->Color = clBlack;
    for(int i=0; i<=N; i++){
        canvas->MoveTo(0, i*blockSize);
        canvas->LineTo(myWidth, i*blockSize);
    }
    for(int i=0; i<=N; i++){
        canvas->MoveTo(i*blockSize, 0);
        canvas->LineTo(i*blockSize, myHeight);
    }
    //draw circles
    canvas->Brush->Color = clRed;
    int offset = (blockSize < 12) ? 1 : 2;
    for(int i=0; i<N; i++){
        canvas->Ellipse(a[i]*blockSize+offset+1, i*blockSize+offset+1, a[i]*blockSize+blockSize-offset, i*blockSize+blockSize-offset);
    }
    FormPaint(NULL);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormPaint(TObject *Sender){
    if(!bmp) return;
    int left = (this->ClientWidth-lstSolutions->Width-Splitter1->Width)/2 - bmp->Width/2 + lstSolutions->Width + Splitter1->Width;
    int top = (this->ClientHeight)/2 - bmp->Height/2;
    BitBlt(this->Canvas->Handle, left, top, bmp->Width, bmp->Height, bmp->Canvas->Handle, 0, 0, SRCCOPY);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormResize(TObject *Sender){
    if(!bmp) return;
    //clear background
    this->Canvas->Brush->Color = clAppWorkSpace;
    this->Canvas->FillRect(::Rect(lstSolutions->Width+Splitter1->Width, 0, this->ClientWidth, this->ClientHeight));
    FormPaint(Sender);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::txtSizeChange(TObject *Sender){
    if(!bmp) return;
    //clear form background
    this->Canvas->Brush->Color = clAppWorkSpace;
    this->Canvas->FillRect(::Rect(lstSolutions->Width+Splitter1->Width, 0, this->ClientWidth, this->ClientHeight));
    lstSolutionsChange(NULL, NULL, ctState);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::mnuSaveClick(TObject *Sender){
    if(!bmp) return;
    TSaveDialog* SaveDialog1 = new TSaveDialog(this);
    try{
        SaveDialog1->Title = "Save Picture As...";
        SaveDialog1->Filter = "Bitmap Files (*.bmp)|*.bmp|All Files (*.*)|*.*";
        SaveDialog1->FilterIndex = 1;
        SaveDialog1->DefaultExt = ".bmp";
        SaveDialog1->FileName = "Untitled.bmp";
        SaveDialog1->Options << ofOverwritePrompt << ofEnableSizing;
        if(!SaveDialog1->Execute()) return;

        bmp->SaveToFile(SaveDialog1->FileName);

    }__finally{
        delete SaveDialog1;
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::mnuShowAllClick(TObject *Sender){
    if(Sender == (TObject*)mnuShowDistinct){
        mnuShowDistinct->Checked = true;
    }else if(Sender == (TObject*)mnuShowAll){
        mnuShowAll->Checked = true;
    }else if(Sender == (TObject*)mnuJustCheck){
        mnuJustCheck->Checked = true;
    }
}
//---------------------------------------------------------------------------

