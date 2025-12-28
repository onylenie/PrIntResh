namespace WindowsFormsApp1
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.listTasks = new System.Windows.Forms.ListBox();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.BtnCreateTask = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCreateProject = new System.Windows.Forms.Button();
            this.txtProjectDescription = new System.Windows.Forms.TextBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.listProjects = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.txtRegisterEmail = new System.Windows.Forms.TextBox();
            this.txtRegisterPassword = new System.Windows.Forms.TextBox();
            this.txtRegisterName = new System.Windows.Forms.TextBox();
            this.btnDeleteProject = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.lblProjectPage = new System.Windows.Forms.Label();
            this.btnNextProjects = new System.Windows.Forms.Button();
            this.btnPrevProjects = new System.Windows.Forms.Button();
            this.txtLimitProjects = new System.Windows.Forms.TextBox();
            this.lblTaskPage = new System.Windows.Forms.Label();
            this.btnPrevTasks = new System.Windows.Forms.Button();
            this.btnNextTasks = new System.Windows.Forms.Button();
            this.txtLimitTasks = new System.Windows.Forms.TextBox();
            this.listComments = new System.Windows.Forms.ListBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblRateLimit = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(279, 42);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(279, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(279, 97);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Авторизация";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // listTasks
            // 
            this.listTasks.FormattingEnabled = true;
            this.listTasks.Location = new System.Drawing.Point(33, 370);
            this.listTasks.Name = "listTasks";
            this.listTasks.Size = new System.Drawing.Size(246, 95);
            this.listTasks.TabIndex = 3;
            this.listTasks.SelectedIndexChanged += new System.EventHandler(this.listTasks_SelectedIndexChanged);
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.Location = new System.Drawing.Point(299, 386);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(246, 20);
            this.txtTaskTitle.TabIndex = 4;
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(299, 429);
            this.txtTaskDescription.Multiline = true;
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(246, 49);
            this.txtTaskDescription.TabIndex = 5;
            // 
            // BtnCreateTask
            // 
            this.BtnCreateTask.Location = new System.Drawing.Point(299, 484);
            this.BtnCreateTask.Name = "BtnCreateTask";
            this.BtnCreateTask.Size = new System.Drawing.Size(120, 23);
            this.BtnCreateTask.TabIndex = 6;
            this.BtnCreateTask.Text = "Создать задачу";
            this.BtnCreateTask.UseVisualStyleBackColor = true;
            this.BtnCreateTask.Click += new System.EventHandler(this.btnCreateTask_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(32, 250);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 25);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "label1";
            // 
            // btnCreateProject
            // 
            this.btnCreateProject.Location = new System.Drawing.Point(605, 192);
            this.btnCreateProject.Name = "btnCreateProject";
            this.btnCreateProject.Size = new System.Drawing.Size(120, 23);
            this.btnCreateProject.TabIndex = 12;
            this.btnCreateProject.Text = "Создать";
            this.btnCreateProject.UseVisualStyleBackColor = true;
            this.btnCreateProject.Click += new System.EventHandler(this.btnCreateProject_Click);
            // 
            // txtProjectDescription
            // 
            this.txtProjectDescription.Location = new System.Drawing.Point(605, 126);
            this.txtProjectDescription.Multiline = true;
            this.txtProjectDescription.Name = "txtProjectDescription";
            this.txtProjectDescription.Size = new System.Drawing.Size(120, 60);
            this.txtProjectDescription.TabIndex = 11;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(605, 83);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(120, 20);
            this.txtProjectName.TabIndex = 10;
            // 
            // listProjects
            // 
            this.listProjects.FormattingEnabled = true;
            this.listProjects.Location = new System.Drawing.Point(462, 67);
            this.listProjects.Name = "listProjects";
            this.listProjects.Size = new System.Drawing.Size(120, 95);
            this.listProjects.TabIndex = 9;
            this.listProjects.Click += new System.EventHandler(this.listProjects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Регистрация";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(81, 122);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 23);
            this.btnRegister.TabIndex = 14;
            this.btnRegister.Text = "Регистрация";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtRegisterEmail
            // 
            this.txtRegisterEmail.Location = new System.Drawing.Point(81, 42);
            this.txtRegisterEmail.Name = "txtRegisterEmail";
            this.txtRegisterEmail.Size = new System.Drawing.Size(100, 20);
            this.txtRegisterEmail.TabIndex = 15;
            // 
            // txtRegisterPassword
            // 
            this.txtRegisterPassword.Location = new System.Drawing.Point(81, 94);
            this.txtRegisterPassword.Name = "txtRegisterPassword";
            this.txtRegisterPassword.Size = new System.Drawing.Size(100, 20);
            this.txtRegisterPassword.TabIndex = 16;
            // 
            // txtRegisterName
            // 
            this.txtRegisterName.Location = new System.Drawing.Point(81, 68);
            this.txtRegisterName.Name = "txtRegisterName";
            this.txtRegisterName.Size = new System.Drawing.Size(100, 20);
            this.txtRegisterName.TabIndex = 17;
            // 
            // btnDeleteProject
            // 
            this.btnDeleteProject.Location = new System.Drawing.Point(605, 221);
            this.btnDeleteProject.Name = "btnDeleteProject";
            this.btnDeleteProject.Size = new System.Drawing.Size(120, 23);
            this.btnDeleteProject.TabIndex = 18;
            this.btnDeleteProject.Text = "Удалить";
            this.btnDeleteProject.UseVisualStyleBackColor = true;
            this.btnDeleteProject.Click += new System.EventHandler(this.btnDeleteProject_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(605, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Название";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(605, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Описание";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Название";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 413);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Описание";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Почта";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Логин";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Пароль";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(228, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Почта";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(228, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Пароль";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(276, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Авторизация";
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Location = new System.Drawing.Point(425, 484);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(120, 23);
            this.btnDeleteTask.TabIndex = 18;
            this.btnDeleteTask.Text = "Удалить";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // lblProjectPage
            // 
            this.lblProjectPage.AutoSize = true;
            this.lblProjectPage.Location = new System.Drawing.Point(486, 173);
            this.lblProjectPage.Name = "lblProjectPage";
            this.lblProjectPage.Size = new System.Drawing.Size(67, 13);
            this.lblProjectPage.TabIndex = 23;
            this.lblProjectPage.Text = "Страница: 1";
            // 
            // btnNextProjects
            // 
            this.btnNextProjects.Location = new System.Drawing.Point(559, 168);
            this.btnNextProjects.Name = "btnNextProjects";
            this.btnNextProjects.Size = new System.Drawing.Size(23, 23);
            this.btnNextProjects.TabIndex = 24;
            this.btnNextProjects.Text = ">";
            this.btnNextProjects.UseVisualStyleBackColor = true;
            this.btnNextProjects.Click += new System.EventHandler(this.btnNextProjects_Click);
            // 
            // btnPrevProjects
            // 
            this.btnPrevProjects.Location = new System.Drawing.Point(462, 168);
            this.btnPrevProjects.Name = "btnPrevProjects";
            this.btnPrevProjects.Size = new System.Drawing.Size(23, 23);
            this.btnPrevProjects.TabIndex = 25;
            this.btnPrevProjects.Text = "<";
            this.btnPrevProjects.UseVisualStyleBackColor = true;
            this.btnPrevProjects.Click += new System.EventHandler(this.btnPrevProjects_Click);
            // 
            // txtLimitProjects
            // 
            this.txtLimitProjects.Location = new System.Drawing.Point(462, 197);
            this.txtLimitProjects.Name = "txtLimitProjects";
            this.txtLimitProjects.Size = new System.Drawing.Size(120, 20);
            this.txtLimitProjects.TabIndex = 26;
            this.txtLimitProjects.Text = "10";
            // 
            // lblTaskPage
            // 
            this.lblTaskPage.AutoSize = true;
            this.lblTaskPage.Location = new System.Drawing.Point(121, 476);
            this.lblTaskPage.Name = "lblTaskPage";
            this.lblTaskPage.Size = new System.Drawing.Size(67, 13);
            this.lblTaskPage.TabIndex = 27;
            this.lblTaskPage.Text = "Страница: 1";
            // 
            // btnPrevTasks
            // 
            this.btnPrevTasks.Location = new System.Drawing.Point(33, 471);
            this.btnPrevTasks.Name = "btnPrevTasks";
            this.btnPrevTasks.Size = new System.Drawing.Size(29, 23);
            this.btnPrevTasks.TabIndex = 28;
            this.btnPrevTasks.Text = "<";
            this.btnPrevTasks.UseVisualStyleBackColor = true;
            this.btnPrevTasks.Click += new System.EventHandler(this.btnPrevTasks_Click);
            // 
            // btnNextTasks
            // 
            this.btnNextTasks.Location = new System.Drawing.Point(250, 471);
            this.btnNextTasks.Name = "btnNextTasks";
            this.btnNextTasks.Size = new System.Drawing.Size(29, 23);
            this.btnNextTasks.TabIndex = 29;
            this.btnNextTasks.Text = ">";
            this.btnNextTasks.UseVisualStyleBackColor = true;
            this.btnNextTasks.Click += new System.EventHandler(this.btnNextTasks_Click);
            // 
            // txtLimitTasks
            // 
            this.txtLimitTasks.Location = new System.Drawing.Point(33, 513);
            this.txtLimitTasks.Name = "txtLimitTasks";
            this.txtLimitTasks.Size = new System.Drawing.Size(246, 20);
            this.txtLimitTasks.TabIndex = 30;
            this.txtLimitTasks.Text = "10";
            // 
            // listComments
            // 
            this.listComments.FormattingEnabled = true;
            this.listComments.Location = new System.Drawing.Point(582, 370);
            this.listComments.Name = "listComments";
            this.listComments.Size = new System.Drawing.Size(246, 95);
            this.listComments.TabIndex = 31;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(582, 471);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(246, 42);
            this.txtComment.TabIndex = 32;
            // 
            // btnAddComment
            // 
            this.btnAddComment.Location = new System.Drawing.Point(582, 519);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(246, 23);
            this.btnAddComment.TabIndex = 33;
            this.btnAddComment.Text = "Добавить комментарий";
            this.btnAddComment.UseVisualStyleBackColor = true;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(465, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(229, 31);
            this.label12.TabIndex = 34;
            this.label12.Text = "Список проектов";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(27, 327);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(188, 31);
            this.label13.TabIndex = 35;
            this.label13.Text = "Список задач";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(576, 327);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(297, 31);
            this.label14.TabIndex = 35;
            this.label14.Text = "Список комментариев";
            // 
            // lblRateLimit
            // 
            this.lblRateLimit.AutoSize = true;
            this.lblRateLimit.Location = new System.Drawing.Point(241, 211);
            this.lblRateLimit.Name = "lblRateLimit";
            this.lblRateLimit.Size = new System.Drawing.Size(84, 13);
            this.lblRateLimit.TabIndex = 36;
            this.lblRateLimit.Text = "Лимит 100/100";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(32, 205);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(349, 29);
            this.label15.TabIndex = 37;
            this.label15.Text = "Статус последнего действия";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 226);
            this.panel1.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(462, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(341, 251);
            this.panel2.TabIndex = 39;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 564);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblRateLimit);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAddComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.listComments);
            this.Controls.Add(this.txtLimitTasks);
            this.Controls.Add(this.btnNextTasks);
            this.Controls.Add(this.btnPrevTasks);
            this.Controls.Add(this.lblTaskPage);
            this.Controls.Add(this.txtLimitProjects);
            this.Controls.Add(this.btnPrevProjects);
            this.Controls.Add(this.btnNextProjects);
            this.Controls.Add(this.lblProjectPage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.btnDeleteProject);
            this.Controls.Add(this.txtRegisterName);
            this.Controls.Add(this.txtRegisterPassword);
            this.Controls.Add(this.txtRegisterEmail);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateProject);
            this.Controls.Add(this.txtProjectDescription);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.listProjects);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.BtnCreateTask);
            this.Controls.Add(this.txtTaskDescription);
            this.Controls.Add(this.txtTaskTitle);
            this.Controls.Add(this.listTasks);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ListBox listTasks;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.TextBox txtTaskDescription;
        private System.Windows.Forms.Button BtnCreateTask;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCreateProject;
        private System.Windows.Forms.TextBox txtProjectDescription;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.ListBox listProjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox txtRegisterEmail;
        private System.Windows.Forms.TextBox txtRegisterPassword;
        private System.Windows.Forms.TextBox txtRegisterName;
        private System.Windows.Forms.Button btnDeleteProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Label lblProjectPage;
        private System.Windows.Forms.Button btnNextProjects;
        private System.Windows.Forms.Button btnPrevProjects;
        private System.Windows.Forms.TextBox txtLimitProjects;
        private System.Windows.Forms.Label lblTaskPage;
        private System.Windows.Forms.Button btnPrevTasks;
        private System.Windows.Forms.Button btnNextTasks;
        private System.Windows.Forms.TextBox txtLimitTasks;
        private System.Windows.Forms.ListBox listComments;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRateLimit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

