namespace Visualisation_Labyrynth
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Lab = new System.Windows.Forms.PictureBox();
            this.buttonFindPath = new System.Windows.Forms.Button();
            this.labelX = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.comboBoxAlghorythms = new System.Windows.Forms.ComboBox();
            this.labelChoose = new System.Windows.Forms.Label();
            this.buttonBuildNewLab = new System.Windows.Forms.Button();
            this.textBoxNewWidth = new System.Windows.Forms.TextBox();
            this.textBoxNewHeight = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.buttonClearWalls = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Lab)).BeginInit();
            this.SuspendLayout();
            // 
            // Lab
            // 
            this.Lab.Location = new System.Drawing.Point(12, 100);
            this.Lab.Name = "Lab";
            this.Lab.Size = new System.Drawing.Size(500, 500);
            this.Lab.TabIndex = 0;
            this.Lab.TabStop = false;
            this.Lab.Click += new System.EventHandler(this.Lab_Click);
            this.Lab.Paint += new System.Windows.Forms.PaintEventHandler(this.Lab_Paint);
            this.Lab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Lab_MouseDown);
            this.Lab.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Lab_MouseMove);
            this.Lab.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Lab_MouseUp);
            // 
            // buttonFindPath
            // 
            this.buttonFindPath.Location = new System.Drawing.Point(12, 52);
            this.buttonFindPath.Name = "buttonFindPath";
            this.buttonFindPath.Size = new System.Drawing.Size(152, 37);
            this.buttonFindPath.TabIndex = 2;
            this.buttonFindPath.Text = "Найти выход";
            this.buttonFindPath.UseVisualStyleBackColor = true;
            this.buttonFindPath.Click += new System.EventHandler(this.buttonFindPath_Click);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(433, 64);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(12, 13);
            this.labelX.TabIndex = 6;
            this.labelX.Text = "x";
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(200, 67);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(145, 23);
            this.buttonRestart.TabIndex = 7;
            this.buttonRestart.Text = "Начать заново";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // comboBoxAlghorythms
            // 
            this.comboBoxAlghorythms.FormattingEnabled = true;
            this.comboBoxAlghorythms.Items.AddRange(new object[] {
            "Алгоритм А*",
            "Алгоритм Дейкстры",
            "Алгоритм Ли"});
            this.comboBoxAlghorythms.Location = new System.Drawing.Point(12, 25);
            this.comboBoxAlghorythms.Name = "comboBoxAlghorythms";
            this.comboBoxAlghorythms.Size = new System.Drawing.Size(152, 21);
            this.comboBoxAlghorythms.TabIndex = 8;
            // 
            // labelChoose
            // 
            this.labelChoose.AutoSize = true;
            this.labelChoose.Location = new System.Drawing.Point(17, 9);
            this.labelChoose.Name = "labelChoose";
            this.labelChoose.Size = new System.Drawing.Size(108, 13);
            this.labelChoose.TabIndex = 9;
            this.labelChoose.Text = "Выберите алгоритм";
            // 
            // buttonBuildNewLab
            // 
            this.buttonBuildNewLab.Location = new System.Drawing.Point(367, 9);
            this.buttonBuildNewLab.Name = "buttonBuildNewLab";
            this.buttonBuildNewLab.Size = new System.Drawing.Size(145, 23);
            this.buttonBuildNewLab.TabIndex = 10;
            this.buttonBuildNewLab.Text = "Построить лабиринт";
            this.buttonBuildNewLab.UseVisualStyleBackColor = true;
            this.buttonBuildNewLab.Click += new System.EventHandler(this.buttonBuildNewLab_Click);
            // 
            // textBoxNewWidth
            // 
            this.textBoxNewWidth.Location = new System.Drawing.Point(384, 61);
            this.textBoxNewWidth.Name = "textBoxNewWidth";
            this.textBoxNewWidth.Size = new System.Drawing.Size(43, 20);
            this.textBoxNewWidth.TabIndex = 11;
            // 
            // textBoxNewHeight
            // 
            this.textBoxNewHeight.Location = new System.Drawing.Point(451, 61);
            this.textBoxNewHeight.Name = "textBoxNewHeight";
            this.textBoxNewHeight.Size = new System.Drawing.Size(42, 20);
            this.textBoxNewHeight.TabIndex = 12;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(382, 41);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(46, 13);
            this.labelWidth.TabIndex = 13;
            this.labelWidth.Text = "Ширина";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(450, 41);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(45, 13);
            this.labelHeight.TabIndex = 14;
            this.labelHeight.Text = "Высота";
            // 
            // buttonClearWalls
            // 
            this.buttonClearWalls.AccessibleRole = System.Windows.Forms.AccessibleRole.Client;
            this.buttonClearWalls.Location = new System.Drawing.Point(200, 38);
            this.buttonClearWalls.Name = "buttonClearWalls";
            this.buttonClearWalls.Size = new System.Drawing.Size(145, 23);
            this.buttonClearWalls.TabIndex = 15;
            this.buttonClearWalls.Text = "Убрать стены";
            this.buttonClearWalls.UseVisualStyleBackColor = true;
            this.buttonClearWalls.Click += new System.EventHandler(this.buttonClearWalls_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(200, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(145, 23);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить лабиринт";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 612);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClearWalls);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.textBoxNewHeight);
            this.Controls.Add(this.textBoxNewWidth);
            this.Controls.Add(this.buttonBuildNewLab);
            this.Controls.Add(this.labelChoose);
            this.Controls.Add(this.comboBoxAlghorythms);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.buttonFindPath);
            this.Controls.Add(this.Lab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабиринт";
            ((System.ComponentModel.ISupportInitialize)(this.Lab)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Lab;
        private System.Windows.Forms.Button buttonFindPath;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.ComboBox comboBoxAlghorythms;
        private System.Windows.Forms.Label labelChoose;
        private System.Windows.Forms.Button buttonBuildNewLab;
        private System.Windows.Forms.TextBox textBoxNewWidth;
        private System.Windows.Forms.TextBox textBoxNewHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Button buttonClearWalls;
        private System.Windows.Forms.Button buttonSave;
    }
}

