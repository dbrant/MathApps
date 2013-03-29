/*
    Prime Spiral Generator
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
#include <ExtCtrls.hpp>
#include <ComCtrls.hpp>
#include <Dialogs.hpp>
#include <XPMan.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
    TScrollBox *ScrollBox1;
    TColorDialog *ColorDialog1;
    TTabControl *TabControl1;
    TLabel *Label1;
    TEdit *txtNumIntegers;
    TShape *shpBackground;
    TLabel *Label2;
    TLabel *Label3;
    TShape *shpPrimes;
    TShape *shpTwinPrimes;
    TLabel *Label4;
    TLabel *Label5;
    TShape *shpMersennePrimes;
    TButton *cmdGo;
    TButton *cmdSave;
    TProgressBar *ProgressBar1;
    TLabel *Label6;
    TLabel *lblNumPrimes;
    TLabel *Label7;
    TEdit *txtPolX2;
    TEdit *txtPolX;
    TEdit *txtPolC;
    TLabel *Label8;
    TLabel *Label9;
    TPaintBox *PaintBox1;
    void __fastcall cmdGoClick(TObject *Sender);
    void __fastcall cmdAnalyzeClick(TObject *Sender);
    void __fastcall cmdSaveClick(TObject *Sender);
    void __fastcall shpBackgroundMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
    void __fastcall PaintBox1Paint(TObject *Sender);
    void __fastcall FormDestroy(TObject *Sender);
    void __fastcall FormCloseQuery(TObject *Sender, bool &CanClose);
private:	// User declarations

    Graphics::TBitmap* bmp;
    bool working;

public:		// User declarations
    __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
