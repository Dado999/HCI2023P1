namespace Damir_Filipovic_HCI2023
{
    partial class Order
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.priceLabel = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.orderButton = new System.Windows.Forms.Button();
            this.adressLabel = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.TextBox();
            this.cityLabel = new System.Windows.Forms.TextBox();
            this.surnameLabel = new System.Windows.Forms.TextBox();
            this.numberLabel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(97)))), ((int)(((byte)(170)))));
            this.splitContainer1.Panel1.BackgroundImage = global::Damir_Filipovic_HCI2023.Properties.Resources.Logo;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.priceLabel);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(97)))), ((int)(((byte)(170)))));
            this.priceLabel.Location = new System.Drawing.Point(3, 34);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(0, 25);
            this.priceLabel.TabIndex = 6;
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1.Controls.Add(this.adressLabel);
            this.splitContainer2.Panel1.Controls.Add(this.nameLabel);
            this.splitContainer2.Panel1.Controls.Add(this.cityLabel);
            this.splitContainer2.Panel1.Controls.Add(this.surnameLabel);
            this.splitContainer2.Panel1.Controls.Add(this.numberLabel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(97)))), ((int)(((byte)(170)))));
            this.splitContainer2.Panel2.BackgroundImage = global::Damir_Filipovic_HCI2023.Properties.Resources.Logo;
            this.splitContainer2.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.splitContainer2.Size = new System.Drawing.Size(530, 450);
            this.splitContainer2.SplitterDistance = 272;
            this.splitContainer2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(97)))), ((int)(((byte)(170)))));
            this.panel1.Controls.Add(this.orderButton);
            this.panel1.Location = new System.Drawing.Point(3, 330);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 55);
            this.panel1.TabIndex = 5;
            // 
            // orderButton
            // 
            this.orderButton.BackColor = System.Drawing.Color.Transparent;
            this.orderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.orderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderButton.ForeColor = System.Drawing.Color.White;
            this.orderButton.Location = new System.Drawing.Point(-16, -37);
            this.orderButton.Name = "orderButton";
            this.orderButton.Size = new System.Drawing.Size(302, 128);
            this.orderButton.TabIndex = 6;
            this.orderButton.Text = "ORDER";
            this.orderButton.UseVisualStyleBackColor = false;
            this.orderButton.Click += new System.EventHandler(this.orderButton_Click_1);
            // 
            // adressLabel
            // 
            this.adressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adressLabel.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.adressLabel.Location = new System.Drawing.Point(3, 276);
            this.adressLabel.Name = "adressLabel";
            this.adressLabel.Size = new System.Drawing.Size(264, 26);
            this.adressLabel.TabIndex = 4;
            this.adressLabel.Text = "Address";
            this.adressLabel.Click += new System.EventHandler(this.adressLabel_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(3, 76);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(264, 26);
            this.nameLabel.TabIndex = 0;
            // 
            // cityLabel
            // 
            this.cityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cityLabel.Location = new System.Drawing.Point(3, 226);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(264, 26);
            this.cityLabel.TabIndex = 3;
            // 
            // surnameLabel
            // 
            this.surnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surnameLabel.Location = new System.Drawing.Point(3, 126);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(264, 26);
            this.surnameLabel.TabIndex = 1;
            // 
            // numberLabel
            // 
            this.numberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberLabel.Location = new System.Drawing.Point(3, 176);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(264, 26);
            this.numberLabel.TabIndex = 2;
            // 
            // Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Order";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order";
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox adressLabel;
        private System.Windows.Forms.TextBox cityLabel;
        private System.Windows.Forms.TextBox numberLabel;
        private System.Windows.Forms.TextBox surnameLabel;
        private System.Windows.Forms.TextBox nameLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button orderButton;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}