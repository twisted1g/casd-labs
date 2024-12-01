namespace CompareArrayListAndLinkedList
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
            components = new Container();
            zedGraph = new ZedGraphControl();
            comboBox = new ComboBox();
            button = new Button();
            textBox = new TextBox();
            SuspendLayout();
            // 
            // zedGraph
            // 
            zedGraph.Location = new Point(0, 0);
            zedGraph.Margin = new Padding(4, 5, 4, 5);
            zedGraph.Name = "zedGraph";
            zedGraph.ScrollGrace = 0D;
            zedGraph.ScrollMaxX = 0D;
            zedGraph.ScrollMaxY = 0D;
            zedGraph.ScrollMaxY2 = 0D;
            zedGraph.ScrollMinX = 0D;
            zedGraph.ScrollMinY = 0D;
            zedGraph.ScrollMinY2 = 0D;
            zedGraph.Size = new Size(999, 1008);
            zedGraph.TabIndex = 0;
            zedGraph.UseExtendedPrintDialog = true;
            // 
            // comboBox
            // 
            comboBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(new object[] { "Get", "Put", "Remove" });
            comboBox.Location = new Point(1015, 369);
            comboBox.Margin = new Padding(3, 4, 3, 4);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(273, 37);
            comboBox.TabIndex = 1;
            comboBox.Text = "Choose operation";
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            // 
            // button
            // 
            button.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Location = new Point(1052, 601);
            button.Margin = new Padding(3, 4, 3, 4);
            button.Name = "button";
            button.Size = new Size(178, 155);
            button.TabIndex = 2;
            button.Text = "Compare";
            button.UseVisualStyleBackColor = true;
            button.Visible = false;
            button.Click += button_Click;
            // 
            // textBox
            // 
            textBox.Location = new Point(1015, 140);
            textBox.Margin = new Padding(3, 4, 3, 4);
            textBox.Name = "textBox";
            textBox.Size = new Size(215, 27);
            textBox.TabIndex = 3;
            textBox.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1304, 1024);
            Controls.Add(textBox);
            Controls.Add(button);
            Controls.Add(comboBox);
            Controls.Add(zedGraph);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.TextBox textBox;
    }
}

