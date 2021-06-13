namespace HkClothes
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.name_product = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.category = new System.Windows.Forms.ComboBox();
            this.product_size = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.TextBox();
            this.add_product = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::HkClothes.Properties.Resources.image_100px;
            this.pictureBox1.InitialImage = global::HkClothes.Properties.Resources.image_100px;
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 266);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // name_product
            // 
            this.name_product.Location = new System.Drawing.Point(229, 29);
            this.name_product.Name = "name_product";
            this.name_product.Size = new System.Drawing.Size(247, 20);
            this.name_product.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Category";
            // 
            // category
            // 
            this.category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category.FormattingEnabled = true;
            this.category.Items.AddRange(new object[] {
            "Shirt",
            "T-Shirt",
            "Hoodies",
            "Short",
            "Pants",
            "Sweatshirt"});
            this.category.Location = new System.Drawing.Point(229, 68);
            this.category.Name = "category";
            this.category.Size = new System.Drawing.Size(121, 21);
            this.category.TabIndex = 3;
            // 
            // product_size
            // 
            this.product_size.FormattingEnabled = true;
            this.product_size.Items.AddRange(new object[] {
            "S",
            "M",
            "L",
            "XL",
            "XXL"});
            this.product_size.Location = new System.Drawing.Point(229, 108);
            this.product_size.Name = "product_size";
            this.product_size.Size = new System.Drawing.Size(120, 79);
            this.product_size.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Price";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(229, 206);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(120, 20);
            this.price.TabIndex = 5;
            this.price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // add_product
            // 
            this.add_product.Location = new System.Drawing.Point(232, 256);
            this.add_product.Name = "add_product";
            this.add_product.Size = new System.Drawing.Size(75, 23);
            this.add_product.TabIndex = 6;
            this.add_product.Text = "Add Product";
            this.add_product.UseVisualStyleBackColor = true;
            this.add_product.Click += new System.EventHandler(this.add_product_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 306);
            this.Controls.Add(this.add_product);
            this.Controls.Add(this.price);
            this.Controls.Add(this.product_size);
            this.Controls.Add(this.category);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.name_product);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox name_product;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox category;
        private System.Windows.Forms.CheckedListBox product_size;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.Button add_product;
    }
}

