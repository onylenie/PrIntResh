using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ApiClient _apiClient;
        private int _selectedProjectId = -1;
        private int _selectedTaskId = -1;
        private int _currentProjectPage = 0;
        private int _projectLimit = 10;
        private int _currentTaskPage = 0;
        private int _taskLimit = 10;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
            DebugLog("Форма инициализирована");

            UpdatePaginationLabels();

            listProjects.MouseDoubleClick += new MouseEventHandler(listProjects_MouseDoubleClick);
            listTasks.MouseDoubleClick += new MouseEventHandler(listTasks_MouseDoubleClick);
            listTasks.SelectedIndexChanged += new EventHandler(listTasks_SelectedIndexChanged);
        }

        private void DebugLog(string message)
        {
            Console.WriteLine($"[DEBUG] {DateTime.Now:HH:mm:ss} - {message}");
        }

        private void UpdatePaginationLabels()
        {
            lblProjectPage.Text = $"Страница: {_currentProjectPage + 1}";
            lblTaskPage.Text = $"Страница: {_currentTaskPage + 1}";
        }

        private void listProjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listProjects.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var projectId = GetSelectedProjectId();
                if (projectId != -1)
                {
                    ShowProjectDetails(projectId);
                }
            }
        }

        private async void ShowProjectDetails(int projectId)
        {
            try
            {
                var projects = await _apiClient.GetProjects(100, 0);
                var project = projects.FirstOrDefault(p => p.Id == projectId);

                if (project != null)
                {
                    var details = new StringBuilder();
                    details.AppendLine($"Проект: {project.Name}");
                    details.AppendLine($"Описание: {project.Description ?? "Нет описания"}");

                    MessageBox.Show(details.ToString(), "Детали проекта",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listTasks.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var taskId = GetSelectedTaskId();
                if (taskId != -1)
                {
                    ShowTaskDetails(taskId);
                }
            }
        }

        private async void ShowTaskDetails(int taskId)
        {
            try
            {
                var tasks = await _apiClient.GetTasks(_selectedProjectId, 100, 0);
                var task = tasks.FirstOrDefault(t => t.Id == taskId);

                if (task != null)
                {
                    var comments = await _apiClient.GetComments(taskId);

                    var details = new StringBuilder();
                    details.AppendLine($"Задача: {task.Title}");
                    details.AppendLine($"ID: {task.Id}");
                    details.AppendLine($"Статус: {task.Status}");
                    details.AppendLine($"Приоритет: {task.Priority}");
                    details.AppendLine($"Описание: {task.Description ?? "Нет описания"}");

                    if (comments != null && comments.Length > 0)
                    {
                        details.AppendLine();
                        details.AppendLine($"Комментарии ({comments.Length}):");
                        foreach (var comment in comments)
                        {
                            details.AppendLine($"  [{comment.CreatedAt:HH:mm}] {comment.Body}");
                        }
                    }

                    MessageBox.Show(details.ToString(), "Детали задачи",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetToken()
        {
            return _apiClient.GetType().GetField("_token",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .GetValue(_apiClient) as string ?? "";
        }

        private async void listTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebugLog($"Событие SelectedIndexChanged для listTasks");

            _selectedTaskId = GetSelectedTaskId();
            DebugLog($"Выбрана задача ID: {_selectedTaskId}");

            if (_selectedTaskId != -1)
            {
                DebugLog($"Загрузка комментариев для задачи {_selectedTaskId}");
                await LoadComments(_selectedTaskId);
            }
            else
            {
                DebugLog("Задача не выбрана");
                listComments.Items.Clear();
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            DebugLog($"Нажата кнопка Регистрация. Email: {txtRegisterEmail.Text}");

            try
            {
                DebugLog("Отправка запроса на регистрацию...");

                var success = await _apiClient.Register(
                    txtRegisterEmail.Text,
                    txtRegisterPassword.Text,
                    txtRegisterName.Text);

                DebugLog($"Результат регистрации: {success}");

                if (success)
                {
                    DebugLog("Регистрация успешна!");
                    lblStatus.Text = "Регистрация успешна!";
                    panel1.Visible = false;
                    panel2.Visible = false;
                    await LoadProjects();
                }
                else
                {
                    DebugLog("Регистрация не удалась");
                    lblStatus.Text = "Ошибка регистрации";
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Исключение при регистрации: {ex.Message}");
                lblStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            DebugLog($"Нажата кнопка Войти. Email: {txtEmail.Text}");

            try
            {
                DebugLog($"Отправка запроса на логин...");

                var success = await _apiClient.Login(txtEmail.Text, txtPassword.Text);
                DebugLog($"Результат логина: {success}");

                if (success)
                {
                    DebugLog("Логин успешен!");
                    lblStatus.Text = "Успешный вход!";
                    panel1.Visible = false;
                    panel2.Visible = false;
                    await LoadProjects();
                }
                else
                {
                    DebugLog("Логин не удался");
                    lblStatus.Text = "Ошибка входа";
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Исключение при логине: {ex.Message}");
                lblStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private async Task LoadProjects()
        {
            DebugLog("Загрузка проектов...");

            try
            {
                var projects = await _apiClient.GetProjects(_projectLimit, _currentProjectPage * _projectLimit);
                DebugLog($"Получено проектов: {projects?.Length ?? 0}");

                listProjects.Items.Clear();

                foreach (var project in projects)
                {
                    listProjects.Items.Add($"{project.Id}: {project.Name}");
                }
                lblStatus.Text = $"Загружено проектов: {projects.Length}";
                UpdatePaginationLabels();

                DebugLog("Проекты загружены в ListBox");
            }
            catch (Exception ex)
            {
                DebugLog($"Ошибка загрузки проектов: {ex.Message}");
                lblStatus.Text = $"Ошибка загрузки: {ex.Message}";
            }
        }

        private async void btnCreateProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show("Введите название проекта", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DebugLog($"Нажата кнопка Создать проект. Название: {txtProjectName.Text}");

            try
            {
                DebugLog("Отправка запроса на создание проекта...");

                string idempotencyKey = $"project_{txtProjectName.Text.GetHashCode():X}";
                DebugLog($"Идемпотентный ключ: {idempotencyKey}");

                var project = await _apiClient.CreateProject(
                    txtProjectName.Text,
                    txtProjectDescription.Text,
                    idempotencyKey);

                DebugLog($"Результат создания проекта: {(project != null ? "Успех" : "Неудача")}");

                if (project != null)
                {
                    DebugLog($"Создан проект ID: {project.Id}, Name: {project.Name}");
                    lblStatus.Text = $"Проект создан: {project.Name}";

                    txtProjectName.Clear();
                    txtProjectDescription.Clear();

                    await LoadProjects();
                }
                else
                {
                    DebugLog("Проект не создан (null)");
                    lblStatus.Text = "Ошибка создания проекта";
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Исключение при создании проекта: {ex.Message}");
                lblStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private async void btnDeleteProject_Click(object sender, EventArgs e)
        {
            var projectId = GetSelectedProjectId();
            if (projectId == -1)
            {
                MessageBox.Show("Выберите проект для удаления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Удалить выбранный проект?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DebugLog($"Удаление проекта ID: {projectId}");
                    var success = await _apiClient.DeleteProject(projectId);

                    if (success)
                    {
                        DebugLog("Проект удален успешно");
                        lblStatus.Text = "Проект удален";
                        await LoadProjects();
                    }
                    else
                    {
                        DebugLog("Ошибка удаления проекта");
                        lblStatus.Text = "Ошибка удаления проекта";
                    }
                }
                catch (Exception ex)
                {
                    DebugLog($"Исключение при удалении проекта: {ex.Message}");
                    lblStatus.Text = $"Ошибка: {ex.Message}";
                }
            }
        }

        private async void listProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebugLog($"Выбран проект в списке: {listProjects.SelectedItem}");

            if (listProjects.SelectedItem != null)
            {
                var projectId = GetSelectedProjectId();
                if (projectId != -1)
                {
                    DebugLog($"Распознан projectId: {projectId}");
                    _selectedProjectId = projectId;
                    _currentTaskPage = 0;
                    await LoadTasks(projectId);
                }
            }
        }

        private async Task LoadTasks(int projectId)
        {
            DebugLog($"Загрузка задач для проекта {projectId}...");

            try
            {
                var tasks = await _apiClient.GetTasks(projectId, _taskLimit, _currentTaskPage * _taskLimit);
                DebugLog($"Получено задач: {tasks?.Length ?? 0}");

                listTasks.Items.Clear();
                _selectedTaskId = -1;
                listComments.Items.Clear();

                foreach (var task in tasks)
                {
                    listTasks.Items.Add($"{task.Id}: {task.Title} [{task.Status}]");
                }
                lblStatus.Text = $"Загружено задач: {tasks.Length}";
                UpdatePaginationLabels();

                DebugLog("Задачи загружены в ListBox");
            }
            catch (Exception ex)
            {
                DebugLog($"Ошибка загрузки задач: {ex.Message}");
                lblStatus.Text = $"Ошибка загрузки задач: {ex.Message}";
            }
        }

        private async void btnCreateTask_Click(object sender, EventArgs e)
        {
            if (_selectedProjectId == -1)
            {
                MessageBox.Show("Выберите проект!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Введите название задачи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DebugLog($"Нажата кнопка Создать задачу. Проект: {_selectedProjectId}, Заголовок: {txtTaskTitle.Text}");

            try
            {
                DebugLog("Отправка запроса на создание задачи...");

                string idempotencyKey = $"task_{_selectedProjectId}_{txtTaskTitle.Text.GetHashCode():X}";
                DebugLog($"Идемпотентный ключ: {idempotencyKey}");

                var task = await _apiClient.CreateTask(
                    _selectedProjectId,
                    txtTaskTitle.Text,
                    txtTaskDescription.Text,
                    idempotencyKey);

                DebugLog($"Результат создания задачи: {(task != null ? "Успех" : "Неудача")}");

                if (task != null)
                {
                    DebugLog($"Создана задача ID: {task.Id}, Title: {task.Title}");
                    lblStatus.Text = $"Задача создана: {task.Title}";

                    txtTaskTitle.Clear();
                    txtTaskDescription.Clear();

                    await LoadTasks(_selectedProjectId);
                }
                else
                {
                    DebugLog("Задача не создана (null)");
                    lblStatus.Text = "Ошибка создания задачи";
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Исключение при создании задачи: {ex.Message}");
                lblStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private async void btnDeleteTask_Click(object sender, EventArgs e)
        {
            var taskId = GetSelectedTaskId();
            if (taskId == -1)
            {
                MessageBox.Show("Выберите задачу для удаления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Удалить выбранную задачу?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DebugLog($"Удаление задачи ID: {taskId}");
                    var success = await _apiClient.DeleteTask(taskId);

                    if (success)
                    {
                        DebugLog("Задача удалена успешно");
                        lblStatus.Text = "Задача удалена";
                        await LoadTasks(_selectedProjectId);
                    }
                    else
                    {
                        DebugLog("Ошибка удаления задачи");
                        lblStatus.Text = "Ошибка удаления задачи";
                    }
                }
                catch (Exception ex)
                {
                    DebugLog($"Исключение при удалении задачи: {ex.Message}");
                    lblStatus.Text = $"Ошибка: {ex.Message}";
                }
            }
        }

        private async Task LoadComments(int taskId)
        {
            DebugLog($"Загрузка комментариев для задачи {taskId}...");

            try
            {
                var comments = await _apiClient.GetComments(taskId, 10, 0);
                DebugLog($"Получено комментариев: {comments?.Length ?? 0}");

                listComments.Items.Clear();

                foreach (var comment in comments)
                {
                    listComments.Items.Add($"[{comment.CreatedAt:HH:mm}] {comment.Body}");
                }

                lblStatus.Text = $"Загружено комментариев: {comments.Length}";
            }
            catch (Exception ex)
            {
                DebugLog($"Ошибка загрузки комментариев: {ex.Message}");
            }
        }

        private async void btnAddComment_Click(object sender, EventArgs e)
        {
            if (_selectedTaskId == -1)
            {
                MessageBox.Show("Выберите задачу!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Введите текст комментария", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DebugLog($"Добавление комментария к задаче {_selectedTaskId}");

            try
            {
                var comment = await _apiClient.CreateComment(
                    _selectedTaskId,
                    txtComment.Text,
                    Guid.NewGuid().ToString());

                if (comment != null)
                {
                    DebugLog($"Комментарий добавлен ID: {comment.Id}");
                    lblStatus.Text = "Комментарий добавлен";

                    txtComment.Clear();
                    await LoadComments(_selectedTaskId);
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Исключение при добавлении комментария: {ex.Message}");
                lblStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private async void btnPrevProjects_Click(object sender, EventArgs e)
        {
            if (_currentProjectPage > 0)
            {
                _currentProjectPage--;
                await LoadProjects();
            }
        }

        private async void btnNextProjects_Click(object sender, EventArgs e)
        {
            _currentProjectPage++;
            await LoadProjects();
        }

        private async void btnPrevTasks_Click(object sender, EventArgs e)
        {
            if (_currentTaskPage > 0 && _selectedProjectId != -1)
            {
                _currentTaskPage--;
                await LoadTasks(_selectedProjectId);
            }
        }

        private async void btnNextTasks_Click(object sender, EventArgs e)
        {
            if (_selectedProjectId != -1)
            {
                _currentTaskPage++;
                await LoadTasks(_selectedProjectId);
            }
        }

        private async void txtLimitProjects_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtLimitProjects.Text, out int limit) && limit > 0)
            {
                _projectLimit = limit;
                _currentProjectPage = 0;
                await LoadProjects();
            }
        }

        private async void txtLimitTasks_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtLimitTasks.Text, out int limit) && limit > 0 && _selectedProjectId != -1)
            {
                _taskLimit = limit;
                _currentTaskPage = 0;
                await LoadTasks(_selectedProjectId);
            }
        }

        private int GetSelectedProjectId()
        {
            if (listProjects.SelectedItem != null)
            {
                var parts = listProjects.SelectedItem.ToString().Split(':');
                if (int.TryParse(parts[0], out int id))
                    return id;
            }
            return -1;
        }

        private int GetSelectedTaskId()
        {
            if (listTasks.SelectedItem != null)
            {
                var parts = listTasks.SelectedItem.ToString().Split(':');
                if (int.TryParse(parts[0], out int id))
                    return id;
            }
            return -1;
        }        

        private void Form1_Load(object sender, EventArgs e)
        {
            DebugLog("Форма загружена");
        }
    }
}
