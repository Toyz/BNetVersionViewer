namespace Version_Viewer
{
    partial class MainForm
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
            this.gameSelectionBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gameDataListView = new System.Windows.Forms.ListView();
            this.bgDLCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // gameSelectionBox
            // 
            this.gameSelectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameSelectionBox.FormattingEnabled = true;
            this.gameSelectionBox.Location = new System.Drawing.Point(86, 12);
            this.gameSelectionBox.Name = "gameSelectionBox";
            this.gameSelectionBox.Size = new System.Drawing.Size(486, 21);
            this.gameSelectionBox.TabIndex = 0;
            this.gameSelectionBox.SelectedIndexChanged += new System.EventHandler(this.gameSelectionBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Game";
            // 
            // gameDataListView
            // 
            this.gameDataListView.FullRowSelect = true;
            this.gameDataListView.GridLines = true;
            this.gameDataListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.gameDataListView.Location = new System.Drawing.Point(15, 39);
            this.gameDataListView.MultiSelect = false;
            this.gameDataListView.Name = "gameDataListView";
            this.gameDataListView.Size = new System.Drawing.Size(711, 244);
            this.gameDataListView.TabIndex = 2;
            this.gameDataListView.UseCompatibleStateImageBehavior = false;
            this.gameDataListView.View = System.Windows.Forms.View.Details;
            // 
            // bgDLCheck
            // 
            this.bgDLCheck.AutoSize = true;
            this.bgDLCheck.Location = new System.Drawing.Point(578, 14);
            this.bgDLCheck.Name = "bgDLCheck";
            this.bgDLCheck.Size = new System.Drawing.Size(148, 17);
            this.bgDLCheck.TabIndex = 3;
            this.bgDLCheck.Text = "View Background Version";
            this.bgDLCheck.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 295);
            this.Controls.Add(this.bgDLCheck);
            this.Controls.Add(this.gameDataListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameSelectionBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "BNet Version Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox gameSelectionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView gameDataListView;
        private System.Windows.Forms.CheckBox bgDLCheck;
    }
}

