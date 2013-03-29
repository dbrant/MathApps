Public Class Form1

    'Copyright (c) 2009-2013 Dmitry Brant <me@dmitrybrant.com>
    '
    'This software is free software; you can redistribute it and/or modify
    'it under the terms of the GNU General Public License as published by
    'the Free Software Foundation; either version 2 of the License, or
    '(at your option) any later version.
    '
    'This program is distributed in the hope that it will be useful,
    'but WITHOUT ANY WARRANTY; without even the implied warranty of
    'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    'GNU General Public License for more details.
    '
    'You should have received a copy of the GNU General Public License along
    'with this program; if not, write the Free Software Foundation, Inc., 51
    'Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

    Dim bmp As Bitmap
    Dim startX, startY
    Dim loaded As Boolean = False

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        bmp = Nothing
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bmp = New Bitmap(PictureBox1.Width, PictureBox1.Width, Imaging.PixelFormat.Format32bppArgb)
        startX = PictureBox1.Width / 2
        startY = PictureBox1.Height / 2
        loaded = True
        cmdDraw_Click(Nothing, Nothing)
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'recreate the bitmap
        bmp = Nothing
        If PictureBox1.Width <= 0 Then Exit Sub
        If PictureBox1.Height <= 0 Then Exit Sub
        bmp = New Bitmap(PictureBox1.Width, PictureBox1.Width, Imaging.PixelFormat.Format32bppArgb)

        Dim size As Integer
        If PictureBox1.Width > PictureBox1.Height Then
            size = 2 ^ Int(Math.Log(PictureBox1.Height) / Math.Log(2))
        Else
            size = 2 ^ Int(Math.Log(PictureBox1.Width) / Math.Log(2))
        End If
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.Black)
        On Error Resume Next

        Dim x, y As Integer
        For x = 0 To size
            For y = 0 To size
                If x And y Then
                Else
                    bmp.SetPixel(x, y, Color.Chartreuse)
                End If
            Next y
        Next x
        PictureBox1.Image = bmp
    End Sub


    Private Sub cmdDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles udLineSize.ValueChanged, udIterations.ValueChanged
        If Not loaded Then Exit Sub

        'recreate the bitmap
        bmp = Nothing
        If PictureBox1.Width <= 0 Then Exit Sub
        If PictureBox1.Height <= 0 Then Exit Sub
        bmp = New Bitmap(PictureBox1.Width, PictureBox1.Height, Imaging.PixelFormat.Format32bppArgb)

        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(lblBackColor.BackColor)
        Dim thePen As Pen
        thePen = New Pen(lblForeColor.BackColor)

        'iterate
        Dim numIterations As Integer
        Dim curIndex, tempIndex, i, j As Integer

        numIterations = Int(udIterations.Value)
        If numIterations < 1 Then Exit Sub
        If udLineSize.Value < 1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        Dim poo(2 ^ (numIterations + 1)) As Boolean    'false = left, true = right

        poo(0) = True
        curIndex = 1

        For i = 1 To numIterations
            'add a right turn
            poo(curIndex) = True
            tempIndex = curIndex - 1
            curIndex = curIndex + 1
            'then add the reverse of previous turns, in reverse order
            For j = tempIndex To 0 Step -1
                poo(curIndex) = Not poo(j)
                curIndex = curIndex + 1
            Next
        Next

        'now draw it
        Dim moveLength As Integer
        Dim x, y, prevX, prevY, direction As Integer
        x = startX
        y = startY
        prevX = x
        prevY = y
        moveLength = udLineSize.Value

        direction = 0 '0=right, 1=down, 2=left, 3=up

        For i = 0 To curIndex - 1
            If poo(i) Then 'move right
                If direction = 0 Then
                    direction = 1
                    y = y + moveLength
                ElseIf direction = 1 Then
                    direction = 2
                    x = x - moveLength
                ElseIf direction = 2 Then
                    direction = 3
                    y = y - moveLength
                ElseIf direction = 3 Then
                    direction = 0
                    x = x + moveLength
                End If
            Else 'move left
                If direction = 0 Then
                    direction = 3
                    y = y - moveLength
                ElseIf direction = 1 Then
                    direction = 0
                    x = x + moveLength
                ElseIf direction = 2 Then
                    direction = 1
                    y = y + moveLength
                ElseIf direction = 3 Then
                    direction = 2
                    x = x - moveLength
                End If
            End If

            g.DrawLine(thePen, prevX, prevY, x, y)

            prevX = x
            prevY = y
        Next

        PictureBox1.Image = bmp
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click, cmdRight.Click, cmdLeft.Click, cmdDown.Click
        If sender.Equals(cmdUp) Then
            startY = startY - 50
        ElseIf sender.Equals(cmdDown) Then
            startY = startY + 50
        ElseIf sender.Equals(cmdLeft) Then
            startX = startX - 50
        ElseIf sender.Equals(cmdRight) Then
            startX = startX + 50
        End If
        cmdDraw_Click(Nothing, Nothing)
    End Sub

    Private Sub lblBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBackColor.Click
        ColorDialog1.Color = lblBackColor.BackColor
        If ColorDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
        lblBackColor.BackColor = ColorDialog1.Color
        cmdDraw_Click(Nothing, Nothing)
    End Sub

    Private Sub lblForeColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblForeColor.Click
        ColorDialog1.Color = lblForeColor.BackColor
        If ColorDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
        lblForeColor.BackColor = ColorDialog1.Color
        cmdDraw_Click(Nothing, Nothing)
    End Sub

End Class
