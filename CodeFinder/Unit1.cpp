/*
    Bible Code Finder
    Finds equidistant letter spacing codes in large text files.
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
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;

TColor myColors[8] = { clBlue, clGreen, clRed, clFuchsia, clNavy, clGray, clPurple };

//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
: TForm(Owner),
  theBuffer(NULL),
  stopped(true)
{
    DragAcceptFiles(this->Handle, TRUE);
    bmp = new Graphics::TBitmap;
    bmp->PixelFormat = pf24bit;

    bmp->Canvas->Font->Assign(FontDialog1->Font);
    charWidth = bmp->Canvas->TextWidth("A");
    charHeight = bmp->Canvas->TextHeight("A");

    SetWindowLong(txtFrameSize->Handle, GWL_STYLE, GetWindowLong(txtFrameSize->Handle, GWL_STYLE) | ES_NUMBER);
    SetWindowLong(txtMinDelta->Handle, GWL_STYLE, GetWindowLong(txtMinDelta->Handle, GWL_STYLE) | ES_NUMBER);
    SetWindowLong(txtMaxDelta->Handle, GWL_STYLE, GetWindowLong(txtMaxDelta->Handle, GWL_STYLE) | ES_NUMBER);
    SetWindowLong(txtMaxResults->Handle, GWL_STYLE, GetWindowLong(txtMaxResults->Handle, GWL_STYLE) | ES_NUMBER);

    Panel2->DoubleBuffered = true;

    RedrawBitmap();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::mnuExitClick(TObject *Sender){
    Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::mnuAboutClick(TObject *Sender){
	MessageBox(this->Handle, L"Bible Code Finder! Also works for texts other than the Bible!\nCopyright © 2007-2013 Dmitry Brant\n\nhttp://dmitrybrant.com", Application->Title.c_str(), MB_ICONINFORMATION);
}
//---------------------------------------------------------------------------

//Handler for Dragging/Dropping files
void __fastcall TForm1::WMDropFiles(TWMDropFiles &message){
    unsigned int filecount = DragQueryFile((HDROP)message.Drop, 0xFFFFFFFF, NULL, 0);
    //for(unsigned int i=0; i<filecount; i++){
    // ok, ok, only do the first file
    if(filecount > 0){
        String fileName;
        fileName.SetLength(MAX_PATH);
        int length = DragQueryFile((HDROP)message.Drop, 0, fileName.c_str(), fileName.Length());
        fileName.SetLength(length);
        ProcessFile(fileName);
    }
    DragFinish((HDROP)message.Drop);
}
//------------------------------------------------------------------


void __fastcall TForm1::FormPaint(TObject *Sender){
    if(!bmp) return;
    BitBlt(lblPic->Canvas->Handle, lblPic->Left, lblPic->Top, lblPic->Width-1, lblPic->Height-1, bmp->Canvas->Handle, 0, 0, SRCCOPY);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormResize(TObject *Sender){
    RedrawBitmap();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Splitter1Moved(TObject *Sender){
    RedrawBitmap();
}
//---------------------------------------------------------------------------


void __fastcall TForm1::mnuOpenClick(TObject *Sender){
    TOpenDialog* OpenDialog1 = new TOpenDialog(this);
    try{
        OpenDialog1->Title = "Open File...";
        OpenDialog1->Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        OpenDialog1->DefaultExt = ".txt";
        OpenDialog1->Options << ofFileMustExist << ofPathMustExist << ofEnableSizing;
        if(!OpenDialog1->Execute()) return;
        ProcessFile(OpenDialog1->FileName);
    }__finally{
        delete OpenDialog1;
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::mnuSaveImageClick(TObject *Sender){
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

void __fastcall TForm1::FormClose(TObject *Sender, TCloseAction &Action){
    if(theBuffer){
        delete [] theBuffer;
        theBuffer = NULL;
    }

    for(int i=0; i<lstResults->Items->Count; i++){
        delete (MyResult*)lstResults->Items->Item[i]->Data;
    }
    lstResults->Items->BeginUpdate();
    lstResults->Items->Clear();
    lstResults->Items->EndUpdate();

    if(bmp) delete bmp;
    bmp = NULL;
}
//---------------------------------------------------------------------------

void TForm1::ProcessFile(String fName){
    int iFileHandle;
    if(theBuffer) delete [] theBuffer;
    theBuffer=NULL;

    for(int i=0; i<lstResults->Items->Count; i++){
        delete (MyResult*)lstResults->Items->Item[i]->Data;
    }
    lstResults->Items->BeginUpdate();
    lstResults->Items->Clear();
    lstResults->Items->EndUpdate();

    char* rawBuffer=NULL;
    Screen->Cursor = crHourGlass;
    try{
        if(!FileExists(fName)) return;
        iFileHandle = FileOpen(fName, fmOpenRead);
        if(iFileHandle == -1){
			MessageBox(this->Handle, L"Could not open the specified file.", L"Error", MB_ICONEXCLAMATION);
			return;
        }
        int fileLength = FileSeek(iFileHandle,0,2);
        FileSeek(iFileHandle,0,0);
        rawBuffer = new char[fileLength+1];
        FileRead(iFileHandle, rawBuffer, fileLength);
        FileClose(iFileHandle);

        //strip whitespace and punctuation from text
        theBuffer = new char[fileLength+1];
        bufSize = 0;
        char *c=rawBuffer, c1;
        for(int i=0; i<fileLength; i++){
            c1 = *c++;
            if((c1 >= 'A') && (c1 <= 'Z')){
                theBuffer[bufSize++] = c1;
            }
            else if(((c1 >= 'a') && (c1 <= 'z'))){
                theBuffer[bufSize++] = c1-32;
            }
        }

        this->Caption = (Application->Title + " - " + fName);
        textPosition = 0;

    }__finally{
        if(rawBuffer){
            delete [] rawBuffer;
        }
        Screen->Cursor = crDefault;
        RedrawBitmap();
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ScrollBar1Scroll(TObject *Sender, TScrollCode ScrollCode, int &ScrollPos){
    ScrollPos = 50;
    if(!theBuffer) return;
    bool scrolled=false;
    if(ScrollCode == scLineUp){
        textPosition -= textCols;
        if(textPosition < 0) textPosition = 0;
        scrolled = true;
    }
    else if(ScrollCode == scLineDown){
        textPosition += textCols;
        if(textPosition >= bufSize) textPosition = bufSize-1;
        scrolled = true;
    }
    else if(ScrollCode == scPageUp){
        textPosition -= (textCols*textRows);
        if(textPosition < 0) textPosition = 0;
        scrolled = true;
    }
    else if(ScrollCode == scPageDown){
        textPosition += (textCols*textRows);
        if(textPosition >= bufSize) textPosition = bufSize-1;
        scrolled = true;
    }
    if(scrolled) RedrawBitmap();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::ScrollBar2Scroll(TObject *Sender, TScrollCode ScrollCode, int &ScrollPos){
    ScrollPos = 50;
    if(!theBuffer) return;
    bool scrolled=false;
    if((ScrollCode == scLineUp) || (ScrollCode == scPageUp)){
        textPosition -= 1;
        if(textPosition < 0) textPosition = 0;
        scrolled = true;
    }
    else if((ScrollCode == scLineDown) || (ScrollCode == scPageDown)){
        textPosition += 1;
        if(textPosition >= bufSize) textPosition = bufSize-1;
        scrolled = true;
    }
    if(scrolled) RedrawBitmap();
}
//---------------------------------------------------------------------------

void TForm1::RedrawBitmap(){
    if(!bmp) return;
    TCanvas *pCanvas = bmp->Canvas;
    int width=lblPic->Width, height=lblPic->Height;
    if(width != bmp->Width) bmp->Width = width;
    if(height != bmp->Height) bmp->Height = height;

    //clear the bitmap first
    pCanvas->Brush->Color = (TColor)0xFFFFFF;
    pCanvas->Brush->Style = bsSolid;
    pCanvas->FillRect(TRect(0, 0, width-1, height-1));
    
    if(theBuffer){

        //calculate number of text rows & columns
        textCols = (lblPic->Width / charWidth);
        textRows = (lblPic->Height / charHeight);
        pCanvas->Pen->Color = clBlack;
        pCanvas->Font->Color = clBlack;

        if(textCols && textRows){
            //draw text
            char* textLine = new char[textCols+1];
            int pos=textPosition, x=lblPic->Left, y=lblPic->Top;
            for(int j=0; j<textRows; j++){
                if(pos >= bufSize) break;
                memcpy(textLine, &theBuffer[pos], pos+textCols < bufSize ? textCols : bufSize - pos);
                textLine[pos+textCols < bufSize ? textCols : bufSize - pos] = 0;
                pos += textCols;
                pCanvas->TextOut(x, y, String(textLine));
                y += charHeight;
            }

            delete [] textLine;

            if(lstResults->Selected){

                MyResult *r = (MyResult*)lstResults->Selected->Data;
                for(int i=0; i<r->numWords; i++){

                    pCanvas->Pen->Color = myColors[i];
                    pCanvas->Pen->Width = 3;
                    pCanvas->Pen->Mode = pmNotXor;
                    int lx, ly;
                    bool lineStarted=false;

                    for(unsigned int j=0; j<r->words[i].length; j++){
                        int localPosition = r->words[i].start - textPosition;
                        localPosition += (j*r->words[i].delta);
                        lx = (localPosition % textCols)*charWidth + (charWidth/2);
                        ly = (localPosition / textCols)*charHeight + (charHeight/2);
                        if((lx<0) || (ly<0)) continue;
                        if((lx>width) || (ly>height)) continue;

                        if(!lineStarted){
                            pCanvas->MoveTo(lx, ly);
                            lineStarted = true;
                        }else{
                            pCanvas->LineTo(lx, ly);
                        }
                    }

                    pCanvas->Brush->Color = myColors[i];
                    pCanvas->Pen->Color = clWhite;
                    pCanvas->Pen->Width = 1;
                    pCanvas->Pen->Mode = pmCopy;
                    pCanvas->Font->Color = clWhite;

                    for(unsigned int j=0; j<r->words[i].length; j++){
                        int localPosition = r->words[i].start - textPosition;
                        localPosition += (j*r->words[i].delta);
                        lx = (localPosition % textCols)*charWidth;
                        ly = (localPosition / textCols)*charHeight;
                        if((lx<0) || (ly<0)) continue;
                        if((lx>width) || (ly>height)) continue;

                        pCanvas->TextOut(lx, ly, String(theBuffer[r->words[i].start+(j*r->words[i].delta)]));
                    }
                }
            }
        }
    }
    
    FormPaint(NULL);
}
//---------------------------------------------------------------------------



void TForm1::LookForWord(int globalOffset, char* buffer, int bufferSize, int whichWord, int maxWords, MyResult* r){
    char* word = theWords[whichWord];
    int wordLen = strlen(word);
    int bufPtr=0, delta, ptr, i;
    bool found, hardBreak=false;
    while(bufPtr < bufferSize){

        for(delta=minDelta; delta<=maxDelta; delta++){

            //search for the word L-to-R
            ptr = bufPtr;
            found = true;
            for(i=0; i<wordLen; i++){
                if(i>0){
                    ptr += delta;
                    if(ptr >= bufferSize){
                        hardBreak = true;
                        found = false;
                        break;
                    }
                }
                if(word[i] != buffer[ptr]){
                    found = false;
                    break;
                }
            }
            if(hardBreak){ hardBreak=false; break; }
            if(found){
                //yes!
                r->words[whichWord].start = globalOffset+bufPtr;
                r->words[whichWord].length = wordLen;
                r->words[whichWord].delta = delta;

                if((whichWord+1) >= maxWords){
                    //found all!
                    r->numWords = maxWords;

					if(lstResults->Items->Count < txtMaxResults->Text.ToInt()){
                        TListItem* item = lstResults->Items->Add();
                        String caption;
                        for(int w=0; w<r->numWords; w++){
                            if(w>0) caption += ", ";
                            caption += (String(r->words[w].start) + "(" + String(r->words[w].delta) + ")");
                        }
                        item->Caption = caption;
                        item->ImageIndex = 1;
                        MyResult* res = new MyResult;
                        memcpy(res, r, sizeof(MyResult));
                        item->Data = (void*)res;
                        
                        if(rbFindOne->Checked){
                            //how do we exit the entire recursion?
                            //like this?
                            stopped = true;
                        }
                    }else{
                        stopped = true;
                    }

                }else{
                    LookForWord(globalOffset, buffer, bufferSize, whichWord+1, maxWords, r);
                }
            }


            //search for the word R-to-L
            ptr = bufPtr;
            found = true;
            for(i=wordLen-1; i>=0; i--){
                if(i<wordLen-1){
                    ptr += delta;
                    if(ptr >= bufferSize){
                        hardBreak = true;
                        found = false;
                        break;
                    }
                }
                if(word[i] != buffer[ptr]){
                    found = false;
                    break;
                }
            }
            if(hardBreak){ hardBreak=false; break; }
            if(found){
                //yes!
                r->words[whichWord].start = globalOffset+bufPtr;
                r->words[whichWord].length = wordLen;
                r->words[whichWord].delta = delta;

                if((whichWord+1) >= maxWords){
                    //found all!
                    r->numWords = maxWords;

                    if(lstResults->Items->Count < txtMaxResults->Text.ToInt()){
                        TListItem* item = lstResults->Items->Add();
                        String caption;
                        for(int w=0; w<r->numWords; w++){
                            if(w>0) caption += ", ";
                            caption += (String(r->words[w].start) + "(" + String(r->words[w].delta) + ")");
                        }
                        item->Caption = caption;
                        item->ImageIndex = 1;
                        MyResult* res = new MyResult;
                        memcpy(res, r, sizeof(MyResult));
                        item->Data = (void*)res;

                        if(rbFindOne->Checked){
                            //how do we exit the entire recursion?
                            //like this?
                            stopped = true;
                        }

                    }else{
                        stopped = true;
                    }

                }else{
                    LookForWord(globalOffset, buffer, bufferSize, whichWord+1, maxWords, r);
                }
            }

        }

        bufPtr++;

    }
}
//---------------------------------------------------------------------------



void __fastcall TForm1::cmdStartClick(TObject *Sender){
    //this ought to be fun...
	int frameSize=txtFrameSize->Text.ToIntDef(2000);
    if(frameSize > bufSize) frameSize = bufSize;
	minDelta=txtMinDelta->Text.ToIntDef(1);
	maxDelta=txtMaxDelta->Text.ToIntDef(300);

    if(!theBuffer){
		MessageBox(this->Handle, L"No file is loaded.", L"Error", MB_ICONEXCLAMATION);
        return;
    }
    if(frameSize < 4){
		MessageBox(this->Handle, L"Frame Size is too small.", L"Error", MB_ICONEXCLAMATION);
        return;
    }
    if(minDelta < 1){
		MessageBox(this->Handle, L"Minimum Delta is too small.", L"Error", MB_ICONEXCLAMATION);
        return;
    }
    if(maxDelta > frameSize){
        MessageBox(this->Handle, L"Maximum Delta is too great.", L"Error", MB_ICONEXCLAMATION);
        return;
    }

    for(int i=0; i<lstResults->Items->Count; i++){
        delete (MyResult*)lstResults->Items->Item[i]->Data;
    }
    lstResults->Items->BeginUpdate();
    lstResults->Items->Clear();
    lstResults->Items->EndUpdate();

    int bufStart=0, numWords=0, wordLen, bufPtr;
    String currentWord;

    for(int z=0; z<7; z++){
        switch(z){
        case 0: currentWord = txtWord1->Text; break;
        case 1: currentWord = txtWord2->Text; break;
        case 2: currentWord = txtWord3->Text; break;
        case 3: currentWord = txtWord4->Text; break;
        case 4: currentWord = txtWord5->Text; break;
        case 5: currentWord = txtWord6->Text; break;
        case 6: currentWord = txtWord7->Text; break;
        }
        if(currentWord == ""){
			numWords = z; break;
		}else{
			strcpy(theWords[z], AnsiString(currentWord.Trim()).c_str());
        }
    }

    if(!numWords){
		MessageBox(this->Handle, L"No words entered for searching.", L"Error", MB_ICONEXCLAMATION);
        return;
    }

    cmdStart->Enabled = false;
    cmdStop->Enabled = true;
    stopped = false;
    Screen->Cursor = crHourGlass;

    MyResult* r = new MyResult;
    DWORD ticks = GetTickCount();
    while((bufStart+frameSize) <= bufSize){

        LookForWord(bufStart, &theBuffer[bufStart], frameSize, 0, numWords, r);

        //bufStart++;
        bufStart += (frameSize/2);

        if((GetTickCount() - ticks) > 500){
            ticks = GetTickCount();
            StatusBar1->Panels->Items[0]->Text = (String((bufStart*100)/bufSize) + "% Complete...");
            Application->ProcessMessages();
        }

        if(stopped) break;
    }

    cmdStart->Enabled = true;
    cmdStop->Enabled = false;
    stopped = true;
    Screen->Cursor = crDefault;

    delete r;
    StatusBar1->Panels->Items[0]->Text = "";

	MessageBox(this->Handle, (String(lstResults->Items->Count) + (lstResults->Items->Count==1 ? " result" : " results") + " found.").c_str(), L"Search Complete", MB_ICONINFORMATION);
    if(lstResults->Items->Count){
        PageControl1->ActivePage = tsResults;
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::lstResultsChange(TObject *Sender, TListItem *Item, TItemChange Change){
    if(lstResults->Selected){
        MyResult* r = (MyResult*)lstResults->Selected->Data;
        textPosition = r->words[0].start;
    }
    RedrawBitmap();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::cmdStopClick(TObject *Sender){
    stopped = true;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormCloseQuery(TObject *Sender, bool &CanClose){
    if(!stopped){
		MessageBox(this->Handle, L"Cannot close while running.", L"Error", MB_ICONEXCLAMATION);
        CanClose = false;
        return;
    }
    CanClose = true;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::mnuSelectFontClick(TObject *Sender){
    FontDialog1->Execute();
    bmp->Canvas->Font->Assign(FontDialog1->Font);
    charWidth = bmp->Canvas->TextWidth("A");
    charHeight = bmp->Canvas->TextHeight("A");
    RedrawBitmap();
}
//---------------------------------------------------------------------------

