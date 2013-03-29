object Form1: TForm1
  Left = 145
  Top = 225
  HorzScrollBar.Tracking = True
  HorzScrollBar.Visible = False
  VertScrollBar.Tracking = True
  VertScrollBar.Visible = False
  Caption = 'Prime Spiral by Dmitry Brant - http://dmitrybrant.com'
  ClientHeight = 506
  ClientWidth = 725
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCloseQuery = FormCloseQuery
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object ScrollBox1: TScrollBox
    Left = 149
    Top = 0
    Width = 576
    Height = 506
    HorzScrollBar.Tracking = True
    VertScrollBar.Tracking = True
    Align = alClient
    BorderStyle = bsNone
    Color = clAppWorkSpace
    ParentColor = False
    TabOrder = 0
    ExplicitWidth = 419
    ExplicitHeight = 448
    object PaintBox1: TPaintBox
      Left = 0
      Top = 0
      Width = 201
      Height = 185
      OnPaint = PaintBox1Paint
    end
  end
  object TabControl1: TTabControl
    Left = 0
    Top = 0
    Width = 149
    Height = 506
    Align = alLeft
    DoubleBuffered = True
    ParentDoubleBuffered = False
    TabOrder = 1
    Tabs.Strings = (
      'Settings')
    TabIndex = 0
    ExplicitHeight = 448
    DesignSize = (
      149
      506)
    object Label1: TLabel
      Left = 8
      Top = 32
      Width = 98
      Height = 13
      Caption = 'Number of Integers:'
      Transparent = True
    end
    object shpBackground: TShape
      Left = 108
      Top = 96
      Width = 25
      Height = 15
      Anchors = [akTop, akRight]
      Brush.Color = clBlack
      OnMouseDown = shpBackgroundMouseDown
    end
    object Label2: TLabel
      Left = 44
      Top = 96
      Width = 60
      Height = 13
      Alignment = taRightJustify
      Anchors = [akTop, akRight]
      Caption = 'Background:'
      Transparent = True
    end
    object Label3: TLabel
      Left = 69
      Top = 116
      Width = 35
      Height = 13
      Alignment = taRightJustify
      Anchors = [akTop, akRight]
      Caption = 'Primes:'
      Transparent = True
    end
    object shpPrimes: TShape
      Left = 108
      Top = 116
      Width = 25
      Height = 15
      Anchors = [akTop, akRight]
      OnMouseDown = shpBackgroundMouseDown
    end
    object shpTwinPrimes: TShape
      Left = 108
      Top = 136
      Width = 25
      Height = 15
      Anchors = [akTop, akRight]
      Brush.Color = clLime
      OnMouseDown = shpBackgroundMouseDown
    end
    object Label4: TLabel
      Left = 44
      Top = 136
      Width = 60
      Height = 13
      Alignment = taRightJustify
      Anchors = [akTop, akRight]
      Caption = 'Twin Primes:'
      Transparent = True
    end
    object Label5: TLabel
      Left = 19
      Top = 156
      Width = 85
      Height = 13
      Alignment = taRightJustify
      Anchors = [akTop, akRight]
      Caption = 'Mersenne Primes:'
      Transparent = True
    end
    object shpMersennePrimes: TShape
      Left = 108
      Top = 156
      Width = 25
      Height = 15
      Anchors = [akTop, akRight]
      Brush.Color = clRed
      OnMouseDown = shpBackgroundMouseDown
    end
    object Label6: TLabel
      Left = 8
      Top = 348
      Width = 66
      Height = 13
      Caption = 'Primes found:'
      Transparent = True
    end
    object lblNumPrimes: TLabel
      Left = 80
      Top = 348
      Width = 6
      Height = 13
      Caption = '0'
      Transparent = True
    end
    object Label7: TLabel
      Left = 8
      Top = 184
      Width = 54
      Height = 13
      Caption = 'Polynomial:'
      Transparent = True
    end
    object Label8: TLabel
      Left = 36
      Top = 204
      Width = 22
      Height = 13
      Caption = 'n'#178' +'
      Transparent = True
    end
    object Label9: TLabel
      Left = 88
      Top = 204
      Width = 17
      Height = 13
      Caption = 'n +'
      Transparent = True
    end
    object txtNumIntegers: TEdit
      Left = 8
      Top = 48
      Width = 65
      Height = 21
      MaxLength = 9
      TabOrder = 0
      Text = '100000'
    end
    object cmdGo: TButton
      Left = 24
      Top = 252
      Width = 93
      Height = 23
      Caption = '&Draw'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 1
      OnClick = cmdGoClick
    end
    object cmdSave: TButton
      Left = 24
      Top = 284
      Width = 93
      Height = 23
      Caption = '&Save Image...'
      TabOrder = 2
      OnClick = cmdSaveClick
    end
    object ProgressBar1: TProgressBar
      Left = 8
      Top = 324
      Width = 129
      Height = 13
      Anchors = [akLeft, akTop, akRight]
      TabOrder = 3
    end
    object txtPolX2: TEdit
      Left = 8
      Top = 200
      Width = 25
      Height = 21
      MaxLength = 9
      TabOrder = 4
      Text = '0'
    end
    object txtPolX: TEdit
      Left = 60
      Top = 200
      Width = 25
      Height = 21
      MaxLength = 9
      TabOrder = 5
      Text = '1'
    end
    object txtPolC: TEdit
      Left = 107
      Top = 200
      Width = 30
      Height = 21
      MaxLength = 9
      TabOrder = 6
      Text = '0'
    end
  end
  object ColorDialog1: TColorDialog
    Options = [cdFullOpen, cdAnyColor]
    Left = 100
    Top = 64
  end
end
