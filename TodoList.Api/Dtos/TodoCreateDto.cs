namespace TodoList.Api.Dtos
{
    public class TodoCreateDto
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
    }
}