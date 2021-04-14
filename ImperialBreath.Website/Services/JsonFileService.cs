using ImperialBreath.Website.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Task = ImperialBreath.Website.Models.Task;

namespace ImperialBreath.Website.Services
{
    public class JsonFileService
    {
        public JsonFileService(IWebHostEnvironment webHostEnvironment)
        {
            this.WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private string TasksFilePath 
        { 
            get => Path.Combine(WebHostEnvironment.ContentRootPath, "data", "tasks.json"); 
        }
        private string SubjectsFilePath
        {
            get => Path.Combine(WebHostEnvironment.ContentRootPath, "data", "subjects.json");
        }
        private string UsersFilePath
        {
            get => Path.Combine(WebHostEnvironment.ContentRootPath, "data", "users.json");
        }

        public IEnumerable<Task> GetTasks()
        {
            if (!File.Exists(TasksFilePath))
                CreateEmptyJsonFile(TasksFilePath);
            using var jsonFileReader = File.OpenText(TasksFilePath);
            return JsonSerializer.Deserialize<Task[]>(jsonFileReader.ReadToEnd());
        }

        public IEnumerable<Subject> GetSubjects()
        {
            if (!File.Exists(SubjectsFilePath))
                CreateEmptyJsonFile(SubjectsFilePath);
            using var jsonFileReader = File.OpenText(SubjectsFilePath);
            return JsonSerializer.Deserialize<Subject[]>(jsonFileReader.ReadToEnd());
        }

        public IEnumerable<User> GetUsers()
        {
            if (!File.Exists(UsersFilePath))
                CreateEmptyJsonFile(UsersFilePath);
            using var jsonFileReader = File.OpenText(UsersFilePath);
            return JsonSerializer.Deserialize<User[]>(jsonFileReader.ReadToEnd());
        }

        private void CreateEmptyJsonFile(string path)
        {
            if (File.Exists(path))
                return;
            File.Create(path).Close();
            using TextWriter textWriter = new StreamWriter(path);
            textWriter.Write("[]");
        }

        public void AddTask(int subjectId, string summary, string content, DateTime deadline)
        {
            List<Task> tasks = GetTasks().ToList();

            Task newTask = new Task()
            {
                TaskId = tasks.Count() == 0 ? 1 : tasks.Last().TaskId + 1,
                SubjectId = subjectId,
                Summary = summary,
                Content = content,
                Deadline = deadline,
                WrittenDate = DateTime.Today
            };

            tasks.Add(newTask);

            SaveTasks(tasks);
        }

        public void RemoveTask(int taskId)
        {
            List<Task> tasks = GetTasks().ToList();
            Task query = tasks.First(x => x.TaskId == taskId);

            tasks.Remove(query);

            SaveTasks(tasks);
        }

        public void UpdateTask(int taskId, string summary, string content, DateTime deadline)
        {
            List<Task> tasks = GetTasks().ToList();

            Task task = tasks.First(value => value.TaskId == taskId);
            task.Summary = summary;
            task.Content = content;
            task.Deadline = deadline;

            SaveTasks(tasks);
        }

        public void AddSubject(string name)
        {
            List<Subject> subjects = GetSubjects().ToList();
            Subject newSubject = new Subject()
            {
                SubjectId = subjects.Count + 1,
                Name = name
            };

            subjects.Add(newSubject);

            SaveSubjects(subjects);
        }

        public void RemoveSubject(int subjectId)
        {
            List<Subject> subjects = GetSubjects().Where(subject => subject.SubjectId != subjectId).ToList();

            SaveSubjects(subjects);
        }

        public void SaveTasks(List<Task> tasks)
        {
            SaveFile(TasksFilePath, tasks);
        }

        public void SaveSubjects(List<Subject> subjects)
        {
            SaveFile(SubjectsFilePath, subjects);
        }

        public void SaveUsers(List<User> users)
        {
            SaveFile(UsersFilePath, users);
        }

        
        public void SaveFile<T>(string path, List<T> list)
        {
            File.Create(path).Close();
            using FileStream fileStream = File.OpenWrite(path);
            JsonSerializer.Serialize<IEnumerable<T>>(new Utf8JsonWriter(fileStream,
                new JsonWriterOptions
                {
                    Indented = true
                }), list
             );
        }
    }
}
