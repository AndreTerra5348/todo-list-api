namespace TodoList.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<Todo> Todos { get; set; } = new Collection<Todo>();
    }
}