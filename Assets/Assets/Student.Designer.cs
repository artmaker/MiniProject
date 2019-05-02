namespace Assets
{
    partial class Student
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
            this.components = new System.ComponentModel.Container();
            this.iTAssetsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.assetsDataSet = new Assets.AssetsDataSet();
            this.iT_AssetsTableAdapter = new Assets.AssetsDataSetTableAdapters.IT_AssetsTableAdapter();
            this.dataview = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iTAssetsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).BeginInit();
            this.SuspendLayout();
            // 
            // iTAssetsBindingSource
            // 
            this.iTAssetsBindingSource.DataMember = "IT_Assets";
            this.iTAssetsBindingSource.DataSource = this.assetsDataSet;
            // 
            // assetsDataSet
            // 
            this.assetsDataSet.DataSetName = "AssetsDataSet";
            this.assetsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // iT_AssetsTableAdapter
            // 
            this.iT_AssetsTableAdapter.ClearBeforeFill = true;
            // 
            // dataview
            // 
            this.dataview.AllowUserToAddRows = false;
            this.dataview.AllowUserToDeleteRows = false;
            this.dataview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataview.Location = new System.Drawing.Point(25, 102);
            this.dataview.Name = "dataview";
            this.dataview.ReadOnly = true;
            this.dataview.Size = new System.Drawing.Size(848, 408);
            this.dataview.TabIndex = 1;
            this.dataview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataview_CellContentClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(770, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add Student";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 33);
            this.label1.TabIndex = 6;
            this.label1.Text = "Students";
            // 
            // Student
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(885, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataview);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Student";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iTAssetsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private AssetsDataSet assetsDataSet;
        private System.Windows.Forms.BindingSource iTAssetsBindingSource;
        private AssetsDataSetTableAdapters.IT_AssetsTableAdapter iT_AssetsTableAdapter;
        private System.Windows.Forms.DataGridView dataview;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

