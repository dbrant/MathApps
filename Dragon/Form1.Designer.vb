<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.udIterations = New System.Windows.Forms.NumericUpDown()
        Me.udLineSize = New System.Windows.Forms.NumericUpDown()
        Me.cmdLeft = New System.Windows.Forms.Button()
        Me.cmdRight = New System.Windows.Forms.Button()
        Me.cmdUp = New System.Windows.Forms.Button()
        Me.cmdDown = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.lblBackColor = New System.Windows.Forms.Label()
        Me.lblForeColor = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udIterations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udLineSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Location = New System.Drawing.Point(12, 64)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(597, 425)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Iterations:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Line Size:"
        '
        'udIterations
        '
        Me.udIterations.Location = New System.Drawing.Point(76, 12)
        Me.udIterations.Name = "udIterations"
        Me.udIterations.Size = New System.Drawing.Size(45, 20)
        Me.udIterations.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.udIterations, "Number of iterations to calculate for the dragon curve.")
        Me.udIterations.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'udLineSize
        '
        Me.udLineSize.Location = New System.Drawing.Point(76, 38)
        Me.udLineSize.Name = "udLineSize"
        Me.udLineSize.Size = New System.Drawing.Size(45, 20)
        Me.udLineSize.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.udLineSize, "Length of each line segment in the dragon curve.")
        Me.udLineSize.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'cmdLeft
        '
        Me.cmdLeft.Location = New System.Drawing.Point(138, 27)
        Me.cmdLeft.Name = "cmdLeft"
        Me.cmdLeft.Size = New System.Drawing.Size(16, 16)
        Me.cmdLeft.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmdLeft, "Move left")
        Me.cmdLeft.UseVisualStyleBackColor = True
        '
        'cmdRight
        '
        Me.cmdRight.Location = New System.Drawing.Point(166, 27)
        Me.cmdRight.Name = "cmdRight"
        Me.cmdRight.Size = New System.Drawing.Size(16, 16)
        Me.cmdRight.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.cmdRight, "Move right")
        Me.cmdRight.UseVisualStyleBackColor = True
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(152, 12)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(16, 16)
        Me.cmdUp.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cmdUp, "Move up")
        Me.cmdUp.UseVisualStyleBackColor = True
        '
        'cmdDown
        '
        Me.cmdDown.Location = New System.Drawing.Point(152, 42)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(16, 16)
        Me.cmdDown.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmdDown, "Move down")
        Me.cmdDown.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        '
        'ColorDialog1
        '
        Me.ColorDialog1.AnyColor = True
        Me.ColorDialog1.FullOpen = True
        '
        'lblBackColor
        '
        Me.lblBackColor.BackColor = System.Drawing.Color.Black
        Me.lblBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBackColor.Location = New System.Drawing.Point(276, 13)
        Me.lblBackColor.Name = "lblBackColor"
        Me.lblBackColor.Size = New System.Drawing.Size(20, 17)
        Me.lblBackColor.TabIndex = 18
        '
        'lblForeColor
        '
        Me.lblForeColor.BackColor = System.Drawing.Color.Chartreuse
        Me.lblForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblForeColor.Location = New System.Drawing.Point(276, 39)
        Me.lblForeColor.Name = "lblForeColor"
        Me.lblForeColor.Size = New System.Drawing.Size(20, 17)
        Me.lblForeColor.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(203, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Background:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(235, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Lines:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(621, 501)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblForeColor)
        Me.Controls.Add(Me.lblBackColor)
        Me.Controls.Add(Me.cmdDown)
        Me.Controls.Add(Me.cmdUp)
        Me.Controls.Add(Me.cmdRight)
        Me.Controls.Add(Me.cmdLeft)
        Me.Controls.Add(Me.udLineSize)
        Me.Controls.Add(Me.udIterations)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Form1"
        Me.Text = "Dragon Curve - http://dmitrybrant.com"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udIterations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udLineSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents udIterations As System.Windows.Forms.NumericUpDown
    Friend WithEvents udLineSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmdLeft As System.Windows.Forms.Button
    Friend WithEvents cmdRight As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents lblBackColor As System.Windows.Forms.Label
    Friend WithEvents lblForeColor As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
