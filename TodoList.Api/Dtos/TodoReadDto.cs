namespace TodoList.Api.Dtos
{
    public class TodoReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
    }
}