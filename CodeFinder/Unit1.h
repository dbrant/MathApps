/*
    Bible Code Finder
    Finds equidistant letter spacing codes in large text files.
    Copyright (C) 2007 Dmitry Brant
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

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ComCtrls.hpp>
#include <ExtCtrls.hpp>
#include <Menus.hpp>
#include <ImgList.hpp>
#include <Dialogs.hpp>
#include <XPMan.hpp>
//---------------------------------------------------------------------------


typedef struct _WordResult{
    unsigned int start;     //start of word in the file
    unsigned int length;    //how many letters in the word
    unsigned int delta;     //space between letters in word
}WordResult;

class MyResult{
public:
    MyResult(){ numWords=0; ZeroMemory(words, 7*sizeof(WordResult)); }
    int numWords;
    WordResult words[7];
};



class TForm1 : public TForm
{
__published:	// IDE-managed Components
    TStatusBar *StatusBar1;
    TPanel *Panel1;
    TPageControl *PageControl1;
    TTabSheet *tsSettings;
    TTabSheet *tsResults;
    TGroupBox *GroupBox1;
    TEdit *txtWord1;
    TEdit *txtWord2;
    TEdit *txtWord3;
    TEdit *txtWord4;
    TEdit *txtWord5;
    TEdit *txtWord6;
    TEdit *txtWord7;
    TLabel *Label2;
    TEdit *txtFrameSize;
    TLabel *Label3;
    TEdit *txtMinDelta;
    TLabel *Label4;
    TEdit *txtMaxDelta;
    TListView *lstResults;
    TSplitter *Splitter1;
    TButton *cmdStart;
    TMainMenu *MainMenu1;
    TMenuItem *File1;
    TMenuItem *Help1;
    TMenuItem *mnuAbout;
    TMenuItem *mnuOpen;
    TMenuItem *N1;
    TMenuItem *mnuExit;
    TMenuItem *mnuSaveImage;
    TMenuItem *N2;
    TImageList *ImageList1;
    TPanel *Panel2;
    TScrollBar *ScrollBar1;
    TPanel *Panel3;
    TScrollBar *ScrollBar2;
    TLabel *Label1;
    TPaintBox *lblPic;
    TButton *cmdStop;
    TRadioButton *rbFindOne;
    TRadioButton *rbFindAll;
    TMenuItem *Options1;
    TMenuItem *mnuSelectFont;
    TFontDialog *FontDialog1;
    TLabel *Label5;
    TEdit *txtMaxResults;
    TXPManifest *XPManifest1;
    void __fastcall mnuOpenClick(TObject *Sender);
    void __fastcall ScrollBar1Scroll(TObject *Sender, TScrollCode ScrollCode, int &ScrollPos);
    void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
    void __fastcall mnuExitClick(TObject *Sender);
    void __fastcall FormPaint(TObject *Sender);
    void __fastcall FormResize(TObject *Sender);
    void __fastcall Splitter1Moved(TObject *Sender);
    void __fastcall ScrollBar2Scroll(TObject *Sender, TScrollCode ScrollCode, int &ScrollPos);
    void __fastcall cmdStartClick(TObject *Sender);
    void __fastcall mnuAboutClick(TObject *Sender);
    void __fastcall mnuSaveImageClick(TObject *Sender);
    void __fastcall lstResultsChange(TObject *Sender, TListItem *Item,
          TItemChange Change);
    void __fastcall cmdStopClick(TObject *Sender);
    void __fastcall FormCloseQuery(TObject *Sender, bool &CanClose);
    void __fastcall mnuSelectFontClick(TObject *Sender);
private:	// User declarations
    void ProcessFile(String fName);
    void RedrawBitmap();
    char* theBuffer;
    int bufSize;
    bool stopped;

    char theWords[7][255];
    int minDelta, maxDelta;

    int textPosition, textRows, textCols, charWidth, charHeight;

    void LookForWord(int globalOffset, char* buffer, int bufferSize, int whichWord, int maxWords, MyResult* r);

    Graphics::TBitmap *bmp;
public:		// User declarations
    __fastcall TForm1(TComponent* Owner);
    void __fastcall WMDropFiles(TWMDropFiles&);
    BEGIN_MESSAGE_MAP
    MESSAGE_HANDLER(WM_DROPFILES, TWMDropFiles, WMDropFiles);
    END_MESSAGE_MAP(TForm);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
