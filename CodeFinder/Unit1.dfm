object Form1: TForm1
  Left = 178
  Top = 156
  HorzScrollBar.Visible = False
  VertScrollBar.Visible = False
  Caption = 'Bible Code Finder'
  ClientHeight = 472
  ClientWidth = 687
  Color = clBtnFace
  DoubleBuffered = True
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  Menu = MainMenu1
  OldCreateOrder = False
  Position = poScreenCenter
  OnClose = FormClose
  OnCloseQuery = FormCloseQuery
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object Splitter1: TSplitter
    Left = 558
    Top = 0
    Width = 6
    Height = 452
    Align = alRight
    AutoSnap = False
    Color = clBtnFace
    ParentColor = False
    ResizeStyle = rsUpdate
    OnMoved = Splitter1Moved
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 452
    Width = 687
    Height = 20
    Panels = <
      item
        Style = psOwnerDraw
        Width = 50
      end>
  end
  object Panel1: TPanel
    Left = 564
    Top = 0
    Width = 123
    Height = 452
    Align = alRight
    BevelOuter = bvNone
    TabOrder = 1
    object PageControl1: TPageControl
      Left = 0
      Top = 0
      Width = 123
      Height = 452
      ActivePage = tsSettings
      Align = alClient
      TabOrder = 0
      object tsSettings: TTabSheet
        Caption = 'Settings'
        DesignSize = (
          115
          424)
        object Label2: TLabel
          Left = 10
          Top = 208
          Width = 56
          Height = 13
          Alignment = taRightJustify
          Anchors = [akTop, akRight]
          Caption = 'Frame Size:'
          Transparent = True
        end
        object Label3: TLabel
          Left = 18
          Top = 232
          Width = 48
          Height = 13
          Alignment = taRightJustify
          Anchors = [akTop, akRight]
          Caption = 'Min Delta:'
          Transparent = True
        end
        object Label4: TLabel
          Left = 14
          Top = 256
          Width = 52
          Height = 13
          Alignment = taRightJustify
          Anchors = [akTop, akRight]
          Caption = 'Max Delta:'
          Transparent = True
        end
        object Label5: TLabel
          Left = 4
          Top = 388
          Width = 62
          Height = 13
          Alignment = taRightJustify
          Anchors = [akTop, akRight]
          Caption = 'Max Results:'
          Transparent = True
        end
        object GroupBox1: TGroupBox
          Left = 8
          Top = 4
          Width = 100
          Height = 193
          Anchors = [akLeft, akTop, akRight]
          Caption = 'Words to Find'
          TabOrder = 0
          DesignSize = (
            100
            193)
          object txtWord1: TEdit
            Left = 8
            Top = 20
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 0
          end
          object txtWord2: TEdit
            Left = 8
            Top = 44
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 1
          end
          object txtWord3: TEdit
            Left = 8
            Top = 68
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 2
          end
          object txtWord4: TEdit
            Left = 8
            Top = 92
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 3
          end
          object txtWord5: TEdit
            Left = 8
            Top = 116
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 4
          end
          object txtWord6: TEdit
            Left = 8
            Top = 140
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 5
          end
          object txtWord7: TEdit
            Left = 8
            Top = 164
            Width = 84
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            CharCase = ecUpperCase
            TabOrder = 6
          end
        end
        object txtFrameSize: TEdit
          Left = 71
          Top = 204
          Width = 37
          Height = 21
          Anchors = [akTop, akRight]
          MaxLength = 5
          TabOrder = 1
          Text = '2000'
        end
        object txtMinDelta: TEdit
          Left = 71
          Top = 228
          Width = 37
          Height = 21
          Anchors = [akTop, akRight]
          MaxLength = 4
          TabOrder = 2
          Text = '1'
        end
        object txtMaxDelta: TEdit
          Left = 71
          Top = 252
          Width = 37
          Height = 21
          Anchors = [akTop, akRight]
          MaxLength = 4
          TabOrder = 3
          Text = '300'
        end
        object cmdStart: TButton
          Left = 16
          Top = 284
          Width = 84
          Height = 23
          Anchors = [akLeft, akTop, akRight]
          Caption = '&Start'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 4
          OnClick = cmdStartClick
        end
        object cmdStop: TButton
          Left = 16
          Top = 312
          Width = 84
          Height = 23
          Anchors = [akLeft, akTop, akRight]
          Caption = 'St&op'
          Enabled = False
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = [fsBold]
          ParentFont = False
          TabOrder = 5
          OnClick = cmdStopClick
        end
        object rbFindOne: TRadioButton
          Left = 20
          Top = 348
          Width = 69
          Height = 13
          Caption = 'Find One'
          Checked = True
          TabOrder = 6
          TabStop = True
        end
        object rbFindAll: TRadioButton
          Left = 20
          Top = 364
          Width = 69
          Height = 13
          Caption = 'Find All'
          TabOrder = 7
        end
        object txtMaxResults: TEdit
          Left = 71
          Top = 384
          Width = 37
          Height = 21
          Anchors = [akTop, akRight]
          MaxLength = 5
          TabOrder = 8
          Text = '1000'
        end
      end
      object tsResults: TTabSheet
        Caption = 'Results'
        ImageIndex = 1
        ExplicitLeft = 0
        ExplicitTop = 0
        ExplicitWidth = 0
        ExplicitHeight = 0
        object lstResults: TListView
          Left = 0
          Top = 0
          Width = 115
          Height = 424
          Align = alClient
          Columns = <
            item
              Caption = 'Result'
              Width = 200
            end>
          HideSelection = False
          ReadOnly = True
          SmallImages = ImageList1
          TabOrder = 0
          ViewStyle = vsReport
          OnChange = lstResultsChange
        end
      end
    end
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 558
    Height = 452
    Align = alClient
    BevelOuter = bvNone
    TabOrder = 2
    object lblPic: TPaintBox
      Left = 0
      Top = 0
      Width = 541
      Height = 435
      Align = alClient
      Color = clNone
      ParentColor = False
      OnPaint = FormPaint
    end
    object ScrollBar1: TScrollBar
      Left = 541
      Top = 0
      Width = 17
      Height = 435
      Align = alRight
      Kind = sbVertical
      PageSize = 0
      Position = 50
      TabOrder = 0
      TabStop = False
      OnScroll = ScrollBar1Scroll
    end
    object Panel3: TPanel
      Left = 0
      Top = 435
      Width = 558
      Height = 17
      Align = alBottom
      BevelOuter = bvNone
      TabOrder = 1
      object Label1: TLabel
        Left = 541
        Top = 0
        Width = 17
        Height = 17
        Align = alRight
        AutoSize = False
      end
      object ScrollBar2: TScrollBar
        Left = 0
        Top = 0
        Width = 541
        Height = 17
        Align = alClient
        PageSize = 0
        Position = 50
        TabOrder = 0
        TabStop = False
        OnScroll = ScrollBar2Scroll
      end
    end
  end
  object MainMenu1: TMainMenu
    Left = 12
    Top = 12
    object File1: TMenuItem
      Caption = 'File'
      object mnuOpen: TMenuItem
        Caption = 'Open Text...'
        OnClick = mnuOpenClick
      end
      object N2: TMenuItem
        Caption = '-'
      end
      object mnuSaveImage: TMenuItem
        Caption = 'Save Image...'
        OnClick = mnuSaveImageClick
      end
      object N1: TMenuItem
        Caption = '-'
      end
      object mnuExit: TMenuItem
        Caption = 'Exit'
        OnClick = mnuExitClick
      end
    end
    object Options1: TMenuItem
      Caption = 'Options'
      object mnuSelectFont: TMenuItem
        Caption = 'Select Font...'
        OnClick = mnuSelectFontClick
      end
    end
    object Help1: TMenuItem
      Caption = 'Help'
      object mnuAbout: TMenuItem
        Caption = 'About...'
        ShortCut = 112
        OnClick = mnuAboutClick
      end
    end
  end
  object ImageList1: TImageList
    Left = 44
    Top = 12
    Bitmap = {
      494C010102000400080010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000400000001000000001002000000000000010
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000800000008000000080000000800000008000
      0000800000008000000080000000800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000080000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000080000000FFFFFF0000000000000000000000
      00000000000000000000FFFFFF00800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000080000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000080808000808080008080800000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0080000000FFFFFF0000000000000000000000
      00000000000000000000FFFFFF00800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008080800080808000808080008080800080808000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      000000000000000000000000000080000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000080808000FFFFFF00808080008080800080808000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0080000000FFFFFF000000000000000000FFFF
      FF00800000008000000080000000800000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000080808000FFFFFF00808080008080800080808000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      000000000000000000000000000080000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0080000000FFFFFF0080000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000080808000808080008080800000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0080000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00800000008000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      000000000000FFFFFF0000000000800000008000000080000000800000008000
      0000800000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000424D3E000000000000003E000000
      2800000040000000100000000100010000000000800000000000000000000000
      000000000000000000000000FFFFFF00FFFFFFFF00000000FFFFFFFF00000000
      FFFFFE0000000000FFFFFE0000000000FFFFFE0000000000FFFF800000000000
      FC7F800000000000F83F800000000000F83F800000000000F83F800100000000
      FC7F800300000000FFFF800700000000FFFF807F00000000FFFF80FF00000000
      FFFF81FF00000000FFFFFFFF0000000000000000000000000000000000000000
      000000000000}
  end
  object FontDialog1: TFontDialog
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Courier New'
    Font.Pitch = fpFixed
    Font.Style = []
    Options = [fdEffects, fdFixedPitchOnly, fdShowHelp]
    Left = 76
    Top = 12
  end
  object XPManifest1: TXPManifest
    Left = 8
    Top = 48
  end
end
