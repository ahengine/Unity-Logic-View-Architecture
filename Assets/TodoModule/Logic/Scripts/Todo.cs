using Newtonsoft.Json;

namespace TodoModuleSystem
{
    public class Todo
    {
        public int userId { get; }
        public int id { get; }
        public string title { get; }
        public bool completed { get; }

        [JsonConstructor]
        public Todo(int userId, int id, string title, bool completed) {
            this.userId = userId;
            this.id = id;
            this.title = title;
            this.completed = completed;
        }

        public override string ToString() =>
            "[userId: "+userId+"] [Id: "+id+"] "+title+" is "+completed;
    }
}