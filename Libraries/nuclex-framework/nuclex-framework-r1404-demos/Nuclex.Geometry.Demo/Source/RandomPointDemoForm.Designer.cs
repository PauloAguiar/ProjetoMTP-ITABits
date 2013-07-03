namespace Nuclex.Geometry.Demo {

  partial class RandomPointDemoForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.locationGroup = new System.Windows.Forms.GroupBox();
      this.insideOption = new System.Windows.Forms.RadioButton();
      this.perimeterOption = new System.Windows.Forms.RadioButton();
      this.shapeGroup = new System.Windows.Forms.GroupBox();
      this.discOption = new System.Windows.Forms.RadioButton();
      this.triangleOption = new System.Windows.Forms.RadioButton();
      this.rectangleOption = new System.Windows.Forms.RadioButton();
      this.generateButton = new System.Windows.Forms.Button();
      this.demoPicture = new System.Windows.Forms.PictureBox();
      this.locationGroup.SuspendLayout();
      this.shapeGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.demoPicture)).BeginInit();
      this.SuspendLayout();
      // 
      // locationGroup
      // 
      this.locationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.locationGroup.Controls.Add(this.insideOption);
      this.locationGroup.Controls.Add(this.perimeterOption);
      this.locationGroup.Location = new System.Drawing.Point(335, 12);
      this.locationGroup.Name = "locationGroup";
      this.locationGroup.Size = new System.Drawing.Size(86, 65);
      this.locationGroup.TabIndex = 0;
      this.locationGroup.TabStop = false;
      this.locationGroup.Text = "Location";
      // 
      // insideOption
      // 
      this.insideOption.AutoSize = true;
      this.insideOption.Location = new System.Drawing.Point(6, 42);
      this.insideOption.Name = "insideOption";
      this.insideOption.Size = new System.Drawing.Size(53, 17);
      this.insideOption.TabIndex = 1;
      this.insideOption.TabStop = true;
      this.insideOption.Text = "Inside";
      this.insideOption.UseVisualStyleBackColor = true;
      // 
      // perimeterOption
      // 
      this.perimeterOption.AutoSize = true;
      this.perimeterOption.Location = new System.Drawing.Point(6, 19);
      this.perimeterOption.Name = "perimeterOption";
      this.perimeterOption.Size = new System.Drawing.Size(69, 17);
      this.perimeterOption.TabIndex = 0;
      this.perimeterOption.TabStop = true;
      this.perimeterOption.Text = "Perimeter";
      this.perimeterOption.UseVisualStyleBackColor = true;
      // 
      // shapeGroup
      // 
      this.shapeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.shapeGroup.Controls.Add(this.discOption);
      this.shapeGroup.Controls.Add(this.triangleOption);
      this.shapeGroup.Controls.Add(this.rectangleOption);
      this.shapeGroup.Location = new System.Drawing.Point(335, 83);
      this.shapeGroup.Name = "shapeGroup";
      this.shapeGroup.Size = new System.Drawing.Size(86, 89);
      this.shapeGroup.TabIndex = 1;
      this.shapeGroup.TabStop = false;
      this.shapeGroup.Text = "Shape";
      // 
      // discOption
      // 
      this.discOption.AutoSize = true;
      this.discOption.Location = new System.Drawing.Point(7, 66);
      this.discOption.Name = "discOption";
      this.discOption.Size = new System.Drawing.Size(46, 17);
      this.discOption.TabIndex = 2;
      this.discOption.TabStop = true;
      this.discOption.Text = "Disc";
      this.discOption.UseVisualStyleBackColor = true;
      this.discOption.CheckedChanged += new System.EventHandler(this.discSelected);
      // 
      // triangleOption
      // 
      this.triangleOption.AutoSize = true;
      this.triangleOption.Location = new System.Drawing.Point(6, 42);
      this.triangleOption.Name = "triangleOption";
      this.triangleOption.Size = new System.Drawing.Size(63, 17);
      this.triangleOption.TabIndex = 1;
      this.triangleOption.TabStop = true;
      this.triangleOption.Text = "Triangle";
      this.triangleOption.UseVisualStyleBackColor = true;
      this.triangleOption.CheckedChanged += new System.EventHandler(this.triangleSelected);
      // 
      // rectangleOption
      // 
      this.rectangleOption.AutoSize = true;
      this.rectangleOption.Location = new System.Drawing.Point(6, 19);
      this.rectangleOption.Name = "rectangleOption";
      this.rectangleOption.Size = new System.Drawing.Size(74, 17);
      this.rectangleOption.TabIndex = 0;
      this.rectangleOption.TabStop = true;
      this.rectangleOption.Text = "Rectangle";
      this.rectangleOption.UseVisualStyleBackColor = true;
      this.rectangleOption.CheckedChanged += new System.EventHandler(this.rectangleSelected);
      // 
      // generateButton
      // 
      this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.generateButton.Location = new System.Drawing.Point(335, 295);
      this.generateButton.Name = "generateButton";
      this.generateButton.Size = new System.Drawing.Size(86, 23);
      this.generateButton.TabIndex = 2;
      this.generateButton.Text = "&Generate";
      this.generateButton.UseVisualStyleBackColor = true;
      this.generateButton.Click += new System.EventHandler(this.generateClicked);
      // 
      // demoPicture
      // 
      this.demoPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.demoPicture.Location = new System.Drawing.Point(13, 13);
      this.demoPicture.Name = "demoPicture";
      this.demoPicture.Size = new System.Drawing.Size(316, 305);
      this.demoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.demoPicture.TabIndex = 3;
      this.demoPicture.TabStop = false;
      this.demoPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.demoPicturePainting);
      // 
      // RandomPointDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(433, 330);
      this.Controls.Add(this.demoPicture);
      this.Controls.Add(this.generateButton);
      this.Controls.Add(this.shapeGroup);
      this.Controls.Add(this.locationGroup);
      this.MinimumSize = new System.Drawing.Size(301, 247);
      this.Name = "RandomPointDemoForm";
      this.Text = "Random Point Generation Demo";
      this.locationGroup.ResumeLayout(false);
      this.locationGroup.PerformLayout();
      this.shapeGroup.ResumeLayout(false);
      this.shapeGroup.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.demoPicture)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox locationGroup;
    private System.Windows.Forms.GroupBox shapeGroup;
    private System.Windows.Forms.RadioButton insideOption;
    private System.Windows.Forms.RadioButton perimeterOption;
    private System.Windows.Forms.RadioButton discOption;
    private System.Windows.Forms.RadioButton triangleOption;
    private System.Windows.Forms.RadioButton rectangleOption;
    private System.Windows.Forms.Button generateButton;
    private System.Windows.Forms.PictureBox demoPicture;

  }

} // namespace Nuclex.Geometry.Demo