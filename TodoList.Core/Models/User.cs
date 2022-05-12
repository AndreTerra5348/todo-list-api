using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TodoList.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; } = new Collection<Todo>();
    }
}