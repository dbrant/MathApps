/*
    Using a Neural Network to generate Knight's Tours
    Copyright (C) 2004-2007 Dmitry Brant
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

#ifndef mainH
#define mainH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ComCtrls.hpp>
#include <ExtCtrls.hpp>
#include <XPMan.hpp>
#include <Graphics.hpp>
//---------------------------------------------------------------------------


class TForm1 : public TForm
{
__published:	// IDE-managed Components
    TLabel *lblPic;
    TLabel *Label2;
    TListView *lstStats;
    TGroupBox *GroupBox1;
    TLabel *Label1;
    TLabel *Label3;
    TLabel *Label4;
    TEdit *txtN;
    TButton *cmdGo;
    TUpDown *UpDown1;
    TButton *cmdStop;
    TEdit *txtTrials;
    TUpDown *UpDown2;
    TEdit *txtM;
    TUpDown *UpDown3;
    TCheckBox *chkShowAll;
    TXPManifest *XPManifest1;
	TButton *btnSave;
	TImage *ImageA;
	TImage *ImageB;
	TMemo *txtPoints;
	TLabel *Label5;
    void __fastcall cmdGoClick(TObject *Sender);
    void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
    void __fastcall FormPaint(TObject *Sender);
    void __fastcall cmdStopClick(TObject *Sender);
    void __fastcall FormKeyDown(TObject *Sender, WORD &Key,
          TShiftState Shift);
    void __fastcall FormCloseQuery(TObject *Sender, bool &CanClose);
	void __fastcall btnSaveClick(TObject *Sender);
private:	// User declarations

    void Initialize();
    void DrawNeurons();
    bool CheckHamiltonian();
    bool hamiltonian;

    int i, j;
    int **V, **U, **D, **A;
    int CSIZE, DSIZE, NSIZE, flag;

    Graphics::TBitmap* bmp;
    bool stopped;
    TList* links;

public:		// User declarations
    __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------



typedef struct _link{
    int i;
    int j;
    bool visited;
}link;




extern PACKAGE TForm1 *Form1;


//---------------------------------------------------------------------------
#endif

