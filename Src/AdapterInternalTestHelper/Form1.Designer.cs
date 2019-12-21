namespace AdapterInternalTestHelper
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SendDataToAdapterButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.AdapterMethodsBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.StatusBox = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.DCOutputTextBox = new System.Windows.Forms.RichTextBox();
			this.DCInputTextBox = new System.Windows.Forms.RichTextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.DCOutputDataTypeTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.DCTypeTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "BaseId";
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(57, 19);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(511, 20);
			this.textBox1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(595, 17);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(119, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Get BaseId";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// SendDataToAdapterButton
			// 
			this.SendDataToAdapterButton.Enabled = false;
			this.SendDataToAdapterButton.Location = new System.Drawing.Point(595, 11);
			this.SendDataToAdapterButton.Name = "SendDataToAdapterButton";
			this.SendDataToAdapterButton.Size = new System.Drawing.Size(119, 23);
			this.SendDataToAdapterButton.TabIndex = 3;
			this.SendDataToAdapterButton.Text = "Send To Adapter";
			this.SendDataToAdapterButton.UseVisualStyleBackColor = true;
			this.SendDataToAdapterButton.Click += new System.EventHandler(this.Button2_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(720, 52);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Initial Data";
			// 
			// AdapterMethodsBox
			// 
			this.AdapterMethodsBox.Enabled = false;
			this.AdapterMethodsBox.FormattingEnabled = true;
			this.AdapterMethodsBox.Location = new System.Drawing.Point(142, 13);
			this.AdapterMethodsBox.Name = "AdapterMethodsBox";
			this.AdapterMethodsBox.Size = new System.Drawing.Size(426, 21);
			this.AdapterMethodsBox.TabIndex = 5;
			this.AdapterMethodsBox.SelectedIndexChanged += new System.EventHandler(this.AdapterMethodsBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Adapter Method Name";
			// 
			// StatusBox
			// 
			this.StatusBox.FormattingEnabled = true;
			this.StatusBox.Location = new System.Drawing.Point(6, 19);
			this.StatusBox.Name = "StatusBox";
			this.StatusBox.Size = new System.Drawing.Size(708, 43);
			this.StatusBox.TabIndex = 6;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.DCOutputTextBox);
			this.groupBox2.Controls.Add(this.DCInputTextBox);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.DCOutputDataTypeTextBox);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.DCTypeTextBox);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.AdapterMethodsBox);
			this.groupBox2.Controls.Add(this.SendDataToAdapterButton);
			this.groupBox2.Location = new System.Drawing.Point(12, 70);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(726, 316);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Test ";
			// 
			// DCOutputTextBox
			// 
			this.DCOutputTextBox.Location = new System.Drawing.Point(359, 142);
			this.DCOutputTextBox.Name = "DCOutputTextBox";
			this.DCOutputTextBox.Size = new System.Drawing.Size(346, 169);
			this.DCOutputTextBox.TabIndex = 18;
			this.DCOutputTextBox.Text = "";
			// 
			// DCInputTextBox
			// 
			this.DCInputTextBox.Location = new System.Drawing.Point(6, 142);
			this.DCInputTextBox.Name = "DCInputTextBox";
			this.DCInputTextBox.Size = new System.Drawing.Size(346, 169);
			this.DCInputTextBox.TabIndex = 17;
			this.DCInputTextBox.Text = "";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(356, 126);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Output - Received Data";
			// 
			// DCOutputDataTypeTextBox
			// 
			this.DCOutputDataTypeTextBox.Enabled = false;
			this.DCOutputDataTypeTextBox.Location = new System.Drawing.Point(142, 69);
			this.DCOutputDataTypeTextBox.Name = "DCOutputDataTypeTextBox";
			this.DCOutputDataTypeTextBox.Size = new System.Drawing.Size(426, 20);
			this.DCOutputDataTypeTextBox.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Output Data Type";
			// 
			// DCTypeTextBox
			// 
			this.DCTypeTextBox.Enabled = false;
			this.DCTypeTextBox.Location = new System.Drawing.Point(142, 41);
			this.DCTypeTextBox.Name = "DCTypeTextBox";
			this.DCTypeTextBox.Size = new System.Drawing.Size(426, 20);
			this.DCTypeTextBox.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Input Data Type";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Input - Telegram Data";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.StatusBox);
			this.groupBox3.Location = new System.Drawing.Point(12, 392);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(726, 75);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Status";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(595, 37);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(119, 27);
			this.button2.TabIndex = 19;
			this.button2.Text = "START";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2_Click_1);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(595, 69);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(119, 27);
			this.button3.TabIndex = 20;
			this.button3.Text = "STOP";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(744, 479);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "PE Adapter Internal Test Helper";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button SendDataToAdapterButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox AdapterMethodsBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox StatusBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox DCTypeTextBox;
    private System.Windows.Forms.TextBox DCOutputDataTypeTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RichTextBox DCInputTextBox;
    private System.Windows.Forms.RichTextBox DCOutputTextBox;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
  }
}

