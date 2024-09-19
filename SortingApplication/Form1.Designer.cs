namespace SortingApplication
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
            this.components = new System.ComponentModel.Container();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.testDataComboBox = new System.Windows.Forms.ComboBox();
            this.sortsDataComboBox = new System.Windows.Forms.ComboBox();
            this.generateArraysButton = new System.Windows.Forms.Button();
            this.launchTestsButton = new System.Windows.Forms.Button();
            this.saveResultButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zedGraph
            // 
            this.zedGraph.Location = new System.Drawing.Point(13, 26);
            this.zedGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(849, 761);
            this.zedGraph.TabIndex = 0;
            this.zedGraph.UseExtendedPrintDialog = true;
            // 
            // testDataComboBox
            // 
            this.testDataComboBox.FormattingEnabled = true;
            this.testDataComboBox.Items.AddRange(new object[] {
            "Случайные числа",
            "Отсортированные подмассивы",
            "Отсортированные масивы с перестановками",
            "Полностью отсортированные"});
            this.testDataComboBox.Location = new System.Drawing.Point(898, 152);
            this.testDataComboBox.Name = "testDataComboBox";
            this.testDataComboBox.Size = new System.Drawing.Size(248, 24);
            this.testDataComboBox.TabIndex = 1;
            // 
            // sortsDataComboBox
            // 
            this.sortsDataComboBox.FormattingEnabled = true;
            this.sortsDataComboBox.Items.AddRange(new object[] {
            "Первая группа",
            "Вторая группа",
            "Третья группа"});
            this.sortsDataComboBox.Location = new System.Drawing.Point(898, 281);
            this.sortsDataComboBox.Name = "sortsDataComboBox";
            this.sortsDataComboBox.Size = new System.Drawing.Size(248, 24);
            this.sortsDataComboBox.TabIndex = 2;
            // 
            // generateArraysButton
            // 
            this.generateArraysButton.Location = new System.Drawing.Point(922, 409);
            this.generateArraysButton.Name = "generateArraysButton";
            this.generateArraysButton.Size = new System.Drawing.Size(201, 86);
            this.generateArraysButton.TabIndex = 3;
            this.generateArraysButton.Text = "Сгенерировать массивы";
            this.generateArraysButton.UseVisualStyleBackColor = true;
            this.generateArraysButton.Click += new System.EventHandler(this.generateArraysButton_Click);
            // 
            // launchTestsButton
            // 
            this.launchTestsButton.Location = new System.Drawing.Point(922, 515);
            this.launchTestsButton.Name = "launchTestsButton";
            this.launchTestsButton.Size = new System.Drawing.Size(201, 86);
            this.launchTestsButton.TabIndex = 4;
            this.launchTestsButton.Text = "Запустить тесты";
            this.launchTestsButton.UseVisualStyleBackColor = true;
            this.launchTestsButton.Click += new System.EventHandler(this.launchTestsButton_Click);
            // 
            // saveResultButton
            // 
            this.saveResultButton.Location = new System.Drawing.Point(922, 621);
            this.saveResultButton.Name = "saveResultButton";
            this.saveResultButton.Size = new System.Drawing.Size(201, 86);
            this.saveResultButton.TabIndex = 5;
            this.saveResultButton.Text = "Сохранить результат";
            this.saveResultButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 827);
            this.Controls.Add(this.saveResultButton);
            this.Controls.Add(this.launchTestsButton);
            this.Controls.Add(this.generateArraysButton);
            this.Controls.Add(this.sortsDataComboBox);
            this.Controls.Add(this.testDataComboBox);
            this.Controls.Add(this.zedGraph);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.ComboBox testDataComboBox;
        private System.Windows.Forms.ComboBox sortsDataComboBox;
        private System.Windows.Forms.Button generateArraysButton;
        private System.Windows.Forms.Button launchTestsButton;
        private System.Windows.Forms.Button saveResultButton;
    }
}

