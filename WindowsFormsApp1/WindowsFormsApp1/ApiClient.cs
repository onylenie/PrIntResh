using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public class ApiClient
    {
        private readonly HttpClient _client;
        private string _token;

        public ApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8000/")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> Register(string email, string password, string name)
        {
            try
            {
                Console.WriteLine($"[API] Регистрация: {email}");

                var registerData = new RegisterRequest
                {
                    Email = email,
                    Password = password,
                    Name = name
                };
                var json = JsonConvert.SerializeObject(registerData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("api/v1/auth/register", content);
                Console.WriteLine($"[API] Статус регистрации: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[API] Ответ регистрации: {result}");

                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                    _token = tokenResponse.AccessToken;

                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[API] Ошибка регистрации: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] Исключение при регистрации: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                Console.WriteLine($"[API] Отправка логина: {email}");

                var loginData = new LoginRequest { Email = email, Password = password };
                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"[API] URL: {_client.BaseAddress}api/v1/auth/login");

                var response = await _client.PostAsync("api/v1/auth/login", content);
                Console.WriteLine($"[API] Статус ответа: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[API] Ответ: {result}");

                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                    _token = tokenResponse.AccessToken;

                    Console.WriteLine($"[API] Токен получен: {_token?.Substring(0, Math.Min(20, _token.Length))}...");

                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[API] Ошибка: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] Исключение: {ex.Message}");
                throw;
            }
        }

        public async Task<Project[]> GetProjects(int limit = 10, int offset = 0)
        {
            var response = await _client.GetAsync($"api/v1/projects/?limit={limit}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Project[]>(json);
            }
            return new Project[0];
        }

        public async Task<Project> CreateProject(string name, string description, string idempotencyKey = null)
        {
            var projectData = new { name, description };
            var json = JsonConvert.SerializeObject(projectData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/projects/")
            {
                Content = content
            };

            if (!string.IsNullOrEmpty(idempotencyKey))
                request.Headers.Add("Idempotency-Key", idempotencyKey);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Project>(responseJson);
            }
            return null;
        }

        public async Task<bool> DeleteProject(int projectId)
        {
            var response = await _client.DeleteAsync($"api/v1/projects/{projectId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TaskItem[]> GetTasks(int projectId, int limit = 10, int offset = 0)
        {
            var response = await _client.GetAsync($"api/v1/projects/{projectId}/tasks?limit={limit}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaskItem[]>(json);
            }
            return new TaskItem[0];
        }

        public async Task<TaskItem> CreateTask(int projectId, string title, string description, string idempotencyKey = null)
        {
            var taskData = new { title, description };
            var json = JsonConvert.SerializeObject(taskData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/projects/{projectId}/tasks")
            {
                Content = content
            };

            if (!string.IsNullOrEmpty(idempotencyKey))
                request.Headers.Add("Idempotency-Key", idempotencyKey);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaskItem>(responseJson);
            }
            return null;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var response = await _client.DeleteAsync($"api/v1/tasks/{taskId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Comment[]> GetComments(int taskId, int limit = 10, int offset = 0)
        {
            var response = await _client.GetAsync($"api/v1/tasks/{taskId}/comments?limit={limit}&offset={offset}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Comment[]>(json);
            }
            return new Comment[0];
        }

        public async Task<Comment> CreateComment(int taskId, string body, string idempotencyKey = null)
        {
            var commentData = new CreateCommentRequest { Body = body };
            var json = JsonConvert.SerializeObject(commentData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/tasks/{taskId}/comments")
            {
                Content = content
            };

            if (!string.IsNullOrEmpty(idempotencyKey))
                request.Headers.Add("Idempotency-Key", idempotencyKey);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Comment>(responseJson);
            }
            return null;
        }
    }
}