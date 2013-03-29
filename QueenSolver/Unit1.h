/*
    n-queen puzzle solver
    Copyright (C) 2002-2007 Dmitry Brant
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
#include <ToolWin.hpp>
#include <ImgList.hpp>
#include <Menus.hpp>
#include <XPMan.hpp>
//---------------------------------------------------------------------------

class TheThread;

class TForm1 : public TForm
{
__published:	// IDE-managed Components
    TListView *lstSolutions;
    TCoolBar *CoolBar1;
    TStatusBar *StatusBar1;
    TSplitter *Splitter1;
        TImageList *ImageList2;
    TToolBar *ToolBar1;
    TButton *cmdGo;
    TLabel *Label1;
    TEdit *txtN;
    TUpDown *UpDown1;
    TProgressBar *ProgressBar1;
    TLabel *Label2;
    TLabel *Label3;
    TEdit *txtSize;
    TUpDown *UpDown2;
    TLabel *Label4;
    TPopupMenu *PopupMenu1;
    TMenuItem *mnuSave;
    TMenuItem *N1;
    TMenuItem *mnuShowAll;
    TMenuItem *mnuShowDistinct;
    TMenuItem *mnuJustCheck;
    TXPManifest *XPManifest1;
    void __fastcall cmdGoClick(TObject *Sender);
    void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
    void __fastcall lstSolutionsChange(TObject *Sender, TListItem *Item,
          TItemChange Change);
    void __fastcall FormPaint(TObject *Sender);
    void __fastcall FormResize(TObject *Sender);
    void __fastcall txtSizeChange(TObject *Sender);
    void __fastcall mnuSaveClick(TObject *Sender);
    void __fastcall mnuShowAllClick(TObject *Sender);
private:	// User declarations

    Graphics::TBitmap* bmp;
    TheThread* thread;
    int N;

public:		// User declarations
    bool started;
    __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
