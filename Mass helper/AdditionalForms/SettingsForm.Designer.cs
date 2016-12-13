namespace Mass_helper.AdditionalForms
{
    partial class SettingsForm
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
            this.currentDbTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.choiceDBbutton = new System.Windows.Forms.Button();
            this.autoUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.showNotifyCheckBox = new System.Windows.Forms.CheckBox();
            this.showNotifyForceCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.periodTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabLabelTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // currentDbTextBox
            // 
            this.currentDbTextBox.Location = new System.Drawing.Point(12, 26);
            this.currentDbTextBox.Multiline = true;
            this.currentDbTextBox.Name = "currentDbTextBox";
            this.currentDbTextBox.ReadOnly = true;
            this.currentDbTextBox.Size = new System.Drawing.Size(312, 60);
            this.currentDbTextBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Текущая база данных:";
            // 
            // choiceDBbutton
            // 
            this.choiceDBbutton.Location = new System.Drawing.Point(12, 92);
            this.choiceDBbutton.Name = "choiceDBbutton";
            this.choiceDBbutton.Size = new System.Drawing.Size(312, 32);
            this.choiceDBbutton.TabIndex = 0;
            this.choiceDBbutton.Text = "Выбрать другую базу данных";
            this.choiceDBbutton.UseVisualStyleBackColor = true;
            // 
            // autoUpdateCheckBox
            // 
            this.autoUpdateCheckBox.AutoSize = true;
            this.autoUpdateCheckBox.Location = new System.Drawing.Point(12, 130);
            this.autoUpdateCheckBox.Name = "autoUpdateCheckBox";
            this.autoUpdateCheckBox.Size = new System.Drawing.Size(288, 17);
            this.autoUpdateCheckBox.TabIndex = 1;
            this.autoUpdateCheckBox.Text = "Включить обновление настроек в процессе работы";
            this.autoUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // showNotifyCheckBox
            // 
            this.showNotifyCheckBox.AutoSize = true;
            this.showNotifyCheckBox.Location = new System.Drawing.Point(12, 184);
            this.showNotifyCheckBox.Name = "showNotifyCheckBox";
            this.showNotifyCheckBox.Size = new System.Drawing.Size(290, 17);
            this.showNotifyCheckBox.TabIndex = 2;
            this.showNotifyCheckBox.Text = "Включить отображение напоминаний (из шаблонов)";
            this.showNotifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // showNotifyForceCheckBox
            // 
            this.showNotifyForceCheckBox.AutoSize = true;
            this.showNotifyForceCheckBox.Location = new System.Drawing.Point(12, 207);
            this.showNotifyForceCheckBox.Name = "showNotifyForceCheckBox";
            this.showNotifyForceCheckBox.Size = new System.Drawing.Size(325, 17);
            this.showNotifyForceCheckBox.TabIndex = 3;
            this.showNotifyForceCheckBox.Text = "Показывать напоминания автоматически (при их наличии)";
            this.showNotifyForceCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.saveButton.Location = new System.Drawing.Point(12, 272);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(153, 29);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(171, 272);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(152, 29);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Период обновления:";
            // 
            // periodTextBox
            // 
            this.periodTextBox.Location = new System.Drawing.Point(126, 153);
            this.periodTextBox.Name = "periodTextBox";
            this.periodTextBox.Size = new System.Drawing.Size(65, 20);
            this.periodTextBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "минут";
            // 
            // tabLabelTextBox
            // 
            this.tabLabelTextBox.Location = new System.Drawing.Point(12, 244);
            this.tabLabelTextBox.Name = "tabLabelTextBox";
            this.tabLabelTextBox.Size = new System.Drawing.Size(312, 20);
            this.tabLabelTextBox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Название вкладки:";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.saveButton;
            this.ClientSize = new System.Drawing.Size(336, 313);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabLabelTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.periodTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.showNotifyForceCheckBox);
            this.Controls.Add(this.showNotifyCheckBox);
            this.Controls.Add(this.autoUpdateCheckBox);
            this.Controls.Add(this.choiceDBbutton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentDbTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox currentDbTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button choiceDBbutton;
        private System.Windows.Forms.CheckBox autoUpdateCheckBox;
        private System.Windows.Forms.CheckBox showNotifyCheckBox;
        private System.Windows.Forms.CheckBox showNotifyForceCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox periodTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tabLabelTextBox;
        private System.Windows.Forms.Label label4;
    }
}