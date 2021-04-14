namespace OSM_Geocoding
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proxyListElementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corpDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.valid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fullAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressListElementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.deleteXML = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.resultInfoElementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.OffProxyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonOSM = new System.Windows.Forms.RadioButton();
            this.radioButtonYa = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.proxyListElementBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressListElementBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultInfoElementBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(287, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Пуск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24});
            this.dataGridView1.DataSource = this.proxyListElementBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(1253, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(198, 370);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn22.DataPropertyName = "address";
            this.dataGridViewTextBoxColumn22.HeaderText = "Прокси";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "responseTime";
            this.dataGridViewTextBoxColumn23.HeaderText = "время";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.Width = 50;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "raiting";
            this.dataGridViewTextBoxColumn24.HeaderText = "рейт.";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 40;
            // 
            // proxyListElementBindingSource
            // 
            this.proxyListElementBindingSource.DataSource = typeof(OSM_Geocoding.Form1.ProxyListElement);
            this.proxyListElementBindingSource.DataError += new System.Windows.Forms.BindingManagerDataErrorEventHandler(this.proxyListElementBindingSource_DataError);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.origAddress,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30,
            this.corpDataGridViewTextBoxColumn,
            this.dataGridViewCheckBoxColumn7,
            this.dataGridViewCheckBoxColumn8,
            this.valid,
            this.fullAddress});
            this.dataGridView2.DataSource = this.addressListElementBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(419, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(834, 370);
            this.dataGridView2.TabIndex = 6;
            this.dataGridView2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "row";
            this.dataGridViewTextBoxColumn25.HeaderText = "№";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Width = 30;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "longit";
            this.dataGridViewTextBoxColumn26.HeaderText = "Широта";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "latid";
            this.dataGridViewTextBoxColumn27.HeaderText = "Долгота";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "city";
            this.dataGridViewTextBoxColumn28.HeaderText = "Город";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            // 
            // origAddress
            // 
            this.origAddress.DataPropertyName = "origAddress";
            this.origAddress.HeaderText = "Исходный адрес";
            this.origAddress.Name = "origAddress";
            this.origAddress.ReadOnly = true;
            this.origAddress.Width = 200;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.DataPropertyName = "road";
            this.dataGridViewTextBoxColumn29.HeaderText = "Улица";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.DataPropertyName = "house_number";
            this.dataGridViewTextBoxColumn30.HeaderText = "Дом";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.Width = 40;
            // 
            // corpDataGridViewTextBoxColumn
            // 
            this.corpDataGridViewTextBoxColumn.DataPropertyName = "corp";
            this.corpDataGridViewTextBoxColumn.HeaderText = "Корп";
            this.corpDataGridViewTextBoxColumn.Name = "corpDataGridViewTextBoxColumn";
            this.corpDataGridViewTextBoxColumn.ReadOnly = true;
            this.corpDataGridViewTextBoxColumn.Width = 40;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.DataPropertyName = "isChecking";
            this.dataGridViewCheckBoxColumn7.HeaderText = "";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.ReadOnly = true;
            this.dataGridViewCheckBoxColumn7.Width = 20;
            // 
            // dataGridViewCheckBoxColumn8
            // 
            this.dataGridViewCheckBoxColumn8.DataPropertyName = "Checked";
            this.dataGridViewCheckBoxColumn8.HeaderText = "";
            this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
            this.dataGridViewCheckBoxColumn8.ReadOnly = true;
            this.dataGridViewCheckBoxColumn8.Width = 20;
            // 
            // valid
            // 
            this.valid.DataPropertyName = "valid";
            this.valid.HeaderText = "valid";
            this.valid.Name = "valid";
            this.valid.ReadOnly = true;
            this.valid.Width = 30;
            // 
            // fullAddress
            // 
            this.fullAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fullAddress.DataPropertyName = "fullAddress";
            this.fullAddress.HeaderText = "Полученный адрес";
            this.fullAddress.Name = "fullAddress";
            this.fullAddress.ReadOnly = true;
            // 
            // addressListElementBindingSource
            // 
            this.addressListElementBindingSource.DataSource = typeof(OSM_Geocoding.Form1.AddressListElement);
            this.addressListElementBindingSource.DataError += new System.Windows.Forms.BindingManagerDataErrorEventHandler(this.addressListElementBindingSource_DataError);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(6, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(275, 134);
            this.listBox1.TabIndex = 10;
            // 
            // deleteXML
            // 
            this.deleteXML.Location = new System.Drawing.Point(287, 62);
            this.deleteXML.Name = "deleteXML";
            this.deleteXML.Size = new System.Drawing.Size(75, 23);
            this.deleteXML.TabIndex = 11;
            this.deleteXML.Text = "Удалить";
            this.deleteXML.UseVisualStyleBackColor = true;
            this.deleteXML.Click += new System.EventHandler(this.deleteXML_Click);
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logBox.Location = new System.Drawing.Point(0, 370);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(1451, 185);
            this.logBox.TabIndex = 12;
            this.logBox.Text = "";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewCheckBoxColumn9});
            this.dataGridView3.DataSource = this.resultInfoElementBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(0, 231);
            this.dataGridView3.MinimumSize = new System.Drawing.Size(400, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.Size = new System.Drawing.Size(413, 139);
            this.dataGridView3.TabIndex = 16;
            this.dataGridView3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView3_DataError);
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn31.DataPropertyName = "fileName";
            this.dataGridViewTextBoxColumn31.HeaderText = "Файл";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.DataPropertyName = "filePath";
            this.dataGridViewTextBoxColumn32.HeaderText = "filePath";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Visible = false;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "addressCount";
            this.dataGridViewTextBoxColumn33.FillWeight = 40F;
            this.dataGridViewTextBoxColumn33.HeaderText = "всего";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Width = 40;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.DataPropertyName = "addressCekhed";
            this.dataGridViewTextBoxColumn34.HeaderText = "пров.";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Width = 40;
            // 
            // dataGridViewCheckBoxColumn9
            // 
            this.dataGridViewCheckBoxColumn9.DataPropertyName = "comlete";
            this.dataGridViewCheckBoxColumn9.HeaderText = "";
            this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
            this.dataGridViewCheckBoxColumn9.ReadOnly = true;
            this.dataGridViewCheckBoxColumn9.Width = 20;
            // 
            // resultInfoElementBindingSource
            // 
            this.resultInfoElementBindingSource.DataSource = typeof(OSM_Geocoding.Form1.resultInfoElement);
            this.resultInfoElementBindingSource.DataError += new System.Windows.Forms.BindingManagerDataErrorEventHandler(this.resultInfoElementBindingSource_DataError);
            // 
            // OffProxyButton
            // 
            this.OffProxyButton.Location = new System.Drawing.Point(287, 114);
            this.OffProxyButton.Name = "OffProxyButton";
            this.OffProxyButton.Size = new System.Drawing.Size(74, 35);
            this.OffProxyButton.TabIndex = 19;
            this.OffProxyButton.Text = "Отключить прокси";
            this.OffProxyButton.UseVisualStyleBackColor = true;
            this.OffProxyButton.Click += new System.EventHandler(this.OffProxyButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Время между запросами";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(286, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Установить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(5, 156);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(134, 20);
            this.numericUpDown1.TabIndex = 23;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(286, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // radioButtonOSM
            // 
            this.radioButtonOSM.AutoSize = true;
            this.radioButtonOSM.Checked = true;
            this.radioButtonOSM.Location = new System.Drawing.Point(6, 182);
            this.radioButtonOSM.Name = "radioButtonOSM";
            this.radioButtonOSM.Size = new System.Drawing.Size(49, 17);
            this.radioButtonOSM.TabIndex = 24;
            this.radioButtonOSM.TabStop = true;
            this.radioButtonOSM.Text = "OSM";
            this.radioButtonOSM.UseVisualStyleBackColor = true;
            this.radioButtonOSM.CheckedChanged += new System.EventHandler(this.radioButtonOSM_CheckedChanged);
            // 
            // radioButtonYa
            // 
            this.radioButtonYa.AutoSize = true;
            this.radioButtonYa.Location = new System.Drawing.Point(61, 182);
            this.radioButtonYa.Name = "radioButtonYa";
            this.radioButtonYa.Size = new System.Drawing.Size(61, 17);
            this.radioButtonYa.TabIndex = 25;
            this.radioButtonYa.Text = "Yandex";
            this.radioButtonYa.UseVisualStyleBackColor = true;
            this.radioButtonYa.CheckedChanged += new System.EventHandler(this.radioButtonYa_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.deleteXML);
            this.groupBox1.Controls.Add(this.radioButtonYa);
            this.groupBox1.Controls.Add(this.OffProxyButton);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.radioButtonOSM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(400, 225);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 231);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 555);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.logBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.proxyListElementBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressListElementBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultInfoElementBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource proxyListElementBindingSource;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.BindingSource addressListElementBindingSource;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button deleteXML;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.BindingSource resultInfoElementBindingSource;
        private System.Windows.Forms.Button OffProxyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn responseTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn raitingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckingDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn housenumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressCekhedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn comleteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.RadioButton radioButtonOSM;
        private System.Windows.Forms.RadioButton radioButtonYa;
        private System.Windows.Forms.DataGridViewTextBoxColumn fidernumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn l2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn k2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn origAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn corpDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn valid;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullAddress;
    }
}

