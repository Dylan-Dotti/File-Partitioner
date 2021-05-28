namespace FilePartitioner.GUI
{
    partial class PartitionStrategyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.strategyComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // strategyComboBox
            // 
            this.strategyComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.strategyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.strategyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strategyComboBox.FormattingEnabled = true;
            this.strategyComboBox.Location = new System.Drawing.Point(3, 4);
            this.strategyComboBox.Margin = new System.Windows.Forms.Padding(5);
            this.strategyComboBox.Name = "strategyComboBox";
            this.strategyComboBox.Size = new System.Drawing.Size(477, 24);
            this.strategyComboBox.TabIndex = 2;
            // 
            // PartitionStrategyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.strategyComboBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PartitionStrategyControl";
            this.Size = new System.Drawing.Size(483, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox strategyComboBox;
    }
}
