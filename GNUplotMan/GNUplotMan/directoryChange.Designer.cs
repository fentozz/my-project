namespace GNUplotMan
{
    partial class directoryChange
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Change = new System.Windows.Forms.Button();
            this.Directory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Change
            // 
            this.Change.BackColor = System.Drawing.Color.White;
            this.Change.Cursor = System.Windows.Forms.Cursors.Default;
            this.Change.Dock = System.Windows.Forms.DockStyle.Right;
            this.Change.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Change.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Change.Location = new System.Drawing.Point(245, 0);
            this.Change.Margin = new System.Windows.Forms.Padding(0);
            this.Change.Name = "Change";
            this.Change.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Change.Size = new System.Drawing.Size(17, 22);
            this.Change.TabIndex = 4;
            this.Change.Text = "...";
            this.Change.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Change.UseVisualStyleBackColor = false;
            this.Change.Click += new System.EventHandler(this.Change_Click);
            // 
            // Directory
            // 
            this.Directory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Directory.Location = new System.Drawing.Point(0, 0);
            this.Directory.Name = "Directory";
            this.Directory.Size = new System.Drawing.Size(245, 20);
            this.Directory.TabIndex = 5;
            this.Directory.DoubleClick += new System.EventHandler(this.Directory_DoubleClick);
            // 
            // directoryChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Directory);
            this.Controls.Add(this.Change);
            this.Name = "directoryChange";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(262, 22);
            this.DoubleClick += new System.EventHandler(this.Directory_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Change;
        private System.Windows.Forms.TextBox Directory;
    }
}
