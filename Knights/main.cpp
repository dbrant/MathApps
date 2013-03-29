/*
    Using a Neural Network to generate Knight's Tours
    Copyright (C) 2004-2013 Dmitry Brant
    http://dmitrybrant.com

    Some code adapted from:
    http://www.neuro.sfc.keio.ac.jp/~saito/java/knight.html

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

#include <stdio.h>
#include "main.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;

#define XSIZE       80
#define YSIZE       80
#define CIRCLESIZE	8
#define PENWIDTH	9

//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
: TForm(Owner),
  stopped(true),
 V(NULL), U(NULL), D(NULL), A(NULL), CSIZE(0), DSIZE(0), NSIZE(0)
{
    randomize();
    bmp = new Graphics::TBitmap();
    bmp->PixelFormat = pf24bit;
    bmp->Width = 400;
    bmp->Height = 400;
    bmp->Canvas->Brush->Color = clBtnFace;
    bmp->Canvas->Pen->Color = clBtnFace;
    bmp->Canvas->Pen->Width = 1;
    bmp->Canvas->FillRect(::TRect(0,0,bmp->Width,bmp->Height));
    links = new TList();

    lstStats->DoubleBuffered = true;
}
//---------------------------------------------------------------------------

void TForm1::Initialize(){
	int n, m, count;
	int tempCSize = atoi(txtN->Text.t_str());
	int tempDSize = atoi(txtM->Text.t_str());
    if((tempCSize != CSIZE) || (tempDSize != DSIZE)){
        //clear old arrays
        if(V){ for(int i=0; i<NSIZE; i++) delete [] V[i]; delete [] V; }
        if(U){ for(int i=0; i<NSIZE; i++) delete [] U[i]; delete [] U; }
        if(D){ for(int i=0; i<NSIZE; i++) delete [] D[i]; delete [] D; }
        if(A){ for(int i=0; i<NSIZE; i++) delete [] A[i]; delete [] A; }

        for(int i=0; i<links->Count; i++) delete (link*)links->Items[i];
        links->Clear();

        CSIZE = tempCSize;
        DSIZE = tempDSize;
        NSIZE = CSIZE*DSIZE;

		if(bmp->Width < (CSIZE*XSIZE))
			bmp->Width = CSIZE*XSIZE;
		if(bmp->Height < (DSIZE*YSIZE))
			bmp->Height = DSIZE*YSIZE;

        //create new arrays
        V = new int*[NSIZE]; for(int i=0; i<NSIZE; i++) V[i] = new int[NSIZE];
        U = new int*[NSIZE]; for(int i=0; i<NSIZE; i++) U[i] = new int[NSIZE];
        D = new int*[NSIZE]; for(int i=0; i<NSIZE; i++) D[i] = new int[NSIZE];
        A = new int*[NSIZE]; for(int i=0; i<NSIZE; i++) A[i] = new int[2];

        int numLinks;
        if(CSIZE > DSIZE) numLinks = 4*(CSIZE-2)*(CSIZE-1)+1;
        else numLinks = 4*(DSIZE-2)*(DSIZE-1)+1;
        for(i=0; i<numLinks; i++){ link* l = new link; links->Add(l); }
    }

    for(m=0; m < NSIZE; m++) {
	    for(n=0; n < NSIZE; n++) {
	        U[m][n] = -(int)(rand()%CSIZE);
	        V[m][n] = 0;
	    }
    }
    for(m = 0; m < NSIZE; m++) for(n = 0; n < NSIZE; n++) D[m][n] = 0;

    for(i = 0; i < NSIZE; i++) {
      if(i+1 < CSIZE*(i/CSIZE+1) && i+1-(2*CSIZE) >= 0)
        D[i][i+1-(2*CSIZE)] = 1;
      if(i+2 < CSIZE*(i/CSIZE+1) && i+2-CSIZE >= 0)
        D[i][i+2-CSIZE] = 1;
      if(i+2 < CSIZE*(i/CSIZE+1) && i+2+CSIZE < NSIZE)
        D[i][i+2+CSIZE] = 1;
      if(i+1 < CSIZE*(i/CSIZE+1) && i+1+(2*CSIZE) < NSIZE)
        D[i][i+1+(2*CSIZE)] = 1;
      if(i-1 >= CSIZE*(i/CSIZE) && i-1+(2*CSIZE) < NSIZE)
        D[i][i-1+(2*CSIZE)] = 1;
      if(i-2 >= CSIZE*(i/CSIZE) && i-2+CSIZE < NSIZE)
        D[i][i-2+CSIZE] = 1;
      if(i-2 >= CSIZE*(i/CSIZE) && i-2-CSIZE >= 0)
        D[i][i-2-CSIZE] = 1;
      if(i-1 >= CSIZE*(i/CSIZE) && i-1-(2*CSIZE) >= 0)
        D[i][i-1-(2*CSIZE)] = 1;
    }

    count = -1;
    for(i=0; i<NSIZE; i++){
      if(i%CSIZE == 0){
        count++;
        A[i][0] = XSIZE+XSIZE/2;
        A[i][1] = YSIZE+YSIZE/2+YSIZE*count;
      }
      else {
        A[i][0] = XSIZE+XSIZE/2+XSIZE*(i%CSIZE);
        A[i][1] = YSIZE+YSIZE/2+YSIZE*count;
      }
    }
}
//---------------------------------------------------------------------------


void __fastcall TForm1::FormCloseQuery(TObject *Sender, bool &CanClose){
    CanClose = stopped;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormClose(TObject *Sender, TCloseAction &Action){
    //clear old arrays
    if(V){ for(int i=0; i<NSIZE; i++) delete [] V[i]; delete [] V; }
    if(U){ for(int i=0; i<NSIZE; i++) delete [] U[i]; delete [] U; }
    if(D){ for(int i=0; i<NSIZE; i++) delete [] D[i]; delete [] D; }
    if(A){ for(int i=0; i<NSIZE; i++) delete [] A[i]; delete [] A; }
    delete bmp;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::cmdGoClick(TObject *Sender){

	int newCSIZE = atoi(txtN->Text.t_str());
    if(newCSIZE < 3){
		ShowMessage("Invalid n! Must be >= 3");
        return;
	}
	int newDSIZE = atoi(txtM->Text.t_str());
    if(newDSIZE < 3){
		ShowMessage("Invalid m! Must be >= 3");
        return;
    }

	int targetTrials = atoi(txtTrials->Text.t_str());
    int totalTrials=0, numHamiltonian=0, numNonHamiltonian=0, numDiverge=0;
    int numHamiltonianEpochs=0, numNonHamiltonianEpochs=0;
    stopped = false;
    cmdGo->Enabled = false;
    do{
    
        Initialize();

        int n;
        int t=0, diag=1, dU, k;
        register int sum_row;
        register int sum_col;
        int *U_, *V_, *D_;

        while(diag>0) {
            diag = 0; n = 1;

            for(i = 0; i < NSIZE; i++) {
              U_ = U[i];
              V_ = V[i];
              D_ = D[i];
              for(j = n; j < NSIZE; j++) {

                if(D_[j] == 1) {
                    sum_row = 0; sum_col = 0;
                    for(k=0; k<NSIZE; k++) {
                        if(D_[k])
                            sum_row += V_[k]; //(V[i][k]*D[i][k]);
                        if(D[k][j])
                            sum_col += V[k][j]; //(V[k][j]*D[k][j]);
                    }
                    dU = -(sum_row-2)-(sum_col-2);
                }else {
                    dU = 0;
                }

                U_[j] += dU;

                if(U_[j] > 10) U_[j] = 10;
                if(U_[j] < -10) U_[j] = -10;
                if(U_[j] > 3) V_[j] = 1;
                if(U_[j] < 0 ) V_[j] = 0;
	    
                if(V_[j] == 1) V[j][i] = 1;
                if(V_[j] == 0) V[j][i] = 0;

                diag += (dU == 0 ? 0 : 1);
              }
              n++;
            }

            if(t>1000) break;
            t++;

        }

        if(stopped) break;

        if(t>1000){
            numDiverge++;
            hamiltonian = false;
            if(chkShowAll->Checked) DrawNeurons();
        }else if(CheckHamiltonian()){
            numHamiltonian++;
            hamiltonian = true;
            numHamiltonianEpochs += t;
            DrawNeurons();
        }else{
            hamiltonian = false;
            numNonHamiltonian++;
            numNonHamiltonianEpochs += t;
            if(chkShowAll->Checked) DrawNeurons();
        }
        totalTrials++;

        lstStats->Items->BeginUpdate();
        lstStats->Items->Item[0]->SubItems->Strings[0] = String(numHamiltonian);
        lstStats->Items->Item[0]->SubItems->Strings[1] = String(numHamiltonian*100/totalTrials);
        lstStats->Items->Item[0]->SubItems->Strings[2] = String(numHamiltonian > 0 ? numHamiltonianEpochs/numHamiltonian : 0);
        lstStats->Items->Item[1]->SubItems->Strings[0] = String(numNonHamiltonian);
        lstStats->Items->Item[1]->SubItems->Strings[1] = String(numNonHamiltonian*100/totalTrials);
        lstStats->Items->Item[1]->SubItems->Strings[2] = String(numNonHamiltonian > 0 ? numNonHamiltonianEpochs/numNonHamiltonian : 0);
        lstStats->Items->Item[2]->SubItems->Strings[0] = String(numDiverge);
        lstStats->Items->Item[2]->SubItems->Strings[1] = String(numDiverge*100/totalTrials);
        lstStats->Items->Item[3]->SubItems->Strings[0] = String(totalTrials);
        lstStats->Items->EndUpdate();

        Application->ProcessMessages();

    }while(totalTrials < targetTrials);

    cmdGo->Enabled = true;
    stopped = true;
}
//---------------------------------------------------------------------------

bool TForm1::CheckHamiltonian(){
    int p=1;
    int linkNum=0, linkCount;
    for(int m=0; m<NSIZE; m++) {
        for(int n=p; n<NSIZE; n++) {
            if(V[m][n] == 1) {
                link* l = (link*)links->Items[linkNum++];
                l->i = m; l->j = n; l->visited = false;
            }
        }
        p++;
    }
    linkCount = linkNum;
    //now traverse links
    linkNum=0;
    int numTraversed=0;
    link* l = (link*)links->Items[linkNum];
    int startPt = l->i;
    int nextPt = l->j;
    bool found;
    while(1){
        l->visited = true;
        found = false;
        //find a link with the next endpoint
        for(i=0; i<linkCount; i++){
            link* l2 = (link*)links->Items[i];
            if(l2->visited) continue;
            if(l2->i == nextPt){
                l = l2; nextPt = l2->j; found = true;
                numTraversed++; break;
            }
            if(l2->j == nextPt){
                l = l2; nextPt = l2->i; found = true;
                numTraversed++; break;
            }
        }
        if(!found) break;
        if((nextPt == startPt) || (numTraversed >= (linkCount-1))) break;
    }
    if((nextPt == startPt) && (numTraversed >= (linkCount-1))){
        return true;
    }
    return false;
}
//---------------------------------------------------------------------------

void TForm1::DrawNeurons(){

	TCanvas* canvas = bmp->Canvas;

	//first, clear everything
	canvas->Brush->Color = clBtnFace;
	canvas->Pen->Color = clBtnFace;
	canvas->Pen->Width = 1;
	canvas->FillRect(::TRect(0, 0, bmp->Width, bmp->Height));

	int n, m, p;

	canvas->Brush->Color = clWhite;
	canvas->FillRect(::TRect(XSIZE, YSIZE, (CSIZE+1)*XSIZE, (DSIZE+1)*YSIZE));
	canvas->Pen->Color = clBlack;

	for(int n=0; n<CSIZE; n++){
		for(int m=0; m<DSIZE; m++){
			if(n%2){
			  if(m%2)
				BitBlt(canvas->Handle, XSIZE*n, YSIZE*m, ImageA->Width, ImageA->Height, ImageA->Canvas->Handle, 0, 0, SRCCOPY);
			  else
				BitBlt(canvas->Handle, XSIZE*n, YSIZE*m, ImageB->Width, ImageB->Height, ImageB->Canvas->Handle, 0, 0, SRCCOPY);
			}else{
			  if(m%2)
				BitBlt(canvas->Handle, XSIZE*n, YSIZE*m, ImageB->Width, ImageB->Height, ImageB->Canvas->Handle, 0, 0, SRCCOPY);
			  else
				BitBlt(canvas->Handle, XSIZE*n, YSIZE*m, ImageA->Width, ImageA->Height, ImageA->Canvas->Handle, 0, 0, SRCCOPY);
			}
		}
	}


    p = 1;

    if(hamiltonian){
		canvas->Pen->Width = PENWIDTH;
		canvas->Pen->Color = clRed;
    }else{
        canvas->Pen->Width = 1;
		canvas->Pen->Color = clBlue;
	}

	String pointStr;

	for(m=0; m<NSIZE; m++) {
		for(n=p; n<NSIZE; n++) {
			if(V[m][n] == 1) {
				canvas->MoveTo(A[m][0]-XSIZE, A[m][1]-YSIZE);
				canvas->LineTo(A[n][0]-XSIZE, A[n][1]-YSIZE);
				canvas->Ellipse(A[m][0]-CIRCLESIZE-XSIZE, A[m][1]-CIRCLESIZE-YSIZE, A[m][0]+CIRCLESIZE-XSIZE, A[m][1]+CIRCLESIZE-YSIZE);
				canvas->Ellipse(A[n][0]-CIRCLESIZE-XSIZE, A[n][1]-CIRCLESIZE-YSIZE, A[n][0]+CIRCLESIZE-XSIZE, A[n][1]+CIRCLESIZE-YSIZE);

				//pointStr += (String((A[m][0] - 40) / 80) + ", " + String((A[m][1] - 40) / 80) + "\r\n");
			}
		}
	    p++;
	}

	txtPoints->Text = pointStr;

	TForm1::FormPaint(NULL);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormPaint(TObject *Sender){
	BitBlt(this->Canvas->Handle, lblPic->Left+2, lblPic->Top+2, bmp->Width, bmp->Height, bmp->Canvas->Handle, 0, 0, SRCCOPY);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::cmdStopClick(TObject *Sender){
	stopped = true;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::FormKeyDown(TObject *Sender, WORD &Key, TShiftState Shift)
{
    if(Key == VK_F1){
		Application->MessageBox(L"Knight's Tours with a Neural Network\nCopyright 2004 Dmitry Brant\nhttp://dmitrybrant.com", L"About...", MB_ICONINFORMATION);
    }
}
//---------------------------------------------------------------------------



void __fastcall TForm1::btnSaveClick(TObject *Sender){
	TSaveDialog* SaveDialog1 = new TSaveDialog(Form1);
	try{
		SaveDialog1->Title = "Save Image";
		SaveDialog1->Filter = "Bitmap Image (*.bmp)|*.bmp|All Files (*.*)|*.*";
		SaveDialog1->FilterIndex = 1;
		SaveDialog1->DefaultExt = ".bmp";
		SaveDialog1->FileName = "Untitled.bmp";
		SaveDialog1->Options << ofEnableSizing;
		if(!SaveDialog1->Execute()) return;
		try{
			bmp->SaveToFile(SaveDialog1->FileName);
		}catch(Exception &exception){
			Application->ShowException(&exception);
			return;
		}
	}__finally{
		delete SaveDialog1;
	}
}
//---------------------------------------------------------------------------





