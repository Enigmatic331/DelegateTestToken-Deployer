<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnExec = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHolderAddress = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtToAddress = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAmt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDeployPrivKey = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDeployedAt = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnExec
        '
        Me.btnExec.Location = New System.Drawing.Point(12, 577)
        Me.btnExec.Name = "btnExec"
        Me.btnExec.Size = New System.Drawing.Size(553, 62)
        Me.btnExec.TabIndex = 0
        Me.btnExec.Text = "Execute Token Transfer"
        Me.btnExec.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(226, 25)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Token Holder Address"
        '
        'txtHolderAddress
        '
        Me.txtHolderAddress.Enabled = False
        Me.txtHolderAddress.Location = New System.Drawing.Point(12, 149)
        Me.txtHolderAddress.Name = "txtHolderAddress"
        Me.txtHolderAddress.Size = New System.Drawing.Size(558, 31)
        Me.txtHolderAddress.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 222)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To Address"
        '
        'txtToAddress
        '
        Me.txtToAddress.Location = New System.Drawing.Point(12, 250)
        Me.txtToAddress.Name = "txtToAddress"
        Me.txtToAddress.Size = New System.Drawing.Size(558, 31)
        Me.txtToAddress.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 311)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 25)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Amount"
        '
        'txtAmt
        '
        Me.txtAmt.Location = New System.Drawing.Point(12, 348)
        Me.txtAmt.Name = "txtAmt"
        Me.txtAmt.Size = New System.Drawing.Size(188, 31)
        Me.txtAmt.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 435)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(214, 25)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Deployer Private Key"
        '
        'txtDeployPrivKey
        '
        Me.txtDeployPrivKey.Location = New System.Drawing.Point(12, 463)
        Me.txtDeployPrivKey.Name = "txtDeployPrivKey"
        Me.txtDeployPrivKey.Size = New System.Drawing.Size(558, 31)
        Me.txtDeployPrivKey.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(195, 25)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Token Deployed At"
        '
        'txtDeployedAt
        '
        Me.txtDeployedAt.Enabled = False
        Me.txtDeployedAt.Location = New System.Drawing.Point(12, 52)
        Me.txtDeployedAt.Name = "txtDeployedAt"
        Me.txtDeployedAt.Size = New System.Drawing.Size(558, 31)
        Me.txtDeployedAt.TabIndex = 10
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 663)
        Me.Controls.Add(Me.txtDeployedAt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDeployPrivKey)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtAmt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtToAddress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtHolderAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExec)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "Delegated Token Transfer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExec As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtHolderAddress As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtToAddress As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtAmt As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDeployPrivKey As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtDeployedAt As TextBox
End Class
