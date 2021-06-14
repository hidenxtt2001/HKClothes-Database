namespace HkClothes
{
    partial class UpdateSale
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
            this.percent = new System.Windows.Forms.TextBox();
            this.set_sale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Percent";
            // 
            // percent
            // 
            this.percent.Location = new System.Drawing.Point(94, 18);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(157, 20);
            this.percent.TabIndex = 1;
            this.percent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.percent_KeyPress);
            // 
            // set_sale
            // 
            this.set_sale.Location = new System.Drawing.Point(105, 44);
            this.set_sale.Name = "set_sale";
            this.set_sale.Size = new System.Drawing.Size(75, 23);
            this.set_sale.TabIndex = 2;
            this.set_sale.Text = "Update";
            this.set_sale.UseVisualStyleBackColor = true;
            this.set_sale.Click += new System.EventHandler(this.set_sale_Click);
            // 
            // UpdateSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 89);
            this.Controls.Add(this.set_sale);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateSale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdateSale";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox percent;
        private System.Windows.Forms.Button set_sale;
    }
}